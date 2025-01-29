using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client_Side.Windows;
using BankingEnumeration;

namespace Client
{
    public class WindowManager
    {
        private WindowName _previousWindow;
        private Window _window;


        public WindowManager()
        {
              _window = Application.Current.MainWindow;
        }

        public Window GetCurrentWindow()
        {
            return _window;
        }

        public void SwitchWindow(WindowName nxtWindow)
        {
            
        }

        private Window WindowFactory(WindowName window)
        {
            switch (window)
            {
                case (WindowName.Log_In):
                    return new LogInWindow();
                    break;

                case (WindowName.Dashboard):
                    return new Dashboard();
                    break;
                case (WindowName.Transaction_Execution):
                    return new TransactionExecution();
                    break;
                default:
                    throw new Exception("The window you try to open is not exist in window factory.");
            }
        }

        public void MoveToPreviousWindow()
        {
            this._window.Close();
            this._window = this.WindowFactory(this._previousWindow);
            this._window?.ShowDialog();
        }
    }
}
