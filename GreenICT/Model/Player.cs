using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenICT
{
    class Player
    {
        public int player_id { get; set; }
        public String username { get; set; }
        public String name { get; set; }
        public String password { get; set; }

        public Player(int id, string username, string name, string password)
        {
            this.player_id = id;
            this.username = username;
            this.name = name;
            this.password = password;
        }
    }
}
