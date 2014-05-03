/**
 * Dota 2 Dedicated Server
 * Written by ilian000
 * Based on Dota 2 Custom Realms IRC Code
 */
using System;
using System.Reflection;
using DedicatedServer;

namespace DedicatedServer
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            DedicatedServer d = new DedicatedServer();
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Returns assembly which is required by the application
            switch (args.Name)
            {
                case "Meebey.SmartIrc4net, Version=0.4.0.34161, Culture=neutral, PublicKeyToken=null":
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DedicatedServer.Meebey.SmartIrc4net.dll"))
                    {
                        byte[] assemblyData = new byte[stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);
                        return Assembly.Load(assemblyData);
                    }
                default:
                    return null;
            }

        }
    }
}
