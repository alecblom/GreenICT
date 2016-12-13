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
   //TODO remove logic place in seperate class and pass window references by paassing .this to the other class (all elements should be accesable ) 
   //Log all moves to database


    public partial class GameWindow2 : Window
    {
       
        private Image selectedObject;
        private Image selectedObject2;
        private bool match;
        private BitmapImage success_icon;
        private int score;
        private int size;
        private int moves;
        public GameWindow2(Game game)
        {
            InitializeComponent();
            GameController g = new GameController();
           size = game.getSize();
            gen_grid(size, game);
            score = 0;
            moves = 0;
            updateScore();
            updateMoves();
            //Initialize succes icon for when images are matched
            success_icon = new BitmapImage();
            success_icon.BeginInit();
            success_icon.UriSource = new Uri("C:\\AppImages\\success_icon.png", UriKind.Relative);
            success_icon.CacheOption = BitmapCacheOption.OnLoad;
            success_icon.EndInit();
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
            Button b;
            List<GameObject> gameObjects = game.getGameObjects();
            int x = 0;
            int y = 0;



            for (int outter = 0; outter < row_count - 1; outter++)
            {


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
                    String name = gameObjects[imageIndex].getId().ToString();
                    i.Name = "o_" + name;
                    i.Opacity = 0.0;

                    var myBorder = new Border();
                    myBorder.BorderBrush = Brushes.Black;
                    myBorder.BorderThickness = new Thickness(1);
                    myBorder.Background = Brushes.MediumPurple;
                    Grid.SetRow(myBorder, inner);
                    Grid.SetColumn(myBorder, outter);
                    Grid.SetRow(i, inner);
                    Grid.SetColumn(i, outter);
                    GameWindowGrid.Children.Add(myBorder);
                    GameWindowGrid.Children.Add(i);

                    imageIndex++;
                }

                i = new Image();
                src = new BitmapImage();
                src.BeginInit();
                if(imageIndex == 24)
                {
                    break;
                }
                src.UriSource = new Uri(gameObjects[imageIndex].url, UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                i.Source = src;
                i.Stretch = Stretch.Uniform;
                String name2 = gameObjects[imageIndex].getId().ToString();
                i.Name = "o_" + name2;
                i.Opacity = 0.0;

                var myBorder2 = new Border();
                myBorder2.BorderBrush = Brushes.Black;
                myBorder2.BorderThickness = new Thickness(1);
                myBorder2.Background = Brushes.MediumPurple;
                Grid.SetRow(myBorder2, inner);
                Grid.SetColumn(myBorder2, outter);
                Grid.SetRow(i, inner);
                Grid.SetColumn(i, outter);
                GameWindowGrid.Children.Add(myBorder2);
                GameWindowGrid.Children.Add(i);

               
                imageIndex++;

            }



        }

    

        public void checkMatch()
        {
           
                if (selectedObject2.Name == selectedObject.Name)
                {
                    Console.WriteLine("match found");
                    match = true;
                    updateInfoText(3);
                    continue_button.Visibility = Visibility.Visible;
                    score++;
                    updateScore();
                }
                else
                {
                    Console.WriteLine("No match!");
                    updateInfoText(4);
                    continue_button.Visibility = Visibility.Visible;
                    match = false;
                }
            moves++;
            updateMoves();
        }

        //TODO: check if image clicked is success_icon
        private void GameWindowGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Image dep;
            try
            {
                dep = (Image)e.OriginalSource;
            }
            catch (Exception ex)
            {
                return;
            }
            if (dep.Name == "s")
            {
                return;
            }
            if (selectedObject == null)
            {
                updateInfoText(2);
                dep.Opacity = 10.0;
                selectedObject = dep;
                return;
            }
            else if (selectedObject2 == null)
            {
                if (dep == selectedObject) return;
                dep.Opacity = 10.0;
                selectedObject2 = dep;
                checkMatch();
            }
          
        }

        private void updateInfoText(int i)
        {
           
           switch (i){
                case 1:
                    info_text.Text = "Select an image to reveal it.";
                    break;
                case 2:
                    info_text.Text = "Select a second image to reveal it.";
                    break;
                case 3:
                    info_text.Text = "Images matched !";
                    break;
                case 4:
                    info_text.Text = "No match!";
                    break;
                }
        }

        private void updateScore()
        {
            if(score == size/2)
            {
                score_text.Text = "Game finished !";

            }
            else
            {
                score_text.Text = "Score : " + score;
            }
            
        }

        private void updateMoves()
        {
            move_text.Text = "Moves : " + moves;
        }

        
        private void button_Click_1(object sender, RoutedEventArgs e)
        {

           
            continue_button.Visibility = Visibility.Hidden;
            updateInfoText(1);
            if (match)
            {
                selectedObject.Source = success_icon;
                selectedObject2.Source = success_icon;
                selectedObject.Name = "s";
                selectedObject2.Name = "s";

            
            }
            else
            {
                selectedObject.Opacity = 0;
                selectedObject2.Opacity = 0;
            }

            selectedObject = null;
            selectedObject2 = null;
        }
    }
}
