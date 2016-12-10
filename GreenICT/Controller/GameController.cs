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
        Game curGame;
        
        public void init_game(List<Player> players,int gameObj_amount) //gameobjamount - 20/24/30
        {
            //TODO add some test values and test shuffle
            //implement getting gameobjects + their metadata and storing them in the game object 


            List<GameObject> gameobjects = new List<GameObject>();

            //TODO Get amount of game objects from database (random)
            //TODO Retrieve metdata for all game objects
            
            //Copy and scatter game objects
            foreach(GameObject element in gameobjects)
            {
                gameobjects.Add(element);
            }
          
            //Store these in the game's gameobjects list 

            //Set as current game



            Game g = new Game();
            g.players = players;
            
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
