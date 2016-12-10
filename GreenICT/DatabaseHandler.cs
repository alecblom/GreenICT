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

        static public List<string> initGameObject(int id)
        {
            List<string> data = new List<string>();
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations

                MySqlCommand cmd = new MySqlCommand("SELECT value FROM metadata WHERE GameObject_GameObjectId = " + id, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(reader["value"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return data;
        }

        static public List<int> initGame(int id)
        {
            List<int> data = new List<int>();
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations

                MySqlCommand cmd = new MySqlCommand("SELECT gameobject_gameObjectId FROM gameobject_has_game WHERE game_gameId = " + id, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add((int)reader["value"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return data;
        }

        public String getImageUrl(int objectId)
        {
            String result = "Something went wrong";
            int category = 3;
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