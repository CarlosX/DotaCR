using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dota2CustomRealms
{
    class GameSkipPicks : Game
    {

        public override void ProgressGameStage()
        {
            // If not these game modes, skip hero and skill drafting
            if (GameMode != GameMode.OMG && GameMode != GameMode.LOD && GameMode != GameMode.OMG_Balanced)
            {
                if (Stage == GameStage.Lobby)
                {
                    Stage = GameStage.ServerSetup;
                    if (IsHost)
                    {
                        RaiseSendChannelNotice(this, new SendMessageEventArgs(Channel, GAME_PREGAME));
                    }
                    RaiseUpdateUI(this, new EventArgs());
                    RaiseGameEvent(this, new GameEventArgs(GAME_PREGAME, true));
                }
                else
                {
                    throw new NotImplementedException();
                }
                return;
            }
        }

        public override void ReceiveChannelNotice(string Channel, string SendingPlayer, string Message)
        {
            if (Message.StartsWith(GAME_PREGAME) && SendingPlayer == HostName)
            {
                if (Stage == GameStage.Lobby)
                {
                    ProgressGameStage();
                }
            }
            else
            {
                base.ReceiveChannelNotice(Channel, SendingPlayer, Message);
            }
        }

        public GameSkipPicks(GameMode GameMode, HeroMode HeroMode, SkillMode SkillMode, List<String> AdditionalModes)
            : base(GameMode, HeroMode, SkillMode, AdditionalModes)
        {
        }

        public GameSkipPicks()
            : base()
        {
        }

    }
}