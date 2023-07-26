﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
        public bool Check (int xaxis, int yaxis)
        {
            List<Cell> check = new List<Cell>();
            Cell current = cells.Find(cell => cell.posX == xaxis && cell.posY == yaxis);
            if (current.occupyingFigurine.GetType()!=typeof(Pawn))
            {
                check.AddRange(CalcMoves(current.posX, current.posY));
            }
            else
            {
                List<(int, int)> coord = new List<(int, int)>();
                int sense = yaxis;
                if (current.occupyingFigurine.colour == "black")
                {
                    sense = yaxis + 1;
                }
                else
                {
                    sense = yaxis - 1;
                }
                Cell foesX = cells.Find(cell => cell.posX == xaxis - 1 && cell.posY == sense);
                if (foesX != null && foesX.occupyingFigurine != null)
                {
                    if (foesX.occupyingFigurine.colour != current.occupyingFigurine.colour)
                    {
                        coord = (current.occupyingFigurine.Move(xaxis, yaxis, 1, 2));
                        Cell toAdd = cells.Find(cell => cell.posX == coord[0].Item1 && cell.posY == coord[0].Item2);
                        check.Add(toAdd);
                    }
                }
                Cell foesY = cells.Find(cell => cell.posX == xaxis + 1 && cell.posY == sense);
                if (foesY != null && foesY.occupyingFigurine != null)
                {
                    if (foesY.occupyingFigurine.colour != current.occupyingFigurine.colour)
                    {
                        coord = (current.occupyingFigurine.Move(xaxis, yaxis, 1, 1));
                        Cell toAdd = cells.Find(cell => cell.posX == coord[0].Item1 && cell.posY == coord[0].Item2);
                        check.Add(toAdd);
                    }
                }
            }
            foreach (Cell cell in check)
            {
                if (cell.occupyingFigurine != null)
                { 
                    if (cell.occupyingFigurine.GetType()==typeof(King) && cell.occupyingFigurine.colour!=current.ContentStringFormat)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Checkmate(int xaxis, int yaxis, string colour)
        {
            List<Cell> traj = new List<Cell>(cells.FindAll(cell => cell.posX == xaxis && cell.posY==yaxis));
            List<Cell> king = new List<Cell>(cells.FindAll(cell => cell.occupyingFigurine != null && cell.occupyingFigurine.colour != colour && cell.occupyingFigurine.GetType()==typeof(King)));
            List<Cell> attacker = new List<Cell>(cells.FindAll(cell => cell.occupyingFigurine!=null && cell.occupyingFigurine.colour==colour));
            List<Cell> defender = new List<Cell>(cells.FindAll(cell => cell.occupyingFigurine != null && cell.occupyingFigurine.colour != colour));
            List<Cell> attackermove = attacker.GetRange(0, attacker.Count);
            List<Cell> defendermove = defender.GetRange(0, defender.Count);
            king.AddRange(CalcMoves(king[0].posX, king[0].posY));
            List<(int, int)> coord = new List<(int, int)>();
            foreach (Cell cell in attacker)
            {
                if (cell.occupyingFigurine.GetType() != typeof(Pawn))
                {
                    attackermove.AddRange(CalcMoves(cell.posX, cell.posY));
                }
                else
                {
                    
                    int sense = yaxis;
                    if (cell.occupyingFigurine.colour == "black")
                    {
                        sense = yaxis + 1;
                    }
                    else
                    {
                        sense = yaxis - 1;
                    }
                    Cell foesX = cells.Find(cell => cell.posX == xaxis - 1 && cell.posY == sense);
                    if (foesX != null && foesX.occupyingFigurine != null)
                    {
                        if (foesX.occupyingFigurine.colour != cell.occupyingFigurine.colour)
                        {
                            coord = (cell.occupyingFigurine.Move(xaxis, yaxis, 1, 2));
                            Cell toAdd = cells.Find(cell => cell.posX == coord[0].Item1 && cell.posY == coord[0].Item2);
                            attackermove.Add(toAdd);
                        }
                    }
                    Cell foesY = cells.Find(cell => cell.posX == xaxis + 1 && cell.posY == sense);
                    if (foesY != null && foesY.occupyingFigurine != null)
                    {
                        if (foesY.occupyingFigurine.colour != cell.occupyingFigurine.colour)
                        {
                            coord = (cell.occupyingFigurine.Move(xaxis, yaxis, 1, 1));
                            Cell toAdd = cells.Find(cell => cell.posX == coord[0].Item1 && cell.posY == coord[0].Item2);
                            attackermove.Add(toAdd);
                        }
                    }
                }
            }
            
            foreach (Cell cell in defender)
            {
                defendermove.AddRange(CalcMoves(cell.posX, cell.posY));
            }
            Cell current = traj[0];
            int range = 0;
            
            //trajectory calc

            //to do: Check if the king can move away, or a friendly piece can move in a field that is the figurine or can stand in the way.
            /* if check is declared: call this function. 3 Flags:
             * Generate a list out of: All potential king in danger move. Generate a List of all potential enemy color moves. Substract the duplicates. If it = 0, first flag.
             * Generate a list out of: All fields inbetween the king and the attacker. Generate a List of all potential defender color moves. substract any duplicate. If the List is exactly the same size as before the substraction, second and third flag 
             * trajectory of the Attacker needs a seperate function since only one of these directions is relevant. 
             * the function should look like CalcMoves() with the addotopm that it disregards any path that doesn't contain the king of the opposite colour. This means after every j iteration, it checks if any of the items in that list contain the king of
             * the opposite colour. If it doesn't it clears the list up, if it does, it breaks the loop.
             */
            int trajchk = traj.Count;
            king.RemoveAll(cell => attacker.Contains(cell));
            traj.RemoveAll(cell => defender.Contains(cell));

            Trace.WriteLine(king.Count + " tracking");
            Trace.WriteLine(traj.Count + " " + trajchk);
            if (king.Count == 0 && traj.Count==trajchk)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}