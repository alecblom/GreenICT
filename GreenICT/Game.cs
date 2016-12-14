using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenICT
{
    public class Game
    {
        public int id { get; set; }
        public List<GameObject> gameObjects { get; set; }
        public string state { get; set; }
        private int size;
        public int score { get; set; }
        public int moves { get; set; }

        //Get game by id from database and init it 
        public Game(int id)
        {
            gameObjects = new List<GameObject>();
            this.id = id;

            Game g = DatabaseHandler.initGame(this);
            this.gameObjects = g.gameObjects;
            this.state = g.state;
            this.score = g.score;
            this.moves = g.moves;
            this.size = gameObjects.Count() * 2;
        }

        public Game(int id, List<GameObject> gameObjects, int size, string state)
        {
            this.id = id;
            this.gameObjects = gameObjects;
            this.size = size;
            this.state = state;
        }

        public List<GameObject> getGameObjects()
        {
            return gameObjects;
        }
        public int getId()
        {
            return id;
        }
        public int getSize()
        {
            return size;
        }

    }
}
