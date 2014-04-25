using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Clockwerk
{

    public class ServerResponse : EventArgs
    {

        private ServerResponseType MessageType;
        private string message, source, target;

        public string Target
        {
            get { return target; }
            protected set { target = value; }
        }
        private bool admin, emote;

        public ServerResponseType Type
        {
            get { return MessageType; }
            protected set { MessageType = value; }
        }

        public string Message
        {
            get { return message; }
            protected set { message = value; }
        }

        public string Source
        {
            get { return source; }
            protected set { source = value; }
        }


        /// <summary>
        /// Type of message from the server
        /// </summary>
        public enum ServerResponseType
        {
            /// <summary>
            /// Nickname is available and has been assigned to you
            /// </summary>
            NicknameValid,
            /// <summary>
            /// Nickname is not available
            /// </summary>
            NicknameInvalid,
            /// <summary>
            /// Chat message
            /// </summary>
            Chat,
            /// <summary>
            /// A player has joined a chat channel. If channel is equal to constant GLOBAL_CHANNEL, then they have joined the server
            /// </summary>
            Join,
            /// <summary>
            /// A player has left a chat channel. If channel is equal to constant GLOBAL_CHANNEL, then they have left the server
            /// </summary>
            Part,
            /// <summary>
            /// Errors
            /// </summary>
            Err
        }

        protected ServerResponse()
        {
        }

        /// <summary>
        /// Parse raw server message
        /// </summary>
        /// <param name="JSON">Raw JSON Response from server</param>
        /// <returns></returns>
        public static ServerResponse Parse(string JSON)
        {
            RawServerResponse resp = RawServerResponse.FromJson(JSON);
            ServerResponse res = new ServerResponse();

            if(!Enum.IsDefined(typeof(ServerResponseType), resp.Type)) throw new NotImplementedException("Unimplemented Server Response: " + resp.Type);

            res.Type = (ServerResponseType)Enum.Parse(typeof(ServerResponseType), resp.Type);

            switch(res.Type)
            {
                case ServerResponseType.NicknameValid:
                case ServerResponseType.NicknameInvalid:
                    {
                        // No additional arguments
                        break;
                    }
                case ServerResponseType.Chat:
                case ServerResponseType.Join:
                case ServerResponseType.Part:
                case ServerResponseType.Err:
                    {
                        res.Source = resp.From;
                        res.Target = resp.To;
                        res.Message = resp.Msg;
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException("Server Response Type Not Implemented: " + res.Type.ToString());
                    }
            }

            return res;
        }

        protected class RawServerResponse
        {
            public string Msg, Data, Type, User;
            public string From = "", To = "";
            bool Admin = false, Emote = false;


            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static RawServerResponse FromJson(string value)
            {
                return JsonConvert.DeserializeObject<RawServerResponse>(value);
            }

        }


    }
}
