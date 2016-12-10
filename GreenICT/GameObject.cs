using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenICT
{
    class GameObject
    {
        private int id;
        private string name;
        private string type;
        private string url;
        private string description;

        public GameObject(int id)
        {
            this.id = id;
            List<string> data = DatabaseHandler.initGameObject(id);
            name = data[0];
            type = data[1];
            url = data[2];
            description = data[3];
        }
    }
}
