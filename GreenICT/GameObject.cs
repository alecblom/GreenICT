using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenICT
{
    public class GameObject
    {
        private int id;
        private string name;
        private string type;
        public string url { get; set; }
        private string description;
        public string state { get; set; }

        public GameObject(int id)
        {
            this.id = id;
            List<string> data = DatabaseHandler.initGameObject(id);
            name = data[0];
            type = data[1];
            url = data[2];
            description = data[3];
        }

        public GameObject(int objectId, int gameId)
        {
            this.id = objectId;
            List<string> data = DatabaseHandler.initGameObject(id, gameId);
            name = data[0];
            type = data[1];
            url = data[2];
            description = data[3];
            state = data[4];
        }

        public GameObject(string name, string type, string url, string description)
        {
            this.name = name;
            this.type = type;
            this.url = url;
            this.description = description;

            id = DatabaseHandler.CreateNewGameObject(name, type, url, description);
        }

        public int getId()
        {
            return this.id;
        }
    }
}
