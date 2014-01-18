using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Dota2CustomRealms
{
    class Game
    {

        protected static Random rnd = new Random();

        public enum GameStage
        {
            Lobby = 0,
            HeroDraft = 1,
            SkillDraft = 2,
            ServerSetup = 3,
            ClientSetup = 4,
            DuringGame = 5,
            PostGame = 6
        }

        public Draft<Hero, Hero> HeroDraft;

        public Draft<Skill, List<Skill>> SkillDraft;

        /// <summary>
        /// Sent from player->host to ask to join
        /// </summary>
        public const string GAME_JOIN_REQUEST = "LET_ME_IN!";
        /// <summary>
        /// Sent from host->player if lobby is full
        /// </summary>
        public const string GAME_JOIN_REFUSE_STARTED = "SERVER_FULL!";
        /// <summary>
        /// Sent from host->player if host isn't in lobby mode
        /// </summary>
        public const string GAME_JOIN_REFUSE_FULL = "SERVER_STARTED!";
        /// <summary>
        /// Sent from host->player
        /// </summary>
        public const string GAME_JOIN_ACCEPTED = "JOIN_GAME_NOW_:D";

        /// <summary>
        /// Sent from host->player
        /// </summary>
        public const string GAME_JOIN_REFUSE_KICKED = "JOIN_NO_KICKED";

        /// <summary>
        /// Sent from host->player
        /// </summary>
        public const string GAME_JOIN_REFUSE_BANNED = "JOIN_NO_BANNED";

        /// <summary>
        /// Sent from player->host
        /// </summary>
        public const string GAME_SIDE_REQUEST = "CAN_HAZ_SIDE=";
        /// <summary>
        /// Sent from host->player
        /// </summary>
        public const string GAME_SIDE_REFUSED_FULL = "SIDE_FULL!";
        /// <summary>
        /// Sent from host->channel
        /// </summary>
        public const string GAME_SIDE_ACCEPTED = "PLAYER_SIDE_CHANGE=";

        /// <summary>
        /// Host->player
        /// </summary>
        public const string GAME_SYNC_PLAYERLIST = "PLAYERLISTIS:";
        /// <summary>
        /// Player->host
        /// </summary>
        public const string GAME_SYNC_PLAYERLIST_REQUEST = "PLAYERLISTPLZ!";

        /// <summary>
        /// Host->Channel, Starts the hero picking and also comes with a list of heroes that can be picked
        /// </summary>
        public const string GAME_HERO_PICK_DATA_LIST = "GAMEHEROPICKPOOL=";

        /// <summary>
        /// Player->Host, requests hero pick
        /// </summary>
        public const string GAME_HERO_REQUEST = "CAN_HAZ_HERO=";
        /// <summary>
        /// Host->player, refuses hero pick
        /// </summary>
        public const string GAME_HERO_REFUSE = "NO_HAZ_HERO";
        /// <summary>
        /// Host->channel, assigns player a hero
        /// </summary>
        public const string GAME_HERO_ASSIGN = "PLAYER_HAZ_HERO=";

        /// <summary>
        /// Host->Channel, Starts the skill picking and also comes with a list of skills that can be picked (or ALL to signify all skills)
        /// </summary>
        public const string GAME_SKILL_PICK_POOL = "GAMESKILLPICKPOOL="; // Format GAMESKILLPICKPOOL=0+3,6+1,9,11,55,68+20

        public const string GAME_PLAYER_NEW = "PLAYER_ADD=";


        /// <summary>
        /// Player->Host, requests skill pick
        /// </summary>
        public const string GAME_SKILL_REQUEST = "CAN_HAZ_SKILL=";
        /// <summary>
        /// Host->player, refuses skill pick
        /// </summary>
        public const string GAME_SKILL_REFUSE = "NO_HAZ_SKILL";
        /// <summary>
        /// Host->channel, assigns player a skill
        /// </summary>
        public const string GAME_SKILL_ASSIGN = "PLAYER_SKILL_HERO=";

        /// <summary>
        /// Host->Channel, indicates switch to draft summary screen and server is being configured
        /// </summary>
        public const string GAME_PREGAME = "GAME_PREGAME!";

        // Cheese's protocol request, verbatim :D

        public const string GAME_CUSTOMMOD_CHECK = "CUSTOM_MOD_CHECK=";

        public const string GAME_CUSTOMMOD_YES = "YEA_I_DO";

        public const string GAME_CUSTOMMOD_NO = "NOPE_I_SUCK";

        private List<string> Whitelist = new List<string>();

        public List<string> Bans = null;


        string channel, radiantChannel, spectatorChannel, direChannel, hostName, myName, lobbyName, custommod;

        public string LobbyName
        {
            get { return lobbyName; }
            set { lobbyName = value; }
        }

        public string MyName
        {
            get { return myName; }
            set
            {
                myName = value;
                if (!Whitelist.Contains(myName))
                {
                    Whitelist.Add(myName);
                }
            }
        }

        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }
        GameStage stage = GameStage.Lobby;

        public GameStage Stage
        {
            get { return stage; }
            set { stage = value; }
        }

        public EventfulDictionary<string, Player> Players = new EventfulDictionary<string, Player>();

        public List<string> Blacklist = new List<string>();

        GameMode gameMode;

        internal GameMode GameMode
        {
            get { return gameMode; }
            set { gameMode = value; }
        }
        HeroMode heroMode;

        internal HeroMode HeroMode
        {
            get { return heroMode; }
            set { heroMode = value; }
        }
        SkillMode skillMode;

        internal SkillMode SkillMode
        {
            get { return skillMode; }
            set { skillMode = value; }
        }

        public string CustomMod
        {
            get { return custommod; }
            set { custommod = value; }
        }

        List<String> additionalmodes = new List<String>();

        internal List<String> AdditionalModes
        {
            get { return additionalmodes; }
            set { additionalmodes = value; }
        }

        public Game(GameMode GameMode, HeroMode HeroMode, SkillMode SkillMode, List<String> AdditionalModes)
        {
            this.GameMode = GameMode;
            this.HeroMode = HeroMode;
            this.SkillMode = SkillMode;
            this.AdditionalModes = AdditionalModes;
            if (File.Exists("bans.txt"))
                {
                    string[] bans = File.ReadAllLines("bans.txt");
                    Bans = bans.ToList();
                }
            Players.AfterAdd += new EventfulDictionary<string, Player>.DataChangedEventHandler(Players_AfterAdd);
            Players.AfterRemove += new EventfulDictionary<string, Player>.DataChangedEventHandler(Players_AfterRemove);
        }

        public Game()
        {
            Players.AfterAdd += new EventfulDictionary<string, Player>.DataChangedEventHandler(Players_AfterAdd);
            Players.AfterRemove += new EventfulDictionary<string, Player>.DataChangedEventHandler(Players_AfterRemove);
        }

        void Players_AfterRemove(object sender, EventfulDictionary<string, Player>.DataChangedEventArgs e)
        {
            if (isHost)
            {
                SyncPlayerList();
            }
            UpdateUI(this, new EventArgs());
        }

        void Players_AfterAdd(object sender, EventfulDictionary<string, Player>.DataChangedEventArgs e)
        {
            if (isHost)
            {
                if (!Whitelist.Contains(e.key))
                {
                    Players.Remove(e.key); // This guy wasn't approved, so don't let him in!
                    return; // The remove event will trigger the other stuff for us
                }
                SyncPlayerList();
                if (CustomMod != null && CustomMod.Length > 0)
                {
                    RaiseSendPlayerNotice(this, new SendMessageEventArgs(e.value.Name, GAME_CUSTOMMOD_CHECK + CustomMod));
                }
            }
            UpdateUI(this, new EventArgs());
        }

        bool isHost = false;
        int maxLobbySize = 10;
        string password = "";
        string dotamap = "dota";
        bool diretide = false;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int MaxLobbySize
        {
            get { return maxLobbySize; }
            set { maxLobbySize = value; }
        }

        public bool IsHost
        {
            get { return isHost; }
            set { isHost = value; }
        }

        public string Dotamap
        {
            get { return dotamap; }
            set { dotamap = value; }
        }

        public bool Diretide
        {
            get { return diretide; }
            set { diretide = value; }
        }

        public delegate void GameEventHandler(object sender, GameEventArgs e);
        public delegate void SendMessageEventHandler(object sender, SendMessageEventArgs e);

        public event GameEventHandler GameEvent;
        public event SendMessageEventHandler SendPlayerNotice;
        public event SendMessageEventHandler SendChannelNotice;
        public event SendMessageEventHandler DisplayUserMessage;
        public event EventHandler UpdateUI;


        protected void RaiseGameEvent(object sender, GameEventArgs e)
        {
            if (GameEvent != null)
            {
                GameEvent(sender, e);
            }
        }

        protected void RaiseSendPlayerNotice(object sender, SendMessageEventArgs e)
        {
            if (SendPlayerNotice != null)
            {
                SendPlayerNotice(sender, e);
            }
        }

        protected void RaiseSendChannelNotice(object sender, SendMessageEventArgs e)
        {
            if (SendChannelNotice != null)
            {
                SendChannelNotice(sender, e);
            }
        }

        protected void RaiseDisplayUserMessage(object sender, SendMessageEventArgs e)
        {
            if (DisplayUserMessage != null)
            {
                DisplayUserMessage(sender, e);
            }
        }

        protected void RaiseUpdateUI(object sender, EventArgs e)
        {
            if (UpdateUI != null)
            {
                UpdateUI(sender, e);
            }
        }

        public class GameEventArgs : EventArgs
        {

            public GameEventArgs(string EventName, bool Complete)
            {
                this.EventName = EventName;
                this.Complete = Complete;
            }

            public string EventName;
            public bool Complete;
        }

        public class SendMessageEventArgs : EventArgs
        {

            public SendMessageEventArgs(string Target, string Message)
            {
                this.Target = Target;
                this.Message = Message;
            }

            public string Target, Message;
        }


        public string Channel
        {
            get { return channel; }
            set
            {
                channel = value;
                RadiantChannel = Channel.Replace("#G_", "#R_");
                DireChannel = Channel.Replace("#G_", "#D_");
                SpectatorChannel = Channel.Replace("#G_", "#S_");
            }
        }

        public string RadiantChannel
        {
            get { return radiantChannel; }
            set { radiantChannel = value; }
        }

        public string SpectatorChannel
        {
            get { return spectatorChannel; }
            set { spectatorChannel = value; }
        }


        public string DireChannel
        {
            get { return direChannel; }
            set { direChannel = value; }
        }

        public void CheckCustomModStatus()
        {
            if (Players.ContainsKey(MyName) && CustomMod != null)
            {
                if(Directory.Exists("data\\custom\\" + CustomMod) && Directory.GetFiles("data\\custom\\" + CustomMod).Count() >= 3)
                {
                    if (!Players[MyName].HasMod)
                    {
                        Players[MyName].HasMod = true;
                        RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_CUSTOMMOD_YES));
                        RaiseUpdateUI(this, new EventArgs());
                    }
                }
                else if (Players[MyName].HasMod)
                {
                    Players[MyName].HasMod = false;
                    RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_CUSTOMMOD_NO));
                    RaiseUpdateUI(this, new EventArgs());
                }
            }
        }

        public virtual void ReceiveChannelNotice(string Channel, string SendingPlayer, string Message)
        {
            if (isHost)
            {
                if (Message.StartsWith(GAME_SYNC_PLAYERLIST_REQUEST))
                {
                    SyncPlayerList();
                }
            }
            else
            {
                if (Message.StartsWith(GAME_SIDE_ACCEPTED) && SendingPlayer == HostName) // Host informing clients that a player is switching sides
                {
                    string[] Args = Message.Substring(GAME_SIDE_ACCEPTED.Length).Split('&');
                    Debug.Assert(Enum.IsDefined(typeof(PlayerSide), Args[1]), "Invalid GAME_SIDE_ACCEPTED Message from Host: " + Message);
                    if (Players.ContainsKey(Args[0]))
                    {
                        Players[Args[0]].Side = (PlayerSide)Enum.Parse(typeof(PlayerSide), Args[1]);
                        if (UpdateUI != null)
                        {
                            UpdateUI(this, new EventArgs());
                        }
                        if (DisplayUserMessage != null)
                        {
                            DisplayUserMessage(this, new SendMessageEventArgs("", Args[0] + " changed side to " + Players[Args[0]].Side.ToString()));
                        }
                        if (MyName == Args[0] && GameEvent != null)
                        {
                            GameEvent(this, new GameEventArgs("Changed Side!", true));
                        }
                    }
                    else
                    {
                        SyncPlayerList();
                    }
                }
                else if (Message.StartsWith(GAME_SYNC_PLAYERLIST) && SendingPlayer == HostName)  // Host syncing list of players and their states
                {
                    EventfulDictionary<string, Player> newPlayerList = new EventfulDictionary<string, Player>();
                    string[] PlayersArray = Message.Substring(GAME_SYNC_PLAYERLIST.Length).Split('&');
                    foreach (string PlayerData in PlayersArray)
                    {
                        string[] PlayerSettings = PlayerData.Split(',');
                        Player Dude;
                        if (Players.ContainsKey(PlayerSettings[0]))
                        {
                            Dude = Players[PlayerSettings[0]];
                            newPlayerList.Add(PlayerSettings[0], Dude, false);
                        }
                        else
                        {
                            Dude = new Player();
                            Dude.Name = PlayerSettings[0];
                            newPlayerList.Add(PlayerSettings[0], Dude, false);
                        }
                        //Debug.Assert(Enum.IsDefined(typeof(PlayerSide), PlayerSettings[1]), "Invalid GAME_SYNC_PLAYERLIST Message from Host: " + Message);
                        Dude.Side = (PlayerSide)Enum.Parse(typeof(PlayerSide), PlayerSettings[1]);
                        Dude.Ready = (PlayerSettings[2] != "0" ? true : false);
                    }
                    Players = newPlayerList;
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
                }
                else if (Message.StartsWith(GAME_HERO_PICK_DATA_LIST) && SendingPlayer == HostName) // Message with available heroes in pool. If the stage is lobby, then progress to the hero picking stage
                {
                    List<Hero> Pool = new List<Hero>();
                    string[] HeroList = Message.Substring(GAME_HERO_PICK_DATA_LIST.Length).Split(',');
                    foreach (string HeroLoc in HeroList)
                    {
                        Pool.Add(Hero.List[int.Parse(HeroLoc)]);
                    }

                    if (HeroDraft != null)
                    {
                        HeroDraft.AvailableDraftChoices = Pool;
                        HeroDraft.UpdateUI();
                    }
                    else
                    {
                        HeroDraft = new DraftHero(Players.Values.ToList(), Pool);
                    }

                    if (Stage == GameStage.Lobby)
                    {
                        ProgressGameStage();
                    }
                }
                else if (Message.StartsWith(GAME_HERO_ASSIGN) && SendingPlayer == HostName) // Someone got a hero - Syntax TEXT<PlayerName>&<HeroID>&<PickReason>[,<PlayerName>&<HeroID>&<PickReason>...]
                {
                    string[] Assigns = Message.Substring(GAME_HERO_ASSIGN.Length).Split(',');

                    foreach (string Assign in Assigns)
                    {

                        string[] Args = Assign.Split('&');
                        Hero Hero = Hero.List[int.Parse(Args[1])];
                        HeroDraft.Pick(Players[Args[0]], Hero);
                        //HeroDraft.NextTurn();

                        if (DisplayUserMessage != null)
                        {
                            DisplayUserMessage(this, new SendMessageEventArgs(null, Args[0] + " " + Args[2] + " " + Hero.Name));
                        }
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }

                }
                else if (Message.StartsWith(GAME_SKILL_PICK_POOL) && SendingPlayer == HostName) // Message with available skills in pool. If the stage is hero picking, then progress to the skill picking stage
                {
                    List<Skill> Pool = new List<Skill>();
                    string[] SkillList = Message.Substring(GAME_SKILL_PICK_POOL.Length).Split(','); // Splits into bits of either format <number> or <number>+<runsize>
                    foreach (string SkillRun in SkillList)
                    {
                        if (SkillRun.Contains('+'))
                        {
                            // Its a run
                            string[] Args = SkillRun.Split('+');
                            int Start = int.Parse(Args[0]);
                            int Length = int.Parse(Args[1]);
                            for (int i = 0; i <= Length; i++)
                            {
                                Pool.Add(Skill.List[Start + i]);
                            }
                        }
                        else
                        {
                            Pool.Add(Skill.List[int.Parse(SkillRun)]);
                        }
                    }

                    if (SkillDraft != null)
                    {
                        SkillDraft.AvailableDraftChoices = Pool;
                        SkillDraft.UpdateUI();
                    }
                    else
                    {
                        SkillDraft = new DraftSkill(Players.Values.ToList(), Pool);
                    }

                    if (Stage == GameStage.HeroDraft)
                    {
                        ProgressGameStage();
                    }
                }
                else if (Message.StartsWith(GAME_SKILL_ASSIGN) && SendingPlayer == HostName) // Assign player a skill - Syntax TEXT<PlayerName>&<SkillID>&<PickReason>[,<PlayerName>&<SkillID>&<PickReason>...]
                {
                    string[] Assigns = Message.Substring(GAME_SKILL_ASSIGN.Length).Split(',');
                    foreach (string Assign in Assigns)
                    {
                        string[] Args = Assign.Split('&');
                        Skill Skill = Skill.List[int.Parse(Args[1])];
                        SkillDraft.Pick(Players[Args[0]], Skill);
                        //SkillDraft.NextTurn();

                        if (DisplayUserMessage != null)
                        {
                            DisplayUserMessage(this, new SendMessageEventArgs(null, Args[0] + " " + Args[2] + " " + Skill.Name));
                        }
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
                }
                else if (Message.StartsWith(GAME_PREGAME) && SendingPlayer == HostName)
                {
                    if (Stage == GameStage.SkillDraft)
                    {
                        ProgressGameStage();
                    }
                }
            }

            if (Message.StartsWith(GAME_CUSTOMMOD_YES))
            {
                Players[SendingPlayer].HasMod = true;
                RaiseUpdateUI(this, new EventArgs());
            }
            else if (Message.StartsWith(GAME_CUSTOMMOD_NO))
            {
                Players[SendingPlayer].HasMod = false;
                RaiseUpdateUI(this, new EventArgs());
            }
        }

        /// <summary>
        /// Removes a player who left
        /// </summary>
        /// <param name="Nickname"></param>
        public void PlayerLeftChannel(string Nickname)
        {
            if (Stage == GameStage.Lobby && isHost)
            {
                if (Players.ContainsKey(Nickname))
                {
                    Players.Remove(Nickname);
                    if (isHost)
                    {
                        SyncPlayerList();
                    }
                }
            }
        }


        public virtual void ReceivePlayerNotice(string Nickname, string Message)
        {

            if (isHost)
            {

                if (Message.StartsWith(GAME_JOIN_REQUEST)) // A player wants to join the lobby
                {
                    if (Stage != GameStage.Lobby) // Too late
                    {
                        SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_JOIN_REFUSE_STARTED));
                    }
                    else if (Players.Count >= MaxLobbySize) // Full
                    {
                        SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_JOIN_REFUSE_FULL));
                    }
                    else if (Blacklist.Contains(Nickname)) // KickBanned
                    {
                        SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_JOIN_REFUSE_KICKED));
                    }
                    else if (Bans != null && HostName.StartsWith("[") && (Bans.Contains(Nickname)))
                    {
                        SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_JOIN_REFUSE_BANNED));
                    }
                    else // Can join :D
                    {
                        Whitelist.Add(Nickname);

                        // Append Sync list with player in it

                        StringBuilder OutMsg = new StringBuilder();
                        bool First = true;
                        foreach (KeyValuePair<string, Player> Player in Players)
                        {
                            if (First)
                            {
                                First = false;
                            }
                            else
                            {
                                OutMsg.Append("&");
                            }
                            OutMsg.Append(Player.Value.Name);
                            OutMsg.Append(",");
                            OutMsg.Append(((int)(Player.Value.Side)).ToString());
                            OutMsg.Append(",");
                            OutMsg.Append(Player.Value.Ready ? "1" : "0");


                        }
                        // Stick them in it so they don't crap out after connecting
                        OutMsg.Append("&");
                        OutMsg.Append(Nickname);
                        OutMsg.Append(",");
                        OutMsg.Append(PlayerSide.Spectator);
                        OutMsg.Append(",");
                        OutMsg.Append("0");

                        if (SendPlayerNotice != null)
                        {
                            SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_JOIN_ACCEPTED + OutMsg.ToString())); // Invite new player to lobby
                        }


                        //SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_PLAYER_NEW + Nickname)); // Lets actually do this when they join the channel
                    }
                }
                else if (Message.StartsWith(GAME_SIDE_REQUEST)) // A player wants to change sides
                {
                    Debug.Assert(Enum.IsDefined(typeof(PlayerSide), Message.Substring(GAME_SIDE_REQUEST.Length)), "Player attempting to change to invalid side: " + Message + "\nYou may safely ignore this message, but please report that it happened.");
                    PlayerSide DesiredSide = (PlayerSide)Enum.Parse(typeof(PlayerSide), Message.Substring(GAME_SIDE_REQUEST.Length));

                    if (DesiredSide != PlayerSide.Spectator)
                    {

                        int SideSize = 0;

                        foreach (KeyValuePair<string, Player> Player in Players)
                        {
                            if (Player.Key != Nickname && Player.Value.Side == DesiredSide)
                            {
                                SideSize++;
                            }
                        }

                        if (SideSize > 4 && SendPlayerNotice != null) // Side is full
                        {
                            SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_SIDE_REFUSED_FULL + ((int)Players[Nickname].Side).ToString())); // Tell player to piss off and stay where they are
                            return;
                        }
                    }
                    Players[Nickname].Side = DesiredSide;
                    if (SendChannelNotice != null)
                    {
                        SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_SIDE_ACCEPTED + Nickname + "&" + DesiredSide.ToString())); // Change player side
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
                    if (DisplayUserMessage != null)
                    {
                        DisplayUserMessage(this, new SendMessageEventArgs("", Nickname + " changed side to " + DesiredSide.ToString()));
                    }
                    if (HostName == Nickname && GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Changed Side!", true));
                    }
                }
                else if (Message.StartsWith(GAME_SYNC_PLAYERLIST_REQUEST))
                {
                    SyncPlayerList();
                }
                else if (Message.StartsWith(GAME_HERO_REQUEST)) // A player is attempting to pick a hero
                {

                    if (Stage != GameStage.HeroDraft)
                    {
                        return;
                    }

                    Hero Hero = Hero.List[int.Parse(Message.Substring(GAME_HERO_REQUEST.Length))];
                    if (HeroMode != Dota2CustomRealms.HeroMode.All_Random && HeroDraft.AvailableDraftChoices.Contains(Hero)) // Ensure hero is available
                    {
                        if (Players.ContainsKey(Nickname) && HeroDraft.IsPlayerTurn(Players[Nickname])) // Ensure its this players turn
                        {
                            // They can pick this hero :D
                            HeroDraft.Pick(Players[Nickname], Hero);
                            //HeroDraft.NextTurn();

                            if (SendChannelNotice != null)
                            {
                                SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_HERO_ASSIGN + Nickname + "&" + Hero.List.IndexOf(Hero).ToString() + "&picked"));
                            }
                            if (DisplayUserMessage != null)
                            {
                                DisplayUserMessage(this, new SendMessageEventArgs(null, Nickname + " picked " + Hero.Name));
                            }
                            if (UpdateUI != null)
                            {
                                UpdateUI(this, new EventArgs());
                            }
                            if (HeroDraft.PlayerDraftPicks.Count == HeroDraft.Turns.Count) // DRAFT COMPLETE
                            {
                                ProgressGameStage();
                            }
                        }
                    }
                    else
                    {
                        if (SendPlayerNotice != null)
                        {
                            SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_HERO_REFUSE));
                        }
                    }
                }
                else if (Message.StartsWith(GAME_SKILL_REQUEST)) // A player is attempting to pick a skill
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
                                    if (SendPlayerNotice != null)
                                    {
                                        SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_SKILL_REFUSE + "Too many ablities!"));
                                    }
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

                                if (SendChannelNotice != null)
                                {
                                    SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_SKILL_ASSIGN + Nickname + "&" + Skill.List.IndexOf(Skill).ToString() + "&picked"));
                                }
                                if (DisplayUserMessage != null)
                                {
                                    DisplayUserMessage(this, new SendMessageEventArgs(null, Nickname + " picked " + Skill.Name));
                                }
                                if (UpdateUI != null)
                                {
                                    UpdateUI(this, new EventArgs());
                                }
                                if (SkillDraft.GetCurrentTurn() == SkillDraft.Turns.Count) // DRAFT COMPLETE
                                {
                                    ProgressGameStage();
                                }
                            }
                            else
                            {
                                if (SendPlayerNotice != null)
                                {
                                    SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_SKILL_REFUSE + "Must pick 1 ultimate and 3 normal skills!"));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (SendPlayerNotice != null)
                        {
                            SendPlayerNotice(this, new SendMessageEventArgs(Nickname, GAME_SKILL_REFUSE + "Not available"));
                        }
                    }
                }
            }
            else if (Nickname == HostName) // Ensure its coming from the appropriate host
            {

                if (Message.StartsWith(GAME_JOIN_ACCEPTED)) // Ensure the GAME_JOIN_ACCEPTED is from the host for this game instance
                {

                    EventfulDictionary<string, Player> newPlayerList = new EventfulDictionary<string, Player>();
                    string[] PlayersArray = Message.Substring(GAME_JOIN_ACCEPTED.Length).Split('&');
                    foreach (string PlayerData in PlayersArray)
                    {
                        string[] PlayerSettings = PlayerData.Split(',');
                        Player Dude;
                        if (Players.ContainsKey(PlayerSettings[0]))
                        {
                            Dude = Players[PlayerSettings[0]];
                            newPlayerList.Add(PlayerSettings[0], Dude, false);
                        }
                        else
                        {
                            Dude = new Player();
                            Dude.Name = PlayerSettings[0];
                            newPlayerList.Add(PlayerSettings[0], Dude, false);
                        }
                        //Debug.Assert(Enum.IsDefined(typeof(PlayerSide), PlayerSettings[1]), "Invalid GAME_SYNC_PLAYERLIST Message from Host: " + Message);
                        Dude.Side = (PlayerSide)Enum.Parse(typeof(PlayerSide), PlayerSettings[1]);
                        Dude.Ready = (PlayerSettings[2] != "0" ? true : false);
                    }

                    Players = newPlayerList;

                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Join success!", true));
                        GameEvent(this, new GameEventArgs(GAME_JOIN_ACCEPTED, true));
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
                    SyncPlayerList();
                }
                else if (Message.StartsWith(GAME_JOIN_REFUSE_FULL))
                {
                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Join failed - Lobby full", true));
                        GameEvent(this, new GameEventArgs(GAME_JOIN_REFUSE_FULL, true));
                    }
                }
                else if (Message.StartsWith(GAME_JOIN_REFUSE_STARTED))
                {
                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Join failed - Started", true));
                        GameEvent(this, new GameEventArgs(GAME_JOIN_REFUSE_STARTED, true));
                    }
                }
                else if (Message.StartsWith(GAME_JOIN_REFUSE_KICKED))
                {
                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Join failed - Kicked", true));
                        GameEvent(this, new GameEventArgs(GAME_JOIN_REFUSE_KICKED, true));
                    }
                }
                else if (Message.StartsWith(GAME_JOIN_REFUSE_BANNED))
                {
                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Join failed - Banned", true));
                        GameEvent(this, new GameEventArgs(GAME_JOIN_REFUSE_BANNED, true));
                    }
                }
                else if (Message.StartsWith(GAME_SIDE_REFUSED_FULL))
                {
                    Players[MyName].Side = (PlayerSide)Enum.Parse(typeof(PlayerSide), Message.Substring(GAME_SIDE_REFUSED_FULL.Length));
                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Can't Change Side", true));
                        GameEvent(this, new GameEventArgs(GAME_SIDE_REFUSED_FULL, true));
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
                }
                else if (Message.StartsWith(GAME_HERO_REFUSE))
                {
                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Pick failed", true));
                        GameEvent(this, new GameEventArgs(GAME_HERO_REFUSE, true));
                    }
                }
                else if (Message.StartsWith(GAME_SKILL_REFUSE))
                {
                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Pick failed", true));
                        GameEvent(this, new GameEventArgs(GAME_SKILL_REFUSE, true));
                    }
                }
                else if (Message.StartsWith(GAME_CUSTOMMOD_CHECK))
                {
                    CustomMod = Message.Substring(GAME_CUSTOMMOD_CHECK.Length);
                    Players[MyName].HasMod = Directory.Exists("data\\custom\\" + CustomMod) && Directory.GetFiles("data\\custom\\" + CustomMod).Count() >= 2;
                    RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, Players[MyName].HasMod ? GAME_CUSTOMMOD_YES : GAME_CUSTOMMOD_NO));
                    RaiseUpdateUI(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// If you're the host, you will sync player list with other players. If not, you will request a sync.
        /// </summary>
        public void SyncPlayerList()
        {
            if (isHost)
            {
                StringBuilder OutMsg = new StringBuilder();
                OutMsg.Append(GAME_SYNC_PLAYERLIST);
                bool First = true;
                foreach (KeyValuePair<string, Player> Player in Players)
                {
                    if (First)
                    {
                        First = false;
                    }
                    else
                    {
                        OutMsg.Append("&");
                    }
                    OutMsg.Append(Player.Value.Name);
                    OutMsg.Append(",");
                    OutMsg.Append(((int)(Player.Value.Side)).ToString());
                    OutMsg.Append(",");
                    OutMsg.Append(Player.Value.Ready ? "1" : "0");
                }
                if (SendChannelNotice != null)
                {
                    SendChannelNotice(this, new SendMessageEventArgs(Channel, OutMsg.ToString()));
                }
            }
            else if (SendPlayerNotice != null)
            {
                SendPlayerNotice(this, new SendMessageEventArgs(HostName, GAME_SYNC_PLAYERLIST_REQUEST));
            }
        }



        /// <summary>
        /// Syncs list of available picks - Currently only does anything if you're the host
        /// </summary>
        protected void SyncHeroList()
        {
            if (isHost)
            {
                if (SendChannelNotice != null)
                {
                    StringBuilder SerialisedPickList = new StringBuilder(GAME_HERO_PICK_DATA_LIST);
                    foreach (Hero aHero in HeroDraft.AvailableDraftChoices)
                    {
                        SerialisedPickList.Append(Hero.List.IndexOf(aHero).ToString());
                        SerialisedPickList.Append(",");
                    }
                    SerialisedPickList.Remove(SerialisedPickList.Length - 1, 1);

                    SendChannelNotice(this, new SendMessageEventArgs(Channel, SerialisedPickList.ToString()));
                }
            }
        }

        /// <summary>
        /// Syncs list of available picks - Currently only does anything if you're the host
        /// </summary>
        protected void SyncSkillList()
        {
            if (isHost)
            {
                if (SendChannelNotice != null)
                {
                    int RunStart = 0;
                    int RunSize = -1;

                    StringBuilder SerialisedPickList = new StringBuilder(GAME_SKILL_PICK_POOL);

                    for (int i = 0; i < Skill.List.Count; i++)
                    {
                        if (SkillDraft.AvailableDraftChoices.Contains(Skill.List[i]))
                        {
                            RunSize++;
                        }
                        else
                        {
                            if (RunSize >= 0)
                            {
                                if (SerialisedPickList.Length > GAME_SKILL_PICK_POOL.Length)
                                {
                                    SerialisedPickList.Append(",");
                                }
                                SerialisedPickList.Append(RunStart.ToString());
                                if (RunSize > 0)
                                {
                                    SerialisedPickList.Append("+");
                                    SerialisedPickList.Append(RunSize.ToString());
                                }
                            }
                            RunStart = i + 1;
                            RunSize = -1;
                        }
                    }
                    if (RunSize >= 0)
                    {
                        if (SerialisedPickList.Length > GAME_SKILL_PICK_POOL.Length)
                        {
                            SerialisedPickList.Append(",");
                        }
                        SerialisedPickList.Append(RunStart.ToString());
                        if (RunSize > 0)
                        {
                            SerialisedPickList.Append("+");
                            SerialisedPickList.Append(RunSize.ToString());
                        }
                    }
                    //System.Windows.Forms.MessageBox.Show(SerialisedPickList.ToString());
                    SendChannelNotice(this, new SendMessageEventArgs(Channel, SerialisedPickList.ToString()));
                }
            }
        }


        public virtual void CheckTime()
        {
            if (Stage == GameStage.HeroDraft && HeroDraft.GetTimeRemaining() < 0 && isHost)
            {
                // Enforce a random pick
                Hero RandomHero = HeroDraft.AvailableDraftChoices[rnd.Next(0, HeroDraft.AvailableDraftChoices.Count)];
                List<Player> Players = HeroDraft.GetCurrentPickers();
                foreach(Player Player in Players)
                {
                    HeroDraft.Pick(Player, RandomHero);
                    //HeroDraft.NextTurn();

                    if (SendChannelNotice != null)
                    {
                        SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_HERO_ASSIGN + Player.Name + "&" + Hero.List.IndexOf(RandomHero).ToString() + "&took too long and randomed"));
                    }
                    if (DisplayUserMessage != null)
                    {
                        DisplayUserMessage(this, new SendMessageEventArgs(null, Player.Name + " took too long and randomed " + RandomHero.Name));
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
                }
                if (HeroDraft.PlayerDraftPicks.Count == HeroDraft.Turns.Count || Players.Count == 0) // DRAFT COMPLETE
                {
                    ProgressGameStage();
                }
            }
            else if (Stage == GameStage.SkillDraft && SkillDraft.GetTimeRemaining() < 0 && isHost)
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

                    foreach (Skill Skill in SkillDraft.AvailableDraftChoices)
                    {
                        if (Skill.IsUltimate == UltimateRandom)
                        {
                            RandomPool.Add(Skill);
                        }
                    }

                    Skill RandomSkill = RandomPool[rnd.Next(0, RandomPool.Count)];

                    if (SkillDraft.PlayerDraftPicks.ContainsKey(Player))
                    {
                        while (!Skill.DetermineSkillCompatibility(RandomSkill, SkillDraft.PlayerDraftPicks[Player]))
                        {
                            RandomSkill = RandomPool[rnd.Next(0, RandomPool.Count)];
                        }
                    }

                    SkillDraft.Pick(Player, RandomSkill);
                    //SkillDraft.NextTurn();

                    if (SendChannelNotice != null)
                    {
                        SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_SKILL_ASSIGN + Player.Name + "&" + Skill.List.IndexOf(RandomSkill).ToString() + "&took too long and randomed"));
                    }
                    if (DisplayUserMessage != null)
                    {
                        DisplayUserMessage(this, new SendMessageEventArgs(null, Player.Name + " took too long and randomed " + RandomSkill.Name));
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
          
                }
                if (SkillDraft.GetCurrentTurn() == SkillDraft.Turns.Count || Players.Count == 0) // DRAFT COMPLETE
                {
                    ProgressGameStage();
                }
            }
        }

        public void RequestJoin(string HostName)
        {
            if (SendPlayerNotice != null)
            {
                SendPlayerNotice(this, new SendMessageEventArgs(HostName, GAME_JOIN_REQUEST));
            }
            if (GameEvent != null)
            {
                GameEvent(this, new GameEventArgs("Join Request sent...", false));
            }
            this.HostName = HostName;
        }

        /// <summary>
        /// Asks host if player can change sides. Should only be called during Lobby stage.
        /// </summary>
        /// <param name="NewSide"></param>
        public void AttemptSideChange(PlayerSide NewSide)
        {
            Debug.Assert(Stage == GameStage.Lobby, "Wrong Game stage to change side!");

            int SideSize = 0;

            if (NewSide != PlayerSide.Spectator)
            {
                foreach (KeyValuePair<string, Player> Player in Players)
                {
                    if (Player.Value.Side == NewSide)
                    {
                        SideSize++;
                    }
                }
                if (SideSize > 4)
                {
                    System.Windows.Forms.MessageBox.Show(NewSide.ToString() + " is full!");
                    return;
                }
            }

            //Players[MyName].Side = NewSide;
            /*if (UpdateUI != null)
            {
                UpdateUI(this, new EventArgs());
            }*/

            if (SendPlayerNotice != null)
            {
                SendPlayerNotice(this, new SendMessageEventArgs(HostName, GAME_SIDE_REQUEST + NewSide.ToString()));
            }
            if (GameEvent != null)
            {
                GameEvent(this, new GameEventArgs("Requesting Side Change...", false));
            }
        }

        /// <summary>
        /// Asks host if a hero can be picked
        /// </summary>
        /// <param name="Hero"></param>
        public void AttemptHeroPick(Hero Hero)
        {
            if (HeroMode == Dota2CustomRealms.HeroMode.All_Random)
            {
                return;
            }


            Debug.Assert(Stage == GameStage.HeroDraft, "Wrong Game stage to pick heroes!");

            if (HeroDraft.IsPlayerTurn(Players[MyName]))
            {
                if (HeroDraft.AvailableDraftChoices.Contains(Hero))
                {
                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Requesting Hero Pick...", false));
                    }
                    HeroDraft.Pick(Players[MyName], Hero);
                    if (isHost)
                    {
                        //HeroDraft.NextTurn();

                        if (SendChannelNotice != null)
                        {
                            SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_HERO_ASSIGN + MyName + "&" + Hero.List.IndexOf(Hero).ToString() + "&picked"));
                        }
                        if (DisplayUserMessage != null)
                        {
                            DisplayUserMessage(this, new SendMessageEventArgs(null, MyName + " picked " + Hero.Name));
                        }
                        if (GameEvent != null)
                        {
                            GameEvent(this, new GameEventArgs("Hero Picked!", true));
                        }
                        if (HeroDraft.PlayerDraftPicks.Count == HeroDraft.Turns.Count) // DRAFT COMPLETE
                        {
                            ProgressGameStage();
                        }
                    }
                    else
                    {
                        if (SendPlayerNotice != null)
                        {
                            SendPlayerNotice(this, new SendMessageEventArgs(HostName, GAME_HERO_REQUEST + Hero.List.IndexOf(Hero).ToString()));
                        }
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("That hero isn't in the draft pool - This isn't supposed to happen");
                }
            }

        }



        /// <summary>
        /// Asks host if a skill can be picked
        /// </summary>
        /// <param name="Skill"></param>
        public virtual void AttemptSkillPick(Skill Skill)
        {
            if (SkillMode == Dota2CustomRealms.SkillMode.All_Random)
            {
                return;
            }

            Debug.Assert(Stage == GameStage.SkillDraft, "Wrong Game stage to pick Skills!");

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

                    if (GameEvent != null)
                    {
                        GameEvent(this, new GameEventArgs("Requesting Skill Pick...", false));
                    }
                    if (isHost)
                    {
                        SkillDraft.Pick(Me, Skill);
                        //SkillDraft.NextTurn();

                        if (SendChannelNotice != null)
                        {
                            SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_SKILL_ASSIGN + MyName + "&" + Skill.List.IndexOf(Skill).ToString() + "&picked"));
                        }
                        if (DisplayUserMessage != null)
                        {
                            DisplayUserMessage(this, new SendMessageEventArgs(null, MyName + " picked " + Skill.Name));
                        }
                        if (GameEvent != null)
                        {
                            GameEvent(this, new GameEventArgs("Skill Picked!", true));
                        }
                        if (SkillDraft.GetCurrentTurn() == SkillDraft.Turns.Count) // DRAFT COMPLETE
                        {
                            ProgressGameStage();
                        }
                    }
                    else
                    {
                        if (SendPlayerNotice != null)
                        {
                            SendPlayerNotice(this, new SendMessageEventArgs(HostName, GAME_SKILL_REQUEST + Skill.List.IndexOf(Skill).ToString()));
                        }
                    }
                    if (UpdateUI != null)
                    {
                        UpdateUI(this, new EventArgs());
                    }
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
        public virtual void ProgressGameStage()
        {
            switch (Stage)
            {
                case GameStage.Lobby: // Lobby -> HeroDraft
                    {

                        RadiantChannel = Channel.Replace("#G_", "#R_");
                        DireChannel = Channel.Replace("#G_", "#D_");
                        SpectatorChannel = Channel.Replace("#G_", "#S_");


                        Stage = GameStage.HeroDraft;

                        if (isHost)
                        {
                            // Start Hero Draft

                            switch (HeroMode)
                            {
                                case Dota2CustomRealms.HeroMode.Draft:
                                    {
                                        List<Hero> RandomDraftPool = new List<Hero>(Hero.List.Count);
                                        RandomDraftPool.AddRange(Hero.List);

                                        // Remove disabled heroes
                                        for (int i = 0; i < RandomDraftPool.Count; i++)
                                        {
                                            if (RandomDraftPool[i].Type == HeroType.Disabled || RandomDraftPool[i].Side == HeroSide.Disabled)
                                            {
                                                RandomDraftPool.RemoveAt(i);
                                                i--;
                                            }
                                        }

                                        while (RandomDraftPool.Count > 22) // Remove random heroes from the draft pool until only 22 remain
                                        {
                                            RandomDraftPool.RemoveAt(rnd.Next(0, RandomDraftPool.Count));
                                        }
                                        HeroDraft = new DraftHero(Players.Values.ToList(), RandomDraftPool);
                                        break;
                                    }
                                case Dota2CustomRealms.HeroMode.All_Random:
                                case Dota2CustomRealms.HeroMode.All_Pick:
                                    {
                                        List<Hero> Pool = new List<Hero>(Hero.List.Count);
                                        Pool.AddRange(Hero.List);
                                        // Remove disabled heroes
                                        for (int i = 0; i < Pool.Count; i++)
                                        {
                                            if (Pool[i].Type == HeroType.Disabled || Pool[i].Side == HeroSide.Disabled)
                                            {
                                                Pool.RemoveAt(i);
                                                i--;
                                            }
                                        }

                                        HeroDraft = new DraftHero(Players.Values.ToList(), Hero.List);
                                        break;
                                    }
                            }


                            SyncHeroList();

                            // If the mode is all random, then perform the randoming

                            if (HeroMode == Dota2CustomRealms.HeroMode.All_Random)
                            {
                                StringBuilder MessageOut = new StringBuilder(GAME_HERO_ASSIGN);

                                while (HeroDraft.GetCurrentTurn() != HeroDraft.Turns.Count)
                                {
                                    Hero RandomedHero = HeroDraft.AvailableDraftChoices[rnd.Next(0, HeroDraft.AvailableDraftChoices.Count)];
                                    Player Picker = HeroDraft.GetCurrentPicker();
                                    HeroDraft.Pick(Picker, RandomedHero);
                                    //HeroDraft.NextTurn();


                                    if (MessageOut.Length == GAME_HERO_ASSIGN.Length) // No need to add ,
                                    {
                                        MessageOut.Append(Picker.Name);
                                        MessageOut.Append('&');
                                        MessageOut.Append(Hero.List.IndexOf(RandomedHero).ToString());
                                        MessageOut.Append("&randomed");
                                    }
                                    else if (MessageOut.Length < 350) // Appending to already started list
                                    {
                                        MessageOut.Append(',');
                                        MessageOut.Append(Picker.Name);
                                        MessageOut.Append('&');
                                        MessageOut.Append(Hero.List.IndexOf(RandomedHero).ToString());
                                        MessageOut.Append("&randomed");
                                    }
                                    else // Need to send and restart chain
                                    {
                                        if (SendChannelNotice != null)
                                        {
                                            SendChannelNotice(this, new SendMessageEventArgs(Channel, MessageOut.ToString()));
                                        }
                                        MessageOut = new StringBuilder(GAME_HERO_ASSIGN);
                                        MessageOut.Append(Picker.Name);
                                        MessageOut.Append('&');
                                        MessageOut.Append(Hero.List.IndexOf(RandomedHero).ToString());
                                        MessageOut.Append("&randomed");
                                    }


                                    if (DisplayUserMessage != null)
                                    {
                                        DisplayUserMessage(this, new SendMessageEventArgs(null, Picker.Name + " randomed " + RandomedHero.Name));
                                    }
                                }
                                if (MessageOut.Length > GAME_HERO_ASSIGN.Length && SendChannelNotice != null)
                                {
                                    SendChannelNotice(this, new SendMessageEventArgs(Channel, MessageOut.ToString()));
                                }

                                ProgressGameStage();

                                return;

                            }

                        }

                        if (GameEvent != null)
                        {
                            GameEvent(this, new GameEventArgs(GAME_HERO_PICK_DATA_LIST, true));
                        }

                        if (UpdateUI != null)
                        {
                            UpdateUI(this, new EventArgs());
                        }

                        break;
                    }
                case GameStage.HeroDraft: // HeroDraft -> SkillDraft
                    {

                        Stage = GameStage.SkillDraft;

                        if (isHost)
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


                            if (GameEvent != null)
                            {
                                GameEvent(this, new GameEventArgs(GAME_SKILL_PICK_POOL, true));
                            }

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
                                        while (!Skill.DetermineSkillCompatibility(RandomSkill, SkillDraft.PlayerDraftPicks[Player]))
                                        {
                                            RandomSkill = RandomPool[rnd.Next(0, RandomPool.Count)];
                                        }
                                    }


                                    SkillDraft.Pick(Player, RandomSkill);
                                    //SkillDraft.NextTurn();

                                    RandomPool.Remove(RandomSkill);

                                    if (i % 8 == 0)
                                    {
                                        if (MessageOut != null && SendChannelNotice != null)
                                        {
                                            SendChannelNotice(this, new SendMessageEventArgs(Channel, MessageOut.ToString()));
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

                                    if (DisplayUserMessage != null)
                                    {
                                        DisplayUserMessage(this, new SendMessageEventArgs(null, Player.Name + " randomed " + RandomSkill.Name));
                                    }
                                }

                                for (int i = 0; i < PickerCount; i++)
                                {
                                    Player Player = SkillDraft.GetCurrentPicker();
                                    Skill RandomSkill = RandomUltimatePool[rnd.Next(0, RandomUltimatePool.Count)];

                                    while (!Skill.DetermineSkillCompatibility(RandomSkill, SkillDraft.PlayerDraftPicks[Player]))
                                    {
                                        RandomSkill = RandomPool[rnd.Next(0, RandomPool.Count)];
                                    }

                                    SkillDraft.Pick(Player, RandomSkill);
                                    //SkillDraft.NextTurn();

                                    RandomUltimatePool.Remove(RandomSkill);

                                    if (MessageOut.Length > 350)
                                    {
                                        if (MessageOut != null && SendChannelNotice != null)
                                        {
                                            SendChannelNotice(this, new SendMessageEventArgs(Channel, MessageOut.ToString()));
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
                                    if (DisplayUserMessage != null)
                                    {
                                        DisplayUserMessage(this, new SendMessageEventArgs(null, Player.Name + " randomed " + RandomSkill.Name));
                                    }
                                }

                                if (SendChannelNotice != null)
                                {
                                    SendChannelNotice(this, new SendMessageEventArgs(Channel, MessageOut.ToString()));
                                }

                                if (UpdateUI != null)
                                {
                                    UpdateUI(this, new EventArgs());
                                }

                                ProgressGameStage();

                                return;
                            }

                        }

                        if (GameEvent != null)
                        {
                            GameEvent(this, new GameEventArgs(GAME_SKILL_PICK_POOL, true));
                        }

                        if (UpdateUI != null)
                        {
                            UpdateUI(this, new EventArgs());
                        }

                        break;
                    }
                case GameStage.SkillDraft:
                    {
                        stage = GameStage.ServerSetup;
                        if (UpdateUI != null)
                        {
                            UpdateUI(this, new EventArgs());
                        }
                        if (isHost)
                        {
                            if (SendChannelNotice != null)
                            {
                                SendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_PREGAME));
                            }
                        }
                        if (GameEvent != null)
                        {
                            GameEvent(this, new GameEventArgs(GAME_PREGAME, true));
                        }

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

    }

}