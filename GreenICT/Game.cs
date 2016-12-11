using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenICT
{
    class Game
    {
        public int id { get; set; }
        public List<GameObject> gameObjects { get; set; }
        public string state { get; set; }
        public Game(int id)
        {
            gameObjects = new List<GameObject>();
            this.id = id;
            Game g = DatabaseHandler.initGame(this);
            this.gameObjects = g.gameObjects;
            this.state = g.state;
            foreach(int objectId in data[0])
            {
                GameObject go = new GameObject(objectId);
                gameObjects.Add(go);
            }
        }

    }
}
