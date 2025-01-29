using Server.Classes;

namespace Server
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ServerConnection serverConnection = new ServerConnection();
            await serverConnection.StartAsync();

            //Server Loop
            while (serverConnection.IsRunning)
            {
                if(ModelManagemetViewModel.)
            }
        }
    }
}
