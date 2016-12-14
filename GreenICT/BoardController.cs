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
            this.gameWindow.score = game.score;
            this.gameWindow.moves = game.moves;
            updateMoves();
            updateScore();
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
            if (curGame.state == "started")
            {
                for(int j = 0; j < gameObjects.Count(); j++)
                {
                    GameObject temp = gameObjects[j];
                    temp = new GameObject(temp.getId(), curGame.id);
                    gameObjects[j] = temp;
                }
                gameObjects.AddRange(gameObjects);
                gameObjects.OrderBy(a => Guid.NewGuid());
            }
            for (int outter = 0; outter < row_count; outter++)
            {
                for (inner = 0; inner < col_count; inner++)
                {
                    int x = inner;
                    int y = outter;
                    GameObject go = gameObjects[imageIndex];

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
                    if (go.state == "1")
                    {
                        BitmapImage success_icon = new BitmapImage();
                        success_icon.BeginInit();
                        success_icon.UriSource = new Uri("C:\\AppImages\\success_icon.png", UriKind.Relative);
                        success_icon.CacheOption = BitmapCacheOption.OnLoad;
                        success_icon.EndInit();
                        i.Name = "s";
                        i.Opacity = 1;
                        i.Source = success_icon;
                    }
                    Grid.SetRow(myBorder, y);
                    Grid.SetColumn(myBorder, x);
                    Grid.SetRow(i, y);
                    Grid.SetColumn(i, x);
                    gameWindow.GameWindowGrid.Children.Add(myBorder);
                    gameWindow.GameWindowGrid.Children.Add(i);
                    imageIndex++;
                }
            }
            if (curGame.state == "setup")
            {
                DatabaseHandler.updateGameState("started", curGame.id);
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
                DatabaseHandler.InsertGameEvent("game_" + curGame.id, "finished", 1, curGame.id);
                gameWindow.info_text.Visibility = Visibility.Hidden;
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
