using Server.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerViewModel
    {
        private static ServerViewModel _instance;

        private static readonly object _lock = new object();

        public readonly DataBaseViewModel UserInteractionVM;
        public readonly BCPViewModel BCPManagerVM;
        public readonly CommandProcessingViewModel CommandProcessingVM;
        public readonly ModelManagemetViewModel ModelManagementVM;

        private ServerViewModel()
        {
            UserInteractionVM = new DataBaseViewModel();
            BCPManagerVM = new BCPViewModel();
            CommandProcessingVM = new CommandProcessingViewModel();
            ModelManagementVM = new ModelManagemetViewModel();
        }

        public static ServerViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServerViewModel();
                        }
                    }
                }
                return _instance;
            }
        }
    }

}
