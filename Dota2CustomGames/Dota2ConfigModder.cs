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

        public bool Modified = false;

        //public static readonly string KeyBind = "j";

        List<string> AutoExecLines = new List<string>();

        string AutoExecConnectLine = "";
        public bool AddSkillReqs = false;

        

        string FileText = "";

        static Dota2ConfigModder()
        {
            string[] Trans = File.ReadAllLines(Files.SKILL_DOTA_EQUIV);
            foreach (string Line in Trans)
            {
                string[] Tran = Line.Split(',');
                DotaSkillNameTranslations.Add(Tran[0], Tran[1]);
            }

            Trans = File.ReadAllLines(Files.SKILL_REQS);
            foreach (string Line in Trans)
            {
                string[] Tran = Line.Split(',');
                if (DotaSkillRequirements.ContainsKey(Tran[0]))
                {
                    System.Windows.Forms.MessageBox.Show("Not supposed to have duplicate skill " + Tran[0]);
                }
                else
                {
                    DotaSkillRequirements.Add(Tran[0], new List<string>(Tran.Length - 1));
                    for (int i = 1; i < Tran.Length; i++)
                    {
                        DotaSkillRequirements[Tran[0]].Add(Tran[i]);
                    }
                }
            }

            Trans = File.ReadAllLines(Files.HERO_EQUIV);

            foreach (string Line in Trans)
            {
                string[] Tran = Line.Split('=');
                DotaHeroNameTranslations.Add(Tran[0], Tran[1]);
            }

        }

        static Dictionary<string, string> DotaSkillNameTranslations = new Dictionary<string, string>();
        static Dictionary<string, List<string>> DotaSkillRequirements = new Dictionary<string, List<string>>();
        static Dictionary<string, string> DotaHeroNameTranslations = new Dictionary<string, string>();


        static List<Hero> PickedHeroes = new List<Hero>();

        public static string ConvertSkillName(Skill Skill)
        {

            if (DotaSkillNameTranslations.ContainsKey(Skill.Hero.Name.ToLowerInvariant() + " " + Skill.Name.ToLowerInvariant()))
            {
                return DotaSkillNameTranslations[Skill.Hero.Name.ToLowerInvariant() + " " + Skill.Name.ToLowerInvariant()];
            }
            else
            {
                return (Skill.Hero.Name + "_" + Skill.Name).Replace(" ", "_").ToLowerInvariant();
            }
        }

        public static string ConvertHeroName(Hero Hero)
        {
            if (DotaHeroNameTranslations.ContainsKey(Hero.Name))
            {
                return DotaHeroNameTranslations[Hero.Name];
            }
            else
            {
                return Hero.Name;
            }
        }

        int AutoExecAliasCount = 0;

        
        public Dota2ConfigModder(GameMode mode)
        {
            {
                FileText = File.ReadAllText(Files.NPC_HEROES_TEMPLATE);
            }
            //AutoExecLines.Add("bind \"" + KeyBind + "\" \"D2CR\"");
            //AutoExecLines.Add("alias \"D2CR\" \"D2CR0\"");
            //AutoExecLines.Add("connect IPPORT");
            //ConnectRepNum = AutoExecLines.Count - 1;
            //AutoExecLines.Add("alias \"D2CR1\" \"dota_camera_setpos 8500.000000 8500.000000 982.072876; alias D2CR D2CR2\"");
            //AutoExecLines.Add("dota_camera_setpos -8500.000000 -8500.000000 982.072876");
            //AutoExecLines.Add("alias \"D2CR1\" \"dota_camera_setpos 15000.000000 15000.000000 982.072876; alias D2CR D2CR2\"");
            //AutoExecAliasCount = 1;
        }
        
        public int GetPressesRequired()
        {
            return AutoExecAliasCount + 1;
        }

        public void AddPickedHeroes(List<Hero> PickedHeroList)
        {
            foreach (Hero aHero in PickedHeroList)
            {
                PickedHeroes.Add(aHero);
            }
        }

        public void AlterHero(Hero Hero, Skill Skill1, Skill Skill2, Skill Skill3, Skill Ultimate)
        {

            int HeroTextStart, HeroTextEnd;
            string BeforeHero, AfterHero, HeroText;

            HeroTextStart =  FileText.IndexOf("// HERO: " + Hero.GenerateDota2HeroName());
            HeroTextEnd = FileText.IndexOf("// HERO: ", HeroTextStart + 1);

            Debug.Assert(HeroTextStart != -1, "Hero not found!");

            if (HeroTextEnd == -1)
            {
                HeroTextEnd = FileText.Length;
            }

            BeforeHero = FileText.Substring(0, HeroTextStart);
            AfterHero = FileText.Substring(HeroTextEnd);
            HeroText = FileText.Substring(HeroTextStart, HeroTextEnd - HeroTextStart);

            // Remove current abilities
            int AbilityTextStart = HeroText.IndexOf("\"Ability");
            int AbilityTextEnd = HeroText.IndexOf("// Armor");

            string BeforeAbilities = HeroText.Substring(0, AbilityTextStart);
            string AfterAbilities = HeroText.Substring(AbilityTextEnd);

            // Remove precache if it exists

            if (AfterAbilities.Contains("\"precache\""))
            {
                int CacheStart = AfterAbilities.IndexOf("\"precache\"");
                string BeforeCache = AfterAbilities.Substring(0, CacheStart);
                int CacheEnd = AfterAbilities.IndexOf("}", CacheStart);
                string AfterCache = AfterAbilities.Substring(CacheEnd + 1);

                AfterAbilities = BeforeCache + AfterCache;
            }

            StringBuilder AbilityText = new StringBuilder();

            List<string> Abilities = new List<string>();

            List<string> AbilityDeps = new List<string>();

            AbilityText.Append(BeforeHero);
            AbilityText.Append(BeforeAbilities);

            StringBuilder PreCache = new StringBuilder("\"precache\"\n{\n");

            int Layout = 4;

            List<string> AbilitiesLast = new List<string>();
            List<string> AbilitiesBeforeUlt = new List<string>();

            foreach (Skill Ability in new Skill[] { Skill1, Skill2, Skill3, Ultimate })
            {
                if (DotaSkillRequirements.ContainsKey(Ability.GenerateDota2SkillName()))
                {
                    List<string> AbilitiesBefore = new List<string>();
                    List<string> AbilitiesAfter = new List<string>();

                    foreach (string AbilityDep in DotaSkillRequirements[Ability.GenerateDota2SkillName()])
                    {
                        if (AbilityDep.StartsWith("layout="))
                        {
                            Layout += int.Parse(AbilityDep.Substring(7)) - 4;
                        }
                        else if (AbilityDep.StartsWith("autoexec="))
                        {
                            if (AddSkillReqs)
                            {
                                string Line = AbilityDep.Substring(9);
                                if (!AutoExecLines.Contains(Line))
                                {
                                    AutoExecLines.Add(Line);
                                    AutoExecAliasCount++;
                                }
                            }
                        }
                        else if (AbilityDep.StartsWith("abilitybefore="))
                        {
                            AbilitiesBefore.Add(AbilityDep.Substring(14));
                        }
                        else if (AbilityDep.StartsWith("abilityafter="))
                        {
                            AbilitiesAfter.Add(AbilityDep.Substring(13));
                        }
                        else if (AbilityDep.StartsWith("abilitylast="))
                        {
                            AbilitiesLast.Add(AbilityDep.Substring(12));
                        }
                        else if (AbilityDep.StartsWith("abilitybeforeult="))
                        {
                            AbilitiesBeforeUlt.Add(AbilityDep.Substring(17));
                        }
                        else if (AbilityDep.StartsWith("ability="))
                        {
                            Abilities.Add(AbilityDep.Substring(8));
                        }
                        else if (AbilityDep.StartsWith("extra_npc_data="))
                        {
                            AbilityText.AppendLine(AbilityDep.Substring(15).Replace("\\t", "\t"));
                        }
                        else if (AbilityDep.StartsWith("precache="))
                        {
                            PreCache.AppendLine(AbilityDep.Substring(9).Replace("\\t", "\t"));
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Invalid Ability Requirement:\n" + AbilityDep + "\nPlease report this to the D2CR development team!");
                        }
                    }

                    if (Ability == Ultimate)
                    {
                        Abilities.AddRange(AbilitiesBeforeUlt);
                    }

                    Abilities.AddRange(AbilitiesBefore);
                    Abilities.Add(Ability.GenerateDota2SkillName());
                    Abilities.AddRange(AbilitiesAfter);
                }
                else
                {
                    Abilities.Add(Ability.GenerateDota2SkillName());
                }
            }

            if (Abilities.Count < Layout)
            {
                Abilities.Add("attribute_bonus");
            }
            else
            {
                try
                {
                    Abilities.Insert(Layout, "attribute_bonus");
                }
                catch
                {
                    Abilities.Add("attribute_bonus");
                }
            }
            
            Abilities.AddRange(AbilitiesLast);

            Debug.Assert(Layout <= 6, "AbilityLayout can't be higher than 6!\nIf you ignore this warning, Dota 2 might act wierd.\nThis happened because you picked too many abilities that use multiple ability slots");

            if (Layout != 4)
            {
                AbilityText.Append("\"AbilityLayout\"\t\"");
                AbilityText.Append(Layout.ToString());
                AbilityText.AppendLine("\"");
            }

            int num = 0;

            foreach (string Ability in Abilities)
            {
                num++;
                AbilityText.Append("\"Ability");
                AbilityText.Append(num.ToString());
                AbilityText.Append("\"\t\"");
                AbilityText.Append(Ability);
                AbilityText.AppendLine("\"");
            }

            PreCache.AppendLine("}");

            AbilityText.Append(PreCache.ToString());

            AbilityText.Append(AfterAbilities);
            AbilityText.Append(AfterHero);

            FileText = AbilityText.ToString();
            
        }

        public void SaveNpcHeroes(string FileName)
        {
            if (File.Exists(FileName + ".old"))
            {
                File.Delete( FileName + ".old");
            }
            if (File.Exists(FileName))
            {
                File.Move(FileName, FileName + ".old");
            }
            File.WriteAllText(FileName, FileText);
        }

        public void SaveNpcHeroes(string FileName, Hero Hero)
        {

            int HeroTextStart, HeroTextEnd;
            //string BeforeHero, AfterHero, HeroText;
            string HeroText;

            HeroTextStart = FileText.IndexOf("// HERO: " + Hero.GenerateDota2HeroName());
            HeroTextEnd = FileText.IndexOf("// HERO: ", HeroTextStart + 1);

            Debug.Assert(HeroTextStart != -1, "Hero not found!");

            if (HeroTextEnd == -1)
            {
                HeroTextEnd = FileText.Length;
            }

            //BeforeHero = FileText.Substring(0, HeroTextStart);
            //AfterHero = FileText.Substring(HeroTextEnd);
            HeroText = FileText.Substring(HeroTextStart, HeroTextEnd - HeroTextStart);

            if (File.Exists(FileName))
            {
                File.Move(FileName, FileName + ".old");
            }
            File.WriteAllText(FileName, HeroText);
        }

        public void RevertNpcHeroes(string FileName)
        {
            File.Delete(FileName);
            if (File.Exists(FileName + ".old"))
            {
                File.Move(FileName + ".old", FileName);
            }
        }

        public void AutoExecJoinTeam(PlayerSide Side)
        {
            if (Side == PlayerSide.Radiant)
            {
                AutoExecLines.Add("jointeam good");
                AutoExecAliasCount++;
            }
            else if (Side == PlayerSide.Dire)
            {
                AutoExecLines.Add("jointeam bad");
                AutoExecAliasCount++;
            }
        }

        public void AutoExecConnect(string Location)
        {
            //AutoExecLines.Add("connect " + Location);
            //AutoExecLines[ConnectRepNum] = "connect " + Location;
            AutoExecConnectLine = "connect " + Location;
        }

     /*   public void SaveAutoexec(string FileName)
        {
            StringBuilder AutoExec = new StringBuilder();

            AutoExec.AppendLine(File.ReadAllText("autoexec template.txt"));
            foreach (string Line in AutoExecLines)
            {
                AutoExec.AppendLine(Line);
            }

            File.WriteAllText(FileName, AutoExec.ToString());
        }*/
        public void EditAutoexec(string FileName)
        {
            // NO LONGER EDITING AUTOEXEC


            //StringBuilder AutoExec = new StringBuilder();

            //if (File.Exists(FileName))
            //{
            //    AutoExec.AppendLine(File.ReadAllText(FileName));
            //    //File.Move(FileName, FileName + ".old");
            //}

            //AutoExec.AppendLine("// START OF DOTA 2 CUSTOM REALMS CODE");
            //AutoExec.AppendLine("alias \"D2CR\" \"" + AutoExecConnectLine + "\"");
            ////AutoExec.AppendLine("alias \"D2CR" + AutoExecAliasCount.ToString() + "\" \"unbind KP_END\"");

            ////foreach (string Line in AutoExecLines)
            ////{
            ////    AutoExec.AppendLine(Line);
            ////}

            ////AutoExecLines.Add("alias \"D2CR" + AutoExecAliasCount.ToString() + "\" \"echo done\"");

            //File.WriteAllText(FileName, AutoExec.ToString());
        }

        public void RevertAutoexec(string FileName)
        {

            // NO LONGER EDITING AUTOEXEC

            //if (File.Exists(FileName))
            //{
            //    string Text = File.ReadAllText(FileName);

            //    int Loc = Text.IndexOf("// START OF DOTA 2 CUSTOM REALMS CODE");

            //    if (Loc != -1)
            //    {
            //        File.WriteAllText(FileName, Text.Substring(0, Loc));
            //    }
            //}
        }

        /*
        public void SaveAutoHotKeyCommands()
        {
            StringBuilder Autohotkey = new StringBuilder();

            Autohotkey.AppendLine(AutoExecConnectLine);

            foreach (string Line in AutoExecLines)
            {
                Autohotkey.AppendLine(Line);
            }
            File.WriteAllText("input.txt", Autohotkey.ToString());
        }*/


        public void AlterActivelist(string FileName, Hero Hero)
        {
            string[] Activelist = File.ReadAllLines(Files.ACTIVELIST_TEMPLATE);

            int Enabled = 0;

            string HeroActivelistName = Hero.GenerateDota2HeroName().ToLowerInvariant().Replace(" ", "_");
            if(Hero.Name == "Centaur Warrunner")
            {
                HeroActivelistName = "centaur";
            }
            if (Hero.Name == "Lifestealer")
            {
                HeroActivelistName = "life_stealer";
            }
            if (HeroActivelistName == "shredder_timbersaw")
            {
                HeroActivelistName = "shredder";
            }
            for (int i = 0; i < Activelist.Length; i++)
            {
                if (Activelist[i].Contains(HeroActivelistName))
                {
                    Activelist[i] = Activelist[i].Replace("0", "1");
                    Enabled++;
                }
                else
                {
                    Activelist[i] = Activelist[i].Replace("1", "0");
                }
            }

            Debug.Assert(Enabled > 0, "No heroes in Activelist match your picked hero!\nYou can safely ignore this error if you want, but you will have to pick your hero at the pick screen.");

            Debug.Assert(Enabled == 1, "Incorrect number of heroes left enabled for this player in Activelist: " + Enabled.ToString() + "\nYou can continue safely, but you will need to ensure you select the correct hero at the pick screen");

            if (Enabled == 0)
            {
                Activelist = File.ReadAllLines(Files.ACTIVELIST_TEMPLATE); // reload it so user isn't screwed
            }

            if (File.Exists(FileName + ".old"))
            {
                File.Delete(FileName + ".old");
            }
            if(File.Exists(FileName))
            {
                File.Move(FileName, FileName + ".old");
            }

            File.WriteAllLines(FileName, Activelist);
        }

        public void RevertActivelist(string FileName)
        {
            if (File.Exists(FileName + ".old"))
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
                File.Move(FileName + ".old", FileName);
            }
            if (File.Exists(FileName))
            {
                File.WriteAllText(FileName,File.ReadAllText(FileName).Replace("0", "1"));
            }
        }


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

        public void ModSoundsManifest(string FileName)
        {
            if (File.Exists(FileName))
            {
                if (File.Exists(FileName + ".old"))
                {
                    File.Delete(FileName + ".old");
                }
                File.Move(FileName, FileName + ".old");
            }
            File.Copy(Files.SOUNDS_MANIFEST, FileName);
        }

        public void RevertSoundsManifest(string FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            if (File.Exists(FileName + ".old"))
            {
                File.Move(FileName + ".old", FileName);
                File.Delete(FileName + ".old");
            }
        }

        public void ModNPCAbilities(string FileName)
        {
            if (File.Exists(FileName))
            {
                if (File.Exists(FileName + ".old"))
                {
                    File.Delete(FileName + ".old");
                }
                File.Move(FileName, FileName + ".old");
            }
            File.Copy(Files.NPC_ABILITIES, FileName);
        }

        public void RevertNPCAbilities(string FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            if (File.Exists(FileName + ".old"))
            {
                File.Move(FileName + ".old", FileName);
                File.Delete(FileName + ".old");
            }
        }

        public void ModItems(string FileName)
        {
            if (File.Exists(FileName))
            {
                if (File.Exists(FileName + ".old"))
                {
                    File.Delete(FileName + ".old");
                }
                File.Move(FileName, FileName + ".old");
            }
            File.Copy(Files.NPC_ITEMS, FileName);
        }

        public void RevertItems(string FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            if (File.Exists(FileName + ".old"))
            {
                File.Move(FileName + ".old", FileName);
                File.Delete(FileName + ".old");
            }
        }

        public void ModNPC_CUSTOM(string FileName)
        {
            if (File.Exists(FileName))
            {
                if (File.Exists(FileName + ".old"))
                {
                    File.Delete(FileName + ".old");
                }
                File.Move(FileName, FileName + ".old");
            }
            File.Copy(Files.NPC_HEROES_TEMPLATE, FileName);
        }

        public void RevertNPC_CUSTOM(string FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            if (File.Exists(FileName + ".old"))
            {
                File.Move(FileName + ".old", FileName);
                File.Delete(FileName + ".old");
            }
        }


        public void InstallDiretideMinimap()
        {
            if (!Directory.Exists(Properties.Settings.Default.Dota2Path + "dota\\materials\\overviews"))
            {
                Directory.CreateDirectory(Properties.Settings.Default.Dota2Path + "dota\\materials\\overviews");
            }
            if (!File.Exists(Properties.Settings.Default.Dota2Path + "dota\\materials\\overviews\\dota_diretide_12.vmt"))
            {
                File.Copy("data\\dota_diretide_12.vmt", Properties.Settings.Default.Dota2Path + "dota\\materials\\overviews\\dota_diretide_12.vmt");
            }
            if (!File.Exists(Properties.Settings.Default.Dota2Path + "dota\\materials\\overviews\\dota_diretide_12.vtf"))
            {
                File.Copy("data\\dota_diretide_12.vtf", Properties.Settings.Default.Dota2Path + "dota\\materials\\overviews\\dota_diretide_12.vtf");
            }
        }


        bool ModdedScreen = false;

        public void InstallCustomScreen()
        {
            if (!Directory.Exists(Properties.Settings.Default.Dota2Path + "dota\\materials\\console"))
            {
                Directory.CreateDirectory(Properties.Settings.Default.Dota2Path + "dota\\materials\\console");
            }
            if (!File.Exists(Properties.Settings.Default.Dota2Path + "dota\\materials\\console\\background01.vtf") && !File.Exists(Properties.Settings.Default.Dota2Path + "dota\\materials\\console\\background01_widescreen.vtf"))
            {
                File.Copy("data\\load.vtf", Properties.Settings.Default.Dota2Path + "dota\\materials\\console\\background01.vtf");
                File.Copy("data\\load.vtf", Properties.Settings.Default.Dota2Path + "dota\\materials\\console\\background01_widescreen.vtf");
                ModdedScreen = true;
            }
        }

        public void RevertCustomScreen()
        {
            if (ModdedScreen)
            {
                ModdedScreen = false;
                File.Delete(Properties.Settings.Default.Dota2Path + "dota\\materials\\console\\background01.vtf");
                File.Delete(Properties.Settings.Default.Dota2Path + "dota\\materials\\console\\background01_widescreen.vtf");
            }
        }

    }
}
