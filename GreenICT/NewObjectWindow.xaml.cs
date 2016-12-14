using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GreenICT
{
    /// <summary>
    /// Interaction logic for NewObjectWindow.xaml
    /// </summary>
    public partial class NewObjectWindow : Window
    {
        public NewObjectWindow()
        {
            Button btn = new Button();
            btn.Name = "create_gameobject";
            btn.Click += btn_click;

            InitializeComponent();
        }

        private void btn_click(object sender, RoutedEventArgs e)
        {
            string name = name_box.Text;
            string type = type_box.Text;
            string url = url_box.Text;
            string description = description_box.Text;

            if (name.Length < 4 || url.Length < 4 || description.Length < 4)
            {
                error_label.Content = "Please fill in all the fields";
                return;
            }
            else
            {
                DatabaseHandler.CreateNewGameObject(name, type, url, description);
            }
        }
    }
}
