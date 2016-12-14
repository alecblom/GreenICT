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

        static public List<int> getGameList()
        {
            List<int> data = new List<int>();
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations

                MySqlCommand cmd = new MySqlCommand("SELECT gameId FROM game", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add((int)reader["gameId"]);
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

        static public Game initGame(Game g)
        {
            List<int> objectids = new List<int>();
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations

                MySqlCommand cmd = new MySqlCommand("SELECT gameobject_gameObjectId FROM gameobject_has_game WHERE game_gameId = " + g.id, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    objectids.Add((int)reader["gameobject_gameObjectId"]);
                }
                foreach (int objectId in objectids)
                {
                    GameObject go = new GameObject(objectId);
                    g.gameObjects.Add(go);
                }
                reader.Close();
                cmd = new MySqlCommand("SELECT state FROM game WHERE gameId = " + g.id, conn);
                g.state = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            return g;
        }

        static public int createGame()
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

        //TODO , take parameters to set x,y and state.
        //Rights now, state is always unflipped since this method is used for binding the gameobeject to the game for the first time
        //Updating will be done somwhere else
        static public int bindGame_GameObj(int gameId,int gameObjId)
        {
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO gameobject_has_game(gameobject_gameObjectId,game_gameId,x,y,state) VALUES("+gameObjId+","+gameId+",1,1,\"unmatched\")", conn);
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


        
        public static void InsertGameEvent(String actionElement, String actionPerformed, int playerID, int gameID)
        {
            string connStr = "server=localhost;user=root;database=green_ict;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                //TODO fix timestamp with valid sql date?
                //Check why id isnt loading
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO gameevent(actionElement,actionPerformed,timestamp,Player_playerId,Game_gameId) VALUES(\"" + actionElement + "\",\"" + actionPerformed + "\",\"" + sqlFormattedDate + "\"," + playerID + "," + gameID+")", conn);
                cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
           
        }

    }
}