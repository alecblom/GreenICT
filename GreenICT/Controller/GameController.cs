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
       
        
        public List<GameObject> init_game(int gameObj_amount) //gameobjamount - 20/24/30
        {

            //Get all game objects
            DatabaseHandler dh = new DatabaseHandler();
            List<GameObject> ret = dh.getGameObjects();             //List used to store return value from database
            List<GameObject> randomgameObjects = new List<GameObject>();  //List used to store randomly chosen objects
            List<GameObject> finalgameObjects;
          
            //Get required amount of random gameobjs from ret
            if ((gameObj_amount / 2) > ret.Count)
            {
                Console.WriteLine("Not enough game objects to create this game !");
                return null;
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

            //Copy and scatter game objects
            finalgameObjects = new List<GameObject>(randomgameObjects);
            finalgameObjects.AddRange(randomgameObjects);
            shuffle(finalgameObjects);

            return finalgameObjects;
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
