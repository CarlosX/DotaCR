/*
 * Server Monitor Class
 * Writen by ilian000
 * Based on Dota 2 Cusotm Realms server code
 */ 


using Dota2CustomRealms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;

namespace DedicatedServer
{
    class ServerMonitor
    {
        public string user;
        public int port;
        public int processid;

        public ServerMonitor(String user, int port)
        {
            this.user = user;
            this.port = port;
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

        public void MonitorProcess(ProcessStartInfo s, String servername, IrcClient ircClient)
        {
            Process Dota2Server = Process.Start(s);
            this.processid = Dota2Server.Id;

            Console.WriteLine("Started server for user '" + this.user + "' with process id " + this.processid);

            IntPtr Dota2ServerWindow = IntPtr.Zero;

            IntPtr ServerWindow = IntPtr.Zero; while (!Dota2Server.HasExited)
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
                        Dota2ServerWindow = ServerWindow;
                    }
                    
                    else if (Title.ToLowerInvariant() == servername.ToLowerInvariant() || Title.ToLowerInvariant() == "dota 2") // Server is working
                    {
                        /*
                        if (Game.AdditionalModes.Contains("AllTalk"))
                        {
                            SetForegroundWindow(ServerWindow);
                            Thread.Sleep(100);
                            SendKeys.SendWait("sv_alltalk 1");
                            SendKeys.SendWait("{ENTER}");
                        }
                        */

                        Thread.Sleep(5000);

                        ircClient.SendMessage(SendType.Notice, user, "SERVERREADY");

                        break;
                    }
                }
            }
        }
    }
}
