using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Classes
{
    public class DatabaseManager : IDisposable
    {
        private SqlConnection connection;
        private readonly string connectionString;

        // בונה את המחלקה עם מחרוזת החיבור הנדרשת
        public DatabaseManager()
        {
            this.connectionString = "Server=192.168.1.254,49170;User Id=program;Password=program1234;Encrypt=False;TrustServerCertificate=True;";

            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
        }

        // פותח את החיבור למסד הנתונים
        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        // סוגר את החיבור למסד הנתונים
        public void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        // מבצע שאילתת SELECT ומחזיר את התוצאות כ־DataTable
        public DataTable ExecuteQuery(string query)
        {
            DataTable resultTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(resultTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // ניתן לטפל בשגיאות באופן מותאם אישית
                throw new Exception("Error executing query: " + ex.Message);
            }
            return resultTable;
        }

        // מבצע שאילתות שאינן מחזירות נתונים (כגון INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query)
        {
            int affectedRows = 0;
            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות בהתאם
                throw new Exception("Error executing non-query: " + ex.Message);
            }
            return affectedRows;
        }

        // יישום IDisposable לשחרור המשאבים
        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                connection.Dispose();
            }
        }
    }
}
