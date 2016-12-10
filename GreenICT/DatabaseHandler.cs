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
        #region GAMEOBJECTS 

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


        static public int CreateNewGameObject(string name, string type, string url, string description)
        {
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO gameObject(timestamp) VALUES(NOW())", conn);
                cmd1.ExecuteNonQuery();
                int objID = (int)cmd1.LastInsertedId;
                MySqlCommand cmd2 = new MySqlCommand("INSERT INTO metadata(value, category_categoryId, GameObject_gameObjectId) VALUES(@value, @category, " + objID + ")", conn);
                cmd2.Parameters.AddWithValue("@value", name);
                cmd2.Parameters.AddWithValue("@category", 1);
                cmd2.ExecuteNonQuery();

                cmd2.Parameters["@value"].Value = type;
                cmd2.Parameters["@category"].Value = 2;
                cmd2.ExecuteNonQuery();

                cmd2.Parameters["@value"].Value = url;
                cmd2.Parameters["@category"].Value = 3;
                cmd2.ExecuteNonQuery();

                cmd2.Parameters["@value"].Value = description;
                cmd2.Parameters["@category"].Value = 4;
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return 1;
        }


        static public List<GameObject> getGameObjects()
        {
            List<GameObject> data = new List<GameObject>();
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations

                MySqlCommand cmd = new MySqlCommand("SELECT* FROM gameObject", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new GameObject(reader.GetInt32("gameObjectId")));
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

        #endregion

        #region GAMES

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
                    data.Add((int)reader["gameobject_gameObjectId"]);
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

        static public int CreateGame()
        {
            int objID = 0;
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO game(state) VALUES(\"started\")", conn);
                cmd1.ExecuteNonQuery();
                objID = (int)cmd1.LastInsertedId;
                
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return objID;
        }
        static public int BindGame_GameObj(int gameId,int gameObjId)
        {
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO gameobject_has_game(gameobject_gameObjectId,game_gameId) VALUES("+gameObjId+","+gameId+")", conn);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return 1;
        }
        #endregion


    }
}