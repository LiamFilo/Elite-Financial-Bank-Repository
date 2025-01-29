﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Client_Side.Windows;
using ICommand = BankingCommunication.ICommand;

namespace Client
{
    public class UserInteractionViewModel
    {

        public UserInteractionViewModel() 
        {

        }

        public void UserCommandHandler(ICommand command)
        {
            try
            {
                ClientViewModel.Instance.ModelManagemetVM.ExecuteCommandInModel(command);
            }
            catch (Exception ex) {ExceptionHandler.ExceptionHandle(ex);}
        }
    }
}
