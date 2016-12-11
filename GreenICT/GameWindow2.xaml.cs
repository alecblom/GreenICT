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
    /// Interaction logic for GameWindow2.xaml
    /// </summary>
    /// 


        //implement lowest amount of pictures in database to creae 20 sized game
        //call gen grid using results from init game 
        //
    public partial class GameWindow2 : Window
    {
        public GameWindow2()
        {
            int gameSizeRequested = 20;
            InitializeComponent();
            GameController g = new GameController();
            g.init_game(gameSizeRequested);
            Game game = g.getCurrentGame();
            gen_grid(gameSizeRequested, game);

        }

        private void gen_grid(int size, Game game)
        {
            int col_count;
            int row_count;
            switch (size)
            {
                case 20:
                    col_count = 4;
                    row_count = 5;
                    break;
                case 24:
                    col_count = 4;
                    row_count = 6;
                    break;
                case 30:
                    col_count = 5;
                    row_count = 6;
                    break;
                default:
                    return;
                   
            }

            for (int c = 0; c <col_count; c++)
            {
                ColumnDefinition col = new ColumnDefinition();
                //col.Width = new GridLength(1, GridUnitType.Star);
                col.Width = new GridLength(1, GridUnitType.Star);
                GameWindowGrid.ColumnDefinitions.Add(col);                
            }
            for (int d = 0; d < row_count; d++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                GameWindowGrid.RowDefinitions.Add(row);
            }

            fillGrid(col_count, row_count, game);

        }

        private void fillGrid(int cc, int rc,Game game)
        { 
            int col_count = cc;
            int row_count = rc;
            int inner = 0;
            int imageIndex = 0;
            Image i;
            BitmapImage src;
            List<GameObject> gameObjects = game.getGameObjects();




            for (int outter = 0; outter < row_count-1; outter++){

                
                for (inner = 0; inner < col_count; inner++)
                {
                    i = new Image();
                    src = new BitmapImage();
                    src.BeginInit();
                    src.UriSource = new Uri(gameObjects[imageIndex].url, UriKind.Relative);
                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.EndInit();
                    i.Source = src;
                    i.Stretch = Stretch.Uniform;

                    Grid.SetRow(i, inner);
                    Grid.SetColumn(i, outter);
                    GameWindowGrid.Children.Add(i);
                    imageIndex++;
                }

                i = new Image();
                src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(gameObjects[imageIndex].url, UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                i.Source = src;
                i.Stretch = Stretch.Uniform;

                Grid.SetRow(i, inner);
                Grid.SetColumn(i, outter);
                GameWindowGrid.Children.Add(i);
                imageIndex++;

            }
            int x = imageIndex;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
