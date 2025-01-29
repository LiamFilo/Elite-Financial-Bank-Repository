using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientViewModel
    {
        private static ClientViewModel _instance;

        private static readonly object _lock = new object();

        public readonly UserInteractionViewModel UserInteractionVM;
        public readonly BCPManagerViewModel BCPManagerVM;
        public readonly CommandProcessingViewModel CommandProcessingVM;
        public readonly ModelManagemetViewModel ModelManagemetVM;

        private ClientViewModel()
        {
            UserInteractionVM = new UserInteractionViewModel();
            BCPManagerVM = new BCPManagerViewModel();
            CommandProcessingVM = new CommandProcessingViewModel();
            ModelManagemetVM = new ModelManagemetViewModel();
        }

        public static ClientViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ClientViewModel();
                        }
                    }
                }
                return _instance;
            }
        }
    }

}
