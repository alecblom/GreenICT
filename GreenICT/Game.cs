using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenICT
{
    class Game
    {
        private int id;
        private List<GameObject> gameObjects;


        //Get game by id from database and init it 
        public Game(int id)
        {
            this.id = id;
            List<int> data = DatabaseHandler.initGame(id);
            foreach (int objectId in data)
            {
                GameObject go = new GameObject(objectId);
                gameObjects.Add(go);
            }
        }

        public Game(int id, List<GameObject> gameObjects)
        {
            this.id = id;
            this.gameObjects = gameObjects;
        }

        public List<GameObject> getGameObjects()
        {
            return gameObjects;
        }
        public int getId()
        {
            return id;
        }


    }
}
