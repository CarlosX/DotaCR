using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dota2CustomRealms
{
    class DraftSkill : Draft<Skill, List<Skill>>
    {

        public DraftSkill(List<Player> Players, List<Skill> Pool)
            : base(Players, Pool)
        {

            foreach (Player Player in Players)
            {
                PlayerDraftPicks.Add(Player, new List<Skill>());
            }

            Turns.AddRange(Turns); // x2
            Turns.AddRange(Turns); // x4
            UpdateUI();
        }

        /// <summary>
        /// Removes skill from picking pool and adds pick to picking list
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Skill"></param>
        public override void Pick(Player Player, Skill Skill)
        {
            if (!PlayerDraftPicks.ContainsKey(Player))
            {
                PlayerDraftPicks.Add(Player, new List<Skill>());
                PlayerDraftPicks[Player].Add(Skill);
            }
            else
            {
                PlayerDraftPicks[Player].Add(Skill);
            }
            if (AvailableDraftChoices.Contains(Skill))
            {
                AvailableDraftChoices.Remove(Skill);
                Skill.SetPickBox(false);
            }
            base.Pick(Player, Skill);
        }

        public override void UpdateUI()
        {
            List<Skill> PickedSkills = new List<Skill>();
            foreach (KeyValuePair<Player, List<Skill>> Player in PlayerDraftPicks)
            {
                PickedSkills.AddRange(Player.Value);
            }

            foreach (Skill EachSkill in Skill.List)
            {
                if (AvailableDraftChoices.Contains(EachSkill))
                {
                    EachSkill.SetPickBox(true);
                    EachSkill.PickBox.Visible = true;
                }
                else if (PickedSkills.Contains(EachSkill))
                {
                    EachSkill.SetPickBox(false);
                    EachSkill.PickBox.Visible = true;
                }
                else
                {
                    EachSkill.PickBox.Visible = false;
                }
            }
        }


    }
}
