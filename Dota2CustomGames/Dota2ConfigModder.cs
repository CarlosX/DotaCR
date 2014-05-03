using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Dota2CustomRealms
{
    class Dota2ConfigModder
    {

        public static string DetermineServerName(string FileName)
        {
            if (File.Exists(FileName))
            {
                string[] Lines = File.ReadAllLines(FileName);
                foreach (string Line in Lines)
                {
                    string LineTrimmed = Line.Trim().ToLowerInvariant();
                    if(LineTrimmed.StartsWith("hostname"))
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

        public static void SetServerName(string FileName, string ServerName)
        {
            if (File.Exists(FileName))
            {
                string[] Lines = File.ReadAllLines(FileName);
                bool Found = false;
                for (int i = 0; i < Lines.Length; i++ )
                {
                    string LineTrimmed = Lines[i].Trim().ToLowerInvariant();
                    if (LineTrimmed.StartsWith("hostname"))
                    {
                        Lines[i] = "hostname \"" + ServerName + "\"";
                        Found = true;
                    }
                }
                if (!Found)
                {
                    File.AppendAllText(FileName, "\r\nhostname \"" + ServerName + "\"");
                }
                else
                {
                    File.WriteAllLines(FileName, Lines);
                }

            }
            else
            {
                File.WriteAllText(FileName, "hostname \"" + ServerName + "\"");
            }
        }

       private const string DOTACR_AUTOEXEC = "\r\n\r\n//Required for DotaCR Frota compatibility\r\nupdate_addon_paths\r\n//End of DotaCR compatibility stuff\r\n";

        public static void UpdateAutoexec()
        {
            string file_path = Properties.Settings.Default.Dota2Path + "\\dota\\cfg\\autoexec.cfg";

            if (File.Exists(file_path))
            {
                string[] lines = File.ReadAllLines(file_path);

                if (!lines.Contains("update_addon_paths"))
                {
                    File.AppendAllText(file_path, DOTACR_AUTOEXEC);
                }
            }
            else
            {
                File.AppendAllText(file_path, DOTACR_AUTOEXEC);
            }
        }

    }

   
}
