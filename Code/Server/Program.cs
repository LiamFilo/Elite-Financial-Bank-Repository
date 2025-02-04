using Server.Classes;
using System.Windows.Input;
using BankingCommunication;
using ICommand = BankingCommunication.ICommand;
using System.Threading.Channels;
using System.Data;


namespace Server
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            BCPViewModel bcpVM = ServerViewModel.Instance.BCPManagerVM;
            ModelManagemetViewModel modelManagemetVM = ServerViewModel.Instance.ModelManagementVM;
            Request currRequestToProcess;
            ICommand currCommandToExecute;
            string currUserID;

            bcpVM.StartListening();

            //Server Loop
            while (bcpVM.KeepListening)
            {
                //Handle Packets
                if (modelManagemetVM.PriorityQueue.IsEmptyQueue())
                    continue;

                Console.WriteLine("handle requests");
                currRequestToProcess = modelManagemetVM.PriorityQueue.GetNextRequest();
                currCommandToExecute = currRequestToProcess.CommandToExecute;
                currUserID = currRequestToProcess.UserID;
                ServerViewModel.Instance.CommandProcessingVM.HandleCommand(currCommandToExecute, currUserID);


            }
        }
    }
}
