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
            //HeroDraft = 1,
            //SkillDraft = 2,
            ServerSetup = 3,
            ClientSetup = 4,
            DuringGame = 5,
            PostGame = 6
        }


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

        public const string GAME_PLAYER_NEW = "PLAYER_ADD=";


     
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
                else if (Message.StartsWith(GAME_PREGAME) && SendingPlayer == HostName)
                {
                    if (Stage == GameStage.Lobby)
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
        /// Will advance the game from lobby to hero picking, hero picking to skill picking, and so on
        /// </summary>
        public virtual void ProgressGameStage()
        {
            switch (Stage)
            {
                case GameStage.Lobby: // Lobby -> ServerSetup
                    {

                        RadiantChannel = Channel.Replace("#G_", "#R_");
                        DireChannel = Channel.Replace("#G_", "#D_");
                        SpectatorChannel = Channel.Replace("#G_", "#S_");


                        Stage = GameStage.ServerSetup;


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