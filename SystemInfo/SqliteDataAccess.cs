using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfo
{
    public class SqliteDataAccess
    {
        public static List<DataLine> LoadCollectedData()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DataLine>("select * from CollectorData", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveDataLine(DataLine dataline)
        {
            using (var cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                var cmd = new SQLiteCommand(cnn);
                cmd.CommandText = "insert into CollectorData (ProcessName, ProcessId, Status, StartTime, " +
                    "EndTime, IPAddress, MacAddress, Description) values (@ProcessName, @ProcessId, " +
                    "@Status, @StartTime, @EndTime, @IPAddress, @MacAddress, @Description)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@ProcessName", dataline.ProcessName);
                cmd.Parameters.AddWithValue("@ProcessId", dataline.ProcessId);
                cmd.Parameters.AddWithValue("@Status", dataline.Status);
                cmd.Parameters.AddWithValue("@StartTime", dataline.StartTime);
                cmd.Parameters.AddWithValue("@EndTime", dataline.EndTime);
                cmd.Parameters.AddWithValue("@IPAddress", dataline.IPAddress);
                cmd.Parameters.AddWithValue("@MacAddress", dataline.MacAddress);
                cmd.Parameters.AddWithValue("@Description", dataline.Description);

                cmd.ExecuteNonQuery();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
