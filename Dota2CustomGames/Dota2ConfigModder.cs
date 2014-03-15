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

  

    }
}
