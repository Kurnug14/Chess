using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int xTemp;
        int yTemp;
        string player = "white";
        string colourTemp;
        bool winner = false;
        public MainWindow()
        {
            InitializeComponent();
            MakeField();
        }
        public void MakeField()
        {
            player = "white";
            winner = false;
            temp = null;
            Player.Content = "White";
            Player.Background = Brushes.White;
            Player.Foreground = Brushes.Black;
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
                if (clicked.occupyingFigurine.colour == player)
                { 
                checkMove.AddRange(gamefield.CalcMoves(clicked.posX, clicked.posY));
                    temp = clicked.occupyingFigurine;
                    clicked.occupyingFigurine = null;
                    clicked.PlaceFigurine();
                    xTemp = clicked.posX;
                    yTemp = clicked.posY;
                }
            }
            else if (temp != null && checkMove.Contains(clicked))
            {
                if (clicked.occupyingFigurine!= null) { 
                if (clicked.occupyingFigurine.GetType()==typeof(King))
                {
                    MessageBox.Show(player + " has won!");
                        winner = true;
                }
                }
                clicked.occupyingFigurine = temp;
                clicked.PlaceFigurine();
                temp = null;
                if ( clicked.occupyingFigurine!= null ) { 
                if (clicked.occupyingFigurine.GetType() == typeof(Pawn) && (clicked.posY == 7 || clicked.posY == 0))
                {
                    xTemp = clicked.posX; 
                    yTemp = clicked.posY;
                    colourTemp = clicked.occupyingFigurine.colour;
                    foreach (UIElement element in board.Children)
                    {
                        element.IsEnabled = false;
                    }
                    PromotionField(clicked.occupyingFigurine.colour);   
                }
                }
                if (player == "white" && (xTemp != clicked.posX || yTemp != clicked.posY))
                {
                    player = "black";
                    Player.Content= player;
                    Player.Background = Brushes.Black;
                    Player.Foreground = Brushes.White;
                }
                else if (player == "black" && (xTemp != clicked.posX || yTemp != clicked.posY)) 
                {
                    player = "white";
                    Player.Content = player;
                    Player.Background = Brushes.White;
                    Player.Foreground = Brushes.Black;
                }
                
                checkMove.Clear();
                if (winner == true)
                {
                    MakeField();
                }
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            MakeField();
        }

        private void PromotionField(string colour)
        {
            Image qpic = new Image();
            Image rpic = new Image();
            Image kpic = new Image();
            Image bpic = new Image();
            Cell qcell = new Cell();
            qcell.occupyingFigurine = new Queen(colour);
            qpic.Source = new BitmapImage(new Uri(qcell.occupyingFigurine.symbol, UriKind.Relative));
            Queen.Content = qpic;
            Cell rcell = new Cell();
            rcell.occupyingFigurine = new Rook(colour);
            rpic.Source = new BitmapImage(new Uri(rcell.occupyingFigurine.symbol, UriKind.Relative));
            Rook.Content = rpic;
            Cell kcell = new Cell(); 
            kcell.occupyingFigurine = new Knight(colour);
            kpic.Source = new BitmapImage(new Uri(kcell.occupyingFigurine.symbol, UriKind.Relative));
            Knight.Content = kpic;
            Cell bcell = new Cell();
            bcell.occupyingFigurine = new Bishop(colour);
            bpic.Source = new BitmapImage(new Uri(bcell.occupyingFigurine.symbol, UriKind.Relative));
            Bishop.Content = bpic;
            promo.Visibility= Visibility.Visible;
        }

        private void Promo_Click(object sender, RoutedEventArgs e)
        {
            string sendername = ((Button)sender).Name;
            Debug.Content= sendername;
            Cell cell = gamefield.cells.Find(cell => cell.posX == xTemp && cell.posY == yTemp);
            switch (sendername)
            {
                case "Queen":
                    cell.occupyingFigurine = new Queen(colourTemp);
                break;
                case "Rook":
                cell.occupyingFigurine = new Rook(colourTemp);
                break;
                case "Knight":
                    cell.occupyingFigurine = new Knight(colourTemp);
                    break;
                case "Bishop":
                    cell.occupyingFigurine = new Bishop(colourTemp); 
                    break;
            }
            cell.PlaceFigurine();
            promo.Visibility = Visibility.Collapsed;
            foreach (UIElement element in board.Children)
            {
                element.IsEnabled = true;
            }
        }
    }
}

