using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using WebSocket4Net;
using Newtonsoft.Json;


namespace Clockwerk
{

    public class RealmConnector
    {

        public const string GLOBAL_CHANNEL = "";

        public event EventHandler OnConnect;
        public event EventHandler<ServerResponse> OnChatMessage;
        public event EventHandler<ServerResponse> OnAuthenticationFailure;
        public event EventHandler<ServerResponse> OnAuthenticationSuccess;

        public event EventHandler<ServerResponse> OnUserJoinChannel;
        public event EventHandler<ServerResponse> OnUserPartChannel;

        public event EventHandler<ServerResponse> OnUserConnect;
        public event EventHandler<ServerResponse> OnUserDisconnect;

        public event EventHandler<ServerResponse> OnDeprecatedNotice;

        public event EventHandler OnDisconnect;
        public event EventHandler<ClockwerkError> OnError;
        
        private WebSocket client;

        private string nickname;

        public string Nickname
        {
            get { return nickname; }
            protected set { nickname = value; }
        }

        public RealmConnector(string Server = "game.windrunner.mx", int Port = 8000)
        {
            client = new WebSocket("ws://" + Server + ":" + Port.ToString() +  "/");
            client.Opened += client_Opened;
            client.Closed += client_Closed;
            client.Error += client_Error;
            client.MessageReceived += client_MessageReceived;
        }

        void client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            ServerResponse resp = ServerResponse.Parse(e.Message);

            switch (resp.Type)
            {
                case ServerResponse.ServerResponseType.Chat:
                    {
                        if (OnChatMessage != null) OnChatMessage(this, resp);
                        break;
                    }
                case ServerResponse.ServerResponseType.NicknameInvalid:
                    {
                        if (OnAuthenticationFailure != null) OnAuthenticationFailure(this, resp);
                        break;
                    }
                case ServerResponse.ServerResponseType.NicknameValid:
                    {
                        if (OnAuthenticationSuccess != null) OnAuthenticationSuccess(this, resp);
                        break;
                    }
                case ServerResponse.ServerResponseType.DeprecatedNoticeSignalling:
                    {
                        if (OnDeprecatedNotice != null) OnDeprecatedNotice(this, resp);
                        break;
                    }
                case ServerResponse.ServerResponseType.Join:
                    {
                        if (resp.Target == RealmConnector.GLOBAL_CHANNEL)
                        {
                            if (OnUserConnect != null) OnUserConnect(this, resp);
                        }
                        else
                        {
                            if (OnUserJoinChannel != null) OnUserJoinChannel(this, resp);
                        }
                        break;
                    }
                case ServerResponse.ServerResponseType.Part:
                    {
                        if (resp.Target == RealmConnector.GLOBAL_CHANNEL)
                        {
                            if (OnUserDisconnect != null) OnUserDisconnect(this, resp);
                        }
                        else
                        {
                            if (OnUserPartChannel != null) OnUserPartChannel(this, resp);
                        }
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException("Server Response Not Implemented: " + resp.Type.ToString());
                    }
            }

        }

        public void SendChatMessage(string Message, string Target = RealmConnector.GLOBAL_CHANNEL)
        {
            Dictionary<string, string> Output = new Dictionary<string, string>();
            Output.Add("Type", ServerRequestType.Chat.ToString());
            Output.Add("Msg", Message);
            if (Target != RealmConnector.GLOBAL_CHANNEL) Output.Add("To", Target);
            try
            {
                client.Send(JsonConvert.SerializeObject(Output));
            }
            catch(Exception ex)
            {
                if (OnError != null) OnError(this, new ClockwerkError(ex));
            }
        }

        public void Connect(string Name)
        {
            this.Nickname = Name;
            client.Open();
        }

        void client_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            if (OnError != null) OnError(this, new ClockwerkError(e.Exception));
        }

        void client_Closed(object sender, EventArgs e)
        {
            if (OnDisconnect != null) OnDisconnect(this, e);
        }

        void client_Opened(object sender, EventArgs e)
        {
            if (OnConnect != null) OnConnect(this, new EventArgs());
            client.Send(JsonConvert.SerializeObject(new Dictionary<string, string> { 
            { "Type", ServerRequestType.RequestNick.ToString() },
            { "Nick", this.Nickname }
            }));
        }

        enum ServerRequestType
        {
            RequestNick,
            Chat
        }
    }

    public class ClockwerkError : EventArgs
    {
        public Exception Error;

        public ClockwerkError(Exception Err)
        {
            Error = Err;
        }
    }
}
