using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class GameField
    {
        public List<Cell> cells= new List<Cell>();
        public int width;
        public int height;
        public List<Cell> potentialmoves= new List<Cell>();

        public void CalcMoves(int xaxis, int yaxis)
        {
            int range = 0;
            Cell current = cells.Find(cell=> cell.posX== xaxis&& cell.posY== yaxis);
            if (current.occupyingFigurine.GetType() == typeof(King) || current.occupyingFigurine.GetType() == typeof(Pawn) || current.occupyingFigurine.GetType() == typeof(Knight))
            {
                range = 1;
            }
            else
            {
                range = 7;
            }
            
        }
    }
}
