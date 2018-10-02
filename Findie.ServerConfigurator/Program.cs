using System;
using Findie.ServerConfigurator.FindieLogger;

namespace Findie.ServerConfigurator
{
   public class Program
    {
        static void Main(string[] args)
        {
            Logger.Log("Starting server", LoggerMessages.Info);

            try
            {
                FindieServer.Program.Main(null);
            }
            catch (Exception ex)
            {
                Logger.Log($"Can't run the server! {ex.Message}", LoggerMessages.Error);
            }        
        }
    }
}