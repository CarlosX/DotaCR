using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dota2CustomRealms
{
    class GameBalanced : Game
    {


        public GameBalanced(GameMode GameMode, HeroMode HeroMode, SkillMode SkillMode, List<String> AdditionalModes)
            : base(GameMode, HeroMode, SkillMode, AdditionalModes)
        {
        }

        public GameBalanced()
            : base()
        {
        }

        public override void ReceivePlayerNotice(string Nickname, string Message)
        {
            if (base.IsHost && Message.StartsWith(GAME_SKILL_REQUEST))
            {
                if (Stage != GameStage.SkillDraft)
                {
                    return;
                }

                Skill Skill = Skill.List[int.Parse(Message.Substring(GAME_SKILL_REQUEST.Length))];
                if (SkillMode != Dota2CustomRealms.SkillMode.All_Random && SkillDraft.AvailableDraftChoices.Contains(Skill)) // Ensure skill is available
                {
                    if (Players.ContainsKey(Nickname) && SkillDraft.IsPlayerTurn(Players[Nickname])) // Ensure its this players turn
                    {
                        int Normal = 0, Ultimate = 0;
                        if (SkillDraft.PlayerDraftPicks.ContainsKey(Players[Nickname]))
                        {

                            if (!Skill.DetermineSkillCompatibility(Skill, SkillDraft.PlayerDraftPicks[Players[Nickname]]))
                            {
                               RaiseSendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_SKILL_REFUSE + "Too many ablities!"));
                                return;
                            }

                            if (!Skill.IsAllowedWith(SkillDraft.PlayerDraftPicks[Players[Nickname]]) || !Skill.IsAllowedWithH(HeroDraft.PlayerDraftPicks[Players[Nickname]]))
                            {
                                RaiseSendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_SKILL_REFUSE + "Banned combination!"));
                                return;
                            }

                            foreach (Skill Picked in SkillDraft.PlayerDraftPicks[Players[Nickname]])
                            {
                                if (Picked.IsUltimate)
                                {
                                    Ultimate++;
                                }
                                else
                                {
                                    Normal++;
                                }
                            }
                        }

                        if ((Normal < 3 && !Skill.IsUltimate) || (Ultimate == 0 && Skill.IsUltimate))
                        {

                            SkillDraft.Pick(Players[Nickname], Skill);
                            //SkillDraft.NextTurn();


                            RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_SKILL_ASSIGN + Nickname + "&" + Skill.List.IndexOf(Skill).ToString() + "&picked"));
                            RaiseDisplayUserMessage(this, new SendMessageEventArgs(null, Nickname + " picked " + Skill.Name));
                            RaiseUpdateUI(this, new EventArgs());
                         
                            if (SkillDraft.GetCurrentTurn() == SkillDraft.Turns.Count) // DRAFT COMPLETE
                            {
                                ProgressGameStage();
                            }
                        }
                        else
                        {
      
                            RaiseSendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_SKILL_REFUSE + "Must pick 1 ultimate and 3 normal skills!"));
                            
                        }
                    }
                }
                else
                {
                    RaiseSendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_SKILL_REFUSE + "Not available"));
                }
            }
            else
            {
                base.ReceivePlayerNotice(Nickname, Message);
            }
        }

        public override void ReceiveChannelNotice(string Channel, string SendingPlayer, string Message)
        {
            if (Message.StartsWith(GAME_SKILL_ASSIGN) && SendingPlayer == HostName) // Assign player a skill - Syntax TEXT<PlayerName>&<SkillID>&<PickReason>[,<PlayerName>&<SkillID>&<PickReason>...]
            {
                string[] Assigns = Message.Substring(GAME_SKILL_ASSIGN.Length).Split(',');
                foreach (string Assign in Assigns)
                {
                    string[] Args = Assign.Split('&');
                    if (Players[Args[0]].Name == MyName)
                    {
                        Skill Skill = Skill.List[int.Parse(Args[1])];
                        List<Skill> SkillList = SkillDraft.PlayerDraftPicks[Players[Args[0]]].ToList();
                        SkillList.Add(Skill);
                        foreach(Skill aSkill in SkillDraft.AvailableDraftChoices)
                        {
                            if (!aSkill.IsAllowedWith(SkillList))
                            {
                                aSkill.SetPickBox(false);
                            }
                        }
                    }
                }
            }
            base.ReceiveChannelNotice(Channel, SendingPlayer, Message);
        }

        public override void CheckTime()
        {
            if (Stage == GameStage.SkillDraft && SkillDraft.GetTimeRemaining() < 0 && IsHost)
            {
                // Enforce a random pick

                List<Player> Players = SkillDraft.GetCurrentPickers();
                foreach(Player Player in Players)
                {
                    // Determine if we need an ultimate or not
                    int Normal = 0, Ultimate = 0;
                    bool UltimateRandom = false;
                    if (SkillDraft.PlayerDraftPicks.ContainsKey(Player))
                    {
                        foreach (Skill Picked in SkillDraft.PlayerDraftPicks[Player])
                        {
                            if (Picked.IsUltimate)
                            {
                                Ultimate++;
                            }
                            else
                            {
                                Normal++;
                            }
                        }
                        if (Normal == 3)
                        {
                            UltimateRandom = true;
                        }
                    } // Otherwise they have no skill(s), therefore we can random whatever we like (but lets be nice and random a non ultimate for them)

                    List<Skill> RandomPool = new List<Skill>();

                    foreach (Skill aSkill in SkillDraft.AvailableDraftChoices)
                    {
                        if (aSkill.IsUltimate == UltimateRandom)
                        {
                            RandomPool.Add(aSkill);
                        }
                    }

                    Skill RandomSkill = RandomPool[rnd.Next(0, RandomPool.Count)];

                    if (SkillDraft.PlayerDraftPicks.ContainsKey(Player))
                    {
                        int i = 0;
                        while (i == 0)
                        {
                            RandomSkill = RandomPool[rnd.Next(0, RandomPool.Count)];
                            if (RandomSkill.IsAllowedWithH(HeroDraft.PlayerDraftPicks[Player]))
                            {
                                if (Skill.DetermineSkillCompatibility(RandomSkill, SkillDraft.PlayerDraftPicks[Player]))
                                {
                                    if (RandomSkill.IsAllowedWith(SkillDraft.PlayerDraftPicks[Player]))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    SkillDraft.Pick(Player, RandomSkill);
                    //SkillDraft.NextTurn();

                    RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_SKILL_ASSIGN + Player.Name + "&" + Skill.List.IndexOf(RandomSkill).ToString() + "&took too long and randomed"));
                    RaiseDisplayUserMessage(this, new SendMessageEventArgs(null, Player.Name + " took too long and randomed " + RandomSkill.Name));
                    RaiseUpdateUI(this, new EventArgs());
                  
                }
                if (SkillDraft.GetCurrentTurn() == SkillDraft.Turns.Count) // DRAFT COMPLETE
                {
                    ProgressGameStage();
                }
            }
        }


        /// <summary>
        /// Asks host if a skill can be picked
        /// </summary>
        /// <param name="Skill"></param>
        public override void AttemptSkillPick(Skill Skill)
        {
            if (SkillMode == Dota2CustomRealms.SkillMode.All_Random)
            {
                return;
            }

            //Debug.Assert(Stage == GameStage.SkillDraft, "Wrong Game stage to pick Skills!");

            Player Me = Players[MyName];
            if (SkillDraft.IsPlayerTurn(Me))
            {
                int Ultimate = 0, Normal = 0;
                if (SkillDraft.PlayerDraftPicks.ContainsKey(Me))
                {

                    if (!Skill.DetermineSkillCompatibility(Skill, SkillDraft.PlayerDraftPicks[Me]))
                    {
                        System.Windows.Forms.MessageBox.Show("You can't pick this skill, it'd add too many abilities");
                        return;
                    }

                    if (!Skill.IsAllowedWith(SkillDraft.PlayerDraftPicks[Me]))
                    {
                        System.Windows.Forms.MessageBox.Show("You can't pick this skill, it's banned with previously picked skills");
                        return;
                    }

                    if (!Skill.IsAllowedWithH(HeroDraft.PlayerDraftPicks[Me]))
                    {
                        System.Windows.Forms.MessageBox.Show("You can't pick this skill, it's banned with your hero.");
                        return;
                    }

                    foreach (Skill Picked in SkillDraft.PlayerDraftPicks[Me])
                    {
                        if (Picked.IsUltimate)
                        {
                            Ultimate++;
                        }
                        else
                        {
                            Normal++;
                        }
                    }
                }

                if (Skill.IsUltimate && Ultimate > 0 || !Skill.IsUltimate && Normal == 3)
                {
                    System.Windows.Forms.MessageBox.Show("You must pick 3 regular skills and 1 ultimate!");
                    return;
                }

                if (SkillDraft.AvailableDraftChoices.Contains(Skill))
                {

  
                    RaiseGameEvent(this, new GameEventArgs("Requesting Skill Pick...", false));
                    
                    if (IsHost)
                    {
                        SkillDraft.Pick(Me, Skill);

                        List<Skill> SkillList = SkillDraft.PlayerDraftPicks[Me].ToList();
                        //SkillList.Add(Skill);
                        foreach (Skill aSkill in SkillDraft.AvailableDraftChoices)
                        {
                            if (!aSkill.IsAllowedWith(SkillList) || !aSkill.IsAllowedWithH(HeroDraft.PlayerDraftPicks[Me]))
                            {
                                aSkill.SetPickBox(false);
                            }
                        }

                        //SkillDraft.NextTurn();

                 
                        RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_SKILL_ASSIGN + MyName + "&" + Skill.List.IndexOf(Skill).ToString() + "&picked"));    
                        RaiseDisplayUserMessage(this, new SendMessageEventArgs(null, MyName + " picked " + Skill.Name));
                        RaiseGameEvent(this, new GameEventArgs("Skill Picked!", true));
                        
                        if (SkillDraft.GetCurrentTurn() == SkillDraft.Turns.Count) // DRAFT COMPLETE
                        {
                            ProgressGameStage();
                        }
                    }
                    else
                    {
                        RaiseSendPlayerNotice(this, new SendMessageEventArgs(HostName, GAME_SKILL_REQUEST + Skill.List.IndexOf(Skill).ToString()));
                    }
                    RaiseUpdateUI(this, new EventArgs());
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("That skill isn't in the draft pool - This isn't supposed to happen");
                }
            }

        }

        /// <summary>
        /// Will advance the game from lobby to hero picking, hero picking to skill picking, and so on
        /// </summary>
        public override void ProgressGameStage()
        {
            switch (Stage)
            {
                case GameStage.HeroDraft: // HeroDraft -> SkillDraft
                    {

                        Stage = GameStage.SkillDraft;

                        if (IsHost)
                        {

                            List<Skill> Pool = new List<Skill>();
                            List<Skill> Normals = new List<Skill>();
                            List<Skill> Ultimates = new List<Skill>();
                            Normals.AddRange(Skill.NormalList);
                            Ultimates.AddRange(Skill.UltimateList);

                            switch (SkillMode)
                            {
                                case Dota2CustomRealms.SkillMode.Draft:
                                    {
                                        while (Normals.Count > 60) // Remove random skills from the draft pool until only 60 remain
                                        {
                                            Normals.RemoveAt(rnd.Next(0, Normals.Count));
                                        }
                                        while (Ultimates.Count > 20) // Remove random ultimate skills from the draft pool until only 20 remain
                                        {
                                            Ultimates.RemoveAt(rnd.Next(0, Ultimates.Count));
                                        }
                                        Pool.AddRange(Normals);
                                        Pool.AddRange(Ultimates);
                                        SkillDraft = new DraftSkill(Players.Values.ToList(), Pool);
                                        break;
                                    }
                                case Dota2CustomRealms.SkillMode.All_Random:
                                    {
                                        Pool.AddRange(Skill.NormalList);
                                        Pool.AddRange(Skill.UltimateList);
                                        SkillDraft = new DraftSkill(Players.Values.ToList(), Pool);
                                        break;
                                    }
                            }


                            SyncSkillList();


                            RaiseGameEvent(this, new GameEventArgs(GAME_SKILL_PICK_POOL, true));

                            // If the mode is all random, then perform the randoming

                            if (SkillMode == Dota2CustomRealms.SkillMode.All_Random)
                            {

                                List<Skill> RandomPool = new List<Skill>();
                                List<Skill> RandomUltimatePool = new List<Skill>();

                                foreach (Skill aSkill in SkillDraft.AvailableDraftChoices)
                                {
                                    if (aSkill.IsUltimate)
                                    {
                                        RandomUltimatePool.Add(aSkill);
                                    }
                                    else
                                    {
                                        RandomPool.Add(aSkill);
                                    }
                                }

                                int PickerCount = SkillDraft.Turns.Count / 4;

                                StringBuilder MessageOut = null;

                                for (int i = 0; i < PickerCount * 3; i++)
                                {
                                    Player Player = SkillDraft.GetCurrentPicker();
                                    Skill RandomSkill = RandomPool[rnd.Next(0, RandomPool.Count)];

                                    if (SkillDraft.PlayerDraftPicks.ContainsKey(Player))
                                    {
                                        while (!Skill.DetermineSkillCompatibility(RandomSkill, SkillDraft.PlayerDraftPicks[Player]) || !RandomSkill.IsAllowedWith(SkillDraft.PlayerDraftPicks[Player]) || !RandomSkill.IsAllowedWithH(HeroDraft.PlayerDraftPicks[Player]))
                                        {
                                            RandomSkill = RandomPool[rnd.Next(0, RandomPool.Count)];
                                        }
                                    }


                                    SkillDraft.Pick(Player, RandomSkill);
                                    //SkillDraft.NextTurn();

                                    RandomPool.Remove(RandomSkill);

                                    if (i % 8 == 0)
                                    {
                                        if (MessageOut != null)
                                        {
                                            RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, MessageOut.ToString()));
                                        }
                                        MessageOut = new StringBuilder(GAME_SKILL_ASSIGN);
                                        MessageOut.Append(Player.Name);
                                        MessageOut.Append('&');
                                        MessageOut.Append(Skill.List.IndexOf(RandomSkill).ToString());
                                        MessageOut.Append("&randomed");
                                    }
                                    else
                                    {
                                        MessageOut.Append(',');
                                        MessageOut.Append(Player.Name);
                                        MessageOut.Append('&');
                                        MessageOut.Append(Skill.List.IndexOf(RandomSkill).ToString());
                                        MessageOut.Append("&randomed");
                                    }


                                    RaiseDisplayUserMessage(this, new SendMessageEventArgs(null, Player.Name + " randomed " + RandomSkill.Name));
                                }

                                for (int i = 0; i < PickerCount; i++)
                                {
                                    Player Player = SkillDraft.GetCurrentPicker();
                                    Skill RandomSkill = RandomUltimatePool[rnd.Next(0, RandomUltimatePool.Count)];

                                    while (!Skill.DetermineSkillCompatibility(RandomSkill, SkillDraft.PlayerDraftPicks[Player]) || !RandomSkill.IsAllowedWith(SkillDraft.PlayerDraftPicks[Player]) || !RandomSkill.IsAllowedWithH(HeroDraft.PlayerDraftPicks[Player]))
                                    {
                                        RandomSkill = RandomUltimatePool[rnd.Next(0, RandomUltimatePool.Count)];
                                    }

                                    SkillDraft.Pick(Player, RandomSkill);
                                    //SkillDraft.NextTurn();

                                    RandomUltimatePool.Remove(RandomSkill);

                                    if (MessageOut.Length > 350)
                                    {
                                        if (MessageOut != null)
                                        {
                                            RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, MessageOut.ToString()));
                                        }
                                        MessageOut = new StringBuilder(GAME_SKILL_ASSIGN);
                                        MessageOut.Append(Player.Name);
                                        MessageOut.Append('&');
                                        MessageOut.Append(Skill.List.IndexOf(RandomSkill).ToString());
                                        MessageOut.Append("&randomed");
                                    }
                                    else
                                    {
                                        MessageOut.Append(',');
                                        MessageOut.Append(Player.Name);
                                        MessageOut.Append('&');
                                        MessageOut.Append(Skill.List.IndexOf(RandomSkill).ToString());
                                        MessageOut.Append("&randomed");
                                    }
               
                                    RaiseDisplayUserMessage(this, new SendMessageEventArgs(null, Player.Name + " randomed " + RandomSkill.Name));
                                }

        
                                RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, MessageOut.ToString()));
                                RaiseUpdateUI(this, new EventArgs());
                       
                                ProgressGameStage();

                                return;
                            }

                        }
                        Player curr = null;
                        foreach (string player in Players.Keys)
                        {
                            if (MyName == player)
                            {
                                curr = Players[player];
                            }
                        }
                        if (curr != null)
                        {
                            foreach (Skill aSkill in SkillDraft.AvailableDraftChoices)
                            {
                                if (!aSkill.IsAllowedWithH(HeroDraft.PlayerDraftPicks[curr]))
                                {
                                    aSkill.SetPickBox(false);
                                }
                            }
                        }
                        RaiseGameEvent(this, new GameEventArgs(GAME_SKILL_PICK_POOL, true));
       
                        RaiseUpdateUI(this, new EventArgs());
           
                        break;
                    }
                default:
                    {
                        base.ProgressGameStage();
                        break;
                    }
            }
        }
    }
}
