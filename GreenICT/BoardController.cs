using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GreenICT
{
    public class BoardController
    {
        public GameWindow gameWindow;
        private int size;
        private Game curGame;
        public BoardController(GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;
        }

        //Generate the game grid in required size
        public void gen_grid(Game game)
        {
            this.curGame = game;
            size = game.getSize();
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

            for (int c = 0; c < col_count; c++)
            {
                ColumnDefinition col = new ColumnDefinition();
                //col.Width = new GridLength(1, GridUnitType.Star);
                col.Width = new GridLength(1, GridUnitType.Star);
                gameWindow.GameWindowGrid.ColumnDefinitions.Add(col);
            }
            for (int d = 0; d < row_count; d++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                gameWindow.GameWindowGrid.RowDefinitions.Add(row);
            }

            fillGrid(col_count, row_count);

        }

        //Fill the grid with random collection of gameobjects
        public void fillGrid(int cc, int rc)
        {
            int col_count = cc;
            int row_count = rc;
            int inner = 0;
            int imageIndex = 0;
            Image i;
            BitmapImage src;
            List<GameObject> gameObjects = curGame.getGameObjects();
           



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
                    gameWindow.GameWindowGrid.Children.Add(myBorder);
                    gameWindow.GameWindowGrid.Children.Add(i);
                    imageIndex++;
                }

                i = new Image();
                src = new BitmapImage();
                src.BeginInit();
                if (imageIndex == 24)
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
                gameWindow.GameWindowGrid.Children.Add(myBorder2);
                gameWindow.GameWindowGrid.Children.Add(i);
                imageIndex++;
            }
        }

        public void checkMatch()
        {

            if (gameWindow.selectedObject2.Name == gameWindow.selectedObject.Name)
            {
                Console.WriteLine("match found");
                gameWindow.match = true;
                updateInfoText(3);
                gameWindow.continue_button.Visibility = Visibility.Visible;
               
                //Get id of the gameobject and log the change to DB
                String id = gameWindow.selectedObject.Name;
                id = id.Substring(2);
                int x = Int32.Parse(id);
                DatabaseHandler.updateGameObj_has_game(x, curGame.id, 1);

            }
            else
            {
                Console.WriteLine("No match!");
                updateInfoText(4);
                gameWindow.continue_button.Visibility = Visibility.Visible;
                gameWindow.match = false;
            }
            gameWindow.moves++;
            updateMoves();
        }



        #region Utility Text Methods
        public void updateInfoText(int i)
        {

            switch (i)
            {
                case 1:
                    gameWindow.info_text.Text = "Select an image to reveal it.";
                    break;
                case 2:
                    gameWindow.info_text.Text = "Select a second image to reveal it.";
                    break;
                case 3:
                    gameWindow.info_text.Text = "Images matched !";
                    break;
                case 4:
                    gameWindow.info_text.Text = "No match!";
                    break;
            }
        }

        public void updateScore()
        {
            
            if (gameWindow.score == size / 2)
            {
                gameWindow.score_text.Text = "Game finished !";
                DatabaseHandler.updateGameState("Finished",curGame.id);
            }
            else
            {
                gameWindow.score_text.Text = "Score : " + gameWindow.score;
            }

        }

        public void updateMoves()
        {
            gameWindow.move_text.Text = "Moves : " + gameWindow.moves;
        }
        #endregion

    }
}
