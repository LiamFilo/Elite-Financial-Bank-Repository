using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BankingEntities;
using BankingCommunication;
using ICommand = BankingCommunication.ICommand;

namespace Client
{
    public class ModelManagemetViewModel
    {
        private User _currentUser;
        public readonly WindowManager WindowManager;

        public User CurrentUser { get { return _currentUser; } }

        public ModelManagemetViewModel()
        {
            
        }

        public void ExecuteCommandInModel(ICommand command)
        {

        }
    }
}
