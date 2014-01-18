using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Dota2CustomRealms
{
    class Hero
    {
        public static List<Hero> List = new List<Hero>();

        public List<string> Tags = new List<string>();
        public static Dictionary<string, List<Hero>> HeroTags = new Dictionary<string, List<Hero>>();

        public static List<string> Bans = new List<string>();

        public void AddTag(string Tag)
        {
            if(Tag.StartsWith("tag="))
            {
                Tag = Tag.Substring(4);
                if (!HeroTags.ContainsKey(Tag))
                {
                    HeroTags.Add(Tag, new List<Hero>());
                }
                HeroTags[Tag].Add(this);
                Tags.Add(Tag);
            }
            else if (Tag.StartsWith("herotag="))
            {
                Tag = Tag.Substring(8);
                if (!HeroTags.ContainsKey(Tag))
                {
                    HeroTags.Add(Tag, new List<Hero>());
                }
                HeroTags[Tag].Add(this);
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
        }

        public Hero(string HeroName, HeroSide HeroSide, HeroType HeroType, Bitmap Image)
        {
            name = HeroName;
            side = HeroSide;
            type = HeroType;
            portrait = Image;
        }

        Bitmap portrait;
        public Bitmap Portrait
        {
            get { return portrait; }
            private set { portrait = value; }
        }

        HeroSide side;

        public HeroSide Side
        {
            get { return side; }
            private set { side = value; }
        }

        HeroType type;

        public HeroType Type
        {
            get { return type; }
            private set { type = value; }
        }

        string name;

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        PictureBox pickBox;

        public PictureBox PickBox
        {
            get { return pickBox; }
            set { pickBox = value; }
        }

        public string GenerateDota2HeroName()
        {
            return this.name; //Dota2ConfigModder.ConvertHeroName(this);
        }

        public override string ToString()
        {

            return this.name;
        }
        /// <summary>
        /// Enables or Disables the picking box for this hero
        /// </summary>
        /// <param name="Enabled"></param>
        public void SetPickBox(bool Enabled)
        {
            if (Enabled)
            {
                if (portrait != null)
                {
                    pickBox.Image = portrait;
                }
                pickBox.Enabled = true;
            }
            else
            {
                Image Image = portrait;
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

    }
}
