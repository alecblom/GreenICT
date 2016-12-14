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
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void Button_New_Game_Object(object sender, RoutedEventArgs e)
        {
            //GameController gc = new GameController();
            //error_message.Visibility = Visibility.Hidden;
            //if ((bool)radioButton_1.IsChecked)
            //{
            //    if ((bool)radioButton_20.IsChecked)
            //    {
            //        GameWindow win2 = new GameWindow(gc.init_game(20));
            //        win2.Show();
            //        this.Close();
            //        return;
            //    }
            //    else if ((bool)radioButton_30.IsChecked)
            //    {
            //        GameWindow win2 = new GameWindow(gc.init_game(30));
            //        win2.Show();
            //        this.Close();
            //        return;
            //    }
            //}

            NewObjectWindow newWindow = new NewObjectWindow();
            newWindow.Show();
            this.Close();
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            CreateGameWindow newWindow = new CreateGameWindow();
            newWindow.Show();
            this.Close();
        }

        private void Button_Resume(object sender, RoutedEventArgs e)
        {
            GameSelect newWindow = new GameSelect();
            newWindow.Show();
            this.Close();
        }
    }
}
