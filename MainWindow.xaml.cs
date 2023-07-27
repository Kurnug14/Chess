using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

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
        string lastfen = "";
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
            gamefield.width = 8; gamefield.height = 8;
            gamefield.cells.Clear();
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
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
                        Margin = new Thickness((i * 100), (j * 100), 0, 0)
                    };

                    if ((i % 2 == 1 && j % 2 == 1) || (i % 2 == 0 && j % 2 == 0))
                    {
                        cell.Background = Brushes.Beige;
                    }
                    if (j == 0)
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
                    else if (j == 1)
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
        private void Moving(object sender, EventArgs e)
        {
            
            Cell clicked = (Cell)sender;
            if (clicked.occupyingFigurine != null && temp == null)
            {
                lastfen = FenGen();
                if (clicked.occupyingFigurine.colour == player)
                {
                    checkMove.AddRange(gamefield.CalcMoves(clicked.posX, clicked.posY, false, false));
                    temp = clicked.occupyingFigurine;
                    clicked.occupyingFigurine = null;
                    clicked.PlaceFigurine();
                    xTemp = clicked.posX;
                    yTemp = clicked.posY;
                }
            }
            else if (temp != null && checkMove.Contains(clicked))
            {
                if (clicked.occupyingFigurine != null)
                {
                    if (clicked.occupyingFigurine.GetType() == typeof(King))
                    {
                        MessageBox.Show(player + " has won!");
                        winner = true;
                    }
                }
                clicked.occupyingFigurine = temp;
                clicked.PlaceFigurine();
                temp = null;
                
                if (clicked.occupyingFigurine != null)
                {
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
                    Player.Content = player;
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
                if (gamefield.Check(clicked.posX, clicked.posY))
                {
                    if (gamefield.Checkmate(clicked.posX, clicked.posY, clicked.occupyingFigurine.colour))
                    {
                        MessageBox.Show(player + " has been checkmated!");
                    }
                    else
                    {
                        MessageBox.Show(player + " is in check!");
                    }
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
            lastfen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w";
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
            promo.Visibility = Visibility.Visible;
        }
        private void Promo_Click(object sender, RoutedEventArgs e)
        {
            string sendername = ((Button)sender).Name;
            Debug.Content = sendername;
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
        private void Save_Click(object sender, RoutedEventArgs e)
        {

            int fencounter = 0;
            string readfile = "";
            int slasher = 0;
            readfile = FenGen();

            SaveFileDialog saveGame = new SaveFileDialog();
            string initPath = AppDomain.CurrentDomain.BaseDirectory;
            string initDir = System.IO.Path.Combine(initPath, "savegames");
            saveGame.InitialDirectory = initDir;
            saveGame.Filter = "Text File |*.txt";
            if (saveGame.ShowDialog() == true)
            {
                string filename = saveGame.FileName;
                try
                {
                    File.WriteAllText(filename, readfile);
                    MessageBox.Show("Game saved successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }
        public string FenGen()
        {
            int fencounter = 0;
            string fennote = "";
            int slasher = 0;
            foreach (Cell cell in gamefield.cells)
            {
                if (cell.occupyingFigurine == null)
                {
                    fencounter++;
                    slasher++;
                }
                else
                {
                    if (fencounter != 0)
                    {
                        fennote += fencounter.ToString();
                        fencounter = 0;
                    }
                    if (cell.occupyingFigurine.GetType() == typeof(King))
                    {
                        if (cell.occupyingFigurine.colour == "white")
                        {
                            fennote += 'K';
                        }
                        else
                        {
                            fennote += 'k';
                        }
                    }
                    else if (cell.occupyingFigurine.GetType() == typeof(Queen))
                    {
                        if (cell.occupyingFigurine.colour == "white")
                        {
                            fennote += 'Q';
                        }
                        else
                        {
                            fennote += 'q';
                        }
                    }
                    else if (cell.occupyingFigurine.GetType() == typeof(Bishop))
                    {
                        if (cell.occupyingFigurine.colour == "white")
                        {
                            fennote += 'B';
                        }
                        else
                        {
                            fennote += 'b';
                        }
                    }
                    else if (cell.occupyingFigurine.GetType() == typeof(Knight))
                    {
                        if (cell.occupyingFigurine.colour == "white")
                        {
                            fennote += 'N';
                        }
                        else
                        {
                            fennote += 'n';
                        }
                    }
                    else if (cell.occupyingFigurine.GetType() == typeof(Rook))
                    {
                        if (cell.occupyingFigurine.colour == "white")
                        {
                            fennote += 'R';
                        }
                        else
                        {
                            fennote += 'r';
                        }
                    }
                    else if (cell.occupyingFigurine.GetType() == typeof(Pawn))
                    {
                        if (cell.occupyingFigurine.colour == "white")
                        {
                            fennote += 'P';
                        }
                        else
                        {
                            fennote += 'p';
                        }
                    }

                    slasher++;
                }
                if (slasher == 8)
                {
                    if (fencounter != 0)
                    {
                        fennote += fencounter.ToString();
                        fencounter = 0;
                    }
                    fennote += '/';
                    slasher = 0;
                    fencounter = 0;
                }
            }
            fennote = fennote.Remove(fennote.Length - 1);
            fennote += ' ';
            if (player == "white")
            {
                fennote += 'w';
            }
            else
            {
                fennote += 'b';
            }
            return fennote;
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openSave = new OpenFileDialog();
            openSave.Filter = "Text File|*.txt";
            string fencode = "";
            
            if (openSave.ShowDialog()==true)
            {
                string fileName = openSave.FileName;
                fencode = File.ReadAllText(fileName);
            }
            LoadFen(fencode);
            
        }
        private void LoadFen (string fencode)
        {
            int fentrack = 0;
            int col = 0;
            int row = 0;
            MakeField();
            foreach (char fen in fencode)
            {
                fentrack++;
                if (Char.IsNumber(fen))
                {

                    int goal = (int)Char.GetNumericValue(fen);
                    for (int i = 0; i < goal; i++)
                    {
                        Cell ecell = gamefield.cells.Find(cell => cell.posX == col && cell.posY == row);
                        ecell.occupyingFigurine = null;
                        col++;
                    }
                }
                else if (Char.IsUpper(fen))
                {
                    Cell wcell = gamefield.cells.Find(cell => cell.posX == col && cell.posY == row);
                    switch (fen)
                    {
                        case 'R':
                            wcell.occupyingFigurine = new Rook("white");
                            break;
                        case 'N':
                            wcell.occupyingFigurine = new Knight("white");
                            break;
                        case 'B':
                            wcell.occupyingFigurine = new Bishop("white");
                            break;
                        case 'Q':
                            wcell.occupyingFigurine = new Queen("white");
                            break;
                        case 'K':
                            wcell.occupyingFigurine = new King("white");
                            break;
                        case 'P':
                            wcell.occupyingFigurine = new Pawn("white");
                            break;
                    }
                    col++;
                }
                else if (Char.IsLower(fen))
                {
                    Cell bcell = gamefield.cells.Find(cell => cell.posX == col && cell.posY == row);
                    switch (fen)
                    {
                        case 'r':
                            bcell.occupyingFigurine = new Rook("black");
                            break;
                        case 'n':
                            bcell.occupyingFigurine = new Knight("black");
                            break;
                        case 'b':
                            bcell.occupyingFigurine = new Bishop("black");
                            break;
                        case 'q':
                            bcell.occupyingFigurine = new Queen("black");
                            break;
                        case 'k':
                            bcell.occupyingFigurine = new King("black");
                            break;
                        case 'p':
                            bcell.occupyingFigurine = new Pawn("black");
                            break;
                    }
                    col++;
                }
                else if (fen == '/')
                {
                    col = 0;
                    row++;
                }
                else if (fen == ' ')
                {
                    break;
                }
            }
            foreach (Cell cell in gamefield.cells)
            {
                cell.PlaceFigurine();
            }
            if (fencode[fentrack] == 'w')
            {
                player = "white";
                Player.Content = player;
                Player.Background = Brushes.White;
                Player.Foreground = Brushes.Black;
            }
            else if (fencode[fentrack] == 'b')
            {
                player = "black";
                Player.Content = player;
                Player.Background = Brushes.Black;
                Player.Foreground = Brushes.White;
            }
        }

        private void revert_Click(object sender, RoutedEventArgs e)
        {
            LoadFen(lastfen);
        }
    }
}