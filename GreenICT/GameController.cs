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
        public void init_game(int gameObj_amount) //gameobjamount - 20/24/30
        {

            //Get all game objects
            List<GameObject> ret = DatabaseHandler.getGameObjects();             //List used to store return value from database
            List<GameObject> randomgameObjects = new List<GameObject>();  //List used to store randomly chosen objects
            List<GameObject> finalgameObjects;
          
            //Get required amount of random gameobjs from ret
            if ((gameObj_amount / 2) > ret.Count)
            {
                Console.WriteLine("Not enough game objects to create this game !");
                return;
            }
            else
            {
                Random rnd = new Random();
                for (int c = 0; c < gameObj_amount / 2; c++)
                {
                    int r = rnd.Next(ret.Count);
                    randomgameObjects.Add(ret[r]);
                    ret.Remove(ret[r]);  //Remove added value so we dont end up adding the same gameobject twice
                }
                
            }
            //insert new game and get its id
            int newGameId = DatabaseHandler.createGame();
            if (newGameId == 0)
            {
                Console.WriteLine("Error occured while tryin to add game to database");
            }
            else
            {
                //Bind used gameobjects to this id
                foreach (GameObject gameObj in randomgameObjects)
                {
                    DatabaseHandler.bindGame_GameObj(newGameId, gameObj.getId());
                }
            }
            //Copy and scatter game objects
            finalgameObjects = new List<GameObject>(randomgameObjects);
            finalgameObjects.AddRange(randomgameObjects);
            shuffle(finalgameObjects);

            //Save all this to a game object
            curGame = new Game(newGameId, finalgameObjects);
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
