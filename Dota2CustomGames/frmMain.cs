using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Meebey.SmartIrc4net;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Web;
using System.Net;
using System.Xml;
using Ionic.Zip;
using Ionic.Zlib;
using Gibbed.Valve.FileFormats;
using Microsoft.VisualBasic;
using System.Net.NetworkInformation;
using Tinker;
using Clockwerk;

namespace Dota2CustomRealms
{
    public partial class frmMain : Form
    {


        public const string UPDATE_SERVER = "http://dota2cr.com"; //"http://dota.windrunner.mx/frota";


        public frmMain()
        {
            InitializeComponent();
        }

        static Random rnd = new Random();

        /// <summary>
        /// Contains a list of controls and how long since they have been interacted with, in order to stop spam button pressing
        /// </summary>
        Dictionary<object, int> ActionSpamPrevention = new Dictionary<object, int>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <returns>True if control is being spammed, false if not</returns>
        private bool DetectButtonSpamming(object control)
        {
            if (ActionSpamPrevention.ContainsKey(control))
            {
                if (ActionSpamPrevention[control] > 20)
                {
                    ActionSpamPrevention[control] = 0;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                ActionSpamPrevention.Add(control, 0);
                return false;
            }
        }

        Game Game = null;

        /// <summary>
        /// List of RichTextBoxes that have new text and need scrolling in the timer
        /// </summary>
        List<RichTextBox> RichTextBoxesThatNeedScrolling = new List<RichTextBox>();

        const string CHAT_COLOURS = "{\\colortbl;\\red79\\green157\\blue204;\\red100\\green100\\blue100;\\red28\\green148\\blue151;\\red126\\green126\\blue126;\\red237\\green73\\blue36;\\red168\\green172\\blue130;\\red17\\green46\\blue162;\\red255\\green0\\blue0;\\red0\\green\\255\\blue0;\\red115\\green115\\blue115;}";

        Dictionary<string, int> ChannelList = new Dictionary<string, int>();
        Dictionary<string, string> Topics = new Dictionary<string, string>(); // TODO: make some nice class for this

        /// <summary>
        /// Used to determine when the IRC /LIST command response ends
        /// </summary>
        bool FilledList = false;

        
        Player SelectedPlayer = null;


        volatile bool ServerReady = false;

        Process Dota2, Dota2Server;
        IntPtr Dota2Window = IntPtr.Zero, Dota2ServerWindow = IntPtr.Zero;
        string Dota2ServerName;
        volatile string HostConnection = null;



        Dictionary<string, TabPage> ChatChannels = new Dictionary<string, TabPage>();

        //IrcClient ircClient;
        RealmConnector ServerConnection;

        

        /// <summary>
        /// Helper function to filter non alphanumeric chars from nicks
        /// </summary>
        /// <param name="Nick"></param>
        /// <returns></returns>
        private string FilterNick(string Nick)
        {
            return Regex.Replace(Nick.Replace(" ", "_"), "[^A-Za-z0-9_]", "");

        }
        


        /// <summary>
        ///  Connects to IRC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnectIRC_Click(object sender, EventArgs e)
        {
            UpdateClientHostUsableStatuses();
            Dictionary<String, String> userdict = new Dictionary<string, string>();
            List<String> vhosts = new List<String>();
            List<String> crstaff = new List<String>();

            tbxChooseNick.Text = FilterNick(tbxChooseNick.Text);
            if (tbxChooseNick.Text.Length < 3 || tbxChooseNick.Text.Length > 30)
            {
                MessageBox.Show("You need to enter a Nickname between 3 and 30 letters long!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!char.IsLetter(tbxChooseNick.Text.FirstOrDefault()))
            {
                MessageBox.Show("Your Nickname must start with a letter.");
            }
            else
            {
                gbxConnect.Enabled = false;
                this.AcceptButton = null;
                btnConnectIRC.Text = "Connecting...";

                gbxBanList.Visible = false;
                Properties.Settings.Default.NickName = tbxChooseNick.Text;

                ServerConnection = new RealmConnector();
                ServerConnection.OnError += ServerConnection_OnError;
                ServerConnection.OnAuthenticationSuccess += ServerConnection_OnAuthenticationSuccess;
                ServerConnection.OnChatMessage += ServerConnection_OnChatMessage;
                ServerConnection.OnAuthenticationFailure += ServerConnection_OnAuthenticationFailure;

                //ircClient.Connect("localhost", 6667);
                try
                {
                    ServerConnection.Connect(tbxChooseNick.Text);
                }
                catch
                {
                    MessageBox.Show("There was an error connecting to the server. Please check your internet connection and try again.");
                    Application.Exit();
                }
                if (!Properties.Settings.Default.ClientSetupComplete)
                {
                    MessageBox.Show("Before you are able to join or host games, you need to go to the settings interface and set up some options.");
                }
            }
        }

        void ServerConnection_OnAuthenticationFailure(object sender, ServerResponse e)
        {
            if (gbxConnect.InvokeRequired)
            {
                gbxConnect.Invoke(new EventHandler<Clockwerk.ServerResponse>(ServerConnection_OnAuthenticationFailure), new object[] { sender, e });
                return;
            }

            gbxConnect.Enabled = true;
            btnConnectIRC.Text = "Connect";
            MessageBox.Show("Can't use that username!\n" + e.Message);
        }


        void ServerConnection_OnChatMessage(object sender, ServerResponse e)
        {
            if (e.Source == ServerConnection.Nickname) return;
            if (gbxConnect.InvokeRequired)
            {
                gbxConnect.Invoke(new EventHandler<Clockwerk.ServerResponse>(ServerConnection_OnChatMessage), new object[] { sender, e });
                return;
            }
            AddChatMessage("Realm", e.Source + ": " + e.Message);
        }

        void ircClient_OnQuit(object sender, QuitEventArgs e)
        {
            if (Game != null)
            {

                if (Game != null && Game.Players.ContainsKey(e.Who))
                {
                    Game_DisplayUserMessage(Game, new Game.SendMessageEventArgs(null, e.Who + " has disconnected"));
                }

                Game.PlayerLeftChannel(e.Who);
                if (Game.HostName == e.Who)
                {
                    LeaveGame();
                    RevertMods(); // Just in case this is their second game this session and they didn't click the done button
                    MessageBox.Show("The host has quit, therefore this lobby has been disbanded.");
                }

            }

            AddChatMessage("#General", e.Who + " has disconnected");




        }

        private void btnLeaveSkills_Click(object sender, EventArgs e)
        {
            if (Game != null)
            {
                if (Game.IsHost)
                {
                    pgbConfigProgress.Visible = true;
                    lblConfigProgressMessage.Visible = true;
                    btnDone.Visible = false;
                    btnManualConnect.Visible = false;
                    HostConnection = null;
                    tabUISections.SelectedTab = tabConnected;

                    ircPart(Game.Channel);

                    /*if (Game.Players[ServerConnection.Nickname].Side == PlayerSide.Dire)
                        ircPart(Game.DireChannel);
                    if (Game.Players[ServerConnection.Nickname].Side == PlayerSide.Radiant)
                        ircPart(Game.RadiantChannel);
                    if (Game.Players[ServerConnection.Nickname].Side == PlayerSide.Spectator)
                        ircPart(Game.SpectatorChannel);
                    */
                    RevertMods();


                    

                    lbxLobbyDirePlayers.Items.Clear();
                    lbxLobbyRadiantPlayers.Items.Clear();
                    lbxLobbySpectators.Items.Clear();

                    // isHost = false;

                    //   Players.Clear();

                    //   Self.Ready = false;
                    btnJoinDire.Text = "Join Dire";
                    btnJoinRadiant.Text = "Join Radiant";

                    DetachGameEvents(Game);
                    Game = null;
                }
                else
                {
                    MessageBox.Show("Only hosts are allowed to use this button.");
                }
            }
        }

        bool VersionCheckSuccess = false;
        StringBuilder KnownIssues = new StringBuilder();
        /// <summary>
        /// True once all known issues have been retrieved
        /// </summary>
        bool GotKnownIssues = false;

        void ircClient_OnMotd(object sender, MotdEventArgs e)
        {
            if (e.MotdMessage.Contains(Properties.Settings.Default.MyVersion + " ") || (e.MotdMessage.ToLowerInvariant().Contains("other clients") && !VersionCheckSuccess))
            {
                if (e.MotdMessage.Contains(" COMPATIBLE"))
                {
                    Properties.Settings.Default.VersionStatus = "COMPATIBLE";
                    Properties.Settings.Default.Save();
                }
                else if (e.MotdMessage.Contains(" INCOMPATIBLE"))
                {
                    Properties.Settings.Default.VersionStatus = "INCOMPATIBLE";
                    Properties.Settings.Default.Save();
                }

                VersionCheckSuccess = true;
                VersionCheck();
            }
            else if (e.MotdMessage.Contains("ISSUES: "))
            {
                KnownIssues.Append(e.MotdMessage.Substring(e.MotdMessage.IndexOf(":") + 2));
            }
            else if (e.MotdMessage.Contains("END OF KNOWN ISSUES"))
            {
                GotKnownIssues = true;
            }
        }

        protected void Link_Clicked (object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void AddChatMessage(string Channel, string Message)
        {
            if (ChatChannels.ContainsKey(Channel))
            {
                RichTextBox rtxChatMessages = (RichTextBox)ChatChannels[Channel].Controls[0];

                /*if (rtxChatMessages.Text.Length > 0)
                {
                    rtxChatMessages.Text += "\n";
                }*/
                string strRTF;
                if (rtxChatMessages.Tag != null)
                {
                    strRTF = rtxChatMessages.Tag.ToString();
                }
                else
                {
                    strRTF = rtxChatMessages.Rtf;
                }

                // Colour code grabbed from http://www.codeproject.com/Articles/15038/C-Formatting-Text-in-a-RichTextBox-by-Parsing-the

                /* 
                 * ADD COLOUR TABLE TO THE HEADER FIRST 
                 * */

                // Search for colour table info, if it exists (which it shouldn't)
                // remove it and replace with our one
                int iCTableStart = strRTF.IndexOf("colortbl;");

                // MR. RTF stuffed up, this can't run more than once or it breaks the rtf formatting :P
                //if (iCTableStart != -1) //then colortbl exists
                //{
                //    //find end of colortbl tab by searching
                //    //forward from the colortbl tab itself
                //    int iCTableEnd = strRTF.IndexOf('}', iCTableStart);
                //    strRTF = strRTF.Remove(iCTableStart, iCTableEnd - iCTableStart);

                //    //now insert new colour table at index of old colortbl tag
                //    strRTF = strRTF.Insert(iCTableStart,
                //        // CHANGE THIS STRING TO ALTER COLOUR TABLE
                //        CHAT_COLOURS);
                //}

                ////colour table doesn't exist yet, so let's make one
                //else
                if(iCTableStart == -1)
                {
                    // find index of start of header
                    int iRTFLoc = strRTF.IndexOf("\\rtf");
                    // get index of where we'll insert the colour table
                    // try finding opening bracket of first property of header first                
                    int iInsertLoc = strRTF.IndexOf('{', iRTFLoc);

                    // if there is no property, we'll insert colour table
                    // just before the end bracket of the header
                    if (iInsertLoc == -1) iInsertLoc = strRTF.IndexOf('}', iRTFLoc) - 1;

                    // insert the colour table at our chosen location                
                    strRTF = strRTF.Insert(iInsertLoc,
                        // CHANGE THIS STRING TO ALTER COLOUR TABLE
                        CHAT_COLOURS);
                }

                // colours
                //1 = self
                //2 = self / otherplayer text
                //3 = otherplayer
                //4 = connected
                //5 = admin
                //6 = otheropponent
                //7 = Verified Hosts
                //8 - left / dc'ed (only for non #General channels)
                //9 - Is hosting a new game message

                Message = Message.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}");

                // Lets format the text!

                Player ChatMessageSender = null;
                bool Enemy = false;
                bool Self = false;


                string MessageInitiator;
                //int IndexOfSpace = Message.IndexOf(' ');
                int IndexOfColon = Message.IndexOf(':');

                if (IndexOfColon != -1)
                {
                    MessageInitiator = Message.Substring(0, IndexOfColon);

                    if (Game != null)
                    {
                        foreach (KeyValuePair<string, Player> Player in Game.Players)
                        {
                            if (MessageInitiator == Player.Key)
                            {
                                ChatMessageSender = Player.Value;
                                if (Game.Players.ContainsKey(ServerConnection.Nickname) && Game.Players[ServerConnection.Nickname].Side != ChatMessageSender.Side)
                                {
                                    Enemy = true;
                                }
                                if (ChatMessageSender.Name == ServerConnection.Nickname)
                                {
                                    Self = true;
                                }
                                break;
                            }
                        }
                    }
                    else if (MessageInitiator == ServerConnection.Nickname)
                    {
                        Self = true;
                    }
                }

                if (IndexOfColon != -1) // Chat message
                {
                    string ChatName = Message.Substring(0, Message.IndexOf(':')).Replace("_", " ");
                    string ChatMessage = Message.Substring(ChatName.Length);
                    if(ChatName.StartsWith("[CR]")) // ADMIN
                    {
                        Message = "\\b\\cf5" + ChatName + "\\b0" + ChatMessage;
                    }
                    else if (ChatName.StartsWith("[VHost]")) // VERIFIED HOSTS
                    {
                        Message = "\\b\\cf7" + ChatName + "\\b0\\cf2" + ChatMessage;
                    }
                    else if (Self)
                    {
                        Message = "\\b\\cf1 " + ChatName + "\\b0\\cf2 " + ChatMessage;
                    }
                    else if (Enemy)
                    {
                        Message = "\\b\\cf6 " + ChatName + "\\b0\\cf2 " + ChatMessage;
                    }
                    else
                    {
                        Message = "\\b\\cf3 " + ChatName + "\\b0\\cf2 " + ChatMessage;
                    }
                }
                else // Some other kind of message
                {
                    if (Channel != "#General" && (Message.EndsWith(" has quit") || Message.EndsWith(" has disconnected") || Message.EndsWith(" has left")))
                    {
                        Message = "\\b\\cf8" + Message + "\\b0";
                    }
                    else if (Message.EndsWith(" is hosting a new game lobby."))
                    {
                        Message = "\\cf9" + Message;
                    }
                    else if (Message.EndsWith(" is hosting a new game lobby!"))
                    {
                        Message = "\\b\\cf7" + Message + "\\b0";
                    }
                    else
                    {
                        Message = "\\cf4" + Message;
                    }
                }
                Message = Message.Replace("\n", "\\par\r\n");
                Message += "\\cf0\\par\r\n}";

                strRTF = strRTF.Remove(strRTF.LastIndexOf('}')) + Message;

                rtxChatMessages.Rtf = strRTF;
                rtxChatMessages.Tag = strRTF;
                //rtxChatMessages.Rtf = rtxChatMessages.Rtf.Remove(rtxChatMessages.Rtf.LastIndexOf('}')) + Message;


                if (!RichTextBoxesThatNeedScrolling.Contains(rtxChatMessages))
                {
                    RichTextBoxesThatNeedScrolling.Add(rtxChatMessages);
                }
            }
        }


        void ircClient_OnPart(object sender, PartEventArgs e)
        {
            AddChatMessage(e.Channel, e.Who + " has left");
            if (Game != null && e.Data.Channel.ToLowerInvariant() == Game.Channel.ToLowerInvariant())
            {
                if (Game != null && Game.Channel == e.Channel && Game.Players.ContainsKey(e.Who))
                {
                    Game.Players.Remove(e.Who);
                    if (Game.HostName == e.Who)
                    {
                        LeaveGame();
                        RevertMods(); // Just in case this is their second game this session and they didn't click the done button
                        MessageBox.Show("The host has left the lobby, therefore this lobby has been disbanded.");
                    }
                }

            }
        }

        void ircClient_OnQueryNotice(object sender, IrcEventArgs e)
        {
            //if (Enum.IsDefined(typeof(PlayerSide), e.Data.Message) == true)
            //{
            //    SwitchSide(e.Data.Nick, e.Data.Message);
            //}
            //if (e.Data.Message.Contains("SIDE?"))
            //{
            //    ircClient.SendMessage(SendType.Notice, e.Data.Nick, Self.Side.ToString());
            //}
            //if (e.Data.Message.Contains("READY?"))
            //{
            //    ircClient.SendMessage(SendType.Notice, e.Data.Nick, (Self.Ready ? "READY!" : "NOTREADYYET!"));
            //}
            if (e.Data.Message == "READY!")
            {
                PlayerStatus(e.Data.Nick, true);
            }
            if (e.Data.Message == "NOTREADYYET!")
            {
                PlayerStatus(e.Data.Nick, false);
            }
            if (Game != null)
            {
                Game.ReceivePlayerNotice(e.Data.Nick, e.Data.Message);
            }
        }

        void ircClient_OnChannelNotice(object sender, IrcEventArgs e)
        {
            if (e.Data.Channel.ToLowerInvariant() == "#general" && e.Data.Message.StartsWith("NEWLOBBY"))
            {
                if (!e.Data.Message.Substring(8).Equals(Properties.Settings.Default.MyVersion))
                {

                }
                else if (e.Data.Nick.StartsWith("[VHost]"))
                {
                    AddChatMessage("#General", e.Data.Nick + " is hosting a new game lobby!");
                    if (Properties.Settings.Default.BeepNew && Game == null)
                    {
                        System.Media.SystemSounds.Beep.Play();
                    }
                    if (Properties.Settings.Default.FlashNew && Game == null)
                    {
                        FlashWindow.Flash(this, 1);
                    }
                }
                else
                {
                    AddChatMessage("#General", e.Data.Nick + " is hosting a new game lobby.");
                    if (Properties.Settings.Default.BeepNew && Game == null)
                    {
                        System.Media.SystemSounds.Beep.Play();
                    }
                    if (Properties.Settings.Default.FlashNew && Game == null)
                    {
                        FlashWindow.Flash(this, 1);
                    }
                }
            }

            if (Game != null && Game.Channel == e.Data.Channel)
            {
                Game.ReceiveChannelNotice(e.Data.Channel, e.Data.Nick, e.Data.Message);
            }
            //if (e.Data.Channel.ToLowerInvariant() == GameChannel.ToLowerInvariant())
            //{
            //    if (e.Data.Message.Contains("READY?"))
            //    {
            //        ircClient.SendMessage(SendType.Notice, e.Data.Nick, (Self.Ready ? "READY!" : "NOTREADYYET!"));
            //    }
            //    if (e.Data.Message.Contains("SIDE?"))
            //    {
            //        ircClient.SendMessage(SendType.Notice, e.Data.Nick, Self.Side.ToString());
            //    }
            if (e.Data.Message == "READY!")
            {
                PlayerStatus(e.Data.Nick, true);
            }
            if (e.Data.Message == "NOTREADYYET!")
            {
                PlayerStatus(e.Data.Nick, false);
            }
            //    if (Enum.IsDefined(typeof(PlayerSide), e.Data.Message) == true)
            //    {
            //        SwitchSide(e.Data.Nick, e.Data.Message);
            //    }
            //    if (e.Data.Message.StartsWith("HEROPICK"))
            //    {
            //        int Time = 30000;
            //        foreach (string Section in e.Data.MessageArray)
            //        {
            //            if (Section.StartsWith("RND"))
            //            {
            //                int SeedValue = int.Parse(Section.Split('=')[1]);
            //                //File.WriteAllText("DEBUG rnd.txt", SeedValue.ToString());
            //                TimedDraft.SharedRandom = new Random(SeedValue);
            //            }
            //            else if (Section.StartsWith("TIMELEFT"))
            //            {
            //                Time = DetermineDraftTime(int.Parse(Section.Split('=')[1]));
            //            }
            //        }
            //        StartGame(Time);
            //    }
            //    if (e.Data.Message.StartsWith("HERO="))
            //    {
            //        foreach (string Section in e.Data.MessageArray)
            //        {
            //            if (Section.StartsWith("TIMELEFT"))
            //            {
            //                if (HeroDraft != null)
            //                {
            //                    HeroDraft.TimeRemaining = DetermineDraftTime(int.Parse(Section.Split('=')[1]));;
            //                }
            //            }
            //        }
            //        HeroDraft.VoluntaryPick(e.Data.MessageArray[0].Substring(5).Replace("_", " "));
            //    }
            //    if (e.Data.Message.StartsWith("SKILL="))
            //    {
            //        foreach (string Section in e.Data.MessageArray)
            //        {
            //            if (Section.StartsWith("TIMELEFT"))
            //            {
            //                if (SkillDraft != null)
            //                {
            //                    SkillDraft.TimeRemaining = DetermineDraftTime(int.Parse(Section.Split('=')[1])); ;
            //                }
            //            }
            //        }
            //        SkillDraft.VoluntaryPick(e.Data.MessageArray[0].Substring(6).Replace("_", " "));
            //    }
            if (e.Data.Message.StartsWith("STARTDOTA="))
            {
                HostConnection = e.Data.Message.Substring(10);
                ServerReady = false;
            }
            if (e.Data.Message == "SERVERREADY")
            {
                //if (SetupGame != null)
                //{
                //    SetupGame.ServerIsReady();
                //}
                ServerReady = true;
            }
            //    //if (e.Data.Message == "PLAYERJOINED")
            //    //{
            //    //    PlayersJoined++;
            //    //}
            //}
        }
        List<String[]> channelsstuff = new List<String[]>();

        void ircClient_OnChannelMessage(object sender, IrcEventArgs e)
        {
            if (e.Data.Message.StartsWith("!!!") && e.Data.Nick.StartsWith("[CR]"))
            {
                MessageBox.Show(e.Data.Message.Substring(4),"Announcement");
            }
            else
            {
                AddChatMessage(e.Data.Channel, e.Data.Nick + ": " + e.Data.Message);
                bool contains = e.Data.Message.IndexOf(ServerConnection.Nickname, StringComparison.OrdinalIgnoreCase) >= 0;
                if (contains)
                {
                    if (Properties.Settings.Default.BeepName)
                        System.Media.SystemSounds.Beep.Play();
                    if (Properties.Settings.Default.FlashName)
                        FlashWindow.Flash(this, 1);
                }
            }
        }

        void ircClient_OnNames(object sender, NamesEventArgs e)
        {
            /* if (e.Channel.ToUpperInvariant() == GameChannel.ToUpperInvariant())
             {
                 foreach (string Name in e.UserList)
                 {

                 }
             }*/
        }

        void ircClient_OnJoin(object sender, JoinEventArgs e)
        {
            if (e.Data.Nick == ServerConnection.Nickname)
            {


                if (!ChatChannels.ContainsKey(e.Channel))
                {
                    ChatChannels.Add(e.Channel, new TabPage());
                }
                if (!gbxGameSize.Controls.Contains(ChatChannels[e.Channel]))
                {
                    gbxGameSize.Controls.Add(ChatChannels[e.Channel]);
                }

                bool SwitchTo = true;
                if (e.Channel.ToUpperInvariant().StartsWith("#G_"))
                {
                    ChatChannels[e.Channel].Text = "Lobby";
                    if (Game != null && Game.IsHost)
                    {
                        string gamemodes = "";
                        foreach (string mode in Game.AdditionalModes)
                        {
                            gamemodes += mode + "/";
                        }
                        string topicstr = "MODE=" + Game.GameMode + " SIZE=" + Game.MaxLobbySize + " HOST=" + ServerConnection.Nickname + " PASS=" + Game.Password + " MAP=" + Game.Dotamap + " VER=" + Properties.Settings.Default.MyVersion + " ADD=" + gamemodes;//+ " IP=" + DetermineExternalIP().Substring(1));
                        if (Game.CustomMod != null && Game.CustomMod.Length > 0)
                        {
                            topicstr += " CUSTOMMOD=" + Game.CustomMod;
                        }
                        //ircClient.RfcTopic(e.Channel, topicstr); 
                    }
                    else if (Game != null)
                    {
                        tabUISections.SelectedTab = tabLobby;
                        //ircClient.SendMessage(SendType.Notice, e.Channel, Game.GAME_SYNC_PLAYERLIST);
                        //SendPlayerNotice(this, new SendMessageEventArgs(HostName, GAME_SYNC_PLAYERLIST_REQUEST));

                    }
                }
                else if (e.Channel.ToUpperInvariant().StartsWith("#R_"))
                {
                    ChatChannels[e.Channel].Text = "Radiant";
                    SwitchTo = false;
                }
                else if (e.Channel.ToUpperInvariant().StartsWith("#D_"))
                {
                    ChatChannels[e.Channel].Text = "Dire";
                    SwitchTo = false;
                }
                else if (e.Channel.ToUpperInvariant().StartsWith("#S_"))
                {
                    ChatChannels[e.Channel].Text = "Spectators";
                    SwitchTo = false;
                }
                else
                {
                    ChatChannels[e.Channel].Text = e.Channel.Replace("#", "");
                }
                ChatChannels[e.Channel].Tag = e.Channel;
                RichTextBox rtxChannelChat = new RichTextBox();
                rtxChannelChat.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(Link_Clicked);

                rtxChannelChat.Text = "";
                rtxChannelChat.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang5129{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}" + CHAT_COLOURS + "\r\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\r\n}";


                rtxChannelChat.Parent = ChatChannels[e.Channel];
                rtxChannelChat.Dock = DockStyle.Fill;
                rtxChannelChat.WordWrap = true;
                rtxChannelChat.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
                rtxChannelChat.ReadOnly = true;
                if (SwitchTo)
                {
                    gbxGameSize.SelectedTab = ChatChannels[e.Channel];
                }
            }
            else
            {
                if (Game != null && Game.IsHost && e.Channel == Game.Channel)
                {
                    Player Player = new Player();
                    Player.Name = e.Who;
                    Game.Players.Add(e.Who, Player);
                }
                AddChatMessage(e.Channel, e.Data.Nick + " has connected");
            }
        }

 
        
        void ServerConnection_OnAuthenticationSuccess(object sender, ServerResponse e)
        {

            if (gbxConnect.InvokeRequired)
            {
                gbxConnect.Invoke(new EventHandler<ServerResponse>(ServerConnection_OnAuthenticationSuccess), new object[] { sender, e });
                return;
            }


            gbxConnect.Enabled = true;

            ircListener.Enabled = true;
            ircListener.Interval = 50;
            timerPlayers.Enabled = true;
            timerPlayers.Interval = 2000;

            gbxChat.Visible = true;

            lblMessageLeft.Text = "Welcome, " + e.Target + "!";
            tabUISections.SelectedTab = tabConnected;



            gbxChat.Visible = true;
            gbxConnect.Enabled = true;

            //ircListener.Enabled = true;
            //ircListener.Interval = 50;
            //timerPlayers.Enabled = true;
            //timerPlayers.Interval = 2000;

            //gbxChat.Visible = true;




            lblMessageLeft.Text = "Welcome!";
            tabUISections.SelectedTab = tabConnected;

            ChatChannels.Add("Realm", new TabPage());
            gbxGameSize.Controls.Add(ChatChannels["Realm"]);
            ChatChannels["Realm"].Tag = "Realm";
            RichTextBox rtxChannelChat = new RichTextBox();
            rtxChannelChat.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(Link_Clicked);

            rtxChannelChat.Text = "";
            rtxChannelChat.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang5129{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}" + CHAT_COLOURS + "\r\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\r\n}";


            rtxChannelChat.Parent = ChatChannels["Realm"];
            rtxChannelChat.Dock = DockStyle.Fill;
            rtxChannelChat.WordWrap = true;
            rtxChannelChat.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            rtxChannelChat.ReadOnly = true;
            gbxGameSize.SelectedTab = ChatChannels["Realm"];


        }
        void ServerConnection_OnError(object sender, ClockwerkError e)
        {
            if (gbxConnect.InvokeRequired)
            {
                gbxConnect.Invoke(new EventHandler<ClockwerkError>(ServerConnection_OnError), new object[]{sender, e});
                return;
            }

            gbxConnect.Enabled = true;
            btnConnectIRC.Text = "Connect";
            MessageBox.Show("There was a problem connecting to the server!\n" + e.Error.Message);
            tabUISections.SelectedTab = tabPreConnect;

            // TODO: Cleanup rest of UI
        }

        private void ircListener_Tick(object sender, EventArgs e)
        {
            //ircClient.Listen(false);


            if (Game != null)
            {
                Game.CheckCustomModStatus();
            }

            if (GotKnownIssues && ChatChannels.ContainsKey("#General"))
            {
                GotKnownIssues = false;
                AddChatMessage("#General", "KNOWN ISSUES:\n" + KnownIssues.ToString().Replace("\\n", "\n"));
            }

            foreach (RichTextBox Box in RichTextBoxesThatNeedScrolling)
            {
                Box.SelectionStart = Box.Text.Length;
                Box.ScrollToCaret();
            }

            foreach (object Timer in ActionSpamPrevention.Keys.ToList())
            {
                ActionSpamPrevention[Timer]++;
                if (ActionSpamPrevention[Timer] > 20)
                {
                    ActionSpamPrevention.Remove(Timer);
                }
            }

            RichTextBoxesThatNeedScrolling.Clear();

        }

        private void timerPlayers_Tick(object sender, EventArgs e)
        {
            int playersingame = 0;
            if (channelsstuff != null)
            {
                foreach (string[] chan in channelsstuff)
                {
                    if (chan[3].StartsWith("#G_"))
                    {
                        playersingame += int.Parse(chan[4]);
                    }
                }
            }
            try
            {
                //lblPlayersOnline.Text = "Players Online: " + ircClient.GetChannel("#General").Users.Count;
                lblPlayersOnline.Enabled = true;
                lblPlayersInGame.Text = "Players Ingame: " + playersingame;
                lblPlayersInGame.Enabled = true;
            }
            catch
            {
                // We don't really care if this causes errors (Jeez aren't we awful developers!)
            }
        }

        private void btnHostLobby_Click(object sender, EventArgs e)
        {
            RevertMods(); // Just in case this is their second game this session and they didn't click the done button

            if (File.Exists(Properties.Settings.Default.Dota2ServerPath + "srcds.exe"))
            {
                if (!Properties.Settings.Default.Dedicated)
                {
                    tabUISections.SelectedTab = tabHostLobby;
                    tbxGameName.Clear();
                    
                    tbxGamePassword.Clear();
                    cbxGameSize.SelectedIndex = 9;
                    chkLobbyPlayerReady.CheckedChanged -= chkLobbyPlayerReady_CheckedChanged;
                    chkLobbyPlayerReady.Checked = false;
                    chkLobbyPlayerReady.CheckedChanged += chkLobbyPlayerReady_CheckedChanged;
                }
                else
                {
                    tabUISections.SelectedTab = tabHostLobby;

                    chkLobbyPlayerReady.CheckedChanged -= chkLobbyPlayerReady_CheckedChanged;
                    chkLobbyPlayerReady.Checked = false;
                    chkLobbyPlayerReady.CheckedChanged += chkLobbyPlayerReady_CheckedChanged;
                }
            }
            else
            {
                MessageBox.Show("You haven't setup a Dota 2 server on your computer.\nPlease visit the settings page to set one up.");
            }
        }

        private void tbxChatMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkConDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConDebug.Checked)
            {
                Properties.Settings.Default.ConDebug = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.ConDebug = false;
                Properties.Settings.Default.Save();
            }
        }

