using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BankingCommunication;
using BankingEntities;
using BankingExceptions;
using Client;
using ICommand = BankingCommunication.ICommand;

namespace Client_Side.Windows
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();


            Console.WriteLine("initializing");
            ClientViewModel.Instance.BCPManagerVM.ConnectToServerAndStartListening();
            this.btnLogin.Click += BtnLogin_Click;

        }




        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("btn Login Invoke");
            string userIDInput = this.txbIDNumber.Text,
                userPhoneNumber = this.txbPhoneNumber.Text;

            try
            {
                //Check All Client Validation:
                UserValidator.IsValidIsraeliID(userIDInput);
                UserValidator.IsValidIsraeliPhoneNumber(userPhoneNumber);

                ICommand command = new LogInCommand(userIDInput, userPhoneNumber);

                ClientViewModel.Instance.UserInteractionVM.UserCommandHandler(command);
            }
            catch (Exception ex) { ExceptionHandler.ExceptionHandle(ex); }
        }

       
    }
}
