using GreenICT.Controller;
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
    /// Interaction logic for CreateGameWindow.xaml
    /// </summary>
    public partial class CreateGameWindow : Window
    {
        public CreateGameWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameController gc = new GameController();
            error_message.Visibility = Visibility.Hidden;
            if ((bool)radioButton_1.IsChecked)
            {
                if ((bool)radioButton_20.IsChecked)
                {
                    GameWindow2 win2 = new GameWindow2(gc.init_game(20));
                    win2.Show();
                    this.Close();
                    return;
                }else if ((bool)radioButton_30.IsChecked)
                {
                    GameWindow2 win2 = new GameWindow2(gc.init_game(30));
                    win2.Show();
                    this.Close();
                    return;
                }
            }

            //error message
            error_message.Visibility = Visibility.Visible;
        }
    }
}
