using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenICT.Controller
{
    class GameController
    {
        private Random rng = new Random();
        private Game curGame;
        /**
        Create a new game and save it in the curGame variable
        */
        public Game init_game(int gameObj_amount) //gameobjamount - 20/24/30
        {

            //Get all game objects
            List<GameObject> ret = DatabaseHandler.getRandomGameObjects(gameObj_amount / 2);             //List used to store return value from database
            List<GameObject> finalgameObjects;
          
           
            //insert new game and get its id
            int newGameId = DatabaseHandler.createGame();
            if (newGameId == 0)
            {
                Console.WriteLine("Error occured while tryin to add game to database");
            }
            else
            {
                //Bind used gameobjects to this id
                foreach (GameObject gameObj in ret)
                {
                    DatabaseHandler.bindGame_GameObj(newGameId, gameObj.getId());
                }
            }
            //Copy and scatter game objects
            finalgameObjects = new List<GameObject>(ret);
            finalgameObjects.AddRange(ret);
            shuffle(finalgameObjects);

            //Save a game event stating we created this new game
            DatabaseHandler.InsertGameEvent("game_" + newGameId, "setup", 1, newGameId);

            //Save all this to a game object
            return new Game(newGameId, finalgameObjects,gameObj_amount, "setup");
        }



        public Game getCurrentGame()
        {
            return curGame;
        }

        //Shuffle gameobj list usng fisher yates shuffle
        public void shuffle(List<GameObject> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                GameObject value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }




    }
}
