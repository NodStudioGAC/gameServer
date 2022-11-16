using System.Collections.Generic;

namespace GameServer.ServerUtils.Models
{
    internal class Line
    {

        #region ATTRIBUTES
        internal List<Cell> cells;
        #endregion

        #region CONSTRUCTOR
        public Line()
        {
            cells = new List<Cell>();
        }
        #endregion

        #region FUNCTIONS
        internal bool IsFull()
        {
            foreach(Cell cell in cells)
                if(cell.state == Cell.STATE.VOID)
                    return false;

            return true;
        }
        internal Cell.STATE IsCompleted()
        {
            Cell.STATE currentState = cells[0].state;
            foreach (Cell cell in cells)
            {
                if (cell.state == Cell.STATE.VOID)
                    return Cell.STATE.VOID;

                if (cell.state != currentState)
                    return Cell.STATE.VOID;
            }

            return currentState;
        }
        #endregion

    }
}
