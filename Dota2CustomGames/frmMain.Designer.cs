namespace Dota2CustomRealms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.webAdvertisement = new System.Windows.Forms.WebBrowser();
            this.ircListener = new System.Windows.Forms.Timer(this.components);
            this.lblMessageLeft = new System.Windows.Forms.Label();
            this.ttpHeroDraftTooltips = new System.Windows.Forms.ToolTip(this.components);
            this.ttpSkillDraftTooltips = new System.Windows.Forms.ToolTip(this.components);
            this.bgwGenerateNpcHeroesAutoexec = new System.ComponentModel.BackgroundWorker();
            this.ofdFindDotaExe = new System.Windows.Forms.OpenFileDialog();
            this.lblVersionSubtitle = new System.Windows.Forms.Label();
            this.ofdFindSrcdsExe = new System.Windows.Forms.OpenFileDialog();
            this.pbxBanner = new System.Windows.Forms.PictureBox();
            this.ofdFindSteam = new System.Windows.Forms.OpenFileDialog();
            this.icoNotifyDraftTurn = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblPlayersOnline = new System.Windows.Forms.Label();
            this.timerPlayers = new System.Windows.Forms.Timer(this.components);
            this.lblPlayersInGame = new System.Windows.Forms.Label();
            this.gbxChat = new System.Windows.Forms.GroupBox();
            this.gbxGameSize = new System.Windows.Forms.TabControl();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.tbxChatMessage = new System.Windows.Forms.TextBox();
            this.tabUISections = new System.Windows.Forms.TabControl();
            this.tabPreConnect = new System.Windows.Forms.TabPage();
            this.gbxConnect = new System.Windows.Forms.GroupBox();
            this.btnConnectIRC = new System.Windows.Forms.Button();
            this.tbxChooseNick = new System.Windows.Forms.TextBox();
            this.lblChooseNick = new System.Windows.Forms.Label();
            this.tabConnected = new System.Windows.Forms.TabPage();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnFindLobby = new System.Windows.Forms.Button();
            this.btnHostLobby = new System.Windows.Forms.Button();
            this.tabHostLobby = new System.Windows.Forms.TabPage();
            this.gbxCustomMod = new System.Windows.Forms.GroupBox();
            this.tbxCustomMod = new System.Windows.Forms.ComboBox();
            this.lblCustomWarn = new System.Windows.Forms.Label();
            this.chkCustomEnable = new System.Windows.Forms.CheckBox();
            this.gbxAdditional = new System.Windows.Forms.GroupBox();
            this.chkBalanced = new System.Windows.Forms.CheckBox();
            this.cbxX2 = new System.Windows.Forms.CheckBox();
            this.cbxAllTalk = new System.Windows.Forms.CheckBox();
            this.cbxWTF = new System.Windows.Forms.CheckBox();
            this.gmxGameMode = new System.Windows.Forms.GroupBox();
            this.cbxGameMode = new System.Windows.Forms.ComboBox();
            this.gbxMap = new System.Windows.Forms.GroupBox();
            this.cbxMap = new System.Windows.Forms.ComboBox();
            this.gbxLobbySize = new System.Windows.Forms.GroupBox();
            this.cbxGameSize = new System.Windows.Forms.ComboBox();
            this.gbxGamePassword = new System.Windows.Forms.GroupBox();
            this.tbxGamePassword = new System.Windows.Forms.TextBox();
            this.btnCancelHosting = new System.Windows.Forms.Button();
            this.btnHostGame = new System.Windows.Forms.Button();
            this.gbxSkillMode = new System.Windows.Forms.GroupBox();
            this.radSkillDraft = new System.Windows.Forms.RadioButton();
            this.radSkillRandom = new System.Windows.Forms.RadioButton();
            this.gbxGameName = new System.Windows.Forms.GroupBox();
            this.tbxGameName = new System.Windows.Forms.TextBox();
            this.gbxHeroMode = new System.Windows.Forms.GroupBox();
            this.radHeroDraft = new System.Windows.Forms.RadioButton();
            this.radHeroRandom = new System.Windows.Forms.RadioButton();
            this.radHeroPick = new System.Windows.Forms.RadioButton();
            this.lblHostLobbyMessage = new System.Windows.Forms.Label();
            this.tabJoin = new System.Windows.Forms.TabPage();
            this.btnModDL = new System.Windows.Forms.Button();
            this.cbxLocked = new System.Windows.Forms.CheckBox();
            this.lblJoinAttemptText = new System.Windows.Forms.Label();
            this.lblGameListRefresh = new System.Windows.Forms.Label();
            this.btnCancelJoining = new System.Windows.Forms.Button();
            this.btnGameListRefresh = new System.Windows.Forms.Button();
            this.grdGamesList = new System.Windows.Forms.DataGridView();
            this.colChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGameName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSkillsHeroes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlayers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomMod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabLobby = new System.Windows.Forms.TabPage();
            this.gbxGameInfo = new System.Windows.Forms.GroupBox();
            this.lblAdditional = new System.Windows.Forms.Label();
            this.labelMaxPlayers = new System.Windows.Forms.Label();
            this.labelMap = new System.Windows.Forms.Label();
            this.labelHeroSkills = new System.Windows.Forms.Label();
            this.labelHost = new System.Windows.Forms.Label();
            this.chkLobbyPlayerReady = new System.Windows.Forms.CheckBox();
            this.btnLobbyKick = new System.Windows.Forms.Button();
            this.btnLobbyRandomiseTeams = new System.Windows.Forms.Button();
            this.lblLobbyPlayerReadyCount = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnLobbyLeave = new System.Windows.Forms.Button();
            this.lblLobbyVersus = new System.Windows.Forms.Label();
            this.gbxLobbySpectators = new System.Windows.Forms.GroupBox();
            this.lbxLobbySpectators = new System.Windows.Forms.ListBox();
            this.gbxLobbyDire = new System.Windows.Forms.GroupBox();
            this.btnJoinDire = new System.Windows.Forms.Button();
            this.lbxLobbyDirePlayers = new System.Windows.Forms.ListBox();
            this.gbxLobbyRadiant = new System.Windows.Forms.GroupBox();
            this.btnJoinRadiant = new System.Windows.Forms.Button();
            this.lbxLobbyRadiantPlayers = new System.Windows.Forms.ListBox();
            this.lblLobbyName = new System.Windows.Forms.Label();
            this.tabDraftHeroPick = new System.Windows.Forms.TabPage();
            this.lblHeroDraftPickingOrder = new System.Windows.Forms.Label();
            this.lblHeroDraftPicks = new System.Windows.Forms.Label();
            this.pbxDraftRadiantPicks = new System.Windows.Forms.PictureBox();
            this.pbxDraftDirePicks = new System.Windows.Forms.PictureBox();
            this.floDraftDireHeroes = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDraftPlayerTurnText = new System.Windows.Forms.Label();
            this.floDraftRadiantHeroes = new System.Windows.Forms.FlowLayoutPanel();
            this.pbxDraftHeroPickDire = new System.Windows.Forms.PictureBox();
            this.pbxDraftHeroPickRadiant = new System.Windows.Forms.PictureBox();
            this.pbxDraftHeroPickIntelligence = new System.Windows.Forms.PictureBox();
            this.pbxDraftHeroPickAgility = new System.Windows.Forms.PictureBox();
            this.pbxDraftHeroPickStrength = new System.Windows.Forms.PictureBox();
            this.gbxDraftHeroPickTimeRemaining = new System.Windows.Forms.GroupBox();
            this.lblDraftHeroPickTimeRemaining = new System.Windows.Forms.Label();
            this.floDraftPlayerOrder = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHeroSelectionDraft = new System.Windows.Forms.Label();
            this.floDraftHeroRadiantIntelligence = new System.Windows.Forms.FlowLayoutPanel();
            this.floDraftHeroDireIntelligence = new System.Windows.Forms.FlowLayoutPanel();
            this.floDraftHeroDireAgility = new System.Windows.Forms.FlowLayoutPanel();
            this.floDraftHeroRadiantAgility = new System.Windows.Forms.FlowLayoutPanel();
            this.floDraftHeroDireStrength = new System.Windows.Forms.FlowLayoutPanel();
            this.floDraftHeroRadiantStrength = new System.Windows.Forms.FlowLayoutPanel();
            this.tabSkillDraft = new System.Windows.Forms.TabPage();
            this.btnLeaveSkills = new System.Windows.Forms.Button();
            this.pbxSkillDraftYourHero = new System.Windows.Forms.PictureBox();
            this.lblSkillDraftSelection = new System.Windows.Forms.Label();
            this.floSkillDirePicks = new System.Windows.Forms.FlowLayoutPanel();
            this.floSkillRadiantPicks = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSkillDraftPickingOrder = new System.Windows.Forms.Label();
            this.lblSkillDraftPicks = new System.Windows.Forms.Label();
            this.pbxSkillRadiantPicks = new System.Windows.Forms.PictureBox();
            this.pbxSkillDirePicks = new System.Windows.Forms.PictureBox();
            this.lblSkillDraftPlayerTurn = new System.Windows.Forms.Label();
            this.gbxSkillDraftTimeRemaining = new System.Windows.Forms.GroupBox();
            this.lblSkillDraftTimeRemaining = new System.Windows.Forms.Label();
            this.floSKillDraftPickingOrder = new System.Windows.Forms.FlowLayoutPanel();
            this.floSkillDraftUltimateSkills = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSkillDraftUltimates = new System.Windows.Forms.Label();
            this.lblSkillDraftNormal = new System.Windows.Forms.Label();
            this.floSkillDraftNormalSkills = new System.Windows.Forms.FlowLayoutPanel();
            this.tabDraftSummary = new System.Windows.Forms.TabPage();
            this.gbxConfiguringMod = new System.Windows.Forms.GroupBox();
            this.btnManualConnect = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.lblConfigProgressMessage = new System.Windows.Forms.Label();
            this.pgbConfigProgress = new System.Windows.Forms.ProgressBar();
            this.floDireTeamSummary = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDraftDireTeamPicks = new System.Windows.Forms.Label();
            this.floRadiantTeamSummary = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDraftRadiantTeamPicks = new System.Windows.Forms.Label();
            this.floPersonalSummary = new System.Windows.Forms.FlowLayoutPanel();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.lblSettings = new System.Windows.Forms.Label();
            this.btnSettingsSaveReturn = new System.Windows.Forms.Button();
            this.gbxSettingsServer = new System.Windows.Forms.GroupBox();
            this.gbxServerOther = new System.Windows.Forms.GroupBox();
            this.chkDedicated = new System.Windows.Forms.CheckBox();
            this.cbxVersionFixDisable = new System.Windows.Forms.CheckBox();
            this.chkConDebug = new System.Windows.Forms.CheckBox();
            this.gbxBanList = new System.Windows.Forms.GroupBox();
            this.btnBanURLLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBanLoad = new System.Windows.Forms.Button();
            this.btnBanSave = new System.Windows.Forms.Button();
            this.tbxBans = new System.Windows.Forms.TextBox();
            this.gbxSettingServerPort = new System.Windows.Forms.GroupBox();
            this.tbxSettingServerPort = new System.Windows.Forms.TextBox();
            this.lblSettingServerPort = new System.Windows.Forms.Label();
            this.btnSettingServerPort = new System.Windows.Forms.Button();
            this.lblSettingsServerStatus = new System.Windows.Forms.Label();
            this.gbxSettingsServerLocation = new System.Windows.Forms.GroupBox();
            this.btnSettingServerInstallationWizard = new System.Windows.Forms.Button();
            this.lblSettingServerOptions = new System.Windows.Forms.Label();
            this.btnSettingsServerLocationChange = new System.Windows.Forms.Button();
            this.lblDota2ServerLocation = new System.Windows.Forms.Label();
            this.gbxSettingsClient = new System.Windows.Forms.GroupBox();
            this.gbxClientOther = new System.Windows.Forms.GroupBox();
            this.chkFlashName = new System.Windows.Forms.CheckBox();
            this.chkBeepName = new System.Windows.Forms.CheckBox();
            this.chkFlashNew = new System.Windows.Forms.CheckBox();
            this.chkBeepNew = new System.Windows.Forms.CheckBox();
            this.gbxSettingsClientSteamDir = new System.Windows.Forms.GroupBox();
            this.btnSettingSteamPath = new System.Windows.Forms.Button();
            this.lblSettingSteamPath = new System.Windows.Forms.Label();
            this.gbxSettingsConsoleKey = new System.Windows.Forms.GroupBox();
            this.btnSettingConsoleKeybindManual = new System.Windows.Forms.Button();
            this.btnSettingDota2ConsoleKeybindDetect = new System.Windows.Forms.Button();
            this.lblSettingDota2ConsoleKeybind = new System.Windows.Forms.Label();
            this.lblSettingsClientStatus = new System.Windows.Forms.Label();
            this.gbxSettingsClientLocation = new System.Windows.Forms.GroupBox();
            this.btnSettingsClientLocationChange = new System.Windows.Forms.Button();
            this.lblDota2ClientLocation = new System.Windows.Forms.Label();
            this.tabServerWizard = new System.Windows.Forms.TabPage();
            this.ServerToSettingsbutton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.detectButton = new System.Windows.Forms.Button();
            this.textPort = new System.Windows.Forms.TextBox();
            this.textIP = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.logText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabMod = new System.Windows.Forms.TabPage();
            this.btnBackMods = new System.Windows.Forms.Button();
            this.btnLoadMods = new System.Windows.Forms.Button();
            this.grdMods = new System.Windows.Forms.DataGridView();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.ttpWTF = new System.Windows.Forms.ToolTip(this.components);
            this.ttpX2 = new System.Windows.Forms.ToolTip(this.components);
            this.dataMods = new System.Data.DataSet();
            this.timerDediCount = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBanner)).BeginInit();
            this.gbxChat.SuspendLayout();
            this.tabUISections.SuspendLayout();
            this.tabPreConnect.SuspendLayout();
            this.gbxConnect.SuspendLayout();
            this.tabConnected.SuspendLayout();
            this.tabHostLobby.SuspendLayout();
            this.gbxCustomMod.SuspendLayout();
            this.gbxAdditional.SuspendLayout();
            this.gmxGameMode.SuspendLayout();
            this.gbxMap.SuspendLayout();
            this.gbxLobbySize.SuspendLayout();
            this.gbxGamePassword.SuspendLayout();
            this.gbxSkillMode.SuspendLayout();
            this.gbxGameName.SuspendLayout();
            this.gbxHeroMode.SuspendLayout();
            this.tabJoin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGamesList)).BeginInit();
            this.tabLobby.SuspendLayout();
            this.gbxGameInfo.SuspendLayout();
            this.gbxLobbySpectators.SuspendLayout();
            this.gbxLobbyDire.SuspendLayout();
            this.gbxLobbyRadiant.SuspendLayout();
            this.tabDraftHeroPick.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftRadiantPicks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftDirePicks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickDire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickRadiant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickIntelligence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickAgility)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickStrength)).BeginInit();
            this.gbxDraftHeroPickTimeRemaining.SuspendLayout();
            this.tabSkillDraft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSkillDraftYourHero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSkillRadiantPicks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSkillDirePicks)).BeginInit();
            this.gbxSkillDraftTimeRemaining.SuspendLayout();
            this.tabDraftSummary.SuspendLayout();
            this.gbxConfiguringMod.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.gbxSettingsServer.SuspendLayout();
            this.gbxServerOther.SuspendLayout();
            this.gbxBanList.SuspendLayout();
            this.gbxSettingServerPort.SuspendLayout();
            this.gbxSettingsServerLocation.SuspendLayout();
            this.gbxSettingsClient.SuspendLayout();
            this.gbxClientOther.SuspendLayout();
            this.gbxSettingsClientSteamDir.SuspendLayout();
            this.gbxSettingsConsoleKey.SuspendLayout();
            this.gbxSettingsClientLocation.SuspendLayout();
            this.tabServerWizard.SuspendLayout();
            this.tabMod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMods)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataMods)).BeginInit();
            this.SuspendLayout();
            // 
            // webAdvertisement
            // 
            this.webAdvertisement.AllowNavigation = false;
            this.webAdvertisement.AllowWebBrowserDrop = false;
            this.webAdvertisement.Dock = System.Windows.Forms.DockStyle.Top;
            this.webAdvertisement.IsWebBrowserContextMenuEnabled = false;
            this.webAdvertisement.Location = new System.Drawing.Point(0, 0);
            this.webAdvertisement.MinimumSize = new System.Drawing.Size(20, 20);
            this.webAdvertisement.Name = "webAdvertisement";
            this.webAdvertisement.ScriptErrorsSuppressed = true;
            this.webAdvertisement.ScrollBarsEnabled = false;
            this.webAdvertisement.Size = new System.Drawing.Size(1165, 119);
            this.webAdvertisement.TabIndex = 0;
            this.webAdvertisement.Url = new System.Uri("", System.UriKind.Relative);
            this.webAdvertisement.WebBrowserShortcutsEnabled = false;
            // 
            // ircListener
            // 
            this.ircListener.Tick += new System.EventHandler(this.ircListener_Tick);
            // 
            // lblMessageLeft
            // 
            this.lblMessageLeft.AutoSize = true;
            this.lblMessageLeft.Location = new System.Drawing.Point(12, 131);
            this.lblMessageLeft.Name = "lblMessageLeft";
            this.lblMessageLeft.Size = new System.Drawing.Size(0, 13);
            this.lblMessageLeft.TabIndex = 2;
            // 
            // ttpHeroDraftTooltips
            // 
            this.ttpHeroDraftTooltips.IsBalloon = true;
            this.ttpHeroDraftTooltips.ToolTipTitle = "Hero";
            // 
            // ttpSkillDraftTooltips
            // 
            this.ttpSkillDraftTooltips.IsBalloon = true;
            this.ttpSkillDraftTooltips.ToolTipTitle = "Skill";
            // 
            // bgwGenerateNpcHeroesAutoexec
            // 
            this.bgwGenerateNpcHeroesAutoexec.WorkerReportsProgress = true;
            this.bgwGenerateNpcHeroesAutoexec.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwGenerateNpcHeroesAutoexec_DoWork);
            this.bgwGenerateNpcHeroesAutoexec.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwGenerateNpcHeroesAutoexec_ProgressChanged);
            this.bgwGenerateNpcHeroesAutoexec.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwGenerateNpcHeroesAutoexec_RunWorkerCompleted);
            // 
            // ofdFindDotaExe
            // 
            this.ofdFindDotaExe.FileName = "dota.exe";
            this.ofdFindDotaExe.Filter = "Dota 2 | dota.exe";
            this.ofdFindDotaExe.RestoreDirectory = true;
            this.ofdFindDotaExe.Title = "Find Dota 2";
            // 
            // lblVersionSubtitle
            // 
            this.lblVersionSubtitle.BackColor = System.Drawing.Color.White;
            this.lblVersionSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionSubtitle.ForeColor = System.Drawing.Color.Red;
            this.lblVersionSubtitle.Location = new System.Drawing.Point(4, 61);
            this.lblVersionSubtitle.Name = "lblVersionSubtitle";
            this.lblVersionSubtitle.Size = new System.Drawing.Size(1154, 55);
            this.lblVersionSubtitle.TabIndex = 4;
            this.lblVersionSubtitle.Text = "Ensure you are using the latest build before connecting to the server!\r\nAutomatic" +
    " updates will be enabled at some point in the near future =)";
            this.lblVersionSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVersionSubtitle.Visible = false;
            // 
            // ofdFindSrcdsExe
            // 
            this.ofdFindSrcdsExe.FileName = "srcds.exe";
            this.ofdFindSrcdsExe.Filter = "Source Dedicated Server | srcds.exe";
            this.ofdFindSrcdsExe.RestoreDirectory = true;
            this.ofdFindSrcdsExe.Title = "Find Dota 2 Server";
            // 
            // pbxBanner
            // 
            this.pbxBanner.BackColor = System.Drawing.Color.White;
            this.pbxBanner.Image = ((System.Drawing.Image)(resources.GetObject("pbxBanner.Image")));
            this.pbxBanner.Location = new System.Drawing.Point(0, 0);
            this.pbxBanner.Name = "pbxBanner";
            this.pbxBanner.Size = new System.Drawing.Size(1165, 119);
            this.pbxBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxBanner.TabIndex = 5;
            this.pbxBanner.TabStop = false;
            // 
            // ofdFindSteam
            // 
            this.ofdFindSteam.FileName = "steam.exe";
            this.ofdFindSteam.Filter = "Steam | steam.exe";
            this.ofdFindSteam.RestoreDirectory = true;
            this.ofdFindSteam.Title = "Find Steam Installation";
            // 
            // icoNotifyDraftTurn
            // 
            this.icoNotifyDraftTurn.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.icoNotifyDraftTurn.BalloonTipText = "Its your turn to pick!";
            this.icoNotifyDraftTurn.BalloonTipTitle = "Drafting";
            this.icoNotifyDraftTurn.Icon = ((System.Drawing.Icon)(resources.GetObject("icoNotifyDraftTurn.Icon")));
            this.icoNotifyDraftTurn.Text = "Dota 2 Custom Realms";
            this.icoNotifyDraftTurn.Visible = true;
            this.icoNotifyDraftTurn.BalloonTipClicked += new System.EventHandler(this.icoNotifyDraftTurn_BalloonTipClicked);
            this.icoNotifyDraftTurn.BalloonTipClosed += new System.EventHandler(this.icoNotifyDraftTurn_BalloonTipClosed);
            this.icoNotifyDraftTurn.Click += new System.EventHandler(this.icoNotifyDraftTurn_Click);
            // 
            // lblPlayersOnline
            // 
            this.lblPlayersOnline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayersOnline.BackColor = System.Drawing.SystemColors.Window;
            this.lblPlayersOnline.Location = new System.Drawing.Point(1, 9);
            this.lblPlayersOnline.Name = "lblPlayersOnline";
            this.lblPlayersOnline.Size = new System.Drawing.Size(110, 16);
            this.lblPlayersOnline.TabIndex = 7;
            this.lblPlayersOnline.Text = "Players Online";
            this.lblPlayersOnline.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerPlayers
            // 
            this.timerPlayers.Tick += new System.EventHandler(this.timerPlayers_Tick);
            // 
            // lblPlayersInGame
            // 
            this.lblPlayersInGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayersInGame.BackColor = System.Drawing.SystemColors.Window;
            this.lblPlayersInGame.Location = new System.Drawing.Point(1, 25);
            this.lblPlayersInGame.Name = "lblPlayersInGame";
            this.lblPlayersInGame.Size = new System.Drawing.Size(110, 20);
            this.lblPlayersInGame.TabIndex = 8;
            this.lblPlayersInGame.Text = "Players Ingame";
            this.lblPlayersInGame.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbxChat
            // 
            this.gbxChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxChat.Controls.Add(this.gbxGameSize);
            this.gbxChat.Controls.Add(this.btnSendMessage);
            this.gbxChat.Controls.Add(this.tbxChatMessage);
            this.gbxChat.Location = new System.Drawing.Point(872, 0);
            this.gbxChat.Name = "gbxChat";
            this.gbxChat.Size = new System.Drawing.Size(293, 522);
            this.gbxChat.TabIndex = 5;
            this.gbxChat.TabStop = false;
            this.gbxChat.Text = "Chat";
            this.gbxChat.Visible = false;
            // 
            // gbxGameSize
            // 
            this.gbxGameSize.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxGameSize.Location = new System.Drawing.Point(9, 20);
            this.gbxGameSize.Name = "gbxGameSize";
            this.gbxGameSize.SelectedIndex = 0;
            this.gbxGameSize.Size = new System.Drawing.Size(277, 462);
            this.gbxGameSize.TabIndex = 2;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMessage.Location = new System.Drawing.Point(265, 488);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(22, 22);
            this.btnSendMessage.TabIndex = 1;
            this.btnSendMessage.Text = ">";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // tbxChatMessage
            // 
            this.tbxChatMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxChatMessage.Location = new System.Drawing.Point(6, 490);
            this.tbxChatMessage.Multiline = true;
            this.tbxChatMessage.Name = "tbxChatMessage";
            this.tbxChatMessage.Size = new System.Drawing.Size(264, 20);
            this.tbxChatMessage.TabIndex = 0;
            this.tbxChatMessage.TextChanged += new System.EventHandler(this.tbxChatMessage_TextChanged);
            this.tbxChatMessage.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tbxChatMessage_PreviewKeyDown);
            // 
            // tabUISections
            // 
            this.tabUISections.Controls.Add(this.tabPreConnect);
            this.tabUISections.Controls.Add(this.tabConnected);
            this.tabUISections.Controls.Add(this.tabHostLobby);
            this.tabUISections.Controls.Add(this.tabJoin);
            this.tabUISections.Controls.Add(this.tabLobby);
            this.tabUISections.Controls.Add(this.tabDraftHeroPick);
            this.tabUISections.Controls.Add(this.tabSkillDraft);
            this.tabUISections.Controls.Add(this.tabDraftSummary);
            this.tabUISections.Controls.Add(this.tabSettings);
            this.tabUISections.Controls.Add(this.tabServerWizard);
            this.tabUISections.Controls.Add(this.tabMod);
            this.tabUISections.ItemSize = new System.Drawing.Size(0, 1);
            this.tabUISections.Location = new System.Drawing.Point(0, 0);
            this.tabUISections.Name = "tabUISections";
            this.tabUISections.SelectedIndex = 0;
            this.tabUISections.Size = new System.Drawing.Size(872, 522);
            this.tabUISections.TabIndex = 6;
            this.tabUISections.TabStop = false;
            // 
            // tabPreConnect
            // 
            this.tabPreConnect.BackColor = System.Drawing.SystemColors.Control;
            this.tabPreConnect.Controls.Add(this.gbxConnect);
            this.tabPreConnect.Location = new System.Drawing.Point(4, 5);
            this.tabPreConnect.Name = "tabPreConnect";
            this.tabPreConnect.Padding = new System.Windows.Forms.Padding(3);
            this.tabPreConnect.Size = new System.Drawing.Size(864, 513);
            this.tabPreConnect.TabIndex = 0;
            this.tabPreConnect.Text = "Connect";
            // 
            // gbxConnect
            // 
            this.gbxConnect.Controls.Add(this.btnConnectIRC);
            this.gbxConnect.Controls.Add(this.tbxChooseNick);
            this.gbxConnect.Controls.Add(this.lblChooseNick);
            this.gbxConnect.Location = new System.Drawing.Point(312, 196);
            this.gbxConnect.Name = "gbxConnect";
            this.gbxConnect.Size = new System.Drawing.Size(240, 121);
            this.gbxConnect.TabIndex = 0;
            this.gbxConnect.TabStop = false;
            this.gbxConnect.Text = "Connect";
            // 
            // btnConnectIRC
            // 
            this.btnConnectIRC.Location = new System.Drawing.Point(21, 73);
            this.btnConnectIRC.Name = "btnConnectIRC";
            this.btnConnectIRC.Size = new System.Drawing.Size(205, 36);
            this.btnConnectIRC.TabIndex = 2;
            this.btnConnectIRC.Text = "Connect";
            this.btnConnectIRC.UseVisualStyleBackColor = true;
            this.btnConnectIRC.Click += new System.EventHandler(this.btnConnectIRC_Click);
            // 
            // tbxChooseNick
            // 
            this.tbxChooseNick.Location = new System.Drawing.Point(20, 47);
            this.tbxChooseNick.Name = "tbxChooseNick";
            this.tbxChooseNick.Size = new System.Drawing.Size(207, 20);
            this.tbxChooseNick.TabIndex = 1;
            // 
            // lblChooseNick
            // 
            this.lblChooseNick.AutoSize = true;
            this.lblChooseNick.Location = new System.Drawing.Point(19, 27);
            this.lblChooseNick.Name = "lblChooseNick";
            this.lblChooseNick.Size = new System.Drawing.Size(58, 13);
            this.lblChooseNick.TabIndex = 0;
            this.lblChooseNick.Text = "Nickname:";
            // 
            // tabConnected
            // 
            this.tabConnected.BackColor = System.Drawing.SystemColors.Control;
            this.tabConnected.Controls.Add(this.btnSettings);
            this.tabConnected.Controls.Add(this.btnFindLobby);
            this.tabConnected.Controls.Add(this.btnHostLobby);
            this.tabConnected.Location = new System.Drawing.Point(4, 5);
            this.tabConnected.Name = "tabConnected";
            this.tabConnected.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnected.Size = new System.Drawing.Size(864, 513);
            this.tabConnected.TabIndex = 1;
            this.tabConnected.Text = "Connected";
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(335, 298);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(191, 72);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnFindLobby
            // 
            this.btnFindLobby.Location = new System.Drawing.Point(335, 220);
            this.btnFindLobby.Name = "btnFindLobby";
            this.btnFindLobby.Size = new System.Drawing.Size(191, 72);
            this.btnFindLobby.TabIndex = 2;
            this.btnFindLobby.Text = "Find Lobby";
            this.btnFindLobby.UseVisualStyleBackColor = true;
            this.btnFindLobby.Click += new System.EventHandler(this.btnFindLobby_Click);
            // 
            // btnHostLobby
            // 
            this.btnHostLobby.Location = new System.Drawing.Point(335, 142);
            this.btnHostLobby.Name = "btnHostLobby";
            this.btnHostLobby.Size = new System.Drawing.Size(191, 72);
            this.btnHostLobby.TabIndex = 1;
            this.btnHostLobby.Text = "Host Lobby";
            this.btnHostLobby.UseVisualStyleBackColor = true;
            this.btnHostLobby.Click += new System.EventHandler(this.btnHostLobby_Click);
            // 
            // tabHostLobby
            // 
            this.tabHostLobby.BackColor = System.Drawing.SystemColors.Control;
            this.tabHostLobby.Controls.Add(this.gbxCustomMod);
            this.tabHostLobby.Controls.Add(this.gbxAdditional);
            this.tabHostLobby.Controls.Add(this.gmxGameMode);
            this.tabHostLobby.Controls.Add(this.gbxMap);
            this.tabHostLobby.Controls.Add(this.gbxLobbySize);
            this.tabHostLobby.Controls.Add(this.gbxGamePassword);
            this.tabHostLobby.Controls.Add(this.btnCancelHosting);
            this.tabHostLobby.Controls.Add(this.btnHostGame);
            this.tabHostLobby.Controls.Add(this.gbxSkillMode);
            this.tabHostLobby.Controls.Add(this.gbxGameName);
            this.tabHostLobby.Controls.Add(this.gbxHeroMode);
            this.tabHostLobby.Controls.Add(this.lblHostLobbyMessage);
            this.tabHostLobby.Location = new System.Drawing.Point(4, 5);
            this.tabHostLobby.Name = "tabHostLobby";
            this.tabHostLobby.Padding = new System.Windows.Forms.Padding(3);
            this.tabHostLobby.Size = new System.Drawing.Size(864, 513);
            this.tabHostLobby.TabIndex = 2;
            this.tabHostLobby.Text = "Host";
            // 
            // gbxCustomMod
            // 
            this.gbxCustomMod.Controls.Add(this.tbxCustomMod);
            this.gbxCustomMod.Controls.Add(this.lblCustomWarn);
            this.gbxCustomMod.Controls.Add(this.chkCustomEnable);
            this.gbxCustomMod.Location = new System.Drawing.Point(11, 422);
            this.gbxCustomMod.Name = "gbxCustomMod";
            this.gbxCustomMod.Size = new System.Drawing.Size(218, 80);
            this.gbxCustomMod.TabIndex = 80;
            this.gbxCustomMod.TabStop = false;
            this.gbxCustomMod.Text = "Custom Mod";
            // 
            // tbxCustomMod
            // 
            this.tbxCustomMod.AllowDrop = true;
            this.tbxCustomMod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbxCustomMod.FormattingEnabled = true;
            this.tbxCustomMod.Location = new System.Drawing.Point(72, 18);
            this.tbxCustomMod.Name = "tbxCustomMod";
            this.tbxCustomMod.Size = new System.Drawing.Size(121, 21);
            this.tbxCustomMod.TabIndex = 2;
            // 
            // lblCustomWarn
            // 
            this.lblCustomWarn.AutoSize = true;
            this.lblCustomWarn.Location = new System.Drawing.Point(6, 43);
            this.lblCustomWarn.Name = "lblCustomWarn";
            this.lblCustomWarn.Size = new System.Drawing.Size(208, 26);
            this.lblCustomWarn.TabIndex = 1;
            this.lblCustomWarn.Text = "WARNING: Do not enable without reading\r\n <d2cr>\\data\\custom\\readme.txt";
            // 
            // chkCustomEnable
            // 
            this.chkCustomEnable.AutoSize = true;
            this.chkCustomEnable.Location = new System.Drawing.Point(7, 20);
            this.chkCustomEnable.Name = "chkCustomEnable";
            this.chkCustomEnable.Size = new System.Drawing.Size(59, 17);
            this.chkCustomEnable.TabIndex = 0;
            this.chkCustomEnable.Text = "Enable";
            this.chkCustomEnable.UseVisualStyleBackColor = true;
            this.chkCustomEnable.CheckedChanged += new System.EventHandler(this.chkCustomEnable_CheckedChanged);
            // 
            // gbxAdditional
            // 
            this.gbxAdditional.Controls.Add(this.chkBalanced);
            this.gbxAdditional.Controls.Add(this.cbxX2);
            this.gbxAdditional.Controls.Add(this.cbxAllTalk);
            this.gbxAdditional.Controls.Add(this.cbxWTF);
            this.gbxAdditional.Location = new System.Drawing.Point(11, 351);
            this.gbxAdditional.Name = "gbxAdditional";
            this.gbxAdditional.Size = new System.Drawing.Size(169, 65);
            this.gbxAdditional.TabIndex = 10;
            this.gbxAdditional.TabStop = false;
            this.gbxAdditional.Text = "Additional Modes";
            // 
            // chkBalanced
            // 
            this.chkBalanced.AutoSize = true;
            this.chkBalanced.Location = new System.Drawing.Point(94, 43);
            this.chkBalanced.Name = "chkBalanced";
            this.chkBalanced.Size = new System.Drawing.Size(71, 17);
            this.chkBalanced.TabIndex = 3;
            this.chkBalanced.Text = "Balanced";
            this.chkBalanced.UseVisualStyleBackColor = true;
            this.chkBalanced.CheckedChanged += new System.EventHandler(this.chkBalanced_CheckedChanged);
            // 
            // cbxX2
            // 
            this.cbxX2.AutoSize = true;
            this.cbxX2.Location = new System.Drawing.Point(94, 20);
            this.cbxX2.Name = "cbxX2";
            this.cbxX2.Size = new System.Drawing.Size(67, 17);
            this.cbxX2.TabIndex = 2;
            this.cbxX2.Text = "x2 Mode";
            this.cbxX2.UseVisualStyleBackColor = true;
            // 
            // cbxAllTalk
            // 
            this.cbxAllTalk.AutoSize = true;
            this.cbxAllTalk.Location = new System.Drawing.Point(7, 44);
            this.cbxAllTalk.Name = "cbxAllTalk";
            this.cbxAllTalk.Size = new System.Drawing.Size(61, 17);
            this.cbxAllTalk.TabIndex = 1;
            this.cbxAllTalk.Text = "All Talk";
            this.cbxAllTalk.UseVisualStyleBackColor = true;
            // 
            // cbxWTF
            // 
            this.cbxWTF.AutoSize = true;
            this.cbxWTF.Location = new System.Drawing.Point(7, 20);
            this.cbxWTF.Name = "cbxWTF";
            this.cbxWTF.Size = new System.Drawing.Size(80, 17);
            this.cbxWTF.TabIndex = 0;
            this.cbxWTF.Text = "WTF Mode";
            this.cbxWTF.UseVisualStyleBackColor = true;
            // 
            // gmxGameMode
            // 
            this.gmxGameMode.Controls.Add(this.cbxGameMode);
            this.gmxGameMode.Location = new System.Drawing.Point(11, 180);
            this.gmxGameMode.Name = "gmxGameMode";
            this.gmxGameMode.Size = new System.Drawing.Size(362, 51);
            this.gmxGameMode.TabIndex = 9;
            this.gmxGameMode.TabStop = false;
            this.gmxGameMode.Text = "Game Mode";
            // 
            // cbxGameMode
            // 
            this.cbxGameMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGameMode.FormattingEnabled = true;
            this.cbxGameMode.Items.AddRange(new object[] {
            "OMG",
            "OMG Greevilings",
            "OMG Diretide",
            "OMG Mid Only",
            "All Pick",
            "Captain\'s Mode",
            "Random Draft",
            "Single Draft",
            "All Random",
            "Diretide",
            "Reverse Captain\'s Mode",
            "Greevilings",
            "Mid Only",
            "New Player Pool"});
            this.cbxGameMode.Location = new System.Drawing.Point(6, 19);
            this.cbxGameMode.Name = "cbxGameMode";
            this.cbxGameMode.Size = new System.Drawing.Size(349, 21);
            this.cbxGameMode.TabIndex = 2;
            this.cbxGameMode.SelectedIndexChanged += new System.EventHandler(this.cbxGameMode_SelectedIndexChanged);
            // 
            // gbxMap
            // 
            this.gbxMap.Controls.Add(this.cbxMap);
            this.gbxMap.Location = new System.Drawing.Point(11, 294);
            this.gbxMap.Name = "gbxMap";
            this.gbxMap.Size = new System.Drawing.Size(362, 51);
            this.gbxMap.TabIndex = 9;
            this.gbxMap.TabStop = false;
            this.gbxMap.Text = "Game Map";
            // 
            // cbxMap
            // 
            this.cbxMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMap.FormattingEnabled = true;
            this.cbxMap.Items.AddRange(new object[] {
            "Dota",
            "Autumn",
            "Winter",
            "Diretide"});
            this.cbxMap.Location = new System.Drawing.Point(7, 20);
            this.cbxMap.Name = "cbxMap";
            this.cbxMap.Size = new System.Drawing.Size(349, 21);
            this.cbxMap.TabIndex = 4;
            // 
            // gbxLobbySize
            // 
            this.gbxLobbySize.Controls.Add(this.cbxGameSize);
            this.gbxLobbySize.Location = new System.Drawing.Point(11, 237);
            this.gbxLobbySize.Name = "gbxLobbySize";
            this.gbxLobbySize.Size = new System.Drawing.Size(362, 51);
            this.gbxLobbySize.TabIndex = 8;
            this.gbxLobbySize.TabStop = false;
            this.gbxLobbySize.Text = "Game Lobby Size";
            // 
            // cbxGameSize
            // 
            this.cbxGameSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGameSize.FormattingEnabled = true;
            this.cbxGameSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11"});
            this.cbxGameSize.Location = new System.Drawing.Point(7, 19);
            this.cbxGameSize.Name = "cbxGameSize";
            this.cbxGameSize.Size = new System.Drawing.Size(349, 21);
            this.cbxGameSize.TabIndex = 3;
            // 
            // gbxGamePassword
            // 
            this.gbxGamePassword.Controls.Add(this.tbxGamePassword);
            this.gbxGamePassword.Location = new System.Drawing.Point(11, 123);
            this.gbxGamePassword.Name = "gbxGamePassword";
            this.gbxGamePassword.Size = new System.Drawing.Size(362, 51);
            this.gbxGamePassword.TabIndex = 7;
            this.gbxGamePassword.TabStop = false;
            this.gbxGamePassword.Text = "Game Password";
            // 
            // tbxGamePassword
            // 
            this.tbxGamePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxGamePassword.Location = new System.Drawing.Point(6, 19);
            this.tbxGamePassword.Name = "tbxGamePassword";
            this.tbxGamePassword.Size = new System.Drawing.Size(350, 20);
            this.tbxGamePassword.TabIndex = 1;
            // 
            // btnCancelHosting
            // 
            this.btnCancelHosting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelHosting.Location = new System.Drawing.Point(531, 467);
            this.btnCancelHosting.Name = "btnCancelHosting";
            this.btnCancelHosting.Size = new System.Drawing.Size(119, 38);
            this.btnCancelHosting.TabIndex = 6;
            this.btnCancelHosting.Text = "Back";
            this.btnCancelHosting.UseVisualStyleBackColor = true;
            this.btnCancelHosting.Click += new System.EventHandler(this.btnCancelHosting_Click);
            // 
            // btnHostGame
            // 
            this.btnHostGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHostGame.Location = new System.Drawing.Point(656, 429);
            this.btnHostGame.Name = "btnHostGame";
            this.btnHostGame.Size = new System.Drawing.Size(202, 76);
            this.btnHostGame.TabIndex = 6;
            this.btnHostGame.Text = "Start Lobby";
            this.btnHostGame.UseVisualStyleBackColor = true;
            this.btnHostGame.Click += new System.EventHandler(this.btnHostGame_Click);
            // 
            // gbxSkillMode
            // 
            this.gbxSkillMode.Controls.Add(this.radSkillDraft);
            this.gbxSkillMode.Controls.Add(this.radSkillRandom);
            this.gbxSkillMode.Location = new System.Drawing.Point(398, 185);
            this.gbxSkillMode.Name = "gbxSkillMode";
            this.gbxSkillMode.Size = new System.Drawing.Size(362, 93);
            this.gbxSkillMode.TabIndex = 79;
            this.gbxSkillMode.TabStop = false;
            this.gbxSkillMode.Text = "Skill Mode";
            // 
            // radSkillDraft
            // 
            this.radSkillDraft.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSkillDraft.Checked = true;
            this.radSkillDraft.Location = new System.Drawing.Point(178, 19);
            this.radSkillDraft.Name = "radSkillDraft";
            this.radSkillDraft.Size = new System.Drawing.Size(110, 55);
            this.radSkillDraft.TabIndex = 3;
            this.radSkillDraft.TabStop = true;
            this.radSkillDraft.Text = "Draft Skills";
            this.radSkillDraft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radSkillDraft.UseVisualStyleBackColor = true;
            // 
            // radSkillRandom
            // 
            this.radSkillRandom.Appearance = System.Windows.Forms.Appearance.Button;
            this.radSkillRandom.Location = new System.Drawing.Point(62, 19);
            this.radSkillRandom.Name = "radSkillRandom";
            this.radSkillRandom.Size = new System.Drawing.Size(110, 55);
            this.radSkillRandom.TabIndex = 2;
            this.radSkillRandom.Text = "Random Skills";
            this.radSkillRandom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radSkillRandom.UseVisualStyleBackColor = true;
            // 
            // gbxGameName
            // 
            this.gbxGameName.Controls.Add(this.tbxGameName);
            this.gbxGameName.Location = new System.Drawing.Point(11, 66);
            this.gbxGameName.Name = "gbxGameName";
            this.gbxGameName.Size = new System.Drawing.Size(362, 51);
            this.gbxGameName.TabIndex = 4;
            this.gbxGameName.TabStop = false;
            this.gbxGameName.Text = "Game Name";
            // 
            // tbxGameName
            // 
            this.tbxGameName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxGameName.Location = new System.Drawing.Point(6, 19);
            this.tbxGameName.Name = "tbxGameName";
            this.tbxGameName.Size = new System.Drawing.Size(350, 20);
            this.tbxGameName.TabIndex = 0;
            this.tbxGameName.TextChanged += new System.EventHandler(this.tbxGameName_TextChanged);
            // 
            // gbxHeroMode
            // 
            this.gbxHeroMode.Controls.Add(this.radHeroDraft);
            this.gbxHeroMode.Controls.Add(this.radHeroRandom);
            this.gbxHeroMode.Controls.Add(this.radHeroPick);
            this.gbxHeroMode.Location = new System.Drawing.Point(398, 81);
            this.gbxHeroMode.Name = "gbxHeroMode";
            this.gbxHeroMode.Size = new System.Drawing.Size(362, 93);
            this.gbxHeroMode.TabIndex = 78;
            this.gbxHeroMode.TabStop = false;
            this.gbxHeroMode.Text = "Hero Mode";
            // 
            // radHeroDraft
            // 
            this.radHeroDraft.Appearance = System.Windows.Forms.Appearance.Button;
            this.radHeroDraft.Location = new System.Drawing.Point(238, 19);
            this.radHeroDraft.Name = "radHeroDraft";
            this.radHeroDraft.Size = new System.Drawing.Size(110, 55);
            this.radHeroDraft.TabIndex = 2;
            this.radHeroDraft.Text = "Draft Heroes";
            this.radHeroDraft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radHeroDraft.UseVisualStyleBackColor = true;
            // 
            // radHeroRandom
            // 
            this.radHeroRandom.Appearance = System.Windows.Forms.Appearance.Button;
            this.radHeroRandom.Location = new System.Drawing.Point(122, 19);
            this.radHeroRandom.Name = "radHeroRandom";
            this.radHeroRandom.Size = new System.Drawing.Size(110, 55);
            this.radHeroRandom.TabIndex = 1;
            this.radHeroRandom.Text = "Random Heroes";
            this.radHeroRandom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radHeroRandom.UseVisualStyleBackColor = true;
            // 
            // radHeroPick
            // 
            this.radHeroPick.Appearance = System.Windows.Forms.Appearance.Button;
            this.radHeroPick.Checked = true;
            this.radHeroPick.Location = new System.Drawing.Point(6, 19);
            this.radHeroPick.Name = "radHeroPick";
            this.radHeroPick.Size = new System.Drawing.Size(110, 55);
            this.radHeroPick.TabIndex = 0;
            this.radHeroPick.TabStop = true;
            this.radHeroPick.Text = "Pick Heroes";
            this.radHeroPick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radHeroPick.UseVisualStyleBackColor = true;
            // 
            // lblHostLobbyMessage
            // 
            this.lblHostLobbyMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHostLobbyMessage.AutoSize = true;
            this.lblHostLobbyMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHostLobbyMessage.Location = new System.Drawing.Point(695, 3);
            this.lblHostLobbyMessage.Name = "lblHostLobbyMessage";
            this.lblHostLobbyMessage.Size = new System.Drawing.Size(163, 25);
            this.lblHostLobbyMessage.TabIndex = 0;
            this.lblHostLobbyMessage.Text = "Hosting Lobby";
            // 
            // tabJoin
            // 
            this.tabJoin.BackColor = System.Drawing.SystemColors.Control;
            this.tabJoin.Controls.Add(this.btnModDL);
            this.tabJoin.Controls.Add(this.cbxLocked);
            this.tabJoin.Controls.Add(this.lblJoinAttemptText);
            this.tabJoin.Controls.Add(this.lblGameListRefresh);
            this.tabJoin.Controls.Add(this.btnCancelJoining);
            this.tabJoin.Controls.Add(this.btnGameListRefresh);
            this.tabJoin.Controls.Add(this.grdGamesList);
            this.tabJoin.Location = new System.Drawing.Point(4, 5);
            this.tabJoin.Name = "tabJoin";
            this.tabJoin.Padding = new System.Windows.Forms.Padding(3);
            this.tabJoin.Size = new System.Drawing.Size(864, 513);
            this.tabJoin.TabIndex = 4;
            this.tabJoin.Text = "Join";
            // 
            // btnModDL
            // 
            this.btnModDL.Location = new System.Drawing.Point(667, 478);
            this.btnModDL.Name = "btnModDL";
            this.btnModDL.Size = new System.Drawing.Size(92, 28);
            this.btnModDL.TabIndex = 6;
            this.btnModDL.Text = "Download Mods";
            this.btnModDL.UseVisualStyleBackColor = true;
            this.btnModDL.Click += new System.EventHandler(this.btnModDL_Click);
            // 
            // cbxLocked
            // 
            this.cbxLocked.AutoSize = true;
            this.cbxLocked.Location = new System.Drawing.Point(534, 483);
            this.cbxLocked.Name = "cbxLocked";
            this.cbxLocked.Size = new System.Drawing.Size(127, 17);
            this.cbxLocked.TabIndex = 5;
            this.cbxLocked.Text = "Hide Locked Lobbies";
            this.cbxLocked.UseVisualStyleBackColor = true;
            // 
            // lblJoinAttemptText
            // 
            this.lblJoinAttemptText.Location = new System.Drawing.Point(102, 485);
            this.lblJoinAttemptText.Name = "lblJoinAttemptText";
            this.lblJoinAttemptText.Size = new System.Drawing.Size(657, 20);
            this.lblJoinAttemptText.TabIndex = 4;
            this.lblJoinAttemptText.Text = "Please wait, attempting to join lobby X...";
            this.lblJoinAttemptText.Visible = false;
            // 
            // lblGameListRefresh
            // 
            this.lblGameListRefresh.AutoSize = true;
            this.lblGameListRefresh.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblGameListRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameListRefresh.Location = new System.Drawing.Point(296, 244);
            this.lblGameListRefresh.Name = "lblGameListRefresh";
            this.lblGameListRefresh.Size = new System.Drawing.Size(272, 25);
            this.lblGameListRefresh.TabIndex = 3;
            this.lblGameListRefresh.Text = "Refreshing Games List...";
            this.lblGameListRefresh.Visible = false;
            // 
            // btnCancelJoining
            // 
            this.btnCancelJoining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelJoining.Location = new System.Drawing.Point(3, 478);
            this.btnCancelJoining.Name = "btnCancelJoining";
            this.btnCancelJoining.Size = new System.Drawing.Size(93, 27);
            this.btnCancelJoining.TabIndex = 2;
            this.btnCancelJoining.Text = "Back";
            this.btnCancelJoining.UseVisualStyleBackColor = true;
            this.btnCancelJoining.Click += new System.EventHandler(this.btnCancelJoining_Click);
            // 
            // btnGameListRefresh
            // 
            this.btnGameListRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameListRefresh.Location = new System.Drawing.Point(765, 478);
            this.btnGameListRefresh.Name = "btnGameListRefresh";
            this.btnGameListRefresh.Size = new System.Drawing.Size(93, 27);
            this.btnGameListRefresh.TabIndex = 1;
            this.btnGameListRefresh.Text = "Refresh List";
            this.btnGameListRefresh.UseVisualStyleBackColor = true;
            this.btnGameListRefresh.Click += new System.EventHandler(this.btnGameListRefresh_Click);
            // 
            // grdGamesList
            // 
            this.grdGamesList.AllowUserToAddRows = false;
            this.grdGamesList.AllowUserToDeleteRows = false;
            this.grdGamesList.AllowUserToOrderColumns = true;
            this.grdGamesList.AllowUserToResizeRows = false;
            this.grdGamesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdGamesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdGamesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdGamesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChannel,
            this.colPass,
            this.colGameName,
            this.colHost,
            this.colMode,
            this.colSkillsHeroes,
            this.colPlayers,
            this.colMap,
            this.colCustomMod});
            this.grdGamesList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdGamesList.Location = new System.Drawing.Point(3, 31);
            this.grdGamesList.MultiSelect = false;
            this.grdGamesList.Name = "grdGamesList";
            this.grdGamesList.ReadOnly = true;
            this.grdGamesList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.grdGamesList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdGamesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdGamesList.ShowEditingIcon = false;
            this.grdGamesList.Size = new System.Drawing.Size(855, 441);
            this.grdGamesList.TabIndex = 0;
            this.grdGamesList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdGamesList_CellContentClick);
            // 
            // colChannel
            // 
            this.colChannel.HeaderText = "Channel";
            this.colChannel.Name = "colChannel";
            this.colChannel.ReadOnly = true;
            this.colChannel.Visible = false;
            // 
            // colPass
            // 
            this.colPass.FillWeight = 25F;
            this.colPass.HeaderText = "Lock";
            this.colPass.Name = "colPass";
            this.colPass.ReadOnly = true;
            // 
            // colGameName
            // 
            this.colGameName.FillWeight = 123F;
            this.colGameName.HeaderText = "Game Name";
            this.colGameName.Name = "colGameName";
            this.colGameName.ReadOnly = true;
            // 
            // colHost
            // 
            this.colHost.FillWeight = 85.44905F;
            this.colHost.HeaderText = "Host";
            this.colHost.Name = "colHost";
            this.colHost.ReadOnly = true;
            // 
            // colMode
            // 
            this.colMode.FillWeight = 65F;
            this.colMode.HeaderText = "Mode";
            this.colMode.Name = "colMode";
            this.colMode.ReadOnly = true;
            // 
            // colSkillsHeroes
            // 
            this.colSkillsHeroes.HeaderText = "Hero / Skill Modes";
            this.colSkillsHeroes.Name = "colSkillsHeroes";
            this.colSkillsHeroes.ReadOnly = true;
            // 
            // colPlayers
            // 
            this.colPlayers.FillWeight = 50F;
            this.colPlayers.HeaderText = "Players";
            this.colPlayers.Name = "colPlayers";
            this.colPlayers.ReadOnly = true;
            // 
            // colMap
            // 
            this.colMap.FillWeight = 75F;
            this.colMap.HeaderText = "Map";
            this.colMap.Name = "colMap";
            this.colMap.ReadOnly = true;
            this.colMap.Visible = false;
            // 
            // colCustomMod
            // 
            this.colCustomMod.HeaderText = "Custom Mod";
            this.colCustomMod.Name = "colCustomMod";
            this.colCustomMod.ReadOnly = true;
            // 
            // tabLobby
            // 
            this.tabLobby.BackColor = System.Drawing.SystemColors.Control;
            this.tabLobby.Controls.Add(this.gbxGameInfo);
            this.tabLobby.Controls.Add(this.chkLobbyPlayerReady);
            this.tabLobby.Controls.Add(this.btnLobbyKick);
            this.tabLobby.Controls.Add(this.btnLobbyRandomiseTeams);
            this.tabLobby.Controls.Add(this.lblLobbyPlayerReadyCount);
            this.tabLobby.Controls.Add(this.btnStart);
            this.tabLobby.Controls.Add(this.btnLobbyLeave);
            this.tabLobby.Controls.Add(this.lblLobbyVersus);
            this.tabLobby.Controls.Add(this.gbxLobbySpectators);
            this.tabLobby.Controls.Add(this.gbxLobbyDire);
            this.tabLobby.Controls.Add(this.gbxLobbyRadiant);
            this.tabLobby.Controls.Add(this.lblLobbyName);
            this.tabLobby.Location = new System.Drawing.Point(4, 5);
            this.tabLobby.Name = "tabLobby";
            this.tabLobby.Padding = new System.Windows.Forms.Padding(3);
            this.tabLobby.Size = new System.Drawing.Size(864, 513);
            this.tabLobby.TabIndex = 3;
            this.tabLobby.Text = "Lobby";
            // 
            // gbxGameInfo
            // 
            this.gbxGameInfo.Controls.Add(this.lblAdditional);
            this.gbxGameInfo.Controls.Add(this.labelMaxPlayers);
            this.gbxGameInfo.Controls.Add(this.labelMap);
            this.gbxGameInfo.Controls.Add(this.labelHeroSkills);
            this.gbxGameInfo.Controls.Add(this.labelHost);
            this.gbxGameInfo.Location = new System.Drawing.Point(237, 35);
            this.gbxGameInfo.Name = "gbxGameInfo";
            this.gbxGameInfo.Size = new System.Drawing.Size(390, 95);
            this.gbxGameInfo.TabIndex = 12;
            this.gbxGameInfo.TabStop = false;
            this.gbxGameInfo.Text = "Game Info";
            // 
            // lblAdditional
            // 
            this.lblAdditional.AutoSize = true;
            this.lblAdditional.Location = new System.Drawing.Point(7, 69);
            this.lblAdditional.Name = "lblAdditional";
            this.lblAdditional.Size = new System.Drawing.Size(91, 13);
            this.lblAdditional.TabIndex = 5;
            this.lblAdditional.Text = "Additional Modes:";
            // 
            // labelMaxPlayers
            // 
            this.labelMaxPlayers.AutoSize = true;
            this.labelMaxPlayers.Location = new System.Drawing.Point(234, 20);
            this.labelMaxPlayers.Name = "labelMaxPlayers";
            this.labelMaxPlayers.Size = new System.Drawing.Size(70, 13);
            this.labelMaxPlayers.TabIndex = 4;
            this.labelMaxPlayers.Text = "Max Players: ";
            // 
            // labelMap
            // 
            this.labelMap.AutoSize = true;
            this.labelMap.Location = new System.Drawing.Point(234, 45);
            this.labelMap.Name = "labelMap";
            this.labelMap.Size = new System.Drawing.Size(34, 13);
            this.labelMap.TabIndex = 2;
            this.labelMap.Text = "Map: ";
            // 
            // labelHeroSkills
            // 
            this.labelHeroSkills.AutoSize = true;
            this.labelHeroSkills.Location = new System.Drawing.Point(7, 45);
            this.labelHeroSkills.Name = "labelHeroSkills";
            this.labelHeroSkills.Size = new System.Drawing.Size(40, 13);
            this.labelHeroSkills.TabIndex = 1;
            this.labelHeroSkills.Text = "Mode: ";
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(7, 20);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(35, 13);
            this.labelHost.TabIndex = 0;
            this.labelHost.Text = "Host: ";
            // 
            // chkLobbyPlayerReady
            // 
            this.chkLobbyPlayerReady.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLobbyPlayerReady.AutoSize = true;
            this.chkLobbyPlayerReady.Location = new System.Drawing.Point(645, 406);
            this.chkLobbyPlayerReady.Name = "chkLobbyPlayerReady";
            this.chkLobbyPlayerReady.Size = new System.Drawing.Size(63, 17);
            this.chkLobbyPlayerReady.TabIndex = 11;
            this.chkLobbyPlayerReady.Text = "Ready?";
            this.chkLobbyPlayerReady.UseVisualStyleBackColor = true;
            this.chkLobbyPlayerReady.CheckedChanged += new System.EventHandler(this.chkLobbyPlayerReady_CheckedChanged);
            // 
            // btnLobbyKick
            // 
            this.btnLobbyKick.Location = new System.Drawing.Point(509, 181);
            this.btnLobbyKick.Name = "btnLobbyKick";
            this.btnLobbyKick.Size = new System.Drawing.Size(114, 30);
            this.btnLobbyKick.TabIndex = 10;
            this.btnLobbyKick.Text = "Kick Player";
            this.btnLobbyKick.UseVisualStyleBackColor = true;
            this.btnLobbyKick.Click += new System.EventHandler(this.btnLobbyKick_Click);
            // 
            // btnLobbyRandomiseTeams
            // 
            this.btnLobbyRandomiseTeams.Location = new System.Drawing.Point(237, 181);
            this.btnLobbyRandomiseTeams.Name = "btnLobbyRandomiseTeams";
            this.btnLobbyRandomiseTeams.Size = new System.Drawing.Size(114, 30);
            this.btnLobbyRandomiseTeams.TabIndex = 9;
            this.btnLobbyRandomiseTeams.Text = "Scramble Teams";
            this.btnLobbyRandomiseTeams.UseVisualStyleBackColor = true;
            this.btnLobbyRandomiseTeams.Click += new System.EventHandler(this.btnLobbyRandomiseTeams_Click);
            // 
            // lblLobbyPlayerReadyCount
            // 
            this.lblLobbyPlayerReadyCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLobbyPlayerReadyCount.AutoSize = true;
            this.lblLobbyPlayerReadyCount.Location = new System.Drawing.Point(642, 426);
            this.lblLobbyPlayerReadyCount.Name = "lblLobbyPlayerReadyCount";
            this.lblLobbyPlayerReadyCount.Size = new System.Drawing.Size(95, 13);
            this.lblLobbyPlayerReadyCount.TabIndex = 8;
            this.lblLobbyPlayerReadyCount.Text = "0/0 Players Ready";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(642, 442);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(216, 63);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnLobbyLeave
            // 
            this.btnLobbyLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLobbyLeave.Location = new System.Drawing.Point(8, 469);
            this.btnLobbyLeave.Name = "btnLobbyLeave";
            this.btnLobbyLeave.Size = new System.Drawing.Size(115, 36);
            this.btnLobbyLeave.TabIndex = 6;
            this.btnLobbyLeave.Text = "Leave Lobby";
            this.btnLobbyLeave.UseVisualStyleBackColor = true;
            this.btnLobbyLeave.Click += new System.EventHandler(this.btnLobbyLeave_Click);
            // 
            // lblLobbyVersus
            // 
            this.lblLobbyVersus.AutoSize = true;
            this.lblLobbyVersus.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLobbyVersus.Location = new System.Drawing.Point(357, 133);
            this.lblLobbyVersus.Name = "lblLobbyVersus";
            this.lblLobbyVersus.Size = new System.Drawing.Size(141, 42);
            this.lblLobbyVersus.TabIndex = 5;
            this.lblLobbyVersus.Text = "Versus";
            this.lblLobbyVersus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbxLobbySpectators
            // 
            this.gbxLobbySpectators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxLobbySpectators.Controls.Add(this.lbxLobbySpectators);
            this.gbxLobbySpectators.Location = new System.Drawing.Point(231, 217);
            this.gbxLobbySpectators.Name = "gbxLobbySpectators";
            this.gbxLobbySpectators.Size = new System.Drawing.Size(402, 288);
            this.gbxLobbySpectators.TabIndex = 4;
            this.gbxLobbySpectators.TabStop = false;
            this.gbxLobbySpectators.Text = "Spectators";
            // 
            // lbxLobbySpectators
            // 
            this.lbxLobbySpectators.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lbxLobbySpectators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxLobbySpectators.FormattingEnabled = true;
            this.lbxLobbySpectators.Location = new System.Drawing.Point(6, 19);
            this.lbxLobbySpectators.Name = "lbxLobbySpectators";
            this.lbxLobbySpectators.Size = new System.Drawing.Size(390, 251);
            this.lbxLobbySpectators.TabIndex = 0;
            this.lbxLobbySpectators.SelectedIndexChanged += new System.EventHandler(this.lbxLobbySpectators_SelectedIndexChanged);
            // 
            // gbxLobbyDire
            // 
            this.gbxLobbyDire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxLobbyDire.Controls.Add(this.btnJoinDire);
            this.gbxLobbyDire.Controls.Add(this.lbxLobbyDirePlayers);
            this.gbxLobbyDire.Location = new System.Drawing.Point(633, 35);
            this.gbxLobbyDire.Name = "gbxLobbyDire";
            this.gbxLobbyDire.Size = new System.Drawing.Size(229, 185);
            this.gbxLobbyDire.TabIndex = 2;
            this.gbxLobbyDire.TabStop = false;
            this.gbxLobbyDire.Text = "Dire";
            // 
            // btnJoinDire
            // 
            this.btnJoinDire.Location = new System.Drawing.Point(6, 146);
            this.btnJoinDire.Name = "btnJoinDire";
            this.btnJoinDire.Size = new System.Drawing.Size(216, 30);
            this.btnJoinDire.TabIndex = 1;
            this.btnJoinDire.Text = "Join Dire";
            this.btnJoinDire.UseVisualStyleBackColor = true;
            this.btnJoinDire.Click += new System.EventHandler(this.btnJoinDire_Click);
            // 
            // lbxLobbyDirePlayers
            // 
            this.lbxLobbyDirePlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxLobbyDirePlayers.FormattingEnabled = true;
            this.lbxLobbyDirePlayers.Location = new System.Drawing.Point(6, 19);
            this.lbxLobbyDirePlayers.Name = "lbxLobbyDirePlayers";
            this.lbxLobbyDirePlayers.Size = new System.Drawing.Size(217, 121);
            this.lbxLobbyDirePlayers.Sorted = true;
            this.lbxLobbyDirePlayers.TabIndex = 0;
            this.lbxLobbyDirePlayers.SelectedIndexChanged += new System.EventHandler(this.lbxLobbyDirePlayers_SelectedIndexChanged);
            // 
            // gbxLobbyRadiant
            // 
            this.gbxLobbyRadiant.Controls.Add(this.btnJoinRadiant);
            this.gbxLobbyRadiant.Controls.Add(this.lbxLobbyRadiantPlayers);
            this.gbxLobbyRadiant.Location = new System.Drawing.Point(8, 35);
            this.gbxLobbyRadiant.Name = "gbxLobbyRadiant";
            this.gbxLobbyRadiant.Size = new System.Drawing.Size(223, 185);
            this.gbxLobbyRadiant.TabIndex = 1;
            this.gbxLobbyRadiant.TabStop = false;
            this.gbxLobbyRadiant.Text = "Radiant";
            // 
            // btnJoinRadiant
            // 
            this.btnJoinRadiant.Location = new System.Drawing.Point(3, 146);
            this.btnJoinRadiant.Name = "btnJoinRadiant";
            this.btnJoinRadiant.Size = new System.Drawing.Size(214, 30);
            this.btnJoinRadiant.TabIndex = 1;
            this.btnJoinRadiant.Text = "Join Radiant";
            this.btnJoinRadiant.UseVisualStyleBackColor = true;
            this.btnJoinRadiant.Click += new System.EventHandler(this.btnJoinRadiant_Click);
            // 
            // lbxLobbyRadiantPlayers
            // 
            this.lbxLobbyRadiantPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxLobbyRadiantPlayers.FormattingEnabled = true;
            this.lbxLobbyRadiantPlayers.Location = new System.Drawing.Point(3, 19);
            this.lbxLobbyRadiantPlayers.Name = "lbxLobbyRadiantPlayers";
            this.lbxLobbyRadiantPlayers.Size = new System.Drawing.Size(214, 121);
            this.lbxLobbyRadiantPlayers.Sorted = true;
            this.lbxLobbyRadiantPlayers.TabIndex = 0;
            this.lbxLobbyRadiantPlayers.SelectedIndexChanged += new System.EventHandler(this.lbxLobbyRadiantPlayers_SelectedIndexChanged);
            // 
            // lblLobbyName
            // 
            this.lblLobbyName.AutoSize = true;
            this.lblLobbyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLobbyName.Location = new System.Drawing.Point(711, 7);
            this.lblLobbyName.Name = "lblLobbyName";
            this.lblLobbyName.Size = new System.Drawing.Size(143, 25);
            this.lblLobbyName.TabIndex = 0;
            this.lblLobbyName.Text = "Lobby Name";
            // 
            // tabDraftHeroPick
            // 
            this.tabDraftHeroPick.BackColor = System.Drawing.SystemColors.Control;
            this.tabDraftHeroPick.Controls.Add(this.lblHeroDraftPickingOrder);
            this.tabDraftHeroPick.Controls.Add(this.lblHeroDraftPicks);
            this.tabDraftHeroPick.Controls.Add(this.pbxDraftRadiantPicks);
            this.tabDraftHeroPick.Controls.Add(this.pbxDraftDirePicks);
            this.tabDraftHeroPick.Controls.Add(this.floDraftDireHeroes);
            this.tabDraftHeroPick.Controls.Add(this.lblDraftPlayerTurnText);
            this.tabDraftHeroPick.Controls.Add(this.floDraftRadiantHeroes);
            this.tabDraftHeroPick.Controls.Add(this.pbxDraftHeroPickDire);
            this.tabDraftHeroPick.Controls.Add(this.pbxDraftHeroPickRadiant);
            this.tabDraftHeroPick.Controls.Add(this.pbxDraftHeroPickIntelligence);
            this.tabDraftHeroPick.Controls.Add(this.pbxDraftHeroPickAgility);
            this.tabDraftHeroPick.Controls.Add(this.pbxDraftHeroPickStrength);
            this.tabDraftHeroPick.Controls.Add(this.gbxDraftHeroPickTimeRemaining);
            this.tabDraftHeroPick.Controls.Add(this.floDraftPlayerOrder);
            this.tabDraftHeroPick.Controls.Add(this.lblHeroSelectionDraft);
            this.tabDraftHeroPick.Controls.Add(this.floDraftHeroRadiantIntelligence);
            this.tabDraftHeroPick.Controls.Add(this.floDraftHeroDireIntelligence);
            this.tabDraftHeroPick.Controls.Add(this.floDraftHeroDireAgility);
            this.tabDraftHeroPick.Controls.Add(this.floDraftHeroRadiantAgility);
            this.tabDraftHeroPick.Controls.Add(this.floDraftHeroDireStrength);
            this.tabDraftHeroPick.Controls.Add(this.floDraftHeroRadiantStrength);
            this.tabDraftHeroPick.Location = new System.Drawing.Point(4, 5);
            this.tabDraftHeroPick.Name = "tabDraftHeroPick";
            this.tabDraftHeroPick.Padding = new System.Windows.Forms.Padding(3);
            this.tabDraftHeroPick.Size = new System.Drawing.Size(864, 513);
            this.tabDraftHeroPick.TabIndex = 5;
            this.tabDraftHeroPick.Text = "Hero Draft";
            // 
            // lblHeroDraftPickingOrder
            // 
            this.lblHeroDraftPickingOrder.AutoSize = true;
            this.lblHeroDraftPickingOrder.Location = new System.Drawing.Point(7, 410);
            this.lblHeroDraftPickingOrder.Name = "lblHeroDraftPickingOrder";
            this.lblHeroDraftPickingOrder.Size = new System.Drawing.Size(71, 13);
            this.lblHeroDraftPickingOrder.TabIndex = 21;
            this.lblHeroDraftPickingOrder.Text = "Picking Order";
            // 
            // lblHeroDraftPicks
            // 
            this.lblHeroDraftPicks.AutoSize = true;
            this.lblHeroDraftPicks.Location = new System.Drawing.Point(373, 462);
            this.lblHeroDraftPicks.Name = "lblHeroDraftPicks";
            this.lblHeroDraftPicks.Size = new System.Drawing.Size(33, 13);
            this.lblHeroDraftPicks.TabIndex = 20;
            this.lblHeroDraftPicks.Text = "Picks";
            // 
            // pbxDraftRadiantPicks
            // 
            this.pbxDraftRadiantPicks.Image = ((System.Drawing.Image)(resources.GetObject("pbxDraftRadiantPicks.Image")));
            this.pbxDraftRadiantPicks.Location = new System.Drawing.Point(354, 479);
            this.pbxDraftRadiantPicks.Name = "pbxDraftRadiantPicks";
            this.pbxDraftRadiantPicks.Size = new System.Drawing.Size(24, 25);
            this.pbxDraftRadiantPicks.TabIndex = 19;
            this.pbxDraftRadiantPicks.TabStop = false;
            // 
            // pbxDraftDirePicks
            // 
            this.pbxDraftDirePicks.Image = ((System.Drawing.Image)(resources.GetObject("pbxDraftDirePicks.Image")));
            this.pbxDraftDirePicks.Location = new System.Drawing.Point(396, 479);
            this.pbxDraftDirePicks.Name = "pbxDraftDirePicks";
            this.pbxDraftDirePicks.Size = new System.Drawing.Size(24, 25);
            this.pbxDraftDirePicks.TabIndex = 18;
            this.pbxDraftDirePicks.TabStop = false;
            // 
            // floDraftDireHeroes
            // 
            this.floDraftDireHeroes.Location = new System.Drawing.Point(426, 462);
            this.floDraftDireHeroes.Name = "floDraftDireHeroes";
            this.floDraftDireHeroes.Size = new System.Drawing.Size(340, 46);
            this.floDraftDireHeroes.TabIndex = 16;
            // 
            // lblDraftPlayerTurnText
            // 
            this.lblDraftPlayerTurnText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDraftPlayerTurnText.Location = new System.Drawing.Point(595, 418);
            this.lblDraftPlayerTurnText.Name = "lblDraftPlayerTurnText";
            this.lblDraftPlayerTurnText.Size = new System.Drawing.Size(260, 34);
            this.lblDraftPlayerTurnText.TabIndex = 9;
            this.lblDraftPlayerTurnText.Text = "Player\'s turn";
            this.lblDraftPlayerTurnText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // floDraftRadiantHeroes
            // 
            this.floDraftRadiantHeroes.Location = new System.Drawing.Point(8, 461);
            this.floDraftRadiantHeroes.Name = "floDraftRadiantHeroes";
            this.floDraftRadiantHeroes.Size = new System.Drawing.Size(340, 46);
            this.floDraftRadiantHeroes.TabIndex = 15;
            // 
            // pbxDraftHeroPickDire
            // 
            this.pbxDraftHeroPickDire.Image = ((System.Drawing.Image)(resources.GetObject("pbxDraftHeroPickDire.Image")));
            this.pbxDraftHeroPickDire.Location = new System.Drawing.Point(493, 7);
            this.pbxDraftHeroPickDire.Name = "pbxDraftHeroPickDire";
            this.pbxDraftHeroPickDire.Size = new System.Drawing.Size(24, 25);
            this.pbxDraftHeroPickDire.TabIndex = 14;
            this.pbxDraftHeroPickDire.TabStop = false;
            // 
            // pbxDraftHeroPickRadiant
            // 
            this.pbxDraftHeroPickRadiant.Image = ((System.Drawing.Image)(resources.GetObject("pbxDraftHeroPickRadiant.Image")));
            this.pbxDraftHeroPickRadiant.Location = new System.Drawing.Point(290, 7);
            this.pbxDraftHeroPickRadiant.Name = "pbxDraftHeroPickRadiant";
            this.pbxDraftHeroPickRadiant.Size = new System.Drawing.Size(24, 25);
            this.pbxDraftHeroPickRadiant.TabIndex = 13;
            this.pbxDraftHeroPickRadiant.TabStop = false;
            // 
            // pbxDraftHeroPickIntelligence
            // 
            this.pbxDraftHeroPickIntelligence.Image = ((System.Drawing.Image)(resources.GetObject("pbxDraftHeroPickIntelligence.Image")));
            this.pbxDraftHeroPickIntelligence.Location = new System.Drawing.Point(417, 339);
            this.pbxDraftHeroPickIntelligence.Name = "pbxDraftHeroPickIntelligence";
            this.pbxDraftHeroPickIntelligence.Size = new System.Drawing.Size(24, 25);
            this.pbxDraftHeroPickIntelligence.TabIndex = 12;
            this.pbxDraftHeroPickIntelligence.TabStop = false;
            // 
            // pbxDraftHeroPickAgility
            // 
            this.pbxDraftHeroPickAgility.Image = ((System.Drawing.Image)(resources.GetObject("pbxDraftHeroPickAgility.Image")));
            this.pbxDraftHeroPickAgility.Location = new System.Drawing.Point(417, 208);
            this.pbxDraftHeroPickAgility.Name = "pbxDraftHeroPickAgility";
            this.pbxDraftHeroPickAgility.Size = new System.Drawing.Size(24, 25);
            this.pbxDraftHeroPickAgility.TabIndex = 11;
            this.pbxDraftHeroPickAgility.TabStop = false;
            // 
            // pbxDraftHeroPickStrength
            // 
            this.pbxDraftHeroPickStrength.Image = ((System.Drawing.Image)(resources.GetObject("pbxDraftHeroPickStrength.Image")));
            this.pbxDraftHeroPickStrength.Location = new System.Drawing.Point(417, 79);
            this.pbxDraftHeroPickStrength.Name = "pbxDraftHeroPickStrength";
            this.pbxDraftHeroPickStrength.Size = new System.Drawing.Size(24, 25);
            this.pbxDraftHeroPickStrength.TabIndex = 10;
            this.pbxDraftHeroPickStrength.TabStop = false;
            // 
            // gbxDraftHeroPickTimeRemaining
            // 
            this.gbxDraftHeroPickTimeRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDraftHeroPickTimeRemaining.Controls.Add(this.lblDraftHeroPickTimeRemaining);
            this.gbxDraftHeroPickTimeRemaining.Location = new System.Drawing.Point(772, 457);
            this.gbxDraftHeroPickTimeRemaining.Name = "gbxDraftHeroPickTimeRemaining";
            this.gbxDraftHeroPickTimeRemaining.Size = new System.Drawing.Size(86, 50);
            this.gbxDraftHeroPickTimeRemaining.TabIndex = 8;
            this.gbxDraftHeroPickTimeRemaining.TabStop = false;
            this.gbxDraftHeroPickTimeRemaining.Text = "Time:";
            // 
            // lblDraftHeroPickTimeRemaining
            // 
            this.lblDraftHeroPickTimeRemaining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDraftHeroPickTimeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDraftHeroPickTimeRemaining.Location = new System.Drawing.Point(3, 16);
            this.lblDraftHeroPickTimeRemaining.Name = "lblDraftHeroPickTimeRemaining";
            this.lblDraftHeroPickTimeRemaining.Size = new System.Drawing.Size(80, 31);
            this.lblDraftHeroPickTimeRemaining.TabIndex = 0;
            this.lblDraftHeroPickTimeRemaining.Text = "0:00";
            this.lblDraftHeroPickTimeRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // floDraftPlayerOrder
            // 
            this.floDraftPlayerOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.floDraftPlayerOrder.Location = new System.Drawing.Point(8, 426);
            this.floDraftPlayerOrder.Name = "floDraftPlayerOrder";
            this.floDraftPlayerOrder.Size = new System.Drawing.Size(581, 22);
            this.floDraftPlayerOrder.TabIndex = 7;
            // 
            // lblHeroSelectionDraft
            // 
            this.lblHeroSelectionDraft.AutoSize = true;
            this.lblHeroSelectionDraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeroSelectionDraft.Location = new System.Drawing.Point(320, 3);
            this.lblHeroSelectionDraft.Name = "lblHeroSelectionDraft";
            this.lblHeroSelectionDraft.Size = new System.Drawing.Size(167, 25);
            this.lblHeroSelectionDraft.TabIndex = 6;
            this.lblHeroSelectionDraft.Text = "Hero Selection";
            // 
            // floDraftHeroRadiantIntelligence
            // 
            this.floDraftHeroRadiantIntelligence.Location = new System.Drawing.Point(6, 289);
            this.floDraftHeroRadiantIntelligence.Name = "floDraftHeroRadiantIntelligence";
            this.floDraftHeroRadiantIntelligence.Size = new System.Drawing.Size(400, 120);
            this.floDraftHeroRadiantIntelligence.TabIndex = 5;
            // 
            // floDraftHeroDireIntelligence
            // 
            this.floDraftHeroDireIntelligence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.floDraftHeroDireIntelligence.Location = new System.Drawing.Point(455, 289);
            this.floDraftHeroDireIntelligence.Name = "floDraftHeroDireIntelligence";
            this.floDraftHeroDireIntelligence.Size = new System.Drawing.Size(400, 120);
            this.floDraftHeroDireIntelligence.TabIndex = 4;
            // 
            // floDraftHeroDireAgility
            // 
            this.floDraftHeroDireAgility.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.floDraftHeroDireAgility.Location = new System.Drawing.Point(455, 161);
            this.floDraftHeroDireAgility.Name = "floDraftHeroDireAgility";
            this.floDraftHeroDireAgility.Size = new System.Drawing.Size(400, 120);
            this.floDraftHeroDireAgility.TabIndex = 3;
            // 
            // floDraftHeroRadiantAgility
            // 
            this.floDraftHeroRadiantAgility.Location = new System.Drawing.Point(6, 161);
            this.floDraftHeroRadiantAgility.Name = "floDraftHeroRadiantAgility";
            this.floDraftHeroRadiantAgility.Size = new System.Drawing.Size(400, 120);
            this.floDraftHeroRadiantAgility.TabIndex = 2;
            // 
            // floDraftHeroDireStrength
            // 
            this.floDraftHeroDireStrength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.floDraftHeroDireStrength.Location = new System.Drawing.Point(455, 35);
            this.floDraftHeroDireStrength.Name = "floDraftHeroDireStrength";
            this.floDraftHeroDireStrength.Size = new System.Drawing.Size(400, 120);
            this.floDraftHeroDireStrength.TabIndex = 1;
            // 
            // floDraftHeroRadiantStrength
            // 
            this.floDraftHeroRadiantStrength.Location = new System.Drawing.Point(6, 35);
            this.floDraftHeroRadiantStrength.Name = "floDraftHeroRadiantStrength";
            this.floDraftHeroRadiantStrength.Size = new System.Drawing.Size(400, 120);
            this.floDraftHeroRadiantStrength.TabIndex = 0;
            // 
            // tabSkillDraft
            // 
            this.tabSkillDraft.BackColor = System.Drawing.SystemColors.Control;
            this.tabSkillDraft.Controls.Add(this.btnLeaveSkills);
            this.tabSkillDraft.Controls.Add(this.pbxSkillDraftYourHero);
            this.tabSkillDraft.Controls.Add(this.lblSkillDraftSelection);
            this.tabSkillDraft.Controls.Add(this.floSkillDirePicks);
            this.tabSkillDraft.Controls.Add(this.floSkillRadiantPicks);
            this.tabSkillDraft.Controls.Add(this.lblSkillDraftPickingOrder);
            this.tabSkillDraft.Controls.Add(this.lblSkillDraftPicks);
            this.tabSkillDraft.Controls.Add(this.pbxSkillRadiantPicks);
            this.tabSkillDraft.Controls.Add(this.pbxSkillDirePicks);
            this.tabSkillDraft.Controls.Add(this.lblSkillDraftPlayerTurn);
            this.tabSkillDraft.Controls.Add(this.gbxSkillDraftTimeRemaining);
            this.tabSkillDraft.Controls.Add(this.floSKillDraftPickingOrder);
            this.tabSkillDraft.Controls.Add(this.floSkillDraftUltimateSkills);
            this.tabSkillDraft.Controls.Add(this.lblSkillDraftUltimates);
            this.tabSkillDraft.Controls.Add(this.lblSkillDraftNormal);
            this.tabSkillDraft.Controls.Add(this.floSkillDraftNormalSkills);
            this.tabSkillDraft.Location = new System.Drawing.Point(4, 5);
            this.tabSkillDraft.Name = "tabSkillDraft";
            this.tabSkillDraft.Padding = new System.Windows.Forms.Padding(3);
            this.tabSkillDraft.Size = new System.Drawing.Size(864, 513);
            this.tabSkillDraft.TabIndex = 6;
            this.tabSkillDraft.Text = "Skill Draft";
            // 
            // btnLeaveSkills
            // 
            this.btnLeaveSkills.Location = new System.Drawing.Point(773, 487);
            this.btnLeaveSkills.Name = "btnLeaveSkills";
            this.btnLeaveSkills.Size = new System.Drawing.Size(86, 23);
            this.btnLeaveSkills.TabIndex = 33;
            this.btnLeaveSkills.Text = "Leave Game";
            this.btnLeaveSkills.UseVisualStyleBackColor = true;
            this.btnLeaveSkills.Click += new System.EventHandler(this.btnLeaveSkills_Click);
            // 
            // pbxSkillDraftYourHero
            // 
            this.pbxSkillDraftYourHero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxSkillDraftYourHero.Location = new System.Drawing.Point(795, 3);
            this.pbxSkillDraftYourHero.Name = "pbxSkillDraftYourHero";
            this.pbxSkillDraftYourHero.Size = new System.Drawing.Size(61, 33);
            this.pbxSkillDraftYourHero.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxSkillDraftYourHero.TabIndex = 32;
            this.pbxSkillDraftYourHero.TabStop = false;
            // 
            // lblSkillDraftSelection
            // 
            this.lblSkillDraftSelection.AutoSize = true;
            this.lblSkillDraftSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkillDraftSelection.Location = new System.Drawing.Point(351, 7);
            this.lblSkillDraftSelection.Name = "lblSkillDraftSelection";
            this.lblSkillDraftSelection.Size = new System.Drawing.Size(162, 25);
            this.lblSkillDraftSelection.TabIndex = 31;
            this.lblSkillDraftSelection.Text = "Skill Selection";
            // 
            // floSkillDirePicks
            // 
            this.floSkillDirePicks.Location = new System.Drawing.Point(427, 433);
            this.floSkillDirePicks.Name = "floSkillDirePicks";
            this.floSkillDirePicks.Size = new System.Drawing.Size(340, 84);
            this.floSkillDirePicks.TabIndex = 30;
            // 
            // floSkillRadiantPicks
            // 
            this.floSkillRadiantPicks.Location = new System.Drawing.Point(9, 433);
            this.floSkillRadiantPicks.Name = "floSkillRadiantPicks";
            this.floSkillRadiantPicks.Size = new System.Drawing.Size(340, 81);
            this.floSkillRadiantPicks.TabIndex = 29;
            // 
            // lblSkillDraftPickingOrder
            // 
            this.lblSkillDraftPickingOrder.AutoSize = true;
            this.lblSkillDraftPickingOrder.Location = new System.Drawing.Point(8, 383);
            this.lblSkillDraftPickingOrder.Name = "lblSkillDraftPickingOrder";
            this.lblSkillDraftPickingOrder.Size = new System.Drawing.Size(71, 13);
            this.lblSkillDraftPickingOrder.TabIndex = 28;
            this.lblSkillDraftPickingOrder.Text = "Picking Order";
            // 
            // lblSkillDraftPicks
            // 
            this.lblSkillDraftPicks.AutoSize = true;
            this.lblSkillDraftPicks.Location = new System.Drawing.Point(374, 438);
            this.lblSkillDraftPicks.Name = "lblSkillDraftPicks";
            this.lblSkillDraftPicks.Size = new System.Drawing.Size(33, 13);
            this.lblSkillDraftPicks.TabIndex = 27;
            this.lblSkillDraftPicks.Text = "Picks";
            // 
            // pbxSkillRadiantPicks
            // 
            this.pbxSkillRadiantPicks.Image = ((System.Drawing.Image)(resources.GetObject("pbxSkillRadiantPicks.Image")));
            this.pbxSkillRadiantPicks.Location = new System.Drawing.Point(355, 455);
            this.pbxSkillRadiantPicks.Name = "pbxSkillRadiantPicks";
            this.pbxSkillRadiantPicks.Size = new System.Drawing.Size(24, 25);
            this.pbxSkillRadiantPicks.TabIndex = 26;
            this.pbxSkillRadiantPicks.TabStop = false;
            // 
            // pbxSkillDirePicks
            // 
            this.pbxSkillDirePicks.Image = ((System.Drawing.Image)(resources.GetObject("pbxSkillDirePicks.Image")));
            this.pbxSkillDirePicks.Location = new System.Drawing.Point(397, 455);
            this.pbxSkillDirePicks.Name = "pbxSkillDirePicks";
            this.pbxSkillDirePicks.Size = new System.Drawing.Size(24, 25);
            this.pbxSkillDirePicks.TabIndex = 25;
            this.pbxSkillDirePicks.TabStop = false;
            // 
            // lblSkillDraftPlayerTurn
            // 
            this.lblSkillDraftPlayerTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkillDraftPlayerTurn.Location = new System.Drawing.Point(596, 394);
            this.lblSkillDraftPlayerTurn.Name = "lblSkillDraftPlayerTurn";
            this.lblSkillDraftPlayerTurn.Size = new System.Drawing.Size(260, 34);
            this.lblSkillDraftPlayerTurn.TabIndex = 24;
            this.lblSkillDraftPlayerTurn.Text = "Player\'s turn";
            this.lblSkillDraftPlayerTurn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxSkillDraftTimeRemaining
            // 
            this.gbxSkillDraftTimeRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxSkillDraftTimeRemaining.Controls.Add(this.lblSkillDraftTimeRemaining);
            this.gbxSkillDraftTimeRemaining.Location = new System.Drawing.Point(773, 433);
            this.gbxSkillDraftTimeRemaining.Name = "gbxSkillDraftTimeRemaining";
            this.gbxSkillDraftTimeRemaining.Size = new System.Drawing.Size(86, 50);
            this.gbxSkillDraftTimeRemaining.TabIndex = 23;
            this.gbxSkillDraftTimeRemaining.TabStop = false;
            this.gbxSkillDraftTimeRemaining.Text = "Time:";
            // 
            // lblSkillDraftTimeRemaining
            // 
            this.lblSkillDraftTimeRemaining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSkillDraftTimeRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkillDraftTimeRemaining.Location = new System.Drawing.Point(3, 16);
            this.lblSkillDraftTimeRemaining.Name = "lblSkillDraftTimeRemaining";
            this.lblSkillDraftTimeRemaining.Size = new System.Drawing.Size(80, 31);
            this.lblSkillDraftTimeRemaining.TabIndex = 0;
            this.lblSkillDraftTimeRemaining.Text = "0:00";
            this.lblSkillDraftTimeRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // floSKillDraftPickingOrder
            // 
            this.floSKillDraftPickingOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.floSKillDraftPickingOrder.Location = new System.Drawing.Point(9, 399);
            this.floSKillDraftPickingOrder.Name = "floSKillDraftPickingOrder";
            this.floSKillDraftPickingOrder.Size = new System.Drawing.Size(581, 22);
            this.floSKillDraftPickingOrder.TabIndex = 22;
            // 
            // floSkillDraftUltimateSkills
            // 
            this.floSkillDraftUltimateSkills.AllowDrop = true;
            this.floSkillDraftUltimateSkills.Location = new System.Drawing.Point(11, 277);
            this.floSkillDraftUltimateSkills.Name = "floSkillDraftUltimateSkills";
            this.floSkillDraftUltimateSkills.Size = new System.Drawing.Size(845, 102);
            this.floSkillDraftUltimateSkills.TabIndex = 3;
            // 
            // lblSkillDraftUltimates
            // 
            this.lblSkillDraftUltimates.AutoSize = true;
            this.lblSkillDraftUltimates.Location = new System.Drawing.Point(8, 261);
            this.lblSkillDraftUltimates.Name = "lblSkillDraftUltimates";
            this.lblSkillDraftUltimates.Size = new System.Drawing.Size(75, 13);
            this.lblSkillDraftUltimates.TabIndex = 2;
            this.lblSkillDraftUltimates.Text = "Ultimate Skills:";
            // 
            // lblSkillDraftNormal
            // 
            this.lblSkillDraftNormal.AutoSize = true;
            this.lblSkillDraftNormal.Location = new System.Drawing.Point(8, 23);
            this.lblSkillDraftNormal.Name = "lblSkillDraftNormal";
            this.lblSkillDraftNormal.Size = new System.Drawing.Size(70, 13);
            this.lblSkillDraftNormal.TabIndex = 1;
            this.lblSkillDraftNormal.Text = "Normal Skills:";
            // 
            // floSkillDraftNormalSkills
            // 
            this.floSkillDraftNormalSkills.Location = new System.Drawing.Point(11, 39);
            this.floSkillDraftNormalSkills.Name = "floSkillDraftNormalSkills";
            this.floSkillDraftNormalSkills.Size = new System.Drawing.Size(845, 219);
            this.floSkillDraftNormalSkills.TabIndex = 0;
            // 
            // tabDraftSummary
            // 
            this.tabDraftSummary.BackColor = System.Drawing.SystemColors.Control;
            this.tabDraftSummary.Controls.Add(this.gbxConfiguringMod);
            this.tabDraftSummary.Controls.Add(this.floDireTeamSummary);
            this.tabDraftSummary.Controls.Add(this.lblDraftDireTeamPicks);
            this.tabDraftSummary.Controls.Add(this.floRadiantTeamSummary);
            this.tabDraftSummary.Controls.Add(this.lblDraftRadiantTeamPicks);
            this.tabDraftSummary.Controls.Add(this.floPersonalSummary);
            this.tabDraftSummary.Location = new System.Drawing.Point(4, 5);
            this.tabDraftSummary.Name = "tabDraftSummary";
            this.tabDraftSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabDraftSummary.Size = new System.Drawing.Size(864, 513);
            this.tabDraftSummary.TabIndex = 7;
            this.tabDraftSummary.Text = "Draft Summary";
            // 
            // gbxConfiguringMod
            // 
            this.gbxConfiguringMod.Controls.Add(this.btnManualConnect);
            this.gbxConfiguringMod.Controls.Add(this.btnDone);
            this.gbxConfiguringMod.Controls.Add(this.lblConfigProgressMessage);
            this.gbxConfiguringMod.Controls.Add(this.pgbConfigProgress);
            this.gbxConfiguringMod.Location = new System.Drawing.Point(508, 27);
            this.gbxConfiguringMod.Name = "gbxConfiguringMod";
            this.gbxConfiguringMod.Size = new System.Drawing.Size(349, 83);
            this.gbxConfiguringMod.TabIndex = 6;
            this.gbxConfiguringMod.TabStop = false;
            this.gbxConfiguringMod.Text = "Progress";
            // 
            // btnManualConnect
            // 
            this.btnManualConnect.Location = new System.Drawing.Point(6, 19);
            this.btnManualConnect.Name = "btnManualConnect";
            this.btnManualConnect.Size = new System.Drawing.Size(165, 52);
            this.btnManualConnect.TabIndex = 3;
            this.btnManualConnect.Text = "Connect Manually";
            this.btnManualConnect.UseVisualStyleBackColor = true;
            this.btnManualConnect.Visible = false;
            this.btnManualConnect.Click += new System.EventHandler(this.btnManualConnect_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(177, 19);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(165, 52);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "Click Here When Game is Finished";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Visible = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lblConfigProgressMessage
            // 
            this.lblConfigProgressMessage.Location = new System.Drawing.Point(11, 56);
            this.lblConfigProgressMessage.Name = "lblConfigProgressMessage";
            this.lblConfigProgressMessage.Size = new System.Drawing.Size(331, 16);
            this.lblConfigProgressMessage.TabIndex = 1;
            this.lblConfigProgressMessage.Text = "Configuring Heroes...";
            this.lblConfigProgressMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbConfigProgress
            // 
            this.pgbConfigProgress.Location = new System.Drawing.Point(6, 19);
            this.pgbConfigProgress.Name = "pgbConfigProgress";
            this.pgbConfigProgress.Size = new System.Drawing.Size(337, 28);
            this.pgbConfigProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbConfigProgress.TabIndex = 0;
            // 
            // floDireTeamSummary
            // 
            this.floDireTeamSummary.Location = new System.Drawing.Point(14, 129);
            this.floDireTeamSummary.Name = "floDireTeamSummary";
            this.floDireTeamSummary.Size = new System.Drawing.Size(410, 378);
            this.floDireTeamSummary.TabIndex = 5;
            // 
            // lblDraftDireTeamPicks
            // 
            this.lblDraftDireTeamPicks.AllowDrop = true;
            this.lblDraftDireTeamPicks.AutoSize = true;
            this.lblDraftDireTeamPicks.Location = new System.Drawing.Point(11, 113);
            this.lblDraftDireTeamPicks.Name = "lblDraftDireTeamPicks";
            this.lblDraftDireTeamPicks.Size = new System.Drawing.Size(95, 13);
            this.lblDraftDireTeamPicks.TabIndex = 4;
            this.lblDraftDireTeamPicks.Text = "Dire Team Picked:";
            // 
            // floRadiantTeamSummary
            // 
            this.floRadiantTeamSummary.Location = new System.Drawing.Point(14, 129);
            this.floRadiantTeamSummary.Name = "floRadiantTeamSummary";
            this.floRadiantTeamSummary.Size = new System.Drawing.Size(410, 376);
            this.floRadiantTeamSummary.TabIndex = 3;
            // 
            // lblDraftRadiantTeamPicks
            // 
            this.lblDraftRadiantTeamPicks.AutoSize = true;
            this.lblDraftRadiantTeamPicks.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDraftRadiantTeamPicks.Location = new System.Drawing.Point(11, 113);
            this.lblDraftRadiantTeamPicks.Name = "lblDraftRadiantTeamPicks";
            this.lblDraftRadiantTeamPicks.Size = new System.Drawing.Size(113, 13);
            this.lblDraftRadiantTeamPicks.TabIndex = 2;
            this.lblDraftRadiantTeamPicks.Text = "Radiant Team Picked:";
            // 
            // floPersonalSummary
            // 
            this.floPersonalSummary.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.floPersonalSummary.Location = new System.Drawing.Point(14, 27);
            this.floPersonalSummary.Name = "floPersonalSummary";
            this.floPersonalSummary.Size = new System.Drawing.Size(481, 83);
            this.floPersonalSummary.TabIndex = 1;
            // 
            // tabSettings
            // 
            this.tabSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabSettings.Controls.Add(this.lblSettings);
            this.tabSettings.Controls.Add(this.btnSettingsSaveReturn);
            this.tabSettings.Controls.Add(this.gbxSettingsServer);
            this.tabSettings.Controls.Add(this.gbxSettingsClient);
            this.tabSettings.Location = new System.Drawing.Point(4, 5);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(864, 513);
            this.tabSettings.TabIndex = 8;
            this.tabSettings.Text = "Settings";
            // 
            // lblSettings
            // 
            this.lblSettings.AutoSize = true;
            this.lblSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettings.Location = new System.Drawing.Point(318, 12);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(98, 25);
            this.lblSettings.TabIndex = 7;
            this.lblSettings.Text = "Settings";
            // 
            // btnSettingsSaveReturn
            // 
            this.btnSettingsSaveReturn.Location = new System.Drawing.Point(6, 476);
            this.btnSettingsSaveReturn.Name = "btnSettingsSaveReturn";
            this.btnSettingsSaveReturn.Size = new System.Drawing.Size(130, 29);
            this.btnSettingsSaveReturn.TabIndex = 2;
            this.btnSettingsSaveReturn.Text = "Back";
            this.btnSettingsSaveReturn.UseVisualStyleBackColor = true;
            this.btnSettingsSaveReturn.Click += new System.EventHandler(this.btnSettingsSaveReturn_Click);
            // 
            // gbxSettingsServer
            // 
            this.gbxSettingsServer.Controls.Add(this.gbxServerOther);
            this.gbxSettingsServer.Controls.Add(this.gbxBanList);
            this.gbxSettingsServer.Controls.Add(this.gbxSettingServerPort);
            this.gbxSettingsServer.Controls.Add(this.lblSettingsServerStatus);
            this.gbxSettingsServer.Controls.Add(this.gbxSettingsServerLocation);
            this.gbxSettingsServer.Location = new System.Drawing.Point(438, 7);
            this.gbxSettingsServer.Name = "gbxSettingsServer";
            this.gbxSettingsServer.Size = new System.Drawing.Size(420, 463);
            this.gbxSettingsServer.TabIndex = 1;
            this.gbxSettingsServer.TabStop = false;
            this.gbxSettingsServer.Text = "Dota 2 Server Settings";
            // 
            // gbxServerOther
            // 
            this.gbxServerOther.Controls.Add(this.chkDedicated);
            this.gbxServerOther.Controls.Add(this.cbxVersionFixDisable);
            this.gbxServerOther.Controls.Add(this.chkConDebug);
            this.gbxServerOther.Location = new System.Drawing.Point(6, 234);
            this.gbxServerOther.Name = "gbxServerOther";
            this.gbxServerOther.Size = new System.Drawing.Size(408, 43);
            this.gbxServerOther.TabIndex = 9;
            this.gbxServerOther.TabStop = false;
            this.gbxServerOther.Text = "Other Settings";
            // 
            // chkDedicated
            // 
            this.chkDedicated.AutoSize = true;
            this.chkDedicated.Location = new System.Drawing.Point(221, 20);
            this.chkDedicated.Name = "chkDedicated";
            this.chkDedicated.Size = new System.Drawing.Size(109, 17);
            this.chkDedicated.TabIndex = 2;
            this.chkDedicated.Text = "Dedicated Server";
            this.chkDedicated.UseVisualStyleBackColor = true;
            this.chkDedicated.CheckedChanged += new System.EventHandler(this.chkDedicated_CheckedChanged);
            // 
            // cbxVersionFixDisable
            // 
            this.cbxVersionFixDisable.AutoSize = true;
            this.cbxVersionFixDisable.Location = new System.Drawing.Point(99, 20);
            this.cbxVersionFixDisable.Name = "cbxVersionFixDisable";
            this.cbxVersionFixDisable.Size = new System.Drawing.Size(115, 17);
            this.cbxVersionFixDisable.TabIndex = 1;
            this.cbxVersionFixDisable.Text = "Disable Version Fix";
            this.cbxVersionFixDisable.UseVisualStyleBackColor = true;
            this.cbxVersionFixDisable.CheckedChanged += new System.EventHandler(this.cbxVersionFixDisable_CheckedChanged);
            // 
            // chkConDebug
            // 
            this.chkConDebug.AutoSize = true;
            this.chkConDebug.Location = new System.Drawing.Point(7, 20);
            this.chkConDebug.Name = "chkConDebug";
            this.chkConDebug.Size = new System.Drawing.Size(85, 17);
            this.chkConDebug.TabIndex = 0;
            this.chkConDebug.Text = "Log Console";
            this.chkConDebug.UseVisualStyleBackColor = true;
            this.chkConDebug.CheckedChanged += new System.EventHandler(this.chkConDebug_CheckedChanged);
            // 
            // gbxBanList
            // 
            this.gbxBanList.Controls.Add(this.btnBanURLLoad);
            this.gbxBanList.Controls.Add(this.label2);
            this.gbxBanList.Controls.Add(this.btnBanLoad);
            this.gbxBanList.Controls.Add(this.btnBanSave);
            this.gbxBanList.Controls.Add(this.tbxBans);
            this.gbxBanList.Location = new System.Drawing.Point(7, 283);
            this.gbxBanList.Name = "gbxBanList";
            this.gbxBanList.Size = new System.Drawing.Size(407, 124);
            this.gbxBanList.TabIndex = 8;
            this.gbxBanList.TabStop = false;
            this.gbxBanList.Text = "Local Bans";
            // 
            // btnBanURLLoad
            // 
            this.btnBanURLLoad.Location = new System.Drawing.Point(337, 19);
            this.btnBanURLLoad.Name = "btnBanURLLoad";
            this.btnBanURLLoad.Size = new System.Drawing.Size(64, 23);
            this.btnBanURLLoad.TabIndex = 4;
            this.btnBanURLLoad.Text = "URL Load";
            this.btnBanURLLoad.UseVisualStyleBackColor = true;
            this.btnBanURLLoad.Click += new System.EventHandler(this.btnBanURLLoad_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "These players will be \r\nunable to join your games. \r\nAdd one per line.\r\n";
            // 
            // btnBanLoad
            // 
            this.btnBanLoad.Location = new System.Drawing.Point(276, 19);
            this.btnBanLoad.Name = "btnBanLoad";
            this.btnBanLoad.Size = new System.Drawing.Size(54, 23);
            this.btnBanLoad.TabIndex = 2;
            this.btnBanLoad.Text = "Load";
            this.btnBanLoad.UseVisualStyleBackColor = true;
            this.btnBanLoad.Click += new System.EventHandler(this.btnBanLoad_Click);
            // 
            // btnBanSave
            // 
            this.btnBanSave.Location = new System.Drawing.Point(276, 48);
            this.btnBanSave.Name = "btnBanSave";
            this.btnBanSave.Size = new System.Drawing.Size(54, 23);
            this.btnBanSave.TabIndex = 1;
            this.btnBanSave.Text = "Save";
            this.btnBanSave.UseVisualStyleBackColor = true;
            this.btnBanSave.Click += new System.EventHandler(this.btnBanSave_Click);
            // 
            // tbxBans
            // 
            this.tbxBans.Location = new System.Drawing.Point(6, 19);
            this.tbxBans.Multiline = true;
            this.tbxBans.Name = "tbxBans";
            this.tbxBans.Size = new System.Drawing.Size(264, 99);
            this.tbxBans.TabIndex = 0;
            // 
            // gbxSettingServerPort
            // 
            this.gbxSettingServerPort.Controls.Add(this.tbxSettingServerPort);
            this.gbxSettingServerPort.Controls.Add(this.lblSettingServerPort);
            this.gbxSettingServerPort.Controls.Add(this.btnSettingServerPort);
            this.gbxSettingServerPort.Location = new System.Drawing.Point(7, 166);
            this.gbxSettingServerPort.Name = "gbxSettingServerPort";
            this.gbxSettingServerPort.Size = new System.Drawing.Size(408, 62);
            this.gbxSettingServerPort.TabIndex = 7;
            this.gbxSettingServerPort.TabStop = false;
            this.gbxSettingServerPort.Text = "Server Port";
            // 
            // tbxSettingServerPort
            // 
            this.tbxSettingServerPort.Location = new System.Drawing.Point(72, 21);
            this.tbxSettingServerPort.Name = "tbxSettingServerPort";
            this.tbxSettingServerPort.Size = new System.Drawing.Size(328, 20);
            this.tbxSettingServerPort.TabIndex = 8;
            // 
            // lblSettingServerPort
            // 
            this.lblSettingServerPort.Location = new System.Drawing.Point(3, 44);
            this.lblSettingServerPort.Name = "lblSettingServerPort";
            this.lblSettingServerPort.Size = new System.Drawing.Size(396, 15);
            this.lblSettingServerPort.TabIndex = 7;
            this.lblSettingServerPort.Text = "Make sure to use port forwarding on your router to forward the chosen port.\r\n";
            // 
            // btnSettingServerPort
            // 
            this.btnSettingServerPort.AllowDrop = true;
            this.btnSettingServerPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSettingServerPort.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSettingServerPort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettingServerPort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettingServerPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettingServerPort.Location = new System.Drawing.Point(6, 19);
            this.btnSettingServerPort.Name = "btnSettingServerPort";
            this.btnSettingServerPort.Size = new System.Drawing.Size(60, 23);
            this.btnSettingServerPort.TabIndex = 5;
            this.btnSettingServerPort.Text = "Change...";
            this.btnSettingServerPort.UseVisualStyleBackColor = false;
            this.btnSettingServerPort.Click += new System.EventHandler(this.btnSettingServerPort_Click);
            // 
            // lblSettingsServerStatus
            // 
            this.lblSettingsServerStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSettingsServerStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSettingsServerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsServerStatus.Location = new System.Drawing.Point(5, 416);
            this.lblSettingsServerStatus.Name = "lblSettingsServerStatus";
            this.lblSettingsServerStatus.Size = new System.Drawing.Size(408, 37);
            this.lblSettingsServerStatus.TabIndex = 5;
            this.lblSettingsServerStatus.Text = "Not Setup";
            this.lblSettingsServerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxSettingsServerLocation
            // 
            this.gbxSettingsServerLocation.Controls.Add(this.btnSettingServerInstallationWizard);
            this.gbxSettingsServerLocation.Controls.Add(this.lblSettingServerOptions);
            this.gbxSettingsServerLocation.Controls.Add(this.btnSettingsServerLocationChange);
            this.gbxSettingsServerLocation.Controls.Add(this.lblDota2ServerLocation);
            this.gbxSettingsServerLocation.Location = new System.Drawing.Point(6, 19);
            this.gbxSettingsServerLocation.Name = "gbxSettingsServerLocation";
            this.gbxSettingsServerLocation.Size = new System.Drawing.Size(408, 141);
            this.gbxSettingsServerLocation.TabIndex = 4;
            this.gbxSettingsServerLocation.TabStop = false;
            this.gbxSettingsServerLocation.Text = "Server Directory";
            // 
            // btnSettingServerInstallationWizard
            // 
            this.btnSettingServerInstallationWizard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSettingServerInstallationWizard.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSettingServerInstallationWizard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSettingServerInstallationWizard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSettingServerInstallationWizard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettingServerInstallationWizard.Location = new System.Drawing.Point(6, 114);
            this.btnSettingServerInstallationWizard.Name = "btnSettingServerInstallationWizard";
            this.btnSettingServerInstallationWizard.Size = new System.Drawing.Size(214, 23);
            this.btnSettingServerInstallationWizard.TabIndex = 7;
            this.btnSettingServerInstallationWizard.Text = "Run Dota 2 Server Installation Wizard...";
            this.btnSettingServerInstallationWizard.UseVisualStyleBackColor = false;
            this.btnSettingServerInstallationWizard.Click += new System.EventHandler(this.btnSettingServerInstallationWizard_Click);
            // 
            // lblSettingServerOptions
            // 
            this.lblSettingServerOptions.Location = new System.Drawing.Point(14, 48);
            this.lblSettingServerOptions.Name = "lblSettingServerOptions";
            this.lblSettingServerOptions.Size = new System.Drawing.Size(376, 65);
            this.lblSettingServerOptions.TabIndex = 6;
            this.lblSettingServerOptions.Text = resources.GetString("lblSettingServerOptions.Text");
            // 
            // btnSettingsServerLocationChange
            // 
            this.btnSettingsServerLocationChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSettingsServerLocationChange.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSettingsServerLocationChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettingsServerLocationChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettingsServerLocationChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettingsServerLocationChange.Location = new System.Drawing.Point(6, 19);
            this.btnSettingsServerLocationChange.Name = "btnSettingsServerLocationChange";
            this.btnSettingsServerLocationChange.Size = new System.Drawing.Size(60, 23);
            this.btnSettingsServerLocationChange.TabIndex = 5;
            this.btnSettingsServerLocationChange.Text = "Change...";
            this.btnSettingsServerLocationChange.UseVisualStyleBackColor = false;
            this.btnSettingsServerLocationChange.Click += new System.EventHandler(this.btnSettingsServerLocationChange_Click);
            // 
            // lblDota2ServerLocation
            // 
            this.lblDota2ServerLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDota2ServerLocation.Location = new System.Drawing.Point(72, 24);
            this.lblDota2ServerLocation.Name = "lblDota2ServerLocation";
            this.lblDota2ServerLocation.Size = new System.Drawing.Size(330, 18);
            this.lblDota2ServerLocation.TabIndex = 4;
            this.lblDota2ServerLocation.Text = "C:\\Dota2Server\\";
            // 
            // gbxSettingsClient
            // 
            this.gbxSettingsClient.Controls.Add(this.gbxClientOther);
            this.gbxSettingsClient.Controls.Add(this.gbxSettingsClientSteamDir);
            this.gbxSettingsClient.Controls.Add(this.gbxSettingsConsoleKey);
            this.gbxSettingsClient.Controls.Add(this.lblSettingsClientStatus);
            this.gbxSettingsClient.Controls.Add(this.gbxSettingsClientLocation);
            this.gbxSettingsClient.Location = new System.Drawing.Point(8, 40);
            this.gbxSettingsClient.Name = "gbxSettingsClient";
            this.gbxSettingsClient.Size = new System.Drawing.Size(420, 430);
            this.gbxSettingsClient.TabIndex = 0;
            this.gbxSettingsClient.TabStop = false;
            this.gbxSettingsClient.Text = "Dota 2 Client Settings";
            // 
            // gbxClientOther
            // 
            this.gbxClientOther.Controls.Add(this.chkFlashName);
            this.gbxClientOther.Controls.Add(this.chkBeepName);
            this.gbxClientOther.Controls.Add(this.chkFlashNew);
            this.gbxClientOther.Controls.Add(this.chkBeepNew);
            this.gbxClientOther.Location = new System.Drawing.Point(6, 201);
            this.gbxClientOther.Name = "gbxClientOther";
            this.gbxClientOther.Size = new System.Drawing.Size(408, 66);
            this.gbxClientOther.TabIndex = 11;
            this.gbxClientOther.TabStop = false;
            this.gbxClientOther.Text = "Other Settings";
            // 
            // chkFlashName
            // 
            this.chkFlashName.AutoSize = true;
            this.chkFlashName.Location = new System.Drawing.Point(155, 43);
            this.chkFlashName.Name = "chkFlashName";
            this.chkFlashName.Size = new System.Drawing.Size(135, 17);
            this.chkFlashName.TabIndex = 15;
            this.chkFlashName.Text = "Flash on name mention";
            this.chkFlashName.UseVisualStyleBackColor = true;
            // 
            // chkBeepName
            // 
            this.chkBeepName.AutoSize = true;
            this.chkBeepName.Location = new System.Drawing.Point(13, 43);
            this.chkBeepName.Name = "chkBeepName";
            this.chkBeepName.Size = new System.Drawing.Size(135, 17);
            this.chkBeepName.TabIndex = 14;
            this.chkBeepName.Text = "Beep on name mention";
            this.chkBeepName.UseVisualStyleBackColor = true;
            this.chkBeepName.CheckedChanged += new System.EventHandler(this.chkBeepName_CheckedChanged);
            // 
            // chkFlashNew
            // 
            this.chkFlashNew.AutoSize = true;
            this.chkFlashNew.Location = new System.Drawing.Point(143, 20);
            this.chkFlashNew.Name = "chkFlashNew";
            this.chkFlashNew.Size = new System.Drawing.Size(165, 17);
            this.chkFlashNew.TabIndex = 13;
            this.chkFlashNew.Text = "Flash Window on New Lobby";
            this.chkFlashNew.UseVisualStyleBackColor = true;
            this.chkFlashNew.CheckedChanged += new System.EventHandler(this.chkFlashNew_CheckedChanged);
            // 
            // chkBeepNew
            // 
            this.chkBeepNew.AutoSize = true;
            this.chkBeepNew.Location = new System.Drawing.Point(13, 20);
            this.chkBeepNew.Name = "chkBeepNew";
            this.chkBeepNew.Size = new System.Drawing.Size(123, 17);
            this.chkBeepNew.TabIndex = 12;
            this.chkBeepNew.Text = "Beep on New Lobby";
            this.chkBeepNew.UseVisualStyleBackColor = true;
            this.chkBeepNew.CheckedChanged += new System.EventHandler(this.chkBeepNew_CheckedChanged);
            // 
            // gbxSettingsClientSteamDir
            // 
            this.gbxSettingsClientSteamDir.Controls.Add(this.btnSettingSteamPath);
            this.gbxSettingsClientSteamDir.Controls.Add(this.lblSettingSteamPath);
            this.gbxSettingsClientSteamDir.Location = new System.Drawing.Point(6, 138);
            this.gbxSettingsClientSteamDir.Name = "gbxSettingsClientSteamDir";
            this.gbxSettingsClientSteamDir.Size = new System.Drawing.Size(408, 54);
            this.gbxSettingsClientSteamDir.TabIndex = 10;
            this.gbxSettingsClientSteamDir.TabStop = false;
            this.gbxSettingsClientSteamDir.Text = "Steam Directory";
            // 
            // btnSettingSteamPath
            // 
            this.btnSettingSteamPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSettingSteamPath.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSettingSteamPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettingSteamPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettingSteamPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettingSteamPath.Location = new System.Drawing.Point(6, 19);
            this.btnSettingSteamPath.Name = "btnSettingSteamPath";
            this.btnSettingSteamPath.Size = new System.Drawing.Size(60, 23);
            this.btnSettingSteamPath.TabIndex = 3;
            this.btnSettingSteamPath.Text = "Change...";
            this.btnSettingSteamPath.UseVisualStyleBackColor = false;
            this.btnSettingSteamPath.Click += new System.EventHandler(this.btnSettingSteamPath_Click);
            // 
            // lblSettingSteamPath
            // 
            this.lblSettingSteamPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSettingSteamPath.Location = new System.Drawing.Point(72, 24);
            this.lblSettingSteamPath.Name = "lblSettingSteamPath";
            this.lblSettingSteamPath.Size = new System.Drawing.Size(330, 18);
            this.lblSettingSteamPath.TabIndex = 2;
            this.lblSettingSteamPath.Text = "C:\\Steam\\";
            // 
            // gbxSettingsConsoleKey
            // 
            this.gbxSettingsConsoleKey.Controls.Add(this.btnSettingConsoleKeybindManual);
            this.gbxSettingsConsoleKey.Controls.Add(this.btnSettingDota2ConsoleKeybindDetect);
            this.gbxSettingsConsoleKey.Controls.Add(this.lblSettingDota2ConsoleKeybind);
            this.gbxSettingsConsoleKey.Location = new System.Drawing.Point(6, 81);
            this.gbxSettingsConsoleKey.Name = "gbxSettingsConsoleKey";
            this.gbxSettingsConsoleKey.Size = new System.Drawing.Size(408, 51);
            this.gbxSettingsConsoleKey.TabIndex = 5;
            this.gbxSettingsConsoleKey.TabStop = false;
            this.gbxSettingsConsoleKey.Text = "Console Keybind";
            // 
            // btnSettingConsoleKeybindManual
            // 
            this.btnSettingConsoleKeybindManual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSettingConsoleKeybindManual.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSettingConsoleKeybindManual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSettingConsoleKeybindManual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSettingConsoleKeybindManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettingConsoleKeybindManual.Location = new System.Drawing.Point(322, 19);
            this.btnSettingConsoleKeybindManual.Name = "btnSettingConsoleKeybindManual";
            this.btnSettingConsoleKeybindManual.Size = new System.Drawing.Size(80, 23);
            this.btnSettingConsoleKeybindManual.TabIndex = 4;
            this.btnSettingConsoleKeybindManual.Text = "Set Manually";
            this.btnSettingConsoleKeybindManual.UseVisualStyleBackColor = false;
            this.btnSettingConsoleKeybindManual.Click += new System.EventHandler(this.btnSettingConsoleKeybindManual_Click);
            // 
            // btnSettingDota2ConsoleKeybindDetect
            // 
            this.btnSettingDota2ConsoleKeybindDetect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSettingDota2ConsoleKeybindDetect.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSettingDota2ConsoleKeybindDetect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettingDota2ConsoleKeybindDetect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettingDota2ConsoleKeybindDetect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettingDota2ConsoleKeybindDetect.Location = new System.Drawing.Point(6, 19);
            this.btnSettingDota2ConsoleKeybindDetect.Name = "btnSettingDota2ConsoleKeybindDetect";
            this.btnSettingDota2ConsoleKeybindDetect.Size = new System.Drawing.Size(60, 23);
            this.btnSettingDota2ConsoleKeybindDetect.TabIndex = 3;
            this.btnSettingDota2ConsoleKeybindDetect.Text = "Detect...";
            this.btnSettingDota2ConsoleKeybindDetect.UseVisualStyleBackColor = false;
            this.btnSettingDota2ConsoleKeybindDetect.Click += new System.EventHandler(this.btnSettingDota2ConsoleKeybindDetect_Click);
            // 
            // lblSettingDota2ConsoleKeybind
            // 
            this.lblSettingDota2ConsoleKeybind.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblSettingDota2ConsoleKeybind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSettingDota2ConsoleKeybind.Location = new System.Drawing.Point(72, 24);
            this.lblSettingDota2ConsoleKeybind.Name = "lblSettingDota2ConsoleKeybind";
            this.lblSettingDota2ConsoleKeybind.Size = new System.Drawing.Size(330, 18);
            this.lblSettingDota2ConsoleKeybind.TabIndex = 2;
            this.lblSettingDota2ConsoleKeybind.Text = "DEL";
            // 
            // lblSettingsClientStatus
            // 
            this.lblSettingsClientStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblSettingsClientStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSettingsClientStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingsClientStatus.Location = new System.Drawing.Point(6, 383);
            this.lblSettingsClientStatus.Name = "lblSettingsClientStatus";
            this.lblSettingsClientStatus.Size = new System.Drawing.Size(408, 37);
            this.lblSettingsClientStatus.TabIndex = 4;
            this.lblSettingsClientStatus.Text = "Setup Complete";
            this.lblSettingsClientStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxSettingsClientLocation
            // 
            this.gbxSettingsClientLocation.Controls.Add(this.btnSettingsClientLocationChange);
            this.gbxSettingsClientLocation.Controls.Add(this.lblDota2ClientLocation);
            this.gbxSettingsClientLocation.Location = new System.Drawing.Point(6, 24);
            this.gbxSettingsClientLocation.Name = "gbxSettingsClientLocation";
            this.gbxSettingsClientLocation.Size = new System.Drawing.Size(408, 51);
            this.gbxSettingsClientLocation.TabIndex = 2;
            this.gbxSettingsClientLocation.TabStop = false;
            this.gbxSettingsClientLocation.Text = "Dota 2 Directory";
            // 
            // btnSettingsClientLocationChange
            // 
            this.btnSettingsClientLocationChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSettingsClientLocationChange.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSettingsClientLocationChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettingsClientLocationChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettingsClientLocationChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettingsClientLocationChange.Location = new System.Drawing.Point(6, 19);
            this.btnSettingsClientLocationChange.Name = "btnSettingsClientLocationChange";
            this.btnSettingsClientLocationChange.Size = new System.Drawing.Size(60, 23);
            this.btnSettingsClientLocationChange.TabIndex = 3;
            this.btnSettingsClientLocationChange.Text = "Change...";
            this.btnSettingsClientLocationChange.UseVisualStyleBackColor = false;
            this.btnSettingsClientLocationChange.Click += new System.EventHandler(this.btnSettingsClientLocationChange_Click);
            // 
            // lblDota2ClientLocation
            // 
            this.lblDota2ClientLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDota2ClientLocation.Location = new System.Drawing.Point(72, 24);
            this.lblDota2ClientLocation.Name = "lblDota2ClientLocation";
            this.lblDota2ClientLocation.Size = new System.Drawing.Size(330, 18);
            this.lblDota2ClientLocation.TabIndex = 2;
            this.lblDota2ClientLocation.Text = "C:\\Dota2\\";
            // 
            // tabServerWizard
            // 
            this.tabServerWizard.BackColor = System.Drawing.SystemColors.Control;
            this.tabServerWizard.Controls.Add(this.ServerToSettingsbutton);
            this.tabServerWizard.Controls.Add(this.forwardButton);
            this.tabServerWizard.Controls.Add(this.detectButton);
            this.tabServerWizard.Controls.Add(this.textPort);
            this.tabServerWizard.Controls.Add(this.textIP);
            this.tabServerWizard.Controls.Add(this.cancelButton);
            this.tabServerWizard.Controls.Add(this.createButton);
            this.tabServerWizard.Controls.Add(this.progressBar);
            this.tabServerWizard.Controls.Add(this.logText);
            this.tabServerWizard.Controls.Add(this.label1);
            this.tabServerWizard.Location = new System.Drawing.Point(4, 5);
            this.tabServerWizard.Name = "tabServerWizard";
            this.tabServerWizard.Padding = new System.Windows.Forms.Padding(3);
            this.tabServerWizard.Size = new System.Drawing.Size(864, 513);
            this.tabServerWizard.TabIndex = 9;
            this.tabServerWizard.Text = "Extractor";
            // 
            // ServerToSettingsbutton
            // 
            this.ServerToSettingsbutton.Location = new System.Drawing.Point(6, 481);
            this.ServerToSettingsbutton.Name = "ServerToSettingsbutton";
            this.ServerToSettingsbutton.Size = new System.Drawing.Size(130, 29);
            this.ServerToSettingsbutton.TabIndex = 18;
            this.ServerToSettingsbutton.Text = "Back";
            this.ServerToSettingsbutton.UseVisualStyleBackColor = true;
            this.ServerToSettingsbutton.Click += new System.EventHandler(this.button3_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.Location = new System.Drawing.Point(552, 482);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(75, 20);
            this.forwardButton.TabIndex = 17;
            this.forwardButton.Text = "Portforward";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // detectButton
            // 
            this.detectButton.Location = new System.Drawing.Point(552, 456);
            this.detectButton.Name = "detectButton";
            this.detectButton.Size = new System.Drawing.Size(75, 20);
            this.detectButton.TabIndex = 16;
            this.detectButton.Text = "Detect IP";
            this.detectButton.UseVisualStyleBackColor = true;
            this.detectButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(633, 482);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(124, 20);
            this.textPort.TabIndex = 15;
            this.textPort.Text = "Port";
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(633, 456);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(124, 20);
            this.textIP.TabIndex = 14;
            this.textIP.Text = "IP";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(774, 482);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(84, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.OnCancel);
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(774, 453);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(84, 23);
            this.createButton.TabIndex = 12;
            this.createButton.Text = "Create Server";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(5, 424);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(853, 23);
            this.progressBar.TabIndex = 11;
            // 
            // logText
            // 
            this.logText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logText.BackColor = System.Drawing.SystemColors.Window;
            this.logText.Location = new System.Drawing.Point(5, 35);
            this.logText.Multiline = true;
            this.logText.Name = "logText";
            this.logText.ReadOnly = true;
            this.logText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logText.Size = new System.Drawing.Size(853, 383);
            this.logText.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(266, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Custom Realms Server Wizard";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabMod
            // 
            this.tabMod.BackColor = System.Drawing.SystemColors.Control;
            this.tabMod.Controls.Add(this.btnBackMods);
            this.tabMod.Controls.Add(this.btnLoadMods);
            this.tabMod.Controls.Add(this.grdMods);
            this.tabMod.Location = new System.Drawing.Point(4, 5);
            this.tabMod.Name = "tabMod";
            this.tabMod.Padding = new System.Windows.Forms.Padding(3);
            this.tabMod.Size = new System.Drawing.Size(864, 513);
            this.tabMod.TabIndex = 10;
            this.tabMod.Text = "Mods";
            // 
            // btnBackMods
            // 
            this.btnBackMods.Location = new System.Drawing.Point(11, 475);
            this.btnBackMods.Name = "btnBackMods";
            this.btnBackMods.Size = new System.Drawing.Size(70, 30);
            this.btnBackMods.TabIndex = 2;
            this.btnBackMods.Text = "Back";
            this.btnBackMods.UseVisualStyleBackColor = true;
            this.btnBackMods.Click += new System.EventHandler(this.btnBackMods_Click);
            // 
            // btnLoadMods
            // 
            this.btnLoadMods.Location = new System.Drawing.Point(772, 475);
            this.btnLoadMods.Name = "btnLoadMods";
            this.btnLoadMods.Size = new System.Drawing.Size(86, 30);
            this.btnLoadMods.TabIndex = 1;
            this.btnLoadMods.Text = "Load Mods";
            this.btnLoadMods.UseVisualStyleBackColor = true;
            this.btnLoadMods.Click += new System.EventHandler(this.btnLoadMods_Click);
            // 
            // grdMods
            // 
            this.grdMods.AllowUserToAddRows = false;
            this.grdMods.AllowUserToDeleteRows = false;
            this.grdMods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMods.Location = new System.Drawing.Point(3, 23);
            this.grdMods.Name = "grdMods";
            this.grdMods.ReadOnly = true;
            this.grdMods.Size = new System.Drawing.Size(855, 446);
            this.grdMods.TabIndex = 0;
            this.grdMods.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdMods_CellContentClick);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tabUISections);
            this.pnlMain.Controls.Add(this.gbxChat);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 119);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1165, 523);
            this.pnlMain.TabIndex = 1;
            // 
            // dataMods
            // 
            this.dataMods.DataSetName = "dataMods";
            // 
            // timerDediCount
            // 
            this.timerDediCount.Interval = 1000;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnConnectIRC;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 642);
            this.Controls.Add(this.lblPlayersInGame);
            this.Controls.Add(this.lblPlayersOnline);
            this.Controls.Add(this.pbxBanner);
            this.Controls.Add(this.lblVersionSubtitle);
            this.Controls.Add(this.lblMessageLeft);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.webAdvertisement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Dota 2 Custom Realms";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBanner)).EndInit();
            this.gbxChat.ResumeLayout(false);
            this.gbxChat.PerformLayout();
            this.tabUISections.ResumeLayout(false);
            this.tabPreConnect.ResumeLayout(false);
            this.gbxConnect.ResumeLayout(false);
            this.gbxConnect.PerformLayout();
            this.tabConnected.ResumeLayout(false);
            this.tabHostLobby.ResumeLayout(false);
            this.tabHostLobby.PerformLayout();
            this.gbxCustomMod.ResumeLayout(false);
            this.gbxCustomMod.PerformLayout();
            this.gbxAdditional.ResumeLayout(false);
            this.gbxAdditional.PerformLayout();
            this.gmxGameMode.ResumeLayout(false);
            this.gbxMap.ResumeLayout(false);
            this.gbxLobbySize.ResumeLayout(false);
            this.gbxGamePassword.ResumeLayout(false);
            this.gbxGamePassword.PerformLayout();
            this.gbxSkillMode.ResumeLayout(false);
            this.gbxGameName.ResumeLayout(false);
            this.gbxGameName.PerformLayout();
            this.gbxHeroMode.ResumeLayout(false);
            this.tabJoin.ResumeLayout(false);
            this.tabJoin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdGamesList)).EndInit();
            this.tabLobby.ResumeLayout(false);
            this.tabLobby.PerformLayout();
            this.gbxGameInfo.ResumeLayout(false);
            this.gbxGameInfo.PerformLayout();
            this.gbxLobbySpectators.ResumeLayout(false);
            this.gbxLobbyDire.ResumeLayout(false);
            this.gbxLobbyRadiant.ResumeLayout(false);
            this.tabDraftHeroPick.ResumeLayout(false);
            this.tabDraftHeroPick.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftRadiantPicks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftDirePicks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickDire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickRadiant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickIntelligence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickAgility)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDraftHeroPickStrength)).EndInit();
            this.gbxDraftHeroPickTimeRemaining.ResumeLayout(false);
            this.tabSkillDraft.ResumeLayout(false);
            this.tabSkillDraft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSkillDraftYourHero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSkillRadiantPicks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSkillDirePicks)).EndInit();
            this.gbxSkillDraftTimeRemaining.ResumeLayout(false);
            this.tabDraftSummary.ResumeLayout(false);
            this.tabDraftSummary.PerformLayout();
            this.gbxConfiguringMod.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.gbxSettingsServer.ResumeLayout(false);
            this.gbxServerOther.ResumeLayout(false);
            this.gbxServerOther.PerformLayout();
            this.gbxBanList.ResumeLayout(false);
            this.gbxBanList.PerformLayout();
            this.gbxSettingServerPort.ResumeLayout(false);
            this.gbxSettingServerPort.PerformLayout();
            this.gbxSettingsServerLocation.ResumeLayout(false);
            this.gbxSettingsClient.ResumeLayout(false);
            this.gbxClientOther.ResumeLayout(false);
            this.gbxClientOther.PerformLayout();
            this.gbxSettingsClientSteamDir.ResumeLayout(false);
            this.gbxSettingsConsoleKey.ResumeLayout(false);
            this.gbxSettingsClientLocation.ResumeLayout(false);
            this.tabServerWizard.ResumeLayout(false);
            this.tabServerWizard.PerformLayout();
            this.tabMod.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMods)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataMods)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webAdvertisement;
        private System.Windows.Forms.Timer ircListener;
        private System.Windows.Forms.Label lblMessageLeft;
        private System.Windows.Forms.ToolTip ttpHeroDraftTooltips;
        private System.Windows.Forms.ToolTip ttpSkillDraftTooltips;
        private System.ComponentModel.BackgroundWorker bgwGenerateNpcHeroesAutoexec;
        private System.Windows.Forms.OpenFileDialog ofdFindDotaExe;
        private System.Windows.Forms.Label lblVersionSubtitle;
        private System.Windows.Forms.OpenFileDialog ofdFindSrcdsExe;
        private System.Windows.Forms.PictureBox pbxBanner;
        private System.Windows.Forms.OpenFileDialog ofdFindSteam;
        private System.Windows.Forms.NotifyIcon icoNotifyDraftTurn;
        private System.Windows.Forms.Label lblPlayersOnline;
        private System.Windows.Forms.Timer timerPlayers;
        private System.Windows.Forms.Label lblPlayersInGame;
        private System.Windows.Forms.GroupBox gbxChat;
        private System.Windows.Forms.TabControl gbxGameSize;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox tbxChatMessage;
        private System.Windows.Forms.TabControl tabUISections;
        private System.Windows.Forms.TabPage tabPreConnect;
        private System.Windows.Forms.GroupBox gbxConnect;
        private System.Windows.Forms.Button btnConnectIRC;
        private System.Windows.Forms.TextBox tbxChooseNick;
        private System.Windows.Forms.Label lblChooseNick;
        private System.Windows.Forms.TabPage tabConnected;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnFindLobby;
        private System.Windows.Forms.Button btnHostLobby;
        private System.Windows.Forms.TabPage tabHostLobby;
        private System.Windows.Forms.GroupBox gbxAdditional;
        private System.Windows.Forms.CheckBox cbxAllTalk;
        private System.Windows.Forms.CheckBox cbxWTF;
        private System.Windows.Forms.GroupBox gmxGameMode;
        private System.Windows.Forms.ComboBox cbxGameMode;
        private System.Windows.Forms.GroupBox gbxMap;
        private System.Windows.Forms.ComboBox cbxMap;
        private System.Windows.Forms.GroupBox gbxLobbySize;
        private System.Windows.Forms.ComboBox cbxGameSize;
        private System.Windows.Forms.GroupBox gbxGamePassword;
        private System.Windows.Forms.TextBox tbxGamePassword;
        private System.Windows.Forms.Button btnCancelHosting;
        private System.Windows.Forms.Button btnHostGame;
        private System.Windows.Forms.GroupBox gbxSkillMode;
        private System.Windows.Forms.RadioButton radSkillDraft;
        private System.Windows.Forms.RadioButton radSkillRandom;
        private System.Windows.Forms.GroupBox gbxGameName;
        private System.Windows.Forms.TextBox tbxGameName;
        private System.Windows.Forms.GroupBox gbxHeroMode;
        private System.Windows.Forms.RadioButton radHeroDraft;
        private System.Windows.Forms.RadioButton radHeroRandom;
        private System.Windows.Forms.RadioButton radHeroPick;
        private System.Windows.Forms.Label lblHostLobbyMessage;
        private System.Windows.Forms.TabPage tabJoin;
        private System.Windows.Forms.CheckBox cbxLocked;
        private System.Windows.Forms.Label lblJoinAttemptText;
        private System.Windows.Forms.Label lblGameListRefresh;
        private System.Windows.Forms.Button btnCancelJoining;
        private System.Windows.Forms.Button btnGameListRefresh;
        private System.Windows.Forms.DataGridView grdGamesList;
        private System.Windows.Forms.TabPage tabLobby;
        private System.Windows.Forms.GroupBox gbxGameInfo;
        private System.Windows.Forms.Label lblAdditional;
        private System.Windows.Forms.Label labelMaxPlayers;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.Label labelHeroSkills;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.CheckBox chkLobbyPlayerReady;
        private System.Windows.Forms.Button btnLobbyKick;
        private System.Windows.Forms.Button btnLobbyRandomiseTeams;
        private System.Windows.Forms.Label lblLobbyPlayerReadyCount;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnLobbyLeave;
        private System.Windows.Forms.Label lblLobbyVersus;
        private System.Windows.Forms.GroupBox gbxLobbySpectators;
        private System.Windows.Forms.ListBox lbxLobbySpectators;
        private System.Windows.Forms.GroupBox gbxLobbyDire;
        private System.Windows.Forms.Button btnJoinDire;
        private System.Windows.Forms.ListBox lbxLobbyDirePlayers;
        private System.Windows.Forms.GroupBox gbxLobbyRadiant;
        private System.Windows.Forms.Button btnJoinRadiant;
        private System.Windows.Forms.ListBox lbxLobbyRadiantPlayers;
        private System.Windows.Forms.Label lblLobbyName;
        private System.Windows.Forms.TabPage tabDraftHeroPick;
        private System.Windows.Forms.Label lblHeroDraftPickingOrder;
        private System.Windows.Forms.Label lblHeroDraftPicks;
        private System.Windows.Forms.PictureBox pbxDraftRadiantPicks;
        private System.Windows.Forms.PictureBox pbxDraftDirePicks;
        private System.Windows.Forms.FlowLayoutPanel floDraftDireHeroes;
        private System.Windows.Forms.Label lblDraftPlayerTurnText;
        private System.Windows.Forms.FlowLayoutPanel floDraftRadiantHeroes;
        private System.Windows.Forms.PictureBox pbxDraftHeroPickDire;
        private System.Windows.Forms.PictureBox pbxDraftHeroPickRadiant;
        private System.Windows.Forms.PictureBox pbxDraftHeroPickIntelligence;
        private System.Windows.Forms.PictureBox pbxDraftHeroPickAgility;
        private System.Windows.Forms.PictureBox pbxDraftHeroPickStrength;
        private System.Windows.Forms.GroupBox gbxDraftHeroPickTimeRemaining;
        private System.Windows.Forms.Label lblDraftHeroPickTimeRemaining;
        private System.Windows.Forms.FlowLayoutPanel floDraftPlayerOrder;
        private System.Windows.Forms.Label lblHeroSelectionDraft;
        private System.Windows.Forms.FlowLayoutPanel floDraftHeroRadiantIntelligence;
        private System.Windows.Forms.FlowLayoutPanel floDraftHeroDireIntelligence;
        private System.Windows.Forms.FlowLayoutPanel floDraftHeroDireAgility;
        private System.Windows.Forms.FlowLayoutPanel floDraftHeroRadiantAgility;
        private System.Windows.Forms.FlowLayoutPanel floDraftHeroDireStrength;
        private System.Windows.Forms.FlowLayoutPanel floDraftHeroRadiantStrength;
        private System.Windows.Forms.TabPage tabSkillDraft;
        private System.Windows.Forms.Button btnLeaveSkills;
        private System.Windows.Forms.PictureBox pbxSkillDraftYourHero;
        private System.Windows.Forms.Label lblSkillDraftSelection;
        private System.Windows.Forms.FlowLayoutPanel floSkillDirePicks;
        private System.Windows.Forms.FlowLayoutPanel floSkillRadiantPicks;
        private System.Windows.Forms.Label lblSkillDraftPickingOrder;
        private System.Windows.Forms.Label lblSkillDraftPicks;
        private System.Windows.Forms.PictureBox pbxSkillRadiantPicks;
        private System.Windows.Forms.PictureBox pbxSkillDirePicks;
        private System.Windows.Forms.Label lblSkillDraftPlayerTurn;
        private System.Windows.Forms.GroupBox gbxSkillDraftTimeRemaining;
        private System.Windows.Forms.Label lblSkillDraftTimeRemaining;
        private System.Windows.Forms.FlowLayoutPanel floSKillDraftPickingOrder;
        private System.Windows.Forms.FlowLayoutPanel floSkillDraftUltimateSkills;
        private System.Windows.Forms.Label lblSkillDraftUltimates;
        private System.Windows.Forms.Label lblSkillDraftNormal;
        private System.Windows.Forms.FlowLayoutPanel floSkillDraftNormalSkills;
        private System.Windows.Forms.TabPage tabDraftSummary;
        private System.Windows.Forms.GroupBox gbxConfiguringMod;
        private System.Windows.Forms.Button btnManualConnect;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Label lblConfigProgressMessage;
        private System.Windows.Forms.ProgressBar pgbConfigProgress;
        private System.Windows.Forms.FlowLayoutPanel floDireTeamSummary;
        private System.Windows.Forms.Label lblDraftDireTeamPicks;
        private System.Windows.Forms.FlowLayoutPanel floRadiantTeamSummary;
        private System.Windows.Forms.Label lblDraftRadiantTeamPicks;
        private System.Windows.Forms.FlowLayoutPanel floPersonalSummary;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Button btnSettingsSaveReturn;
        private System.Windows.Forms.GroupBox gbxSettingsServer;
        private System.Windows.Forms.GroupBox gbxBanList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBanLoad;
        private System.Windows.Forms.Button btnBanSave;
        private System.Windows.Forms.TextBox tbxBans;
        private System.Windows.Forms.GroupBox gbxSettingServerPort;
        private System.Windows.Forms.TextBox tbxSettingServerPort;
        private System.Windows.Forms.Label lblSettingServerPort;
        private System.Windows.Forms.Button btnSettingServerPort;
        private System.Windows.Forms.Label lblSettingsServerStatus;
        private System.Windows.Forms.GroupBox gbxSettingsServerLocation;
        private System.Windows.Forms.Button btnSettingServerInstallationWizard;
        private System.Windows.Forms.Label lblSettingServerOptions;
        private System.Windows.Forms.Button btnSettingsServerLocationChange;
        private System.Windows.Forms.Label lblDota2ServerLocation;
        private System.Windows.Forms.GroupBox gbxSettingsClient;
        private System.Windows.Forms.GroupBox gbxSettingsClientSteamDir;
        private System.Windows.Forms.Button btnSettingSteamPath;
        private System.Windows.Forms.Label lblSettingSteamPath;
        private System.Windows.Forms.GroupBox gbxSettingsConsoleKey;
        private System.Windows.Forms.Button btnSettingConsoleKeybindManual;
        private System.Windows.Forms.Button btnSettingDota2ConsoleKeybindDetect;
        private System.Windows.Forms.Label lblSettingDota2ConsoleKeybind;
        private System.Windows.Forms.Label lblSettingsClientStatus;
        private System.Windows.Forms.GroupBox gbxSettingsClientLocation;
        private System.Windows.Forms.Button btnSettingsClientLocationChange;
        private System.Windows.Forms.Label lblDota2ClientLocation;
        private System.Windows.Forms.TabPage tabServerWizard;
        private System.Windows.Forms.Button ServerToSettingsbutton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button detectButton;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox logText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnBanURLLoad;
        private System.Windows.Forms.GroupBox gbxServerOther;
        private System.Windows.Forms.CheckBox chkConDebug;
        private System.Windows.Forms.ToolTip ttpWTF;
        private System.Windows.Forms.ToolTip ttpX2;
        private System.Windows.Forms.CheckBox cbxX2;
        private System.Windows.Forms.GroupBox gbxCustomMod;
        private System.Windows.Forms.Label lblCustomWarn;
        private System.Windows.Forms.CheckBox chkCustomEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGameName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSkillsHeroes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlayers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomMod;
        private System.Windows.Forms.CheckBox chkBalanced;
        private System.Windows.Forms.ComboBox tbxCustomMod;
        private System.Windows.Forms.CheckBox cbxVersionFixDisable;
        private System.Windows.Forms.GroupBox gbxClientOther;
        private System.Windows.Forms.CheckBox chkFlashNew;
        private System.Windows.Forms.CheckBox chkBeepNew;
        private System.Windows.Forms.CheckBox chkBeepName;
        private System.Windows.Forms.CheckBox chkFlashName;
        private System.Windows.Forms.TabPage tabMod;
        private System.Windows.Forms.DataGridView grdMods;
        private System.Data.DataSet dataMods;
        private System.Windows.Forms.Button btnLoadMods;
        private System.Windows.Forms.Button btnModDL;
        private System.Windows.Forms.Button btnBackMods;
        private System.Windows.Forms.CheckBox chkDedicated;
        private System.Windows.Forms.Timer timerDediCount;
    }
}