using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dota2CustomRealms
{
    class SkillDraft : TimedDraft
    {

        private SkillMode mode;

        public SkillMode Mode
        {
            get { return mode; }
            private set { mode = value; }
        }

        public SkillDraft(SkillMode SkillMode)
        {
            mode = SkillMode;
            if (mode == SkillMode.All_Random)
            {
                UnpickedSkills = Skill.NormalList;
                UnpickedUltimates = Skill.UltimateList;
            }
            else if(mode == SkillMode.Draft)
            {
                UnpickedSkills = Skill.NormalList;
                UnpickedUltimates = Skill.UltimateList;

                while (UnpickedSkills.Count > 60) // Remove random skills from the draft pool until only 60 remain
                {      
                    UnpickedSkills.RemoveAt(SharedRandom.Next(0, UnpickedSkills.Count));
                }
                while (UnpickedUltimates.Count > 20) // Remove random skills from the draft pool until only 20 remain
                {
                    UnpickedUltimates.RemoveAt(SharedRandom.Next(0, UnpickedUltimates.Count));
                }  
            }
        }

        public delegate void SkillPickedEventHandler(object sender, SkillPickEventArgs e);

        public event SkillPickedEventHandler SkillPicked;

        public class SkillPickEventArgs : EventArgs
        {

            public SkillPickEventArgs(Player player, Skill skill)
            {
                Player = player;
                Skill = skill;
            }

            public Player Player;
            public Skill Skill;
        }

        public List<Skill> UnpickedSkills = new List<Skill>();
        public List<Skill> UnpickedUltimates = new List<Skill>();

        public Queue<Player> Turns = new Queue<Player>(40);
        public Dictionary<Player, List<Skill>> RadiantPicks = new Dictionary<Player, List<Skill>>(5);
        public Dictionary<Player, List<Skill>> DirePicks = new Dictionary<Player, List<Skill>>(5);

        public void EnforcePick()
        {
            Player UnluckyPlayer = Turns.Dequeue();

            bool HasUlt = false;

            Debug.Assert(UnluckyPlayer.Side != PlayerSide.Spectator, "Player side shouldn't be Spectator because they're in the drafting list D:");

            Dictionary<Player, List<Skill>> PlayerPickDictionary;

            if (UnluckyPlayer.Side == PlayerSide.Radiant)
            {
                PlayerPickDictionary = RadiantPicks;
            }
            else
            {
                PlayerPickDictionary = DirePicks;
            
            }
            if (!PlayerPickDictionary.ContainsKey(UnluckyPlayer))
            {
                PlayerPickDictionary.Add(UnluckyPlayer, new List<Skill>(4));
            }
            else
            {
                foreach (Skill PlayerSkill in PlayerPickDictionary[UnluckyPlayer])
                {
                    if (PlayerSkill.IsUltimate)
                    {
                        HasUlt = true;
                        break;
                    }
                }
            }
            Skill PickedSkill;
            if (!HasUlt && PlayerPickDictionary[UnluckyPlayer].Count == 3) // needs to pick ultimate
            {
                PickedSkill = UnpickedUltimates[NormalRandom.Next(0, UnpickedUltimates.Count)];
                UnpickedUltimates.Remove(PickedSkill);
            }
            else
            {
                PickedSkill = UnpickedSkills[NormalRandom.Next(0, UnpickedSkills.Count)];
                UnpickedSkills.Remove(PickedSkill);
            }
            PlayerPickDictionary[UnluckyPlayer].Add(PickedSkill);
            SkillPicked(this, new SkillPickEventArgs(UnluckyPlayer, PickedSkill));
        }

        public void VoluntaryPick(string SkillName)
        {
            Skill Skill = null;

            foreach (Skill FindSkill in UnpickedSkills)
            {
                if (FindSkill.Name == SkillName)
                {
                    Skill = FindSkill;
                    UnpickedSkills.Remove(Skill);
                    break;
                }
            }
            if (Skill == null)
            {
                foreach (Skill FindSkill in UnpickedUltimates)
                {
                    if (FindSkill.Name == SkillName)
                    {
                        Skill = FindSkill;
                        UnpickedUltimates.Remove(Skill);
                        break;
                    }
                }
            }
            Debug.Assert(Skill != null, "Skill pool out of sync, a skill was picked by a player but not found in the local skill pool: " + SkillName);

            VoluntaryPick(Skill);
        }

        public void VoluntaryPick(Skill Skill)
        {

            // Fix randoming skill you already picked
            if (Skill.IsUltimate)
            {
                if (UnpickedUltimates.Contains(Skill))
                {
                    UnpickedUltimates.Remove(Skill);
                }
            }
            else
            {
                if (UnpickedSkills.Contains(Skill))
                {
                    UnpickedSkills.Remove(Skill);
                }
            }

            Player Player = Turns.Dequeue();

            Debug.Assert(Player.Side != PlayerSide.Spectator, "Player side shouldn't be Spectator because they're in the drafting list D:");

            Dictionary<Player, List<Skill>> PlayerPickDictionary;

            if (Player.Side == PlayerSide.Radiant)
            {
                PlayerPickDictionary = RadiantPicks;
            }
            else
            {
                PlayerPickDictionary = DirePicks;

            }

            if (!PlayerPickDictionary.ContainsKey(Player))
            {
                PlayerPickDictionary.Add(Player, new List<Skill>(4));
            }
            PlayerPickDictionary[Player].Add(Skill);

            SkillPicked(this, new SkillPickEventArgs(Player, Skill));
        }


    }
}
