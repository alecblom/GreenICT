using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace GreenICT
{
    class DatabaseHandler
    {
        public DatabaseHandler()
        {

        }

        public String getImageUrl(int objectId)
        {
            String result = "Something went wrong";
            int category = 1;
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
                MySqlCommand cmd = new MySqlCommand("SELECT value FROM metadata WHERE Category_categoryid = " + category + " AND GameObject_GameObjectId = " + objectId, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                Object raw = cmd.ExecuteScalar();
                result = (String)raw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return result;
        }
    }
}