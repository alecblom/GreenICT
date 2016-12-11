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
using System.Diagnostics;

namespace GreenICT
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        List<Game> games;
        public GameWindow()
        {
            InitializeComponent();
            games = new List<Game>();
            LoadGames();
        }

        private void LoadGames()
        {
            List<int> gameIds = DatabaseHandler.getGameList();
            foreach(int id in gameIds)
            {
                Game g = new Game(id);
                games.Add(g);
            }

            int y = 0;
            foreach (Game g in games)
            {   
                games_list.RowDefinitions.Add(new RowDefinition());

                Label gameid = new Label();
                gameid.Content = g.id;
                Grid.SetRow(gameid, y);
                Grid.SetColumn(gameid, 0);

                Label state = new Label();
                state.Content = g.state;
                Grid.SetRow(state, y);
                Grid.SetColumn(state, 1);

                Button btn = new Button();
                btn.Content = "Select game";
                btn.Tag = g.id;
                Grid.SetRow(btn, y);
                Grid.SetColumn(btn, 3);
                y++;

                games_list.Children.Add(gameid);
                games_list.Children.Add(state);
                games_list.Children.Add(btn);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {



        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}