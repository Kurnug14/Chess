using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess
{
    public  class Cell : Button
    {
        Image pic = new Image();
        public int posX;
        public int posY;
        public Figurines occupyingFigurine;
        public void PlaceFigurine()
        {
            if (occupyingFigurine != null)
            {
                pic.Source = new BitmapImage(new Uri(occupyingFigurine.symbol, UriKind.Relative));
                Content = pic;
            }
            else if (occupyingFigurine == null)
            {
                Content = "";
            }
        }
    }
}