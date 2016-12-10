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
    public partial class GameWindow2 : Window
    {
        public GameWindow2()
        {
            InitializeComponent();
            // gen_grid(30); //input grid size 20/24/30
            //DatabaseHandler dh = new DatabaseHandler();
            //dh.getGameObjects();
            GameController g = new GameController();
            g.init_game(16);
        }

        private void gen_grid(int size)
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

            fillGrid(col_count, row_count);

        }

        private void fillGrid(int cc, int rc)
        {

            int col_count = cc;
            int row_count = rc;
            int inner = 0;
            Image i;
            BitmapImage src;
            for(int outter = 0; outter < col_count; outter++){
                GameObject go = new GameObject(inner + 1);
                i = new Image();
                src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(go.url, UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                i.Source = src;
                i.Stretch = Stretch.Uniform;

                Grid.SetRow(i, inner);
                Grid.SetColumn(i, outter);
                GameWindowGrid.Children.Add(i);

                for (inner = 0; inner < row_count; inner++)
                {
                     i = new Image();
                     src = new BitmapImage();
                    src.BeginInit();
                    src.UriSource = new Uri(go.url, UriKind.Relative);
                    src.CacheOption = BitmapCacheOption.OnLoad;
                    src.EndInit();
                    i.Source = src;
                    i.Stretch = Stretch.Uniform;

                    Grid.SetRow(i, inner);
                    Grid.SetColumn(i, outter);
                    GameWindowGrid.Children.Add(i);
                }
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
