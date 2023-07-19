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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameField gamefield = new GameField();
        public MainWindow()
        {

            InitializeComponent();
            MakeField();
        }
        public void MakeField()
        {
            gamefield.cells.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                { 
                Cell cell = new Cell()
                {
                    Height = 100,
                    Width = 100,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Background = Brushes.Brown,
                    posX = i,
                    posY = j,
                    Margin = new Thickness((i*100), (j*100), 0 ,0)
                };
                if ((i %2==1 && j%2==1) || (i %2==0 && j%2==0))
                    {
                        cell.Background= Brushes.Beige;
                    }
                if (j==0)
                    {
                        Image pic = new Image();
                        switch (i)
                        {
                            case 0:
                                cell.occupyingFigurine = new Rook("black");
                                pic.Source = new BitmapImage(new Uri(cell.occupyingFigurine.symbol, UriKind.Relative));
                                cell.Content = pic;
                                break;
                            case 1:
                                cell.occupyingFigurine = new Knight("black");
                                pic.Source = new BitmapImage(new Uri(cell.occupyingFigurine.symbol, UriKind.Relative));
                                cell.Content = pic;
                                break;
                            case 2:
                                cell.occupyingFigurine = new Bishop("black");
                                pic.Source = new BitmapImage(new Uri(cell.occupyingFigurine.symbol, UriKind.Relative));
                                cell.Content = pic;
                                break;
                            case 3:
                                cell.occupyingFigurine = new Queen("black");
                                pic.Source = new BitmapImage(new Uri(cell.occupyingFigurine.symbol, UriKind.Relative));
                                cell.Content = pic;
                                break;

                        }
                        
                    }
                else if (j==1)
                    {
                        Image pic = new Image();
                        cell.occupyingFigurine = new Pawn("white");
                        pic.Source = new BitmapImage(new Uri(cell.occupyingFigurine.symbol, UriKind.Relative));
                        cell.Content = pic;
                    }
                else if (j == 6)
                {
                        Image pic = new Image();
                        cell.occupyingFigurine = new Pawn("black");
                        pic.Source = new BitmapImage(new Uri(cell.occupyingFigurine.symbol, UriKind.Relative));
                        cell.Content = pic;
                }

                    gamefield.cells.Add(cell);
                board.Children.Add(cell);
                }
            }
        }
    }
}

