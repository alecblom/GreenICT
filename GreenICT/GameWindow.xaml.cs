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
        List<Image> images;
        public GameWindow()
        {
            InitializeComponent();
            images = new List<Image>();
            loadImages();
        }

        private void loadImages()
        {
            int x = 0;
            int y = 0;
            for (var c = 2; c < 7; c++)
            {
                Image i = new Image();
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri("C:\\AppImages\\harold" + c + ".jpg", UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                i.Source = src;
                i.Width = 300;
                i.Height = 200;
                i.Stretch = Stretch.Uniform;
                Grid.SetRow(i, y);
                Grid.SetColumn(i, x);
                x++;
                if (x > 3)
                {
                    x = 0;
                    y++;
                }



                dataGrid.Children.Add(i);

            }

           /* Image i = new Image();
            BitmapImage src2 = new BitmapImage();
            src2.BeginInit();
            src2.UriSource = new Uri("C:\\AppImages\\harold" + 2 + ".jpg", UriKind.Relative);
            src2.CacheOption = BitmapCacheOption.OnLoad;
            src2.EndInit();
            i.Source = src2;
            i.Width = 300;
            i.Height = 200;
            i.Stretch = Stretch.Uniform;
            Grid.SetRow(i, 0);
            Grid.SetColumn(i, 1);
            dataGrid.Children.Add(i);
            Image i2 = new Image();
            BitmapImage src3 = new BitmapImage();
            src3.BeginInit();
            src3.UriSource = new Uri("C:\\AppImages\\harold" + 3 + ".jpg", UriKind.Relative);
            src3.CacheOption = BitmapCacheOption.OnLoad;
            src3.EndInit();
            i2.Source = src2;
            i2.Width = 300;
            i2.Height = 200;
            i2.Stretch = Stretch.Uniform;
            Grid.SetRow(i2, 0);
            Grid.SetColumn(i2, 0);
            dataGrid.Children.Add(i2);*/
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
