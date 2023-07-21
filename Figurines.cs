using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<int> xDir = new List<int>();
        public List<int> yDir = new List<int>();
        public int directions;
        public abstract List<(int, int)> Move(int axisX, int axisY, int aug, int dir);

    }
    public class King : Figurines
    {

        public King(string team)
        {
            int[] yArr = new int[] { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] xArr = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
            yDir.AddRange(yArr); 
            xDir.AddRange(xArr);
            colour = team;

            if (colour == "white")
            {
                symbol = "images/kingwhite.png";

            }
            else if (colour == "black")
            {
                symbol = "images/kingblack.png";
            }
        }
        
    
        public override List<(int, int)> Move(int axisX, int axisY, int aug, int dir)
        {
            moves.Clear();
            int newx = axisX + (aug * xDir[dir]);
            int newy = axisY + (aug * yDir[dir]);
            moves.Add((newx, newy));
            return moves;
        }
    }

    public class Queen : Figurines
    {
        
        public Queen(string team)
        {
            int[] yArr = new int[] { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] xArr = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
            yDir.AddRange(yArr);
            xDir.AddRange(xArr);
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
        public override List<(int, int)> Move(int axisX, int axisY, int aug, int dir)
        {
            moves.Clear();
            int newx = axisX + (aug * xDir[dir]);
            int newy = axisY + (aug * yDir[dir]);
            moves.Add((newx, newy));
            return moves;
        }
    }
    public class Bishop : Figurines
    {

        public Bishop(string team)
        {
            int[] yArr = new int[] { 1, 1, -1, -1 };
            int[] xArr = new int[] { -1, 1, 1, -1 };
            yDir.AddRange(yArr);
            xDir.AddRange(xArr);
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
        public override List<(int, int)> Move(int axisX, int axisY, int aug, int dir)
        {
            moves.Clear();
            int newx = axisX + (aug * xDir[dir]);
            int newy = axisY + (aug * yDir[dir]);
            moves.Add((newx, newy));
            return moves;
        }
        }
        public class Knight : Figurines
        {

        public Knight(string team)
        {
            int[] yArr = new int[] { 2, 2, -2, -2, 1, -1, 1, -1 };
            int[] xArr = new int[] { 1, -1, 1, -1, 2, 2, -2, -2 };
            yDir.AddRange(yArr);
            xDir.AddRange(xArr);

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
            public override List<(int, int)> Move(int axisX, int axisY, int aug, int dir)
            {
            moves.Clear();
            int newx = axisX + (aug * xDir[dir]);
            int newy = axisY + (aug * yDir[dir]);
            moves.Add((newx, newy));
            return moves;
        }
        }
        public class Rook : Figurines
        {
        
        public Rook(string team)
        {
            int[] xArr = new int[] { 0, 1, 0, -1 };
            int[] yArr = new int[] { 1, 0, -1, 0 };
            xDir.AddRange(xArr);
            yDir.AddRange(yArr);
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
            public override List<(int, int)> Move(int axisX, int axisY, int aug, int dir)
            {
            moves.Clear();
            int newx = axisX + (aug * xDir[dir]);
            int newy = axisY + (aug * yDir[dir]);
            moves.Add((newx, newy));
            return moves;
            }
        }
        public class Pawn : Figurines
        {
            public Pawn(string team)
            {
                int[] yArr = new int[] { 1, 1, 1 };
                int[] xArr = new int[] { 0, 1, -1 };
                yDir.AddRange(yArr);
                xDir.AddRange(xArr);
                colour = team;
                if (colour == "white")
                {
                    symbol = "images/pawnwhite.png";
                }
                else if (colour == "black")
                {
                    symbol = "images/pawnblack.png";
                
                }
            }
            public override List<(int, int)> Move(int axisX, int axisY, int aug, int dir)
            {
            moves.Clear();
            int newx=0;
            int newy=0;
            if (colour == "white")
            {
                newx = axisX + (aug * xDir[dir]);
                newy = axisY - (aug *  yDir[dir]);

            }
            else if (colour == "black")
            {
                newx = axisX + (aug * xDir[dir]);
                newy = axisY + (aug * yDir[dir]);
            }
            Trace.WriteLine(newx+ "" + newy);
            moves.Add((newx, newy));
            return moves;
            }
        }
    }