        private void cbxVersionFixDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxVersionFixDisable.Checked)
            {
                Properties.Settings.Default.DisableVersion = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.DisableVersion = false;
                Properties.Settings.Default.Save();
            }
        }

        private void tbxChatMessage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnSendMessage_Click(sender, e);
            }
        }


        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            tbxChatMessage.Text = tbxChatMessage.Text.Replace("\r", "").Replace("\n", "");
            if (tbxChatMessage.Text.Trim().Length > 0)
            {
                //AddChatMessage((string)gbxGameSize.SelectedTab.Tag, ServerConnection.Nickname + ": " + tbxChatMessage.Text);
                //ircClient.SendMessage(SendType.Message, (string)gbxGameSize.SelectedTab.Tag, tbxChatMessage.Text);
                AddChatMessage("Realm", ServerConnection.Nickname + ": " + tbxChatMessage.Text);
                ServerConnection.SendChatMessage(tbxChatMessage.Text);
                tbxChatMessage.Clear();
                tbxChatMessage.Select();
            }
        }

        private void btnCancelHosting_Click(object sender, EventArgs e)
        {
            tabUISections.SelectedTab = tabConnected;
        }

        /// <summary>
        /// Helper function for joining an IRC Channel
        /// </summary>
        /// <param name="Channel"></param>
        private void ircJoin(string Channel)
        {
            //ircClient.JoinedChannels.Add(Channel);
            //ircClient.RfcJoin(Channel); // channel syncing isn't implemented in meeby irc yet, so we need to manually call this
        }

        private void ircPart(string Channel)
        {
            //ircClient.JoinedChannels.Remove(Channel);
            //ircClient.RfcPart(Channel);

            if (ChatChannels.ContainsKey(Channel))
            {
                gbxGameSize.Controls.Remove(ChatChannels[Channel]);
                ChatChannels.Remove(Channel);
            }

        }

        Stopwatch ircListTimeout = new Stopwatch();

        private void ircList()
        {
            ircListTimeout.Reset();
            ircListTimeout.Start();
            FilledList = false;
            //ircClient.RfcList("");
            while (FilledList == false && ircListTimeout.ElapsedMilliseconds < 2500)
            {
                //ircClient.ListenOnce(false);
                Thread.Sleep(0);
            }
            ircListTimeout.Stop();
        }

        private void btnHostGame_Click(object sender, EventArgs e)
        {

            tbxGameName.Text = Regex.Replace(tbxGameName.Text.Replace(" ", "_"), "[^A-Za-z0-9_]", "");
            if (tbxGameName.Text.Length == 0)
            {
                MessageBox.Show("You need to enter a Game Name!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tbxGameName.Text.Length > 25)
            {
                MessageBox.Show("Game Names can't be over 25 characters in length!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tbxGamePassword.Text.Length > 12)
            {
                MessageBox.Show("Game Password cannot be long than 12 characters in length!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbxAddonType.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select an addon to use!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbxAddonMap.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a map to use!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chkLobbyPlayerReady.Checked = false;


            Game = new Game();
            

            Game.LobbyName = tbxGameName.Text;
            lblLobbyName.Text = tbxGameName.Text;
            
            Game.IsHost = true;
            Game.HostName = ServerConnection.Nickname;
            Game.MyName = ServerConnection.Nickname;
            Game.Password = tbxGamePassword.Text;
            Game.MaxLobbySize = int.Parse(cbxGameSize.Text);

            //Game.Dotamap = cbxMap.Text;
            Game.Dotamap = cbxAddonMap.Text;
            Game.CustomMod = cbxAddonType.Text;

            Player Self = new Player();
            Self.Name = ServerConnection.Nickname;








            Game.Channel = "#G_ " + tbxGameName.Text;


            string ReplaceChars = "01234567890qwertyuiopasdfghjklzxcvbnm";

            // Reduce the chance of two users simultaneously creating identical channels by randomising where in the ReplaceChars to begin

            Random rnd = new Random();
            int ShuffleLoc = rnd.Next(0, ReplaceChars.Length);
            ReplaceChars = ReplaceChars.Substring(ShuffleLoc) + ReplaceChars.Substring(0, ShuffleLoc);

            int i = 0;

            ircList();

            bool Unique = true;

            // Ensure channel name is unique by cycling through the extra uniqueness character in the channel name until a free one is found

            do
            {
                Unique = true;
                string ChannelAttempt = Game.Channel.Replace(" ", ReplaceChars.Substring(i, 1)).ToLowerInvariant();
                foreach (KeyValuePair<string, int> Channel in ChannelList)
                {
                    if (ChannelAttempt == Channel.Key.ToLowerInvariant())
                    {
                        Unique = false;
                        break;
                    }
                }
                i++;
            }
            while (Unique == false && i < ReplaceChars.Length);

            if (i >= ReplaceChars.Length)
            {
                MessageBox.Show("Too many channels with this name, please try a different name.");
                Game.Channel = "";
                return;
            }

            Game.Channel = Game.Channel.Replace(" ", ReplaceChars.Substring(i, 1));


            ircJoin(Game.Channel);

            //JoinGame(GameChannel);
            Game.IsHost = true;

            btnStart.Enabled = Game.IsHost;
            btnLobbyKick.Enabled = Game.IsHost;
            btnLobbyRandomiseTeams.Enabled = Game.IsHost;
            
            if (Game.IsHost)
            {
                if (Game.AdditionalModes.Contains("Dedicated")) // Disable buttons that the host shouldn't be pushing.
                {
                    btnStart.Text = "Starts when all players are ready.";
                    btnJoinDire.Enabled = false;
                    btnJoinRadiant.Enabled = false;
                    chkLobbyPlayerReady.Enabled = false;
                    btnStart.Enabled = false;
                }
                else
                {
                    btnStart.Text = "Start Game";
                    btnStart.Enabled = true;
                    btnJoinRadiant.Enabled = true;
                    chkLobbyPlayerReady.Enabled = true;
                    btnJoinDire.Enabled = true;
                }
                btnLobbyKick.Text = "Kick Player";
                btnLobbyRandomiseTeams.Text = "Scramble Teams";
                btnLobbyKick.Show();
                btnLobbyRandomiseTeams.Show();
                labelHost.Text = "Host: " + ServerConnection.Nickname;
                labelMaxPlayers.Text = "Max Players: " + Game.MaxLobbySize.ToString();
                labelMap.Text = "Map: " + cbxAddonMap.Text;
                labelAddon.Text = "Addon: " + cbxAddonType.Text;
            }
            else
            {
                if (Game.AdditionalModes.Contains("Dedicated"))
                {
                    btnStart.Text = "Starts when all players are ready.";
                }
                else
                {
                    btnStart.Text = "Host Starts";
                    btnLobbyKick.Text = "Host-Only";
                    btnLobbyRandomiseTeams.Text = "Host-Only";
                }
            }


            AttachGameEvents(Game);


            Game.Players.Add(ServerConnection.Nickname, Self);
            if (Game.AdditionalModes.Contains("Dedicated"))
            {
                Game.Players[ServerConnection.Nickname].Dedi = true;
            }
            tabUISections.SelectedTab = tabLobby;

            //UpdatePlayerReadyCount();
            if (tbxGamePassword.Text == "")
            {
                //ircClient.SendMessage(SendType.Notice, "#General", "NEWLOBBY" + Properties.Settings.Default.MyVersion);
            }
            if (!cbxVersionFixDisable.Checked)
            {
                string oldversion, newversion= "";

                using (StreamReader reader = new StreamReader(Properties.Settings.Default.Dota2ServerPath + "\\dota\\steam.inf"))
                {
                    oldversion = reader.ReadLine();
                    oldversion = oldversion.Substring(14);
                }
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("http://api.steampowered.com/IGCVersion_570/GetServerVersion/v1?format=xml");
                    XmlNode node = doc.SelectSingleNode("result/active_version");
                    newversion = node.InnerText;
                }
                catch
                {
                    return;
                }
                if (int.Parse(oldversion) != int.Parse(newversion))
                {
                    StringBuilder blank = new StringBuilder();
                    string[] file = File.ReadAllLines(Properties.Settings.Default.Dota2ServerPath + "\\dota\\steam.inf");
                    foreach (string line in file)
                    {
                        if (line.Contains(oldversion))
                        {
                            string temp = line.Replace(oldversion, newversion);
                            blank.Append(temp + "\r\n");
                            continue;
                        }
                        blank.Append(line + "\r\n");
                        File.WriteAllText(Properties.Settings.Default.Dota2ServerPath + "\\dota\\steam.inf", blank.ToString());
                    }
                    MessageBox.Show("Your Dota 2 Server version (" + oldversion + ") has been updated to match the Dota 2 Client (" + newversion + ").");
                }
            }
        }

        private void tbxGameName_TextChanged(object sender, EventArgs e)
        {
            if (tbxGameName.Text.Length > 26)
            {
                tbxGameName.Text = tbxGameName.Text.Substring(0, 26);
                System.Media.SystemSounds.Beep.Play();
            }
        }

        bool btnGamesListRefreshWait = false;

        private void btnGamesListRefreshAntispam(object sender, EventArgs e)
        {
            btnGamesListRefreshWait = false;
        }

        private void btnGameListRefresh_Click(object sender, EventArgs e)
        {
            if (btnGamesListRefreshWait == true)
            {
                return;
            }
            grdGamesList.Rows.Clear();
            lblGameListRefresh.Visible = true;
            this.Refresh();
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 2000; //In seconds here
            t.AutoReset = true; //Stops it from repeating
            t.Elapsed += new System.Timers.ElapsedEventHandler(btnGamesListRefreshAntispam);

            Dictionary<string, int> Games = new Dictionary<string, int>();
            ircList();
            foreach (KeyValuePair<string, int> Game in ChannelList)
            {
                string Substring = Game.Key.Substring(3);
                if (Game.Key.StartsWith("#G_") && !ChannelList.ContainsKey("#R_" + Substring) && !ChannelList.ContainsKey("#D_" + Substring) && !ChannelList.ContainsKey("#S_" + Substring))
                {
                    Games.Add(Game.Key, Game.Value);
                }
            }
            foreach (KeyValuePair<string, int> Game in Games)
            {
                try
                {
                    string Host = null;
                    string Lock = "No";
                    int MaxPlayers = 0;
                    string pass = "";
                    string version = Properties.Settings.Default.MyVersion;
                    string addon = "", map = "";
                    //int ping = 999;
                    //IPAddress ip = null;


                    string[] GameProperties = Topics[Game.Key].Split(' ');

                    foreach (string GameProp in GameProperties)
                    {
                        if (GameProp.StartsWith("HOST="))
                        {
                            Host = GameProp.Substring(5);
                        }
                        if (GameProp.StartsWith("SIZE="))
                        {
                            MaxPlayers = int.Parse(GameProp.Substring(5));
                        }
                        if (GameProp.StartsWith("PASS="))
                        {
                            pass = GameProp.Substring(5);
                            if (pass != "")
                                Lock = "Yes";
                        }

                        if (GameProp.StartsWith("VER="))
                        {
                            version = GameProp.Substring(4);
                        }

                        if (GameProp.StartsWith("CUSTOMMOD="))
                        {
                            addon = GameProp.Substring(10);
                        }
                        if (GameProp.StartsWith("MAP="))
                        {
                            map = GameProp.Substring(4);
                        }
                        
                        //if (GameProp.StartsWith("IP="))
                        //{
                        //    ip = IPAddress.Parse(GameProp.Substring(3));
                        //}
                    }
                    if (Properties.Settings.Default.MyVersion == version)
                    {
                        if (pass != "" && cbxLocked.Checked == true)
                        {

                        }
                        else if (MaxPlayers == 0)
                        {

                        }
                        else
                        {
                            int rowid = grdGamesList.Rows.Add(new object[] { Game.Key, Lock, Game.Key.Substring(4).Replace("_", " "), Host, Game.Value + "/" + MaxPlayers, addon, map});
                        }
                   }
                }
                catch
                {
                    MessageBox.Show("Invalid game encountered!");
                }
            }
            lblGameListRefresh.Visible = false;
            btnGamesListRefreshWait = true;
            t.Start();
        }

        /*private int PingTest(IPAddress ip)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            int ping = 999;
            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                ping = (int)reply.RoundtripTime;
            }

            return ping;
        }*/

        private void btnFindLobby_Click(object sender, EventArgs e)
        {
            chkLobbyPlayerReady.CheckedChanged -= chkLobbyPlayerReady_CheckedChanged;
            chkLobbyPlayerReady.Checked = false;
            chkLobbyPlayerReady.CheckedChanged += chkLobbyPlayerReady_CheckedChanged;

            RevertMods(); // Just in case this is their second game this session and they didn't click the done button

            lblJoinAttemptText.Visible = false;
            tabUISections.SelectedTab = tabJoin;
            btnGameListRefresh_Click(sender, e);
        }

        private void grdGamesList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)// Prevent crashing if double click on top bit
            {
                return;
            }

            string[] Players = grdGamesList.Rows[e.RowIndex].Cells[4].Value.ToString().Split('/');
            int players = int.Parse(Players[0]);
            int maxplayers = int.Parse(Players[1]);
            if (grdGamesList.Rows[e.RowIndex].Cells[0].Value != null)
            {
                if (players >= maxplayers)
                {
                    MessageBox.Show("Game is full!");
                }
                else
                {
                    if (grdGamesList.Rows[e.RowIndex].Cells[1].Value.ToString() == "No")
                    {
                        lblJoinAttemptText.Text = "Please wait, attempting to join lobby \"" + grdGamesList.Rows[e.RowIndex].Cells[2].Value.ToString() + "\"...";
                        lblJoinAttemptText.Visible = true;
                        JoinGame(grdGamesList.Rows[e.RowIndex].Cells[0].Value.ToString());
                    }
                    else
                    {
                        string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the password for this lobby.", "Password");
                        string pass = "";
                        string[] GameProperties = Topics[grdGamesList.Rows[e.RowIndex].Cells[0].Value.ToString()].Split(' ');

                        foreach (string GameProp in GameProperties)
                        {
                            if (GameProp.StartsWith("PASS="))
                            {
                                pass = GameProp.Substring(5);
                            }
                        }
                        if (input == pass)
                        {
                            lblJoinAttemptText.Text = "Please wait, attempting to join lobby \"" + grdGamesList.Rows[e.RowIndex].Cells[2].Value.ToString() + "\"...";
                            lblJoinAttemptText.Visible = true;
                            JoinGame(grdGamesList.Rows[e.RowIndex].Cells[0].Value.ToString());
                        }
                        else
                            MessageBox.Show("Password incorrect.");
                    }
                }
            }
        }

        private void btnCancelJoining_Click(object sender, EventArgs e)
        {
            tabUISections.SelectedTab = tabConnected;
        }

        /// <summary>
        /// Helper function, joins a game lobby
        /// </summary>
        /// <param name="GameChannel"></param>
        private void JoinGame(string Channel)
        {
            if (Topics.ContainsKey(Channel))
            {

                string Host = null;
                string Addon = "", Map = "";

                string[] GameProperties = Topics[Channel].Split(' ');

                string map = "";

                int MaxPlayers = 0;
                foreach (string GameProp in GameProperties)
                {
                    if (GameProp.StartsWith("HOST="))
                    {
                        Host = GameProp.Substring(5);
                    }
                    if (GameProp.StartsWith("SIZE="))
                    {
                        MaxPlayers = int.Parse(GameProp.Substring(5));
                    }
                    if (GameProp.StartsWith("MAP="))
                    {
                        Map = GameProp.Substring(4);
                    }
                    if (GameProp.StartsWith("CUSTOMMOD="))
                    {
                        Addon = GameProp.Substring(10);
                    }
                }

                if (Host == null)
                {
                    MessageBox.Show("Can't join game at present time. Please refresh games list and try again.");
                }
                else
                {
                    if (Game != null)
                    {
                        // We need to shut down the current game instance first
                        DetachGameEvents(Game);
                        Game = null;
                    }


                    Game = new Game();
                    


                    Game.MyName = ServerConnection.Nickname;
                    Game.Channel = Channel;
                    Game.LobbyName = Channel.Substring(3);
                    Game.HostName = Host;
                    Game.CustomMod = Addon;
                    Game.Dotamap = Map;
                    labelMap.Text = "Map: " + Map ;
                    labelAddon.Text = "Addon: " + Addon;
                    lblLobbyName.Text = Game.LobbyName;
                    AttachGameEvents(Game);
                    btnStart.Enabled = false;
                    btnLobbyKick.Enabled = false;
                    btnLobbyRandomiseTeams.Enabled = false;
                    chkLobbyPlayerReady.Checked = false;
                    labelHost.Text = "Host: " + Host;
                    labelMap.Text = "Map: " + map;
                    labelMaxPlayers.Text = "Max Players: " + MaxPlayers.ToString();
                    btnStart.Text = "Host Starts";
                    btnLobbyKick.Text = "Host-Only";
                    btnLobbyKick.Hide();
                    btnLobbyRandomiseTeams.Text = "Host-Only";
                    btnLobbyRandomiseTeams.Hide();
                    Game.RequestJoin(Host);
                }
            }
        }

        private void LeaveGame()
        {
            if (Game != null)
            {

                ircPart(Game.Channel);

                if (Game.DireChannel != null && ChatChannels.ContainsKey(Game.DireChannel))
                {
                    ircPart(Game.DireChannel);
                    //tabChatrooms.TabPages.Remove(ChatChannels[Game.DireChannel]);
                    //ChatChannels[Game.DireChannel].Dispose();
                    //ChatChannels.Remove(Game.DireChannel);
                }

                if (Game.RadiantChannel != null && ChatChannels.ContainsKey(Game.RadiantChannel))
                {
                    ircPart(Game.RadiantChannel);
                    //tabChatrooms.TabPages.Remove(ChatChannels[Game.RadiantChannel]);
                    //ChatChannels[Game.RadiantChannel].Dispose();
                    //ChatChannels.Remove(Game.RadiantChannel);
                }

                if (Game.SpectatorChannel != null && ChatChannels.ContainsKey(Game.SpectatorChannel))
                {
                    ircPart(Game.SpectatorChannel);
                    //tabChatrooms.TabPages.Remove(ChatChannels[Game.SpectatorChannel]);
                    //ChatChannels[Game.SpectatorChannel].Dispose();
                    //ChatChannels.Remove(Game.SpectatorChannel);
                }


                tabUISections.SelectedTab = tabConnected;


                lbxLobbyDirePlayers.Items.Clear();
                lbxLobbyRadiantPlayers.Items.Clear();
                lbxLobbySpectators.Items.Clear();

                btnJoinDire.Text = "Join Dire";
                btnJoinRadiant.Text = "Join Radiant";
                chkLobbyPlayerReady.Checked = false;

                DetachGameEvents(Game);
                Game = null;
            }

        }

        private void btnJoinRadiant_Click(object sender, EventArgs e)
        {

            if (DetectButtonSpamming(sender))
            {
                return;
            }
            try
            {
                Debug.Assert(Game != null, "Game can't be null at lobby tab!");
                if (Game.Players[ServerConnection.Nickname].Side != PlayerSide.Radiant)
                {
                    Game.AttemptSideChange(PlayerSide.Radiant);
                }
                else
                {
                    Game.AttemptSideChange(PlayerSide.Spectator);
                }
            }
            catch { }
        }

        private void btnJoinDire_Click(object sender, EventArgs e)
        {
            if (DetectButtonSpamming(sender))
            {
                return;
            }
            try
            {
                Debug.Assert(Game != null, "Game can't be null at lobby tab!");
                if (Game.Players[ServerConnection.Nickname].Side != PlayerSide.Dire)
                {
                    Game.AttemptSideChange(PlayerSide.Dire);
                }
                else
                {
                    Game.AttemptSideChange(PlayerSide.Spectator);
                }
            }
            catch { }
        }

        private void VersionCheck()
        {
            if (Properties.Settings.Default.MyVersion != Application.ProductVersion)
            {
                Properties.Settings.Default.MyVersion = Application.ProductVersion;
                Properties.Settings.Default.VersionStatus = "UNKNOWN";
                Properties.Settings.Default.Save();
            }
            else if (Properties.Settings.Default.VersionStatus == "INCOMPATIBLE")
            {
                DialogResult result = MessageBox.Show("Your version is no longer supported. Please click YES to download the latest version","Out of Date",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("The client will now exit and the updater will start.");
                    Properties.Settings.Default.VersionStatus = "UNKNOWN";
                    Properties.Settings.Default.Save();
                    Process.Start("updater.exe");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("This version of Dota 2 Custom Realms is not supported. Please run the program again and update to continue.");
                    Application.Exit();
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            lblPlayersOnline.Enabled = false;
            lblPlayersInGame.Enabled = false;

            btnManualConnect.Visible = false;
            chkDedicated.Enabled = false; // DEDICATED SERVER, ENABLE WHEN IMPLEMENTED/TESTING, DISABLE WHEN RELEASING HOTFIXES.
          
            RefreshSettingsTab(); // Update settings
            tbxChooseNick.Text = Properties.Settings.Default.NickName;
            if (Game != null)
            {
                AttachGameEvents(Game);
            }
            if (Properties.Settings.Default.DisableVersion)
            {
                cbxVersionFixDisable.Checked = true;
            }
            if (Properties.Settings.Default.ConDebug)
            {
                chkConDebug.Checked = true;
            }
            if (Properties.Settings.Default.FlashNew)
            {
                chkFlashNew.Checked = true;
            }
            if (Properties.Settings.Default.BeepNew)
            {
                chkBeepNew.Checked = true;
            }
            if (Properties.Settings.Default.BeepName)
            {
                chkBeepName.Checked = true;
            }
            if (Properties.Settings.Default.FlashName)
            {
                chkFlashName.Checked = true;
            }
            if (Properties.Settings.Default.Dedicated)
            {
                chkDedicated.Checked = true;
            }
            //Hide the progress bar and game state messages. Re-enable when they are functioning better.
            //stsGameState.Hide();
            //pgbGameStateProgress.Height = 0;

            VersionCheck();

            tabUISections.SizeMode = TabSizeMode.Fixed;
            tabUISections.SelectedTab = tabPreConnect;


        }


        #region Game Class Events and so on


        void Game_GameEvent(object sender, Game.GameEventArgs e)
        {
            switch (e.EventName)
            {
                case Game.GAME_JOIN_ACCEPTED:
                    {
                        ircJoin(Game.Channel);
                        break;
                    }
                case Game.GAME_JOIN_REFUSE_STARTED:
                    {
                        lblJoinAttemptText.Visible = false;
                        DetachGameEvents(Game);
                        Game = null;
                        MessageBox.Show("The game you attempted to join has already started!");
                        break;
                    }
                case Game.GAME_JOIN_REFUSE_FULL:
                    {
                        lblJoinAttemptText.Visible = false;
                        DetachGameEvents(Game);
                        Game = null;
                        MessageBox.Show("The game you attempted to join is full!");
                        break;
                    }
                case Game.GAME_JOIN_REFUSE_KICKED:
                    {
                        lblJoinAttemptText.Visible = false;
                        DetachGameEvents(Game);
                        Game = null;
                        MessageBox.Show("You were kicked from this game, and cannot rejoin it.");
                        break;
                    }
                case Game.GAME_JOIN_REFUSE_BANNED:
                    {
                        lblJoinAttemptText.Visible = false;
                        DetachGameEvents(Game);
                        Game = null;
                        MessageBox.Show("You are banned from this host's games, and cannot join.");
                        break;
                    }
                case Game.GAME_PREGAME:
                    {
                        tabUISections.SelectedTab = tabDraftSummary;

                        if (!bgwGenerateNpcHeroesAutoexec.IsBusy)
                        {
                            bgwGenerateNpcHeroesAutoexec.RunWorkerAsync();
                        }

                        break;
                    }
                default:
                    {
                        //lblGameState.Text = e.EventName;
                        //pgbGameStateProgress.Value = (int)(e.Complete ? pgbGameStateProgress.Maximum : pgbGameStateProgress.Maximum / 2);
                        break;
                    }
            }
        }

        void Game_SendPlayerNotice(object sender, Game.SendMessageEventArgs e)
        {
            //ircClient.SendMessage(SendType.Notice, e.Target, e.Message);
        }

        void Game_SendChannelNotice(object sender, Game.SendMessageEventArgs e)
        {
            //ircClient.SendMessage(SendType.Notice, e.Target, e.Message);
        }

        private void AttachGameEvents(Game aGame)
        {
            Game.DisplayUserMessage += new Game.SendMessageEventHandler(Game_DisplayUserMessage);
            Game.SendChannelNotice += new Game.SendMessageEventHandler(Game_SendChannelNotice);
            Game.SendPlayerNotice += new Game.SendMessageEventHandler(Game_SendPlayerNotice);
            Game.GameEvent += new Game.GameEventHandler(Game_GameEvent);
            Game.UpdateUI += new EventHandler(Game_UpdateUI);
            Game.Players.AfterRemove += Players_AfterRemove;
        }

        void Players_AfterRemove(object sender, EventfulDictionary<string, Player>.DataChangedEventArgs e)
        {
            if (Game.IsHost)
            {
                //ircClient.RfcKick(Game.Channel, e.key);
            }
        }

        void Game_UpdateUI(object sender, EventArgs e)
        {
            switch (Game.Stage)
            {
                case Dota2CustomRealms.Game.GameStage.Lobby:
                    {

                        if (lbxLobbyDirePlayers.SelectedIndex != -1)
                        {
                            SelectedPlayer = (Player)lbxLobbyDirePlayers.SelectedItem;
                        }
                        if (lbxLobbyRadiantPlayers.SelectedIndex != -1)
                        {
                            SelectedPlayer = (Player)lbxLobbyRadiantPlayers.SelectedItem;
                        }
                        if (lbxLobbySpectators.SelectedIndex != -1)
                        {
                            SelectedPlayer = (Player)lbxLobbySpectators.SelectedItem;
                        }


                        lbxLobbyDirePlayers.Items.Clear();
                        lbxLobbyRadiantPlayers.Items.Clear();
                        lbxLobbySpectators.Items.Clear();

                        int PlayersReady = 0, PlayersWithMod = 0;

                        foreach (KeyValuePair<string, Player> Player in Game.Players)
                        {
                            if (Player.Value.Ready)
                            {
                                PlayersReady++;
                            }
                            if (Player.Value.HasMod)
                            {
                                PlayersWithMod++;
                            }

                            switch (Player.Value.Side)
                            {
                                case PlayerSide.Radiant:
                                    {
                                        lbxLobbyRadiantPlayers.Items.Add(Player.Value);
                                        if (Player.Value == SelectedPlayer)
                                        {
                                            lbxLobbyRadiantPlayers.SelectedItem = Player.Value;
                                        }
                                        break;
                                    }
                                case PlayerSide.Dire:
                                    {
                                        lbxLobbyDirePlayers.Items.Add(Player.Value);
                                        if (Player.Value == SelectedPlayer)
                                        {
                                            lbxLobbyDirePlayers.SelectedItem = Player.Value;
                                        }
                                        break;
                                    }
                                case PlayerSide.Spectator:
                                    {
                                        lbxLobbySpectators.Items.Add(Player.Value);
                                        if (Player.Value == SelectedPlayer)
                                        {
                                            lbxLobbySpectators.SelectedItem = Player.Value;
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }

                        lblLobbyPlayerReadyCount.Text = PlayersReady.ToString() + "/" + Game.Players.Count + " Players Ready";

                        if (Game.Players.ContainsKey(ServerConnection.Nickname))
                        {
                            switch (Game.Players[ServerConnection.Nickname].Side)
                            {
                                case PlayerSide.Radiant:
                                    {
                                        btnJoinRadiant.Text = "Leave Radiant";
                                        btnJoinDire.Text = "Join Dire";
                                        break;
                                    }
                                case PlayerSide.Dire:
                                    {
                                        btnJoinRadiant.Text = "Join Radiant";
                                        btnJoinDire.Text = "Leave Dire";
                                        break;
                                    }
                                case PlayerSide.Spectator:
                                    {
                                        btnJoinRadiant.Text = "Join Radiant";
                                        btnJoinDire.Text = "Join Dire";
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            btnJoinRadiant.Text = "Join Radiant";
                            btnJoinDire.Text = "Join Dire";
                        }

                        if (Game.IsHost && PlayersWithMod < Game.Players.Count)
                        {
                            btnStart.Enabled = false;
                            btnStart.Text = (Game.Players.Count - PlayersWithMod).ToString() + " Players Need Mod";
                        }
                        else if(Game.IsHost)
                        {
                            btnStart.Enabled = true;
                            btnStart.Text = "Start Game";
                        }

                        break;
                    }
                case Dota2CustomRealms.Game.GameStage.ServerSetup:
                    {
                        
                        tabUISections.SelectedTab = tabDraftSummary;

                        break;
                    }
            }
        }

        private void DetachGameEvents(Game aGame)
        {
            Game.DisplayUserMessage -= Game_DisplayUserMessage;
            Game.SendChannelNotice -= Game_SendChannelNotice;
            Game.SendPlayerNotice -= Game_SendPlayerNotice;
            Game.GameEvent -= Game_GameEvent;
            Game.UpdateUI -= Game_UpdateUI;
            Game.Players.AfterRemove -= Players_AfterRemove;

        }

        void Game_DisplayUserMessage(object sender, Game.SendMessageEventArgs e)
        {
            Game aGame = (Game)sender;
            foreach (string Channel in new string[] { aGame.Channel, aGame.DireChannel, aGame.RadiantChannel, aGame.SpectatorChannel })
            {
                if (Channel != null)
                {
                    AddChatMessage(Channel, e.Message);
                }
            }
        }

        #endregion



        #region Lobby Code


        /// <summary>
        /// Updates a player's status
        /// </summary>
        /// <param name="PlayerName"></param>
        /// <param name="Ready"></param>
        void PlayerStatus(string PlayerName, bool Ready)
        {
            if (Game != null && Game.Players.ContainsKey(PlayerName))
            {
                Player Player = Game.Players[PlayerName];
                if (Ready != Player.Ready)
                {
                    AddChatMessage(Game.Channel, PlayerName + " is " + (Ready ? "now ready!" : "no longer ready."));
                }
                Player.Ready = Ready;
            }
            else
            {
                //Player Player = new Player();
                //Player.Name = PlayerName;
                //Player.Ready = Ready;
                //Player.Side = PlayerSide.Spectator;
                //Players.Add(Player.Name, Player);

                //lbxLobbySpectators.Items.Add(Player);

                //// We don't know their side for sure, so send them another query
                //ircClient.SendMessage(SendType.Notice, Player.Name, "SIDE?");
            }

            UpdatePlayerReadyCount();
        }

        void UpdatePlayerReadyCount()
        {
            if (Game == null) return;

            int ReadyNum = 0, Total = 0;

            foreach (KeyValuePair<string, Player> Player in Game.Players)
            {
                Total++;
                if (Player.Value.Ready) ReadyNum++;
            }

            if (ReadyNum == Total-1 && Game.AdditionalModes.Contains("Dedicated") && Game.IsHost)
            {
                btnStart_Click(null, new EventArgs());
            }


            typeof(ListBox).InvokeMember("RefreshItems",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
              null, lbxLobbyDirePlayers, new object[] { });

            typeof(ListBox).InvokeMember("RefreshItems",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
              null, lbxLobbyRadiantPlayers, new object[] { });

            typeof(ListBox).InvokeMember("RefreshItems",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
              null, lbxLobbySpectators, new object[] { });


            lblLobbyPlayerReadyCount.Text = ReadyNum.ToString() + "/" + Total.ToString() + " Players Ready";

        }



        private void chkLobbyPlayerReady_CheckedChanged(object sender, EventArgs e)
        {
            if (DetectButtonSpamming(sender))
            {

                chkLobbyPlayerReady.CheckedChanged -= chkLobbyPlayerReady_CheckedChanged;
                chkLobbyPlayerReady.Checked = !chkLobbyPlayerReady.Checked;
                chkLobbyPlayerReady.CheckedChanged += chkLobbyPlayerReady_CheckedChanged;

                return;
            }

            if (Game != null && Game.Channel != null  && Game.Channel != ""  && Game.Players.ContainsKey(ServerConnection.Nickname))
            {
                PlayerStatus(Game.Players[ServerConnection.Nickname].Name, chkLobbyPlayerReady.Checked);
                if (chkLobbyPlayerReady.Checked)
                {
                    //ircClient.SendMessage(SendType.Notice, Game.Channel, "READY!");
                }
                else
                {
                    //ircClient.SendMessage(SendType.Notice, Game.Channel, "NOTREADYYET!");
                }
                UpdatePlayerReadyCount();
            }
        }

        private void btnLobbyLeave_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Player> Player in Game.Players.ToList())
            {
                string name = Player.Value.Name;

                if (name != Game.HostName)
                {
                    //ircClient.RfcKick(Game.Channel, name);
                    Game.Players.Remove(name);
                }
            }
            LeaveGame();
        }

        private void btnLobbyKick_Click(object sender, EventArgs e)
        {
            if (!Game.Players.ContainsKey(SelectedPlayer.Name))
            {
                MessageBox.Show("Please select a player to kick!");
            }
            if (Game.IsHost)
            {
                if (SelectedPlayer == null)
                {
                    MessageBox.Show("You have not selected a player to kick!");
                }
                else if (SelectedPlayer.Name == Game.HostName)
                {
                    MessageBox.Show("You cannot kick yourself! Please press Leave Game instead.");
                }
                else
                {
                    Game.Blacklist.Add(SelectedPlayer.Name); // Ban player from rejoining game
                    //ircClient.RfcKick(Game.Channel, SelectedPlayer.Name);
                    if (Game.Players.ContainsKey(SelectedPlayer.Name))
                    {
                        Game.Players.Remove(SelectedPlayer.Name);
                    }
                }
            }
            else
            {
                MessageBox.Show("You don't have permission to kick users in this lobby!");
            }
        }

        private void btnLobbyRandomiseTeams_Click(object sender, EventArgs e)
        {
            if (DetectButtonSpamming(sender))
            {
                return;
            }

            Random rand = new Random();
            List<String> exclusions = new List<String>();
            int playersingame = 0;
            double playersdire = 0, playersradiant = 0;
            foreach (KeyValuePair<string, Player> Player in Game.Players)
            {
                if (Player.Value.Side == 0)
                {
                    exclusions.Add(Player.Value.Name);
                }
                else
                {
                    Player.Value.Side = 0;
                    playersingame++;
                }
            }
            if (playersingame % 2 == 0)
            {
                playersradiant = playersdire = playersingame / 2;
            }
            else
            {
                if (rand.Next(1, 3) == 2)
                {
                    playersradiant = playersingame / 2;
                    playersradiant += 0.5;
                    playersdire = playersingame - playersradiant;
                }
                else
                {
                    playersradiant = playersingame / 2;
                    playersradiant -= .5;
                    playersdire = playersingame - playersradiant;
                }
            }
            foreach (KeyValuePair<string, Player> Player in Game.Players)
            {
                PlayerSide side = Player.Value.Side;

                if (!exclusions.Contains(Player.Value.Name))
                {
                    if (playersdire != 0 && playersradiant != 0)
                    {
                        int randside = rand.Next(1, 3);
                        Player.Value.Side = (PlayerSide)randside;
                        if (randside == 1)
                            playersradiant--;
                        else
                            playersdire--;
                    }
                    else if (playersdire == 0 && playersradiant != 0)
                    {
                        Player.Value.Side = (PlayerSide)1;
                    }
                    else if (playersradiant == 0 && playersdire != 0)
                    {
                        Player.Value.Side = (PlayerSide)2;
                    }
                    else
                    {
                        Player.Value.Side = (PlayerSide)0;
                    }
                }
            }
            Game.SyncPlayerList();
            Game_UpdateUI(Game, new EventArgs());
            //ircClient.SendMessage(SendType.Message, Game.Channel, "Teams have been scrambled!");
            AddChatMessage(Game.Channel, "Teams have been scrambled!");
        }

        /// <summary>
        /// Updates the selections in listboxes after players have been moved, to keep the same one selected
        /// </summary>
        private void EnsureSelectedPlayer()
        {
            if (SelectedPlayer == null)
            {
                return;
            }
            if (lbxLobbyRadiantPlayers.Items.Contains(SelectedPlayer))
            {
                lbxLobbyRadiantPlayers.SelectedItem = SelectedPlayer;
            }
            else
            {
                lbxLobbyRadiantPlayers.SelectedIndex = -1;
            }
            if (lbxLobbyDirePlayers.Items.Contains(SelectedPlayer))
            {
                lbxLobbyDirePlayers.SelectedItem = SelectedPlayer;
            }
            else
            {
                lbxLobbyDirePlayers.SelectedIndex = -1;
            }
            if (lbxLobbySpectators.Items.Contains(SelectedPlayer))
            {
                lbxLobbySpectators.SelectedItem = SelectedPlayer;
            }
            else
            {
                lbxLobbySpectators.SelectedIndex = -1;
            }
        }

        private void lbxLobbyDirePlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxLobbyDirePlayers.SelectedIndex != -1)
            {
                SelectedPlayer = (Player)lbxLobbyDirePlayers.SelectedItem;
                lbxLobbyRadiantPlayers.SelectedIndex = -1;
                lbxLobbySpectators.SelectedIndex = -1;
            }
        }

        private void lbxLobbyRadiantPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxLobbyRadiantPlayers.SelectedIndex != -1)
            {
                SelectedPlayer = (Player)lbxLobbyRadiantPlayers.SelectedItem;
                lbxLobbyDirePlayers.SelectedIndex = -1;
                lbxLobbySpectators.SelectedIndex = -1;
            }
        }

        private void lbxLobbySpectators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxLobbySpectators.SelectedIndex != -1)
            {
                SelectedPlayer = (Player)lbxLobbySpectators.SelectedItem;
                lbxLobbyRadiantPlayers.SelectedIndex = -1;
                lbxLobbyDirePlayers.SelectedIndex = -1;
            }
        }

        #endregion

        #region Picking (needs updates)

 



        private void btnStart_Click(object sender, EventArgs e)
        {
            // The check to skip drafitng is now performed in Game.cs
            //if (Game.GameMode == GameMode.OMG || Game.GameMode == GameMode.LOD || Game.GameMode == GameMode.OMG_Balanced)
            //{
                Debug.Assert(Game != null && Game.Stage == Game.GameStage.Lobby);
                int NonSpecs = 0;
                foreach (Player player in Game.Players.Values)
                {
                    if (player.Side != PlayerSide.Spectator)
                    {
                        NonSpecs++;
                    }
                }
                if (NonSpecs == 0)
                {
                    MessageBox.Show("You cannot start a game with only spectators.");
                    return;
                }

               // ircClient.RfcTopic(Game.Channel, ""); // Remove the topic, so that the game cannot appear in the games list, regardless of what bugs may occur

                Game.ProgressGameStage();
            //}
            //else
            //{
            //    Debug.Assert(Game != null && Game.Stage == Game.GameStage.Lobby);
            //    Game.Stage = Game.GameStage.SkillDraft;
            //    Game.ProgressGameStage();
            //}
        }






        #endregion

        #region Dota 2 Modding

        private void bgwGenerateNpcHeroesAutoexec_DoWork(object sender, DoWorkEventArgs e)
        {         
            if (Game.IsHost)
            {
                if (File.Exists(Properties.Settings.Default.Dota2ServerPath + "dota\\cfg\\autoexec.cfg")) // Delete server autoexec
                {
                    File.Delete(Properties.Settings.Default.Dota2ServerPath + "dota\\cfg\\autoexec.cfg");
                }

                HostConnection = DetermineExternalIP() + ":" + Properties.Settings.Default.ServerPort;

                //Properties.Settings.Default["Dota2ServerPath"] = "C:\\dotaserver\\";
                //Properties.Settings.Default.Save();

               // ircClient.SendMessage(SendType.Notice, Game.Channel, "STARTDOTA=" + HostConnection);

                // TODO: Apply below fix by determining computer's IP
                //Dota2ConfigModder.AutoExecConnect(HostConnection);
                // FIX: Users who can't connect to their external IP couldn't join their own game in Dota 2 client

                //Dota2ConfigModder.SaveAutoHotKeyCommands()

                Dota2ServerName = Dota2ConfigModder.DetermineServerName(Properties.Settings.Default.Dota2ServerPath + "dota\\cfg\\server.cfg");
                string gamemodecommand = "";
                string gamemapcommand = "";
                if (Game.GameMode == GameMode.OMG_Diretide)
                {
                    gamemodecommand = " +dota_force_gamemode " + (int)GameMode.Diretide;
                    gamemapcommand = " -map " + Game.Dotamap;
                }
                else if (Game.GameMode == GameMode.OMG_Greevilings)
                {
                    gamemodecommand = " +dota_force_gamemode " + (int)GameMode.Greevilings;
                    gamemapcommand = " -map " + Game.Dotamap;
                }
                else if (Game.GameMode == GameMode.OMG_Mid_Only)
                {
                    gamemodecommand = " +dota_force_gamemode " + (int)GameMode.Mid_Only;
                    gamemapcommand = " -map " + Game.Dotamap;
                }
                else if (Game.GameMode != GameMode.OMG && Game.GameMode != GameMode.LOD && Game.GameMode != GameMode.OMG_Balanced)
                {
                    gamemodecommand = " +dota_force_gamemode " + (int)Game.GameMode;
                    gamemapcommand = " -map " + Game.Dotamap;
                }
                string debugcommand = "";
                if (Properties.Settings.Default.ConDebug)
                {
                    debugcommand = " -condebug";
                }
                // FIXED: Make srcds bind to all available IPs on computer
                ProcessStartInfo serverStart = new ProcessStartInfo(Properties.Settings.Default.Dota2ServerPath + "srcds.exe", "-console -game dota -port " + Properties.Settings.Default.ServerPort.ToString() + " +maxplayers " + Math.Max(10, Game.Players.Count) + " +dota_local_custom_enable 1 +dota_local_custom_game " + Game.CustomMod + " +dota_local_custom_map " + Game.CustomMod + " +dota_force_gamemode 15 +update_addon_paths +map " + Game.Dotamap);
                //ProcessStartInfo serverStart = new ProcessStartInfo(Properties.Settings.Default.Dota2ServerPath + "srcds.exe", "-console -game dota -port " + Properties.Settings.Default.ServerPort.ToString() + gamemodecommand + " -maxplayers " + Math.Max(10, Game.Players.Count));

                serverStart.WorkingDirectory = Properties.Settings.Default.Dota2ServerPath.Substring(0, Properties.Settings.Default.Dota2ServerPath.Length - 1);

                Dota2Server = Process.Start(serverStart);

                Dota2ServerWindow = IntPtr.Zero;
                bool GameModeSent = false;

                //Dota2Server.StandardInput.WriteLine("sv_cheats 1");
                IntPtr ServerWindow = IntPtr.Zero;
                while (!Dota2Server.HasExited)
                {
                    Thread.Sleep(1000);

                    if (ServerWindow == IntPtr.Zero)
                    {
                        ServerWindow = FindWindow("ConsoleWindowClass", "SOURCE DEDICATED SERVER");
                    }

                    if (ServerWindow != IntPtr.Zero)
                    {
                        string Title = GetWindowTextRaw(ServerWindow);

                        if (Title == "SOURCE DEDICATED SERVER") // Server loading
                        {
                            bgwGenerateNpcHeroesAutoexec.ReportProgress(70, "Dota 2 Server Loading...");
                            Dota2ServerWindow = ServerWindow;
                        }
                        else if (Title.ToLowerInvariant() == Dota2ServerName.ToLowerInvariant() || Title.ToLowerInvariant() == "dota 2") // Server is working
                        {
                           /* if (!GameModeSent && Game.GameMode != GameMode.OMG && Game.GameMode != GameMode.LOD && Game.GameMode != GameMode.OMG_Balanced)
                            {
                                SetForegroundWindow(ServerWindow);
                                GameModeSent = true;
                      //          SendKeys.SendWait(gamemodecommand.Substring(1));
                     //           SendKeys.SendWait("{ENTER}");
                                Thread.Sleep(150);
                     //           SendKeys.SendWait(gamemapcommand.Substring(2));
                     //           SendKeys.SendWait("{ENTER}");
                            }
                            Thread.Sleep(2500);*/
                        /*    if (Game.AdditionalModes.Contains("WTF"))
                            {
                                SetForegroundWindow(ServerWindow);
                                SendKeys.SendWait("sv_cheats 1");
                                SendKeys.SendWait("{ENTER}");
                                Thread.Sleep(150);
                                SendKeys.SendWait("dota_ability_debug 1");
                                SendKeys.SendWait("{ENTER}");
                            }*/
                            if (Game.AdditionalModes.Contains("AllTalk"))
                            {
                                SetForegroundWindow(ServerWindow);
                                Thread.Sleep(100);
                                SendKeys.SendWait("sv_alltalk 1");
                                SendKeys.SendWait("{ENTER}");
                            }
                            Dota2 = Process.Start(Properties.Settings.Default.SteamPath + "steam.exe", "steam://rungameid/570");

                            Thread.Sleep(5000);

                            Process.Start(Properties.Settings.Default.SteamPath + "steam.exe", "steam://connect/" + HostConnection);

                           // ircClient.SendMessage(SendType.Notice, Game.Channel, "SERVERREADY");

                            break;
                        }
                    }

                }
            }
            else // NOT THE HOST
            {

                while (HostConnection == null) // Wait for the IRC command with the connection
                {
                    Thread.Sleep(100);
                }

                while (!ServerReady)
                {
                    Thread.Sleep(1000);
                }

                ServerReady = false;
              
                Dota2 = Process.Start(Properties.Settings.Default.SteamPath + "steam.exe", "-applaunch 570 -novid -console -sw -noborder -override_vpk");

                Thread.Sleep(5000);

                Process.Start(Properties.Settings.Default.SteamPath + "steam.exe", "steam://connect/" + HostConnection);
            }         
        }

        private void bgwGenerateNpcHeroesAutoexec_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgbConfigProgress.Value = e.ProgressPercentage;
            lblConfigProgressMessage.Text = (string)e.UserState;

            if (e.UserState.ToString().StartsWith("ERROR="))
            {
                MessageBox.Show("The following problem was encountered:\n" + e.UserState.ToString().Substring(6) + "\nThe game probably isn't going to work, so don't bother waiting.");
            }
        }

        private void bgwGenerateNpcHeroesAutoexec_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pgbConfigProgress.Visible = false;
            lblConfigProgressMessage.Visible = false;
            btnDone.Visible = true;
            timerManualConnect();
        }

        private void btnManualConnect_Click(object sender, EventArgs e)
        {
            if (HostConnection != null)
            {
                Process.Start(Properties.Settings.Default.SteamPath + "steam.exe", "-applaunch 570 -novid -console -sw -noborder -override_vpk +connect " + HostConnection);
            }
        }

        private void timerManualConnect()
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 30000;
            t.Tick += delegate(System.Object o, System.EventArgs e)
            { t.Stop(); btnManualConnect.Visible = true; };
            
            t.Start();   
        }



        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);

        /*[DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);*/


        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);



        public static string GetWindowTextRaw(IntPtr hwnd)
        {
            // Allocate correct string length first
            StringBuilder re = new StringBuilder(10);
            int length = (int)SendMessage(hwnd, (uint)WM.GETTEXTLENGTH, IntPtr.Zero, re);
            StringBuilder sb = new StringBuilder(length + 1);
            SendMessage(hwnd, (uint)WM.GETTEXT, (IntPtr)sb.Capacity, sb);
            Debug.WriteLine(re.ToString());
            return sb.ToString();
        }

        // You can also call FindWindow(default(string), lpWindowName) or FindWindow((string)null, lpWindowName)

        private string DetermineExternalIP()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://checkip.dyndns.org");
            WebResponse rep = req.GetResponse();
            string text = new StreamReader(rep.GetResponseStream()).ReadToEnd();
            text = text.Substring(text.IndexOf(":") + 2);
            text = text.Substring(0, text.IndexOf("<"));
            return text;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            pgbConfigProgress.Visible = true;
            lblConfigProgressMessage.Visible = true;
            btnDone.Visible = false;
            btnManualConnect.Visible = false;
            HostConnection = null;
            tabUISections.SelectedTab = tabConnected;

            if (Game.IsHost)
            {
                try
                {
                    if (!Dota2Server.HasExited)
                    {
                        SetForegroundWindow(Dota2ServerWindow);
                        Thread.Sleep(10);
                        SendKeys.SendWait("disconnect{ENTER}");
                        Thread.Sleep(500);
                        SetForegroundWindow(Dota2ServerWindow);
                        Thread.Sleep(10);
                        SendKeys.SendWait("exit{ENTER}");
                    }
                }
                catch { }
            }

            ircPart(Game.Channel);
            //try
            //{
                if (Game.Players[ServerConnection.Nickname].Side == PlayerSide.Dire)
                    ircPart(Game.DireChannel);
                if (Game.Players[ServerConnection.Nickname].Side == PlayerSide.Radiant)
                    ircPart(Game.RadiantChannel);
                if (Game.Players[ServerConnection.Nickname].Side == PlayerSide.Spectator)
                    ircPart(Game.SpectatorChannel);
            //}
            //catch { }

            RevertMods();


            

            lbxLobbyDirePlayers.Items.Clear();
            lbxLobbyRadiantPlayers.Items.Clear();
            lbxLobbySpectators.Items.Clear();

            // isHost = false;

            //   Players.Clear();

            //   Self.Ready = false;
            btnJoinDire.Text = "Join Dire";
            btnJoinRadiant.Text = "Join Radiant";

            DetachGameEvents(Game);
            Game = null;
        }

        #endregion

        private string[] SplittifyChatMessage(string Text)
        {
            List<string> Lines = new List<string>();
            string[] Parts = Text.Split(' ');

            string newLine = "";

            for (int i = 0; i < Parts.Length; i++)
            {
                if (newLine.Length + 1 + Parts[i].Length <= 45)
                {
                    newLine += " " + Parts[i];
                }
                else if (newLine.Length >= 45)
                {
                    Lines.Add(newLine + " ");
                    newLine = "";
                }
                else if (Parts[i].Length > 45)
                {
                    Lines.Add(newLine + " ");
                    Lines.Add(Parts[i] + " ");
                    newLine = "";
                }
                else
                {
                    Lines.Add(newLine + " ");
                    newLine = Parts[i];
                }
            }
            Lines.Add(newLine);
            for (int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Trim() == "")
                {
                    Lines.RemoveAt(i);
                    i--;
                }
            }
            return Lines.ToArray();
        }


        #region Settings

        private void btnSettings_Click(object sender, EventArgs e)
        {
            lblDota2ClientLocation.Text = Properties.Settings.Default.Dota2Path;
            RefreshSettingsTab();
            tabUISections.SelectedTab = tabSettings;
        }

        private void btnSettingsSaveReturn_Click(object sender, EventArgs e)
        {

            tabUISections.SelectedTab = tabConnected;


            UpdateClientHostUsableStatuses();
        }

        private void btnSettingsClientLocationChange_Click(object sender, EventArgs e)
        {
            /* if (!Properties.Settings.Default.ClientSetupComplete)
  {
      MessageBox.Show("In the properties of Dota 2 in the Steam client, please set the following launch options:\n -novid -console -vpk_override 1");
      Properties.Settings.Default.ClientSetupComplete = true;
      Properties.Settings.Default.Save();
  }

  if ((string)Properties.Settings.Default["Dota2Path"] == "" || File.Exists((string)Properties.Settings.Default["Dota2Path"] + "dota.exe") == false)
  {*/

            if (ofdFindDotaExe.ShowDialog() == DialogResult.OK && ofdFindDotaExe.FileName.ToLowerInvariant().EndsWith("dota.exe"))
            {
                Properties.Settings.Default["Dota2Path"] = ofdFindDotaExe.FileName.Substring(0, ofdFindDotaExe.FileName.Length - 8);
                if (File.Exists((string)Properties.Settings.Default["Dota2Path"] + "srcds.exe"))
                {
                    MessageBox.Show("The dedicated server program, srcds.exe, was found in this directory. Please make sure you are not setting your Dota 2 Path in your server directory.");
                    Properties.Settings.Default["Dota2Path"] = null;
                    return;
                }
                Properties.Settings.Default.Save();

                btnSettingDota2ConsoleKeybindDetect_Click(sender, e);

                RefreshSettingsTab();
            }
        }

        public void RefreshSettingsTab()
        {

            bool Client = true, Server = true;

            // Client Location
            lblDota2ClientLocation.Text = Properties.Settings.Default.Dota2Path;
            if (File.Exists((string)Properties.Settings.Default["Dota2Path"] + "dota.exe"))
            {
                btnSettingsClientLocationChange.BackColor = Color.FromArgb(255, 0, 192, 0);

                // If Dota 2 is in Steam's default installation location, then automatically set it
                if(!File.Exists((string)Properties.Settings.Default["SteamPath"] + "steam.exe") && File.Exists(Properties.Settings.Default.Dota2Path.Substring(0, Properties.Settings.Default.Dota2Path.Length - 29) + "steam.exe"))
                {
                    Properties.Settings.Default["SteamPath"] = Properties.Settings.Default.Dota2Path.Substring(0, Properties.Settings.Default.Dota2Path.Length - 29);
                    Properties.Settings.Default.Save();
                }

            }
            else
            {
                Client = false;
                btnSettingsClientLocationChange.BackColor = Color.FromArgb(255, 192, 0, 0);
            }
            btnSettingsClientLocationChange.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 192, 192, 0);// btnSettingsClientLocationChange.BackColor;
            btnSettingsClientLocationChange.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192, 0);//btnSettingsClientLocationChange.BackColor;

            lblSettingSteamPath.Text = Properties.Settings.Default.SteamPath;
            if (File.Exists((string)Properties.Settings.Default["SteamPath"] + "steam.exe"))
            {
                btnSettingSteamPath.BackColor = Color.FromArgb(255, 0, 192, 0);
            }
            else
            {
                Client = false;
                btnSettingSteamPath.BackColor = Color.FromArgb(255, 192, 0, 0);
            }
            btnSettingSteamPath.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 192, 192, 0);// btnSettingsClientLocationChange.BackColor;
            btnSettingSteamPath.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192, 0);//btnSettingsClientLocationChange.BackColor;

            if (btnSettingsClientLocationChange.BackColor == Color.FromArgb(255, 192, 0, 0))
            {
                Client = false;
                gbxSettingsConsoleKey.Enabled = false;
                btnSettingDota2ConsoleKeybindDetect.BackColor = Color.FromArgb(255, 192, 0, 0);
            }
            else
            {
                gbxSettingsConsoleKey.Enabled = true;
                // Console Key
                if (Properties.Settings.Default.ConsoleKey != null && Properties.Settings.Default.ConsoleKey != "")
                {
                    lblSettingDota2ConsoleKeybind.Text = Properties.Settings.Default.ConsoleKey;
                    btnSettingDota2ConsoleKeybindDetect.BackColor = Color.FromArgb(255, 0, 192, 0);
                }
                else
                {
                    Client = false;
                    lblSettingDota2ConsoleKeybind.Text = "";
                    btnSettingDota2ConsoleKeybindDetect.BackColor = Color.FromArgb(255, 192, 0, 0);
                }
            }

            btnSettingDota2ConsoleKeybindDetect.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 192, 192, 0);
            btnSettingDota2ConsoleKeybindDetect.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192, 0);


            // Check Frota if relevant
            string[] CurrentVersions = new WebClient().DownloadString(UPDATE_SERVER + "/current.txt").Split(',');
            string CurrentFrota = "", CurrentServerFiles = "";
            foreach (string ValuePair in CurrentVersions) // Determine versions
            {
                string[] SplitValuePair = ValuePair.Split('=');
                if (SplitValuePair[0] == "Frota") CurrentFrota = SplitValuePair[1];
                if (SplitValuePair[0] == "ServerFiles") CurrentServerFiles = SplitValuePair[1];
            }
            
            bool ClientFrota = true; // Initialise as true so user is only notified if we can determine that they definitely don't have Frota
            bool ServerFrota = true;
            bool HasValidServerFiles = true;

            if (Properties.Settings.Default.Dota2Path != "" && File.Exists(Properties.Settings.Default.Dota2Path + "dota.exe"))
            { // Check the Dota 2 client for Frota
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);
                try
                {
                    StreamReader version = new StreamReader(Properties.Settings.Default.Dota2Path + "dota\\addons\\frota\\version.txt");
                    ClientFrota = version.ReadLine().Trim() == CurrentFrota;
                    Client = Client && ClientFrota;
                }
                catch (Exception ex)
                {
                    ClientFrota = false;
                    Client = false;
                }
            }

            if (Properties.Settings.Default.Dota2ServerPath != "" && File.Exists(Properties.Settings.Default.Dota2ServerPath + "srcds.exe"))
            { // Check the Dota 2 server for Frota

                string appPath = Path.GetDirectoryName(Application.ExecutablePath);

                try
                {
                    StreamReader version = new StreamReader(Properties.Settings.Default.Dota2ServerPath + "dota\\addons\\frota\\version.txt");
                    ServerFrota = version.ReadLine().Trim() == CurrentFrota;
                    Server = Server && ServerFrota;
                }
                catch (Exception ex)
                {
                    ServerFrota = false;
                    Server = false;
                }

                // Check the Dota 2 server for up to date server files

                try
                {
                    HasValidServerFiles = File.ReadAllText(Properties.Settings.Default.Dota2ServerPath + "dota\\addons\\serverfiles.txt") == CurrentServerFiles;
                    Server = Server && HasValidServerFiles;
                    if (!HasValidServerFiles) File.Delete("Data\\serverfiles.zip");
                }
                catch (Exception ex)
                {
                    ServerFrota = false;
                    Server = false;
                }

            }

            if (!ClientFrota || !ServerFrota)
            {
                if (Properties.Settings.Default.FrotaStatus != "INCOMPATIBLE") MessageBox.Show("You need to install or update your Addon package. Please go to the Settings menu and click \"Update Addons\" :-)");
                Properties.Settings.Default.FrotaStatus = "INCOMPATIBLE";
                Properties.Settings.Default.Save();
                btnUpdateAddons.Visible = true;
            }
            else
            {
                Properties.Settings.Default.FrotaStatus = "COMPATIBLE";
                Properties.Settings.Default.Save();

                if (!HasValidServerFiles)
                {
                    MessageBox.Show("You need to install or update your server files. Please go to the Settings menu and click \"Update Addons\" :-)");
                    btnUpdateAddons.Visible = true;
                }
                else
                {
                    btnUpdateAddons.Visible = false;
                }
            }


            if (Client)
            {
                lblSettingsClientStatus.BackColor = Color.FromArgb(255, 0, 192, 0);
                lblSettingsClientStatus.Text = "Setup Complete";
            }
            else
            {
                lblSettingsClientStatus.BackColor = Color.FromArgb(255, 192, 0, 0);
                lblSettingsClientStatus.Text = "Not Setup";
            }

            Server = Server && Client;

            // Server Location
            lblDota2ServerLocation.Text = Properties.Settings.Default.Dota2ServerPath;
            if (File.Exists((string)Properties.Settings.Default["Dota2ServerPath"] + "srcds.exe"))
            {
                btnSettingsServerLocationChange.BackColor = Color.FromArgb(255, 0, 192, 0);
            }
            else
            {
                Server = false;
                btnSettingsServerLocationChange.BackColor = Color.FromArgb(255, 192, 0, 0);
            }

            // Port
            int PortNum = Properties.Settings.Default.ServerPort;
            tbxSettingServerPort.Text = PortNum.ToString();


            if (PortNum < 256 || PortNum > 65535)
            {
                btnSettingServerPort.BackColor = Color.FromArgb(255, 192, 0, 0);
                Server = false;
            }
            else
            {
                btnSettingServerPort.BackColor = Color.FromArgb(255, 0, 192, 0);
            }


            btnSettingsServerLocationChange.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 192, 192, 0);
            btnSettingsServerLocationChange.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192, 0);
            btnSettingServerPort.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 192, 192, 0);
            btnSettingServerPort.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 192, 0);


            if (Server)
            {
                lblSettingsServerStatus.BackColor = Color.FromArgb(255, 0, 192, 0);
                lblSettingsServerStatus.Text = "Setup Complete";
                RegenerateAddonMapsCombo();
                RegenerateAddonTypesCombo();
            }
            else
            {
                lblSettingsServerStatus.BackColor = Color.FromArgb(255, 192, 0, 0);
                lblSettingsServerStatus.Text = "Not Setup";
            }


            if (Properties.Settings.Default.Dota2ServerPath.Length > 0 && Properties.Settings.Default.Dota2ServerPath == Properties.Settings.Default.Dota2Path)
            {
                Client = false;
                Server = false;

                MessageBox.Show("You can't use the same folder for both your Dota 2 and your Dota 2 Server!");
            }


            btnFindLobby.Enabled = Client;
            btnHostLobby.Enabled = Server;


            Properties.Settings.Default.ClientSetupComplete = Client;

            Properties.Settings.Default.HostSetupComplete = Server;

            Properties.Settings.Default.Save();
        }

        public void UpdateClientHostUsableStatuses()
        {
            RefreshSettingsTab();
        }



        private void btnSettingsServerLocationChange_Click(object sender, EventArgs e)
        {
            if (ofdFindSrcdsExe.ShowDialog() == DialogResult.OK && ofdFindSrcdsExe.FileName.ToLowerInvariant().EndsWith("srcds.exe"))
            {
                Properties.Settings.Default["Dota2ServerPath"] = ofdFindSrcdsExe.FileName.Substring(0, ofdFindSrcdsExe.FileName.Length - 9);
                Properties.Settings.Default.Save();
                RefreshSettingsTab();

            }
        }

        private void lblSettingsClientMessageCommandArgs_Click(object sender, EventArgs e)
        {

        }

        private void btnSettingDota2ConsoleKeybindDetect_Click(object sender, EventArgs e)
        {
            string[] ConfigLines = File.ReadAllLines(Properties.Settings.Default.Dota2Path + "dota\\cfg\\config.cfg");
            foreach (string Line in ConfigLines)
            {
                if (Line.EndsWith("\"toggleconsole\""))
                {
                    string TheLine = Line;
                    TheLine = TheLine.Substring(6);
                    try
                    {
                        TheLine = TheLine.Substring(0, TheLine.IndexOf('"'));
                    }
                    catch
                    {
                        continue;
                    }
                    Properties.Settings.Default.ConsoleKey = TheLine;
                    Properties.Settings.Default.Save();
                }
            }

            RefreshSettingsTab();
        }

        private void btnSettingServerPort_Click(object sender, EventArgs e)
        {
            try
            {
                int PortNum = int.Parse(tbxSettingServerPort.Text);

                if (PortNum < 256)
                {
                    MessageBox.Show("This value is too low, please pick a port number above 255");
                }
                else if (PortNum > 65535)
                {
                    MessageBox.Show("This value is too high, please pick a port number below 65535");
                }
                else
                {
                    Properties.Settings.Default.ServerPort = PortNum;
                    Properties.Settings.Default.Save();
                }
            }
            catch
            {
                MessageBox.Show("You must enter a numeric value!");
            }
            RefreshSettingsTab();
        }

        private void btnSettingServerInstallationWizard_Click(object sender, EventArgs e)
        {
            tabUISections.SelectedTab = tabServerWizard;
            UpdateClientHostUsableStatuses();
        }


        #endregion

        #region Extractor
        string filePath = "log.txt";
        delegate void SetProgressDelegate(int amount, int max);
        private void SetProgress(int amount, int max)
        {
            if (this.progressBar.InvokeRequired)
            {
                SetProgressDelegate callback = new SetProgressDelegate(SetProgress);
                this.Invoke(callback, new object[] { amount, max });
                return;
            }

            this.progressBar.Maximum = max;
            this.progressBar.Value = amount;
        }

        delegate void LogDelegate(string message);
        private void Log(string message)
        {

            if (this.logText.InvokeRequired)
            {
                LogDelegate callback = new LogDelegate(Log);
                this.Invoke(callback, new object[] { message });
                return;
            }

            if (this.logText.Text.Length == 0)
            {
                this.logText.AppendText(message);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine(message);
                }
            }
            else
            {
                this.logText.AppendText(Environment.NewLine + message);
                using (StreamWriter file = new StreamWriter(filePath, true))
                {
                    file.WriteLine(message);
                }
            }
        }

        delegate void EnableButtonsDelegate(bool extract);
        private void EnableButtons(bool extract)
        {
            if (this.createButton.InvokeRequired || this.cancelButton.InvokeRequired)
            {
                EnableButtonsDelegate callback = new EnableButtonsDelegate(EnableButtons);
                this.Invoke(callback, new object[] { extract });
                return;
            }

            this.createButton.Enabled = extract ? true : false;
            this.cancelButton.Enabled = extract ? false : true;
            UpdateClientHostUsableStatuses();
        }

        private Thread ExtractThread;
        private Thread CopyThread;
        private Thread FrotaThread;
        private class ExtractThreadInfo
        {
            public string BasePath;
            public string SavePath;
            public PackageFile Package;
        }
        private class CopyThreadInfo
        {
            public string dota2path;
            public string DestinationPath;
        }
        private class FrotaThreadInfo
        {
            public string DotaServerPath;
            public string DotaClientPath;
        }


        public void UpdateFrota(object oinfo)
        {
            FrotaThreadInfo info = (FrotaThreadInfo)oinfo;

            string zipp = UPDATE_SERVER + "/curfrota.zip";
            Uri uri = new Uri(zipp);

            WebClient client = new WebClient();


            this.Log("Downloading Frota... Please be patient!");
            client.DownloadFile(uri, "frota.zip");

            bool Client = File.Exists(info.DotaClientPath + "dota.exe");
            bool Server = File.Exists(info.DotaServerPath + "srcds.exe");

            string InitialExtractionTarget, OtherExtractionTarget = null;

            if (Client)
            {
                InitialExtractionTarget = info.DotaClientPath + "dota\\addons";
                if (Server) OtherExtractionTarget = info.DotaServerPath + "dota\\addons";
            }
            else if (Server)
            {
                InitialExtractionTarget = info.DotaServerPath + "dota\\addons";
            }
            else
            {
                this.EnableButtons(true);
                this.Log("**** CAN'T UPDATE ADDONS, PATHS FOR CLIENT AND SERVER INVALID ****");
                return;
            }


            Directory.CreateDirectory(InitialExtractionTarget);


            
            this.Log("Downloaded Frota! Waiting for file copy to complete...");

            while(this.CopyThread != null && this.CopyThread.IsAlive)
            {
                Thread.Sleep(500);
            }

            using (ZipFile zip = ZipFile.Read("frota.zip"))
            {
                int filesextracted = 0;
                this.Log("Extracting Frota...");
                foreach (ZipEntry x in zip)
                {
                    bool Ignored = false;
                    /*foreach (string Ignore in IgnoreFiles)
                    {
                        if (x.FileName.EndsWith(Ignore))
                        {
                            Ignored = true;
                            break;
                        }
                    }*/
                    if (Ignored == false)
                    {
                        this.Log(x.FileName);
                        x.Extract(InitialExtractionTarget + "\\frota\\", true);
                    }
                    this.SetProgress(filesextracted++, zip.Count);
                }
            }

            File.Delete("frota.zip");
            this.Log("Completed Extracting Frota!");


            if (Client && Server)
            {

                this.Log("Also copying Frota to server...");

                // What a jolly way to copy a directory!
                // http://stackoverflow.com/questions/58744/best-way-to-copy-the-entire-contents-of-a-directory-in-c-sharp
                // Bit sad there isn't a Directory.Copy method
                Directory.CreateDirectory(info.DotaServerPath + "dota\\addons\\frota");
                new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(info.DotaClientPath + "dota\\addons\\frota", info.DotaServerPath + "dota\\addons\\frota", true);
            }

            Properties.Settings.Default.FrotaStatus = "COMPATIBLE";
            Properties.Settings.Default.Save();
            this.Log("**** SERVER INSTALLATION WIZARD COMPLETE ****");
            if ((this.ExtractThread == null || !this.ExtractThread.IsAlive) && (this.CopyThread == null || !this.CopyThread.IsAlive)) this.EnableButtons(true);
        }

        public void ExtractFiles(object oinfo)
        {
            if (!File.Exists("Data\\serverfiles.zip"))
            {

                if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");

                this.Log("Downloading new serverfiles.zip...");

                Uri uri = new Uri(UPDATE_SERVER + "/serverfiles.zip");

                WebClient downloader = new WebClient();

                downloader.DownloadFile(uri, "Data\\serverfiles.zip");

                this.Log("serverfile.zip download complete, extracting files...");

            }


            ExtractThreadInfo info = (ExtractThreadInfo)oinfo;
            int filesextracted = 0;
            //Extract serverfiles.zip which contains srcds.exe, d2fixup, and metamod.
            using (ZipFile zip = ZipFile.Read("Data\\serverfiles.zip"))
            {
                foreach (ZipEntry x in zip)
                {
                    this.Log(x.FileName);
                    x.Extract(info.SavePath, true);
                    filesextracted++;
                }
            }
            this.Log(String.Format("File extraction complete, {0} files extracted.", filesextracted));

            //Modify gameinfo.txt
            string[] full_file = File.ReadAllLines(info.SavePath + "\\dota\\gameinfo.txt");

            if (!full_file.Contains("			GameBin				|gameinfo_path|addons/metamod/bin"))
            {
                List<string> l = new List<string>();
                l.AddRange(full_file);
                l.Insert(37, "			GameBin				|gameinfo_path|addons/metamod/bin");
                File.WriteAllLines(info.SavePath + "\\dota\\gameinfo.txt", l.ToArray());
                this.Log("Game info change written to gameinfo.txt");
            }

            Properties.Settings.Default.Dota2ServerPath = info.SavePath; //Set the server path of settings as the server path selected in destination path.

            if ((this.FrotaThread == null || !this.FrotaThread.IsAlive) && (this.CopyThread == null || !this.CopyThread.IsAlive))
            {
                this.EnableButtons(true);
            }
            else if ((this.FrotaThread != null && this.FrotaThread.IsAlive))
            {
                this.Log("PLEASE WAIT, still downloading Frota...");
            }
        }



        private void OnCancel(object sender, EventArgs e) // If cancel button is pressed, abort both threads.
        {
            if (this.ExtractThread != null)
            {
                this.ExtractThread.Abort();
            }
            if (this.CopyThread != null)
            {
                this.CopyThread.Abort();
            }
            if (this.FrotaThread != null)
            {
                this.FrotaThread.Abort();
            }
        }

        private void CopyFiles(object oinfo) //Does non-VPK extraction routine. Copy files, extract zip, modify gameinfo.txt.
        {
            int filescopied = 0;
            int dirscreated = 0;
            CopyThreadInfo info = (CopyThreadInfo)oinfo;

            //Copy all directories
            foreach (string dirPath in Directory.GetDirectories(info.dota2path, "*", SearchOption.AllDirectories))
            {
                if (!dirPath.ToLowerInvariant().Contains("addons\\frota")) // Ensure we don't copy over a frota install the user has on their dota 2 client
                {
                    Directory.CreateDirectory(dirPath.Replace(info.dota2path, info.DestinationPath));
                    this.Log(dirPath + " dir copied.");
                    dirscreated++;
                }
            }
            string[] allfiles = Directory.GetFiles(info.dota2path, "*.*", SearchOption.AllDirectories);
            int max = allfiles.Length;
            //Copy all the files
            foreach (string files in allfiles)
            {
                if (!files.ToLowerInvariant().Contains("addons\\frota") && !files.EndsWith(".dem") && !files.EndsWith(".dmp") && !files.EndsWith(".mdmp") && !files.EndsWith(".cab")) // Don't copy over the useless stuff
                {
                    File.Copy(files, files.Replace(info.dota2path, info.DestinationPath), true);
                    this.Log(files + " copied.");
                    this.SetProgress(++filescopied, max);
                }
            }
            this.Log(String.Format("Copy completed with {0} files and {1} directories transferred.", filescopied, dirscreated));


            ExtractThreadInfo exinfo = new ExtractThreadInfo();
            exinfo.SavePath = info.DestinationPath;

            this.ExtractFiles(exinfo);

            this.Log("Server construction complete, wait for Frota installation.");

            if ((this.FrotaThread == null || !this.FrotaThread.IsAlive) && (this.ExtractThread == null || !this.ExtractThread.IsAlive)) this.EnableButtons(true);

        }

        private void createButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog findserverpath = new FolderBrowserDialog(); //Create a dialog for server
            findserverpath.Description = @"Set the location of where you want your server to be located. E.x. C:\dotaserver";
            //Check to see if server path is OK
            string dota2path = "";
            if (Properties.Settings.Default.Dota2Path == null)
            {
                FolderBrowserDialog finddotapath = new FolderBrowserDialog(); //Create a dialog for dota path if not found already
                finddotapath.Description = @"Set the location of where your Dota copy is installed. E.x. C:\Program Files\Steam\steamapps\common\dota 2 beta";
                if (finddotapath.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dota2path = finddotapath.SelectedPath; //Set dota 2 path
            }
            else
            {
                this.Log("Dota 2 Path detected: " + Properties.Settings.Default.Dota2Path);
                dota2path = Properties.Settings.Default.Dota2Path;
            }

            if (findserverpath.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string DestinationPath = findserverpath.SelectedPath + @"\"; //Set server path
            string dota2dota = DestinationPath + "dota";
            string indexName = dota2path + "\\dota\\pak01_dir.vpk";
            this.EnableButtons(false);


            //EXTRACT VPK CALL HERE
            PackageFile package = new PackageFile();
            Stream indexStream = File.OpenRead(indexName);
            package.Deserialize(indexStream);
            indexStream.Close();

            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = 380; // package.Entries.Count;
            this.progressBar.Value = 0;
            string basePath = indexName.Substring(0, indexName.Length - 8);

            ExtractThreadInfo info = new ExtractThreadInfo();
            info.BasePath = basePath;
            info.SavePath = dota2dota;
            info.Package = package;

            CopyThreadInfo copyinfo = new CopyThreadInfo();
            copyinfo.dota2path = dota2path;
            copyinfo.DestinationPath = DestinationPath;

            FrotaThreadInfo frotainfo = new FrotaThreadInfo();
            frotainfo.DotaServerPath = DestinationPath;
            frotainfo.DotaClientPath = dota2path;

            // Now started by the CopyThread
            //this.ExtractThread = new Thread(new ParameterizedThreadStart(ExtractFiles));
            //this.ExtractThread.Start(info);
            this.CopyThread = new Thread(new ParameterizedThreadStart(CopyFiles));
            this.CopyThread.Start(copyinfo);
            this.FrotaThread = new Thread(new ParameterizedThreadStart(UpdateFrota));
            this.FrotaThread.Start(frotainfo);
        }

        private void btnUpdateAddons_Click(object sender, EventArgs e)
        {
            tabUISections.SelectedTab = tabServerWizard;

            this.createButton.Enabled = false;
            this.cancelButton.Enabled = true;

            if (Properties.Settings.Default.FrotaStatus != "COMPATIBLE" || !File.Exists(Properties.Settings.Default.Dota2Path + "dota\\addons\\frota\\version.txt") || !File.Exists(Properties.Settings.Default.Dota2ServerPath + "dota\\addons\\frota\\version.txt"))
            {
                FrotaThreadInfo frotainfo = new FrotaThreadInfo();
                frotainfo.DotaServerPath = Properties.Settings.Default.Dota2ServerPath;
                frotainfo.DotaClientPath = Properties.Settings.Default.Dota2Path;
                this.FrotaThread = new Thread(new ParameterizedThreadStart(UpdateFrota));
                this.FrotaThread.Start(frotainfo);
            }

            if (File.Exists(Properties.Settings.Default.Dota2ServerPath + "srcds.exe"))
            {
                ExtractThreadInfo exinfo = new ExtractThreadInfo();
                exinfo.BasePath = Properties.Settings.Default.Dota2ServerPath;
                exinfo.SavePath = Properties.Settings.Default.Dota2ServerPath;
                this.ExtractThread = new Thread(new ParameterizedThreadStart(ExtractFiles));
                this.ExtractThread.Start(exinfo);
            }
        }

        private string IPCheck()
        {
            string _LocalIPAddress = "x";
            IPHostEntry host = Dns.GetHostByName(Dns.GetHostName());
            if (host.AddressList.Length > 0)
            {
                _LocalIPAddress = host.AddressList[0].ToString();
            }
            return _LocalIPAddress;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = "";
            int port = 0;
            IPAddress address;
            int number = 0;
            if (IPAddress.TryParse(textIP.Text, out address))
            {
                ip = textIP.Text;
            }
            else
            {
                MessageBox.Show("Please input a correctly formatted IP into the IP box. E.x. 127.0.0.1");
                return;
            }
            if (int.TryParse(textPort.Text, out number) && int.Parse(textPort.Text) >= 1025 && int.Parse(textPort.Text) <= 65535)
            {
                port = int.Parse(textPort.Text);
            }
            else
            {
                MessageBox.Show("Please input a port number between 1025 and 65535. Default for DotaCR is 5500");
                return;
            }
            NATUPNPLib.UPnPNATClass upnpnat = new NATUPNPLib.UPnPNATClass();
            NATUPNPLib.IStaticPortMappingCollection mappings = null;
            if (mappings == null)
            {

                try
                {
                    if (upnpnat.NATEventManager != null)
                        mappings = upnpnat.StaticPortMappingCollection;
                    mappings.Add(port, "UDP", port, ip, true, "Dota2CustomRealms");
                    MessageBox.Show("UDP port " + port + " forward complete for IP " + ip + ".\nIf users are still unable to connect to your server on this port, please double check your router settings.");
                }
                catch
                {
                    MessageBox.Show("Your router does not support UPnP protocol or it is not turned on. Please turn it on or port forward manually.");
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string check = IPCheck();
            if (check == "x")
            {
                MessageBox.Show("Cannot detect your local IP address, please go into Command Prompt and type \"ipconfig\" without the quotes and write your IPv4 address in the IP field.");
                return;
            }
            else
            {
                textIP.Text = check;
                return;
            }
        }

        private void ServerToSettingsbutton_click(object sender, EventArgs e)
        {
            // Stop all threads if we go back
            if (this.ExtractThread != null && this.ExtractThread.IsAlive)
            {
                this.ExtractThread.Abort();
            }
            if (this.CopyThread != null && this.CopyThread.IsAlive)
            {
                this.CopyThread.Abort();
            }
            if (this.FrotaThread != null && this.FrotaThread.IsAlive)
            {
                this.FrotaThread.Abort();
            }
            logText.Clear();

            lblDota2ClientLocation.Text = Properties.Settings.Default.Dota2Path;
            RefreshSettingsTab();
            tabUISections.SelectedTab = tabSettings;

            UpdateClientHostUsableStatuses();
        }

        #endregion




        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            RevertMods();
        }

        private void btnSettingSteamPath_Click(object sender, EventArgs e)
        {
            if (ofdFindSteam.ShowDialog() == DialogResult.OK && ofdFindSteam.FileName.ToLowerInvariant().EndsWith("steam.exe"))
            {
                Properties.Settings.Default["SteamPath"] = ofdFindSteam.FileName.Substring(0, ofdFindSteam.FileName.Length - 9);
                Properties.Settings.Default.Save();
                RefreshSettingsTab();
            }
        }

        private void RevertMods()
        {

        }

        private void btnSettingConsoleKeybindManual_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter your console key - Note that this inputbox will only accept letters and numbers, for function keys type F3 and so on", "Console Key");
            Properties.Settings.Default.ConsoleKey = input;
            Properties.Settings.Default.Save();
            RefreshSettingsTab();
        }

        private void btnBanLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists("bans.txt"))
            {
                tbxBans.Text = File.ReadAllText("bans.txt");
            }
        }

        private void btnBanSave_Click(object sender, EventArgs e)
        {
            if (File.Exists("bans.txt"))
            {
                File.Delete("bans.txt");
            }
            File.AppendAllText("bans.txt", tbxBans.Text);
        }

        private void btnBanURLLoad_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the URL to the raw Pastebin data or .txt file", "Pastebin URL");
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(input);
            StreamReader reader = new StreamReader(stream);
            tbxBans.Text = reader.ReadToEnd();
        }

        private void chkFlashNew_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFlashNew.Checked)
            {
                Properties.Settings.Default.FlashNew = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.FlashNew = false;
                Properties.Settings.Default.Save();
            }
        }

        private void chkBeepNew_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBeepNew.Checked)
            {
                Properties.Settings.Default.BeepNew = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.BeepNew = false;
                Properties.Settings.Default.Save();
            }
        }

        private void chkBeepName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBeepName.Checked)
            {
                Properties.Settings.Default.BeepName = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.BeepName = false;
                Properties.Settings.Default.Save();
            }
        }

        private void chkFlashName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFlashName.Checked)
            {
                Properties.Settings.Default.FlashName = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.FlashName = false;
                Properties.Settings.Default.Save();
            }
        }

        private void chkDedicated_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDedicated.Checked)
            {
                Properties.Settings.Default.Dedicated = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Dedicated = false;
                Properties.Settings.Default.Save();
            }
        }

        private void cbxAddonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegenerateAddonMapsCombo();    
        }

        private void RegenerateAddonMapsCombo()
        {
            if (string.IsNullOrEmpty(cbxAddonType.Text))
            {
                cbxAddonMap.Items.Clear();
                return;
            }

            string[] Maps = Directory.GetFiles(Properties.Settings.Default.Dota2ServerPath + "dota\\addons\\" + cbxAddonType.Text + "\\maps", "*.bsp");
            string SelectedItem = cbxAddonMap.Text;
            cbxAddonMap.Items.Clear();
            foreach (string Map in Maps)
            {
                cbxAddonMap.Items.Add(Map.Substring(Properties.Settings.Default.Dota2ServerPath.Length + "dota\\addons\\".Length  + cbxAddonType.Text.Length + "\\maps\\".Length).Replace(".bsp", ""));
            }
            if (Maps.Contains(SelectedItem)) cbxAddonMap.Text = SelectedItem;

            if (cbxAddonMap.Items.Count == 1) cbxAddonMap.SelectedIndex = 0;
        }

        private void RegenerateAddonTypesCombo()
        {
            // TODO: Way to determine what commands to pass to server to use each addon

            string SelectedItem = cbxAddonType.Text;
            cbxAddonType.Items.Clear();

            string[] Addons = Directory.GetDirectories(Properties.Settings.Default.Dota2ServerPath + "dota\\addons");
            foreach (string Addon in Addons)
            {
                if(Directory.Exists(Addon + "\\maps") && Directory.GetFiles(Addon + "\\maps", "*.bsp").Length > 0)
                {
                    cbxAddonType.Items.Add(Addon.Substring(Properties.Settings.Default.Dota2ServerPath.Length + "dota\\addons\\".Length).Replace("frota", "Frota")); // TODO: Less hacky fix for Frota capitalisation
                }
            }

            if (cbxAddonType.Items.Count == 1)
            {
                cbxAddonType.SelectedIndex = 0;
            }
        }
    }
}
