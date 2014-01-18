using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dota2CustomRealms
{
    class Player
    {
        private PlayerSide side;

        private bool hasMod = true;

        public bool HasMod
        {
            get { return hasMod; }
            set { hasMod = value; }
        }

        public PlayerSide Side
        {
          get { return side; }
          set { side = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string FriendlyName
        {
            get { return name.Replace("_", " "); }
        }

        private bool ready;

        public bool Ready
        {
            get { return ready; }
            set { ready = value; }
        }

        private bool dedi = false;

        public bool Dedi
        {
            get { return dedi; }
            set { dedi = value; }
        }

        public override string ToString()
        {
            return FriendlyName + (ready ? " - READY" : "") + (hasMod ? "" : " - NEEDS MOD") + (dedi ? " - HOST" : "");
        }
    }

}
