using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chess
{
    public class GameField
    {
        public List<Cell> cells= new List<Cell>();
        public int width;
        public int height;
        List<Cell> potentialmoves = new List<Cell>();

        public List<Cell> CalcMoves(int xaxis, int yaxis)
        {
            potentialmoves.Clear();
            int range = 0;
      
            Cell current = cells.Find(cell=> cell.posX== xaxis&& cell.posY== yaxis);
            List<(int, int)> coord = new List<(int, int)>();
            if (current.occupyingFigurine.GetType() == typeof(King) || current.occupyingFigurine.GetType() == typeof(Pawn) || current.occupyingFigurine.GetType() == typeof(Knight))
            {
                range = 2;
                if (current.occupyingFigurine.GetType() == typeof(Pawn) && (current.posY == 1 || current.posY==6))
                {
                    range = 3;
                }
            }
            else
            {
                range = 8;
            }
            if (current.occupyingFigurine.GetType() != typeof(Pawn))
            {
                for (int i = 0; i < current.occupyingFigurine.xDir.Count(); i++)
                {
                    for (int j = 0; j < range; j++)
                    {
                        coord = (current.occupyingFigurine.Move(xaxis, yaxis, j, i));
                        Cell toAdd = cells.Find(cell => cell.posX == coord[0].Item1 && cell.posY == coord[0].Item2);
                        if (toAdd != null && toAdd.occupyingFigurine == null || j == 0)
                        {
                            potentialmoves.Add(toAdd);
                        }
                        else if (toAdd != null && toAdd.occupyingFigurine.colour != current.occupyingFigurine.colour)
                        {
                            potentialmoves.Add(toAdd);
                            break;
                        }
                        else if (toAdd == null || toAdd.occupyingFigurine.colour == current.occupyingFigurine.colour)
                        {
                            break;
                        }
                    }
                }
            }

            else
            {
                int sense = yaxis;
                if (current.occupyingFigurine.colour == "black")
                {
                    sense = yaxis + 1;
                }
                else
                {
                    sense = yaxis - 1;
                }
                Cell foesX = cells.Find(cell => cell.posX == xaxis - 1 && cell.posY==sense);
                if (foesX != null && foesX.occupyingFigurine != null)
                {
                    if (foesX.occupyingFigurine.colour != current.occupyingFigurine.colour)
                    {
                        coord = (current.occupyingFigurine.Move(xaxis, yaxis, 1, 2));
                        Cell toAdd = cells.Find(cell => cell.posX == coord[0].Item1 && cell.posY == coord[0].Item2);
                        potentialmoves.Add(toAdd);
                    }
                }
                Cell foesY = cells.Find(cell => cell.posX == xaxis + 1 && cell.posY == sense);
                if (foesY   != null && foesY.occupyingFigurine != null)
                {
                    if (foesY.occupyingFigurine.colour != current.occupyingFigurine.colour) 
                    {
                        coord = (current.occupyingFigurine.Move(xaxis, yaxis, 1, 1));
                    Cell toAdd = cells.Find(cell => cell.posX == coord[0].Item1 && cell.posY == coord[0].Item2);
                        potentialmoves.Add(toAdd);
                    }
                }
                for (int j = 0; j < range; j++)
                {
                    coord = (current.occupyingFigurine.Move(xaxis, yaxis, j, 0));
                    Cell toAdd = cells.Find(cell => cell.posX == coord[0].Item1 && cell.posY == coord[0].Item2);
                    if (toAdd != null && toAdd.occupyingFigurine == null || j == 0)
                    {
                        potentialmoves.Add(toAdd);
                    }
                }
            }
            return potentialmoves;
        }
    }
}

