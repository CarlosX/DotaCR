using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KVLib;
using System.IO;

namespace Tinker
{

    public class Addon
    {

        private string BaseFolder;

        private const string ConfigFile = "DotaCR.txt";

        /// <summary>
        /// Interprets addon located in folder BasePath
        /// </summary>
        /// <param name="BasePath">Folder containing Dota 2 addon</param>
        public Addon(string BasePath)
        {
            if (!BasePath.EndsWith("\\")) BasePath = BasePath + "\\";

            this.BaseFolder = BasePath;

            if (!Directory.Exists(BasePath))
            {
                throw new DirectoryNotFoundException("Addon directory doesn't exist!");
            }

            if (!File.Exists(BasePath + ConfigFile)) // TODO: Attempt to autoconfig or download appropriate config from DotaCR servers
            {
                throw new FileNotFoundException("Addon configuration file doesn't exist!");
            }

            KeyValue[] Data = KVParser.ParseAllKVRootNodes(File.ReadAllText(BasePath + ConfigFile));

            // So far it compiles, that's gotta be a plus!

        }

    }

}
