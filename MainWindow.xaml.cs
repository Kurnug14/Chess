using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        Figurines temp;
        public List<Cell> checkMove = new List<Cell>();
        public MainWindow()
        {
            InitializeComponent();
            MakeField();
        }
        public void MakeField()
        {
            temp = null;
            gamefield.width=8; gamefield.height=8;
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
                        switch (i)
                        {
                            case 0:
                                cell.occupyingFigurine = new Rook("black");
                                break;
                            case 1:
                                cell.occupyingFigurine = new Knight("black");
                                break;
                            case 2:
                                cell.occupyingFigurine = new Bishop("black");
                                break;
                            case 3:
                                cell.occupyingFigurine = new Queen("black");
                                 break;
                            case 4:
                                cell.occupyingFigurine = new King("black");                              
                                break;
                            case 5:
                                cell.occupyingFigurine = new Bishop("black");                            
                                break;
                            case 6:
                                cell.occupyingFigurine = new Knight("black");
                                break;
                            case 7:
                                cell.occupyingFigurine = new Rook("black");
                                break;
                        }
                        
                    }
                    else if (j==1)
                        {
                            cell.occupyingFigurine = new Pawn("black");
                        }
                    else if (j == 6)
                    {        
                            cell.occupyingFigurine = new Pawn("white");
                    }
                    else if (j == 7)
                    {
                        switch (i)
                        {
                            case 0:
                                cell.occupyingFigurine = new Rook("white");
                                break;
                            case 1:
                                cell.occupyingFigurine = new Knight("white");
                                break;
                            case 2:
                                cell.occupyingFigurine = new Bishop("white");
                                break;
                            case 3:
                                cell.occupyingFigurine = new Queen("white");
                                break;
                            case 4:
                                cell.occupyingFigurine = new King("white");
                                break;
                            case 5:
                                cell.occupyingFigurine = new Bishop("white");
                                break;
                            case 6:
                                cell.occupyingFigurine = new Knight("white");
                                break;
                            case 7:
                                cell.occupyingFigurine = new Rook("white");
                                break;
                        }
                        }
                        cell.Click += Moving;
                        gamefield.cells.Add(cell);
                        foreach (Cell setcell in gamefield.cells)
                        {
                            setcell.PlaceFigurine();
                        }
                    
                    board.Children.Add(cell);
                }
            }
        }
        private void Moving (object sender, EventArgs e)
        {
            
            Cell clicked = (Cell)sender;
            if (clicked.occupyingFigurine!= null && temp == null) 
            {
                checkMove.AddRange(gamefield.CalcMoves(clicked.posX, clicked.posY));
                if (checkMove.Count !=0 ) 
                { 
                    temp = clicked.occupyingFigurine;
                    clicked.occupyingFigurine = null;
                    clicked.PlaceFigurine();
                }
            }
            else if (temp != null && checkMove.Contains(clicked))
            {
                
                clicked.occupyingFigurine = temp;
                clicked.PlaceFigurine();
                temp = null;
                checkMove.Clear();
            }

            Debug.Content = clicked.posX.ToString() + clicked.posY.ToString();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MakeField();
        }
    }
}

