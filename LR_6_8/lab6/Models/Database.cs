using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace lab6.Models
{
    public class Database
    {
        public void Log(string messageType, string messageText, DateTime messageDate)
        {
            using (var connection = new SqlConnection(@"Data Source=VANNF2\VANNF2;Initial Catalog=IntProgrLog;Integrated Security=True"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"INSERT INTO [TableLog] (controller, action, date) VALUES (@controller, @action, @date);";

                    command.Parameters.AddWithValue("@controller", messageType);
                    command.Parameters.AddWithValue("@action", messageText);
                    command.Parameters.AddWithValue("@date", messageDate);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}