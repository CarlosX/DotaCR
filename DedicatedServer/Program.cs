/**
 * Dota 2 Dedicated Server
 * Written by ilian000
 * Based on Dota 2 Custom Realms IRC Code
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;
using Meebey.SmartIrc4net;
using System.Net;

namespace DedicatedServer
{
    class Program
    {
        const string serverprefix = "#SERVER_";

        public static IrcClient ircClient = new IrcClient();
        static Dictionary<string, int> ChannelList = new Dictionary<string, int>(); // List of channels - used for unique channel name creation
        static Dictionary<string, string> Topics = new Dictionary<string, string>(); // TODO: make some nice class for this
        static private List<string> Whitelist = new List<string>(); // List of allowed users
        static private List<int> availablePorts = new List<int>(); // List of available ports
        static private List<ServerMonitor> monitors = new List<ServerMonitor>();

        /// <summary>
        /// Used to determine when the IRC /LIST command response ends
        /// </summary>
        static bool FilledList = false;
        static void Main(string[] args)
        {
            Console.Title = "Dota 2 Custom Realms Dedicated Server - Written by ilian000";
            if (!Properties.Settings.Default.SetupDone)
            {
                // Execute first time setup
                firstSetup();
                Console.Clear();
            }

            // Show settings before running
            preLaunch();
        }
        static void firstSetup()
        {
            Console.WriteLine("================================ Edit Settings ================================\n\nPlease enter the path of your Dota 2 Server (e.g. C:\\dotaserver\\):");
            Properties.Settings.Default.ServerPath = Console.ReadLine();

            Console.WriteLine("\nWhat is your nickname?");
            Properties.Settings.Default.Nickname = Console.ReadLine();
            bool retry = true;
            while (retry)
            {
                try
                {
                    Console.WriteLine("\nHow many servers are you willing to run for the community (each server takes around 250MB)?");
                    Properties.Settings.Default.MaxServers = Convert.ToInt16(Console.ReadLine());
                    retry = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Input is not valid: " + e);
                }
            }
            retry = true;
            while (retry)
            {
                try
                {
                    Console.WriteLine("\nWhat is the starting port (each new server is one port higher)?");
                    Properties.Settings.Default.PortStart = Convert.ToInt16(Console.ReadLine());
                    retry = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Input is not valid: " + e);
                }
            }
            Properties.Settings.Default.SetupDone = true;
            Properties.Settings.Default.Save();
            preLaunch();
        }
        static void preLaunch()
        {
            Console.WriteLine("The server is now ready to launch. Please verify the following settings:\n\nPath to server:\t\t{0}\nNickname:\t\t{1}\nMaximum Servers:\t{2}\nStarting port:\t\t{3}\n\nEnter a command: [s]tart, [e]dit", Properties.Settings.Default.ServerPath, Properties.Settings.Default.Nickname, Properties.Settings.Default.MaxServers.ToString(), Properties.Settings.Default.PortStart.ToString());

            while (true)
            {
                ConsoleKeyInfo result = Console.ReadKey();
                if ((result.KeyChar == 's') || (result.KeyChar == 'S'))
                {
                    Console.Clear();
                    startServer(); 
                    break;
                }
                else if ((result.KeyChar == 'e') || (result.KeyChar == 'E'))
                {
                    Console.Clear();
                    firstSetup();
                    break;
                }
            }
            
        }
        static void startServer()
        {
            for (int i = Properties.Settings.Default.PortStart; i < (Properties.Settings.Default.PortStart + Properties.Settings.Default.MaxServers); i++)
            {
                availablePorts.Add(i);
            }
            ircClient.ActiveChannelSyncing = true;
            ircClient.SendDelay = 200;
            ircClient.AutoRetry = false; // TODO: Maybe turn back on?

            ircClient.OnConnected += new EventHandler(ircClient_OnConnected);
            ircClient.OnJoin += new JoinEventHandler(ircClient_OnJoin);
            ircClient.OnRawMessage += new IrcEventHandler(ircClient_OnRawMessage);
            ircClient.OnChannelMessage += new IrcEventHandler(ircClient_OnChannelMessage);
            ircClient.OnQueryNotice += new IrcEventHandler(ircClient_OnQueryNotice);
            ircClient.OnPart += new PartEventHandler(ircClient_OnPart);
            try
            {
                Console.WriteLine("Connecting to Dota 2 Custom Realms server...");
                ircClient.Connect("d2cr.id10ts.info", 8646);
            }
            catch (ConnectionException e)
            {
                Console.WriteLine("There was an error connecting to the server: {0}", e);
            }
        }
        static void ircClient_OnConnected(object sender, EventArgs e)
        {

            ircClient.Login(Properties.Settings.Default.Nickname, "Dedicated server");
            ircJoin(serverprefix + ircClient.Nickname);
            Console.WriteLine("Connected. Listening for server requests...");
            // List channels
            ircList();

            ircClient.Listen();
        
        }
        static void ircClient_OnJoin(object sender, JoinEventArgs e)
        {
            if (e.Data.Nick == ircClient.Nickname)
            {
                if (e.Channel.ToUpperInvariant().StartsWith(serverprefix))
                {
                    ircClient.RfcTopic(e.Channel, "SIZE=" + Properties.Settings.Default.MaxServers + " HOST=" + ircClient.Nickname); 
                }
            }

            // TODO: if(!requesteduser){ kick(e.data.nick) }
        }

        static void ircClient_OnChannelMessage(object sender, IrcEventArgs e){
            
        }

        static private void ircJoin(string Channel)
        {
            ircClient.JoinedChannels.Add(Channel);
            ircClient.RfcJoin(Channel); // channel syncing isn't implemented in meeby irc yet, so we need to manually call this
        }
        
        
        static Stopwatch ircListTimeout = new Stopwatch();

        static private void ircList()
        {
            ircListTimeout.Reset();
            ircListTimeout.Start();
            FilledList = false;
            ircClient.RfcList("");
            while (FilledList == false && ircListTimeout.ElapsedMilliseconds < 2500)
            {
                ircClient.ListenOnce(false);
                Thread.Sleep(0);
            }
            ircListTimeout.Stop();
        }
        static List<String[]> channelsstuff = new List<String[]>();
        static void ircClient_OnRawMessage(object sender, IrcEventArgs e)
        {

            if (e.Data.Type == ReceiveType.QueryNotice)
            {
                
            }

            //File.AppendAllText("irc.log", e.Data.RawMessage + "\r\n");

            if (e.Data.Type == ReceiveType.List)
            {
                if (e.Data.RawMessage.Contains("Channel :Users  Name"))
                {
                    ChannelList.Clear();
                    channelsstuff.Clear();
                    Topics.Clear();
                    FilledList = false;
                }
                else if (e.Data.Message == "End of /LIST")
                {
                    FilledList = true;
                }
                else
                {
                    ChannelList.Add(e.Data.RawMessageArray[3], int.Parse(e.Data.RawMessageArray[4]));
                    channelsstuff.Add(e.Data.RawMessageArray);
                    StringBuilder topic = new StringBuilder();
                    for (int i = 6; i < e.Data.RawMessageArray.Length; i++)
                    {
                        topic.Append(" ");
                        topic.Append(e.Data.RawMessageArray[i]);
                    }
                    Topics.Add(e.Data.RawMessageArray[3], topic.ToString().Trim());
                    
                }
            }
        }
        static string DetermineExternalIP()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://checkip.dyndns.org");
            WebResponse rep = req.GetResponse();
            string text = new StreamReader(rep.GetResponseStream()).ReadToEnd();
            text = text.Substring(text.IndexOf(":") + 2);
            text = text.Substring(0, text.IndexOf("<"));
            return text;
        }
        static string DetermineServerName(string FileName)
        {
            if (File.Exists(FileName))
            {
                string[] Lines = File.ReadAllLines(FileName);
                foreach (string Line in Lines)
                {
                    string LineTrimmed = Line.Trim().ToLowerInvariant();
                    if (LineTrimmed.StartsWith("hostname"))
                    {
                        return Line.Trim().Substring(LineTrimmed.IndexOf('"')).Replace("\"", "").Trim();
                    }
                }
                File.AppendAllText(FileName, "\r\nhostname \"D2 Custom Realms Server\"");
                return "D2 Custom Realms Server";
            }
            else
            {
                File.AppendAllText(FileName, "\r\nhostname \"D2 Custom Realms Server\"");
                return "D2 Custom Realms Server";
            }
        }
        static void ircClient_OnQueryNotice(object sender, IrcEventArgs e)
        {
            if (e.Data.Message == "REQ_S")
            {
                Console.WriteLine("Received server request. Checking quota...");
                ircList();
                foreach (KeyValuePair<string, int> Server in ChannelList)
                {
                    if (Server.Key == serverprefix + Properties.Settings.Default.Nickname)
                    {
                        if (Properties.Settings.Default.MaxServers > Server.Value - 1)
                        {
                            Console.WriteLine("Request accepted. Sending channel instructions to user {0}", e.Data.Nick);
                            Whitelist.Add(e.Data.Nick);
                            ircClient.SendMessage(SendType.Notice, e.Data.Nick, "JOIN " + Server.Key);
                        }
                        else
                        {
                            Console.WriteLine("Servercount over quota. Sending error response to {0}", e.Data.Nick);
                            ircClient.SendMessage(SendType.Notice, e.Data.Nick, "OVERQUOTA");
                        }
                    }
                }


            }
            else if (e.Data.Message.StartsWith("START"))
            {
                if (Whitelist.Contains(e.Data.Nick))
                {
                    // Delete autoexec.cfg
                    if (File.Exists(Properties.Settings.Default.ServerPath + "dota\\cfg\\autoexec.cfg"))
                    {
                        File.Delete(Properties.Settings.Default.ServerPath + "dota\\cfg\\autoexec.cfg");
                    }
                    int serverPort = availablePorts.First();
                    availablePorts.Remove(serverPort);
                    string HostConnection = DetermineExternalIP() + ":" + serverPort;

                    ircClient.SendMessage(SendType.Notice, e.Data.Nick, "STARTDOTA=" + HostConnection); //TODO: get event in client
                    string Dota2ServerName = DetermineServerName(Properties.Settings.Default.ServerPath + "dota\\cfg\\server.cfg");

                    string maxplayers = "", addon = "", map = "";

                    string[] ServerProperties = e.Data.Message.Split(' ');
                    foreach (string ServerProp in ServerProperties)
                    {
                        if (ServerProp.StartsWith("MAXPLAYERS="))
                        {
                            maxplayers = ServerProp.Substring(11);
                        }

                        if (ServerProp.StartsWith("ADDON="))
                        {
                            addon = ServerProp.Substring(6);
                        }

                        if (ServerProp.StartsWith("MAP="))
                        {
                            map = ServerProp.Substring(4);
                        }
                    }
                    // TODO: Fetch from addon instead of hardcoding
                    ProcessStartInfo serverStart = new ProcessStartInfo(Properties.Settings.Default.ServerPath + "srcds.exe", "-console -game dota -port " + serverPort.ToString() + " +maxplayers " + Math.Max(10, int.Parse(maxplayers)) + " +dota_local_addon_enable 1 +dota_local_addon_game " + addon + " +dota_local_addon_map " + addon + " +dota_force_gamemode 15 +update_addon_paths +map " + map);
                    ServerMonitor monitor = new ServerMonitor(e.Data.Nick, serverPort);
                    monitor.MonitorProcess(serverStart, Dota2ServerName, ircClient);
                    monitors.Add(monitor);
                    //Thread serverThread = new Thread(() =>ServerMonitor.MonitorProcess(serverStart, Dota2ServerName, ircClient, e.Data.Nick));
                    //serverThread.Start();
                }
                else
                {
                    // Unauthorized client tries to launch a server
                    ircClient.SendMessage(SendType.Message, e.Data.Nick, "Don't even try.");
                }
            }
        }
        static void ircClient_OnPart(object sender, PartEventArgs e)
        {
            Console.WriteLine("Player '" + e.Who + "' left the server channel.");
            // We can't remove an item in a list which we are enumerating, so we use another temporary list
            List<ServerMonitor> deleteMonitors = new List<ServerMonitor>();

            foreach (var monitor in monitors)
            {
                if (monitor.user == e.Who)
                {
                    try
                    {
                        Process p = Process.GetProcessById(monitor.processid);
                        Console.WriteLine("Killing server with pid " + monitor.processid);
                        p.Kill();
                    }
                    catch
                    {
                        Console.WriteLine("Unable to kill server with pid " + monitor.processid + ". Server probably crashed/quit.");
                    }
                    availablePorts.Add(monitor.port);
                    //monitors.Remove(monitor); - Causes "Collection was modified; enumeration operation may not execute" error
                    deleteMonitors.Add(monitor);

                }
            }

            foreach (var monitor in deleteMonitors)
            {
                monitors.Remove(monitor);
            }
            Whitelist.Remove(e.Who);
        }
    }
}
