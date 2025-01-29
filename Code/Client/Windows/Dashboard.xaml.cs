using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client_Side.Windows
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();


            // יצירת רשומות דינמיות
            var transactions = GenerateTransactions(100);

            // הגדרת מקור הנתונים ל-DataGrid
            dgrLastTransactions.ItemsSource = transactions;

        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        // פונקציה ליצירת רשומות מדומות
        // פונקציה ליצירת רשומות מדומות
        private List<Transaction> GenerateTransactions(int count)
        {
            var transactions = new List<Transaction>();
            var random = new Random();
            string[] types = { "Transfer", "Withdrawal", "Deposit" };
            string[] statuses = { "Money In", "Money Out" };

            for (int i = 1; i <= count; i++)
            {
                transactions.Add(new Transaction
                {
                    Type = types[random.Next(types.Length)],
                    Amount = $"{random.Next(100, 1000)}$",
                    Name = $"User {i}",
                    ID = random.Next(100000, 999999).ToString(),
                    Status = statuses[random.Next(statuses.Length)],
                    Date = DateTime.Now.AddDays(-random.Next(0, 30)).ToString("dd.MM.yyyy"),
                    Hour = DateTime.Now.AddMinutes(-random.Next(0, 1440)).ToString("HH:mm"),
                    Commission = $"{random.Next(1, 100)}$"
                });
            }

            return transactions;
        }
    }

    // מחלקה לנתוני העסקאות
    public class Transaction
{
    public string Type { get; set; }
    public string Amount { get; set; }
    public string Name { get; set; }
    public string ID { get; set; }
    public string Status { get; set; }
    public string Date { get; set; }
    public string Hour { get; set; }
    public string Commission { get; set; }
}
    }


