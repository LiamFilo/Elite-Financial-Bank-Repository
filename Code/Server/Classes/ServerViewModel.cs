using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Server.Classes;

namespace Server {
    public class ServerViewModel
    {
        // Lazy<T> ensures thread-safe, lazy initialization of the singleton instance
        private static readonly Lazy<ServerViewModel> _instance =
            new Lazy<ServerViewModel>(() => new ServerViewModel());

        // Public property to access the single instance
        public static ServerViewModel Instance => _instance.Value;

        // ViewModel properties
        public BCPViewModel BCPManagerVM { get; }
        public DataBaseViewModel DataBaseVM { get; }
        public ModelManagemetViewModel ModelManagementVM { get; }
        public CommandProcessingViewModel CommandProcessingVM { get; }

        // Private constructor to prevent external instantiation
        private ServerViewModel()
        {
            DataBaseVM = new DataBaseViewModel();
            BCPManagerVM = new BCPViewModel();
            ModelManagementVM = new ModelManagemetViewModel();
            CommandProcessingVM = new CommandProcessingViewModel();
        }
    }

}