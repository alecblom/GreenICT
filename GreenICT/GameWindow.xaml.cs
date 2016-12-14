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
 
    public partial class GameWindow : Window
    {
       //TODO update game state upon winning

        public Image selectedObject;
        public Image selectedObject2;
        public bool match;
        private BitmapImage success_icon;
        public int score;
        public int moves;
        private int curGameID;
        public BoardController b;
        public GameWindow(Game game)
        {
            //Init boardcontroller, gamecontroller, and call boardcontrollers gen_grid.
            InitializeComponent();
            GameController g = new GameController();
            b = new BoardController(this);
            b.gen_grid(game);

            //Set score, moves and current game id
            score = 0;
            moves = 0;
            curGameID = game.id;
            b.updateScore();
            b.updateMoves();
            
            //Initialize succes icon for when images are matched
            success_icon = new BitmapImage();
            success_icon.BeginInit();
            success_icon.UriSource = new Uri("C:\\AppImages\\success_icon.png", UriKind.Relative);
            success_icon.CacheOption = BitmapCacheOption.OnLoad;
            success_icon.EndInit();
        }

    


        private void GameWindowGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Try to retrieve an image from originalsource
            Image dep;
            try
            {
                dep = (Image)e.OriginalSource;
            }
            catch (Exception ex)
            {
                return;
            }
            //The image is one of the success icons, ignore it
            if (dep.Name == "s")
            {
                return;
            }
            //If there is 0 object selected, update the text and save reference
            //If there is already 1 object selected, fill the 2nd and check for a match
            if (selectedObject == null)
            {
                b.updateInfoText(2);
                dep.Opacity = 10.0;
                selectedObject = dep;
                return;
            }
            else if (selectedObject2 == null)
            {
                if (dep == selectedObject) return;
                dep.Opacity = 10.0;
                selectedObject2 = dep;
                b.checkMatch();
            }
          
        }

        

        
        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            continue_button.Visibility = Visibility.Hidden; //Hide continue button

            if (match)
            {
                //Replace images with success icon
                selectedObject.Source = success_icon;
                selectedObject2.Source = success_icon;
                
                //Log to db
                String name = selectedObject.Name;
                name = name.Substring(2);
                DatabaseHandler.InsertGameEvent(name, "matched", 1, curGameID);

                //Update selected object name so it wont count as a gamobject when checking matching images
                selectedObject.Name = "s";
                selectedObject2.Name = "s";

                //Add score
                score++;
                b.updateScore();
            }
            else
            {
                //Rehide images
                selectedObject.Opacity = 0;
                selectedObject2.Opacity = 0;
            }
            //"unselect" objects, show first infotext
            b.updateInfoText(1);
            selectedObject = null;
            selectedObject2 = null;
        }
    }
}
