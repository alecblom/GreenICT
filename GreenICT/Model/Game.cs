using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenICT
{
    class Game
    {
        public int game_id { get; set; }
        public String state { get; set; }
        public List<GameObject> gameObjects { get; set; }
        public List<Player> players{ get; set; }

        //public Game(int id, string state, List<GameObject> gameobjects, List<Player> players)
        //{
        //    this.game_id = id;
        //    this.state = state;
        //    this.gameObjects = gameobjects;
        //    this.players = players;

        //}
    }
}
