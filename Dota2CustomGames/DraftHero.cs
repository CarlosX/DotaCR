using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dota2CustomRealms
{
    class DraftHero : Draft<Hero, Hero>
    {

        public DraftHero(List<Player> Players, List<Hero> Pool)
            : base(Players, Pool)
        {
            UpdateUI();
        }

        /// <summary>
        /// Removes hero from picking pool and adds pick to picking list
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Hero"></param>
        public override void Pick(Player Player, Hero Hero)
        {
            if(!PlayerDraftPicks.ContainsKey(Player))
            {
                PlayerDraftPicks.Add(Player, Hero);
            }
            else
            {
                PlayerDraftPicks[Player].SetPickBox(true);
                AvailableDraftChoices.Add(PlayerDraftPicks[Player]);
                PlayerDraftPicks[Player] = Hero;
            }
            if (AvailableDraftChoices.Contains(Hero))
            {
                AvailableDraftChoices.Remove(Hero);
                Hero.SetPickBox(false);
            }
            base.Pick(Player, Hero);
        }

        public override void UpdateUI()
        {
            foreach (Hero EachHero in Hero.List)
            {
                if (AvailableDraftChoices.Contains(EachHero))
                {
                    EachHero.SetPickBox(true);
                    EachHero.PickBox.Visible = true;
                }
                else if (PlayerDraftPicks.ContainsValue(EachHero))
                {
                    EachHero.SetPickBox(false);
                    EachHero.PickBox.Visible = true;
                }
                else
                {
                    EachHero.PickBox.Visible = false;
                }
            }
        }


    }
}
