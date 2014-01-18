using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dota2CustomRealms
{
    class HeroDraft : TimedDraft
    {

        public class HeroPickEventArgs : EventArgs
        {

            public HeroPickEventArgs(Player player, Hero hero)
            {
                Player = player;
                Hero = hero;
            }

            public Player Player;
            public Hero Hero;
        }

        private HeroMode mode;

        public HeroMode Mode
        {
            get { return mode; }
            private set { mode = value; }
        }

        public HeroDraft(HeroMode PickMode)
        {
            UnpickedHeroes = new List<Hero>(Hero.List.Count);
            if (PickMode == HeroMode.All_Pick || PickMode == HeroMode.All_Random) // Both all pick and all random use all available heroes in the pool
            {
                UnpickedHeroes.AddRange(Hero.List);
            }
            else if (PickMode == HeroMode.Draft)
            {
                UnpickedHeroes.AddRange(Hero.List);
                while(UnpickedHeroes.Count > 22) // Remove random heroes from the draft pool until only 22 remain
                {
                    UnpickedHeroes.RemoveAt(SharedRandom.Next(0, UnpickedHeroes.Count));
                }
            }
            mode = PickMode;
        }



        public delegate void HeroPickedEventHandler (object sender, HeroPickEventArgs e);

        public event HeroPickedEventHandler HeroPicked;

        public Queue<Player> Turns = new Queue<Player>(10);
        public Dictionary<Player, Hero> RadiantPicks = new Dictionary<Player, Hero>(5);
        public Dictionary<Player, Hero> DirePicks = new Dictionary<Player, Hero>(5);

        public List<Hero> UnpickedHeroes;


        public void EnforcePick()
        {
            Player UnluckyPlayer = Turns.Dequeue();

            Hero RandomHero = UnpickedHeroes[NormalRandom.Next(0, UnpickedHeroes.Count)];
            UnpickedHeroes.Remove(RandomHero);
            Debug.Assert(UnluckyPlayer.Side != PlayerSide.Spectator, "Player side shouldn't be Spectator because they're in the drafting list D:");
            if (UnluckyPlayer.Side == PlayerSide.Radiant)
            {
                RadiantPicks.Add(UnluckyPlayer, RandomHero);
            }
            else if (UnluckyPlayer.Side == PlayerSide.Dire)
            {
                DirePicks.Add(UnluckyPlayer, RandomHero);
            }
            HeroPicked(this, new HeroPickEventArgs(UnluckyPlayer, RandomHero));
        }

        public void VoluntaryPick(string HeroName)
        {
            Player Player = Turns.Dequeue();

            Hero Hero = null;

            foreach (Hero FindHero in UnpickedHeroes)
            {
                if (FindHero.Name == HeroName)
                {
                    Hero = FindHero;
                    break;
                }
            }

            Debug.Assert(Hero != null, "Hero pool out of sync, a hero was picked by a player but not found in the local hero pool: " + HeroName);

            UnpickedHeroes.Remove(Hero);

            Debug.Assert(Player.Side != PlayerSide.Spectator, "Player side shouldn't be Spectator because they're in the drafting list D:");

            if (Player.Side == PlayerSide.Radiant)
            {
                RadiantPicks.Add(Player, Hero);
            }
            else if (Player.Side == PlayerSide.Dire)
            {
                DirePicks.Add(Player, Hero);
            }

            HeroPicked(this, new HeroPickEventArgs(Player, Hero));
        }

    }
}

