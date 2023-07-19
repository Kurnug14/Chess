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

        public abstract void Move(int axisX, int axisY);

    }
    public class King : Figurines 
    {
        public King(string colour) 
        {
            if (colour == "white")
            { 
                symbol = "images/kingwhite.png";
            }
            else if (colour== "black") 
            {
                symbol = "images/kingblack.png";
            }
        }
        public override void Move(int axisX, int axisY)
        {
            throw new NotImplementedException();
        }
    }

    public class Queen : Figurines
    {
        public Queen(string colour)
        {
            if (colour == "white")
            {
                symbol = "images/queenwhite.png";
            }
            else if (colour == "black")
            {
                symbol = "images/queenblack.png";
            }
        }
        public override void Move(int axisX, int axisY)
        {
            throw new NotImplementedException();
        }
    }
    public class Bishop : Figurines
    {
        public Bishop(string colour)
        {
            if (colour == "white")
            {
                symbol = "images/bishopwhite.png";
            }
            else if (colour == "black")
            {
                symbol = "images/bishopblack.png";
            }
        }
        public override void Move(int axisX, int axisY)
        {
            throw new NotImplementedException();
        }
    }
    public class Knight : Figurines
    {
        public Knight(string colour)
        {
            if (colour == "white")
            {
                symbol = "images/knightwhite.png";
            }
            else if (colour == "black")
            {
                symbol = "images/knightblack.png";
            }
        }
        public override void Move(int axisX, int axisY)
        {
            throw new NotImplementedException();
        }
    }
    public class Rook : Figurines
    {
        public Rook(string colour)
        {
            if (colour == "white")
            {
                symbol = "images/rookwhite.png";
            }
            else if (colour == "black")
            {
                symbol = "images/rookblack.png";
            }
        }
        public override void Move(int axisX, int axisY)
        {
            throw new NotImplementedException();
        }
    }
    public class Pawn : Figurines
    {
        public Pawn(string colour)
        {
            if (colour == "white")
            {
                symbol = "images/pawnwhite.png";
            }
            else if (colour == "black")
            {
                symbol = "images/pawnblack.png";
            }
        }
        public override void Move(int axisX, int axisY)
        {
            throw new NotImplementedException();
        }
    }
}
