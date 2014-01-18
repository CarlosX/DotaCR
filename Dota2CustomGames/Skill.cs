using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Dota2CustomRealms
{
    class Skill
    {
        public static List<Skill> List = new List<Skill>();
        public static List<Skill> UltimateList = new List<Skill>();
        public static List<Skill> NormalList = new List<Skill>();

        public List<string> Tags = new List<string>();
        public static Dictionary<string, List<Skill>> SkillTags = new Dictionary<string, List<Skill>>();

        public List<string> Bans = new List<string>();

        public void AddTag(string Tag)
        {
            if (Tag.StartsWith("tag="))
            {
                Tag = Tag.Substring(4);
                if (!SkillTags.ContainsKey(Tag))
                {
                    SkillTags.Add(Tag, new List<Skill>());
                }
                SkillTags[Tag].Add(this);
                Tags.Add(Tag);
            }
            else if (Tag.StartsWith("ban="))
            {
                Bans.Add(Tag.Substring(4));
            }
            else if (Tag.StartsWith("bantag="))
            {
                Bans.Add(Tag.Substring(7));
            }
            else if (Tag.StartsWith("banherotag="))
            {
                Bans.Add(Tag.Substring(11));
            }
        }

        public Skill(string SkillName, Hero SkillHero, bool SkillIsUltimate, Bitmap SkillIcon)
        {
            name = SkillName;
            hero = SkillHero;
            isUltimate = SkillIsUltimate;
            icon = SkillIcon;
        }

        private string name;

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }


        private Hero hero;

        public Hero Hero
        {
            get { return hero; }
            private set { hero = value; }
        }

        private bool isUltimate;

        public bool IsUltimate
        {
            get { return isUltimate; }
            set { isUltimate = value; }
        }

        private Bitmap icon;

        public Bitmap Icon
        {
            get { return icon; }
            private set { icon = value; }
        }

        private PictureBox pickBox;

        public PictureBox PickBox
        {
            get { return pickBox; }
            set { pickBox = value; }
        }

        public string GenerateDota2SkillName()
        {
            return this.name;  //Dota2ConfigModder.ConvertSkillName(this);
        }

        public override string ToString()
        {

            return this.name;
        }

        /// <summary>
        /// Enables or Disables the picking box for this skill
        /// </summary>
        /// <param name="Enabled"></param>
        public void SetPickBox(bool Enabled)
        {
            if (Enabled)
            {
                if (icon != null)
                {
                    pickBox.Image = icon;
                }
                pickBox.Enabled = true;
            }
            else
            {
                Image Image = icon;
                if (Image != null)
                {
                    Image = (Image)Image.Clone();
                    Graphics gfx = Graphics.FromImage(Image);
                    gfx.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128, 128)), 0, 0, Image.Width, Image.Height);
                    gfx.DrawLine(Pens.Red, 0, 0, Image.Width, Image.Height);
                    gfx.DrawLine(Pens.Red, 0, Image.Height, Image.Width, 0);
                    pickBox.Image = Image;
                    pickBox.Refresh();
                }
                pickBox.Enabled = false;
            }
        }

        /// <summary>
        /// Amount by which this skill increases ability layout size
        /// </summary>
        private int AbilityLayoutIncrease = 0;

        public static void LoadSkillIncompatibilities()
        {
            string[] SkillLines = File.ReadAllLines(Files.SKILL_REQS);
            foreach (string Line in SkillLines)
            {

                if (!Line.Contains("layout="))
                {
                    continue;
                }

                string[] SkillRequirements = Line.Split(',');
                Skill CurrentSkill = null;
                bool Found = false;

                foreach (Skill EachSkill in Skill.List)
                {
                    if (EachSkill.GenerateDota2SkillName() == SkillRequirements[0])
                    {
                        Found = true;
                        CurrentSkill = EachSkill;
                        break;
                    }
                }

                if (Found == false)
                {
                    continue;
                }
                for (int i = 1; i < SkillRequirements.Length; i++)
                {
                    if (SkillRequirements[i].StartsWith("layout="))
                    {
                        int Size = int.Parse(SkillRequirements[i].Substring(7));
                        CurrentSkill.AbilityLayoutIncrease = Math.Max(Size - 4, 0);
                    }
                }
            }

        }

        public static bool DetermineSkillCompatibility(Skill NewSkill, List<Skill> Skills)
        {
            int Increase = 0;
            foreach (Skill EachSkill in Skills)
            {
                Increase += EachSkill.AbilityLayoutIncrease;
            }
            if (NewSkill.AbilityLayoutIncrease + Increase > 2)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check if this skill is pickable given a list of already picked skills
        /// </summary>
        /// <param name="OtherSkills"></param>
        /// <returns></returns>
        public bool IsAllowedWith(List<Skill> OtherSkills)
        {
            foreach (Skill aSkill in OtherSkills)
            {
                foreach (string Tag in Tags) // Check if this skill's tags are banned by aSkill
                {
                    if (aSkill.Bans.Contains(Tag))
                    {
                        return false; // This skill isn't allowed with a picked skill
                    }
                }
                foreach (string Tag in aSkill.Tags) // Check if aSkill's tags are banned by this skill
                {
                    if (Bans.Contains(Tag))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsAllowedWithH(Hero hero)
        {
            foreach (string Tag in hero.Tags) // For each tag in the hero tags (melee/range/rangemid/rangefar)
            {
                if (Bans.Contains(Tag, StringComparer.InvariantCultureIgnoreCase)) // If the current skill's bans contain tag (melee/range/rangemid/rangefar)
                {
                    return false; // This skill isn't allowed with picked hero
                }
            }
            return true;
        }
    }
}
