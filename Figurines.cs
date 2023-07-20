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
    public abstract class Figurines
    {
        public string colour;
        public string symbol;
        public List<(int, int)> moves = new List<(int, int)>();
        public int directions;
        public abstract List<(int, int)> Move(int axisX, int axisY, int dir);

    }
    public class King : Figurines
    {
        public int[] yDir = new int[] { 1, 1, 1, 0, -1, -1, -1, 0 };
        int[] xDir = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
        public King(string team)
        {
            colour = team;
            if (colour == "white")
            {
                symbol = "images/kingwhite.png";
                
            }
            else if (colour == "black")
            {
                symbol = "images/kingblack.png";
            }
            colour = team;
        }
        public override List<(int, int)> Move(int axisX, int axisY, int dir)
        {
            moves.Clear();

            return moves;
        }
    }

    public class Queen : Figurines
    {
        public int[] yDir = new int[] { 1, 1, 1, 0, -1, -1, -1, 0 };
        int[] xDir = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
        public Queen(string team)
        {
            colour = team;
            if (colour == "white")
            {
                symbol = "images/queenwhite.png";
            }
            else if (colour == "black")
            {
                symbol = "images/queenblack.png";
            }
        }
        public override List<(int, int)> Move(int axisX, int axisY, int dir)
        {
            moves.Clear();

            return moves;
        }
    }
    public class Bishop : Figurines
    {
        public int[] yDir = new int[] { 1, 1, -1, -1};
        int[] xDir = new int[] { -1, 1, 1, -1};
        public Bishop(string team)
        {
            colour = team;
            if (colour == "white")
            {
                symbol = "images/bishopwhite.png";
            }
            else if (colour == "black")
            {
                symbol = "images/bishopblack.png";
            }
        }
        public override List<(int, int)> Move(int axisX, int axisY, int dir)
        {
            moves.Clear();

            return moves;
        }
        }
        public class Knight : Figurines
        {

        public int[] yDir = new int[] { 2,  2,  -2,  -2,  1, -1,  1, -1};
        int[] xDir = new int[] { 1, -1,   1,  -1,  2,  2, -2,  -2};
        public Knight(string team)
        {
            colour = team;
            if (colour == "white")
                {
                    symbol = "images/knightwhite.png";
                }
                else if (colour == "black")
                {
                    symbol = "images/knightblack.png";
                }
            }
            public override List<(int, int)> Move(int axisX, int axisY, int dir)
            {
            moves.Clear();

            return moves;
            }
        }
        public class Rook : Figurines
        {
        public int[] yDir = new int[] { 1, 0, -1, 0 };
        int[] xDir = new int[] { 0, 1, 0, -1 };
        public Rook(string team)
        {
            colour = team;
            if (colour == "white")
                {
                    symbol = "images/rookwhite.png";
                }
                else if (colour == "black")
                {
                    symbol = "images/rookblack.png";
                }
            }
            public override List<(int, int)> Move(int axisX, int axisY, int dir)
            {
            moves.Clear();

            return moves;
            }
        }
        public class Pawn : Figurines
        {
            public int[] yDir = new int[] { 1, 1,  1 };
            int[] xDir = new int[] { 0, 1, -1 };
            int colourDir = 0;
        public Pawn(string team)
        {
            colour = team;
            if (colour == "white")
                {
                    symbol = "images/pawnwhite.png";
                }
                else if (colour == "black")
                {
                    symbol = "images/pawnblack.png";
                colourDir = -1;
                }
            }
            public override List<(int, int)> Move(int axisX, int axisY, int dir)
            {
            moves.Clear();
            if (colour == "white")
                {
                    moves.Add((axisX, axisY + 1));
                }
                else if (colour == "black")
                {
                    moves.Add((axisX, axisY - 1));
                }
                return moves;
            }
        }
    }

