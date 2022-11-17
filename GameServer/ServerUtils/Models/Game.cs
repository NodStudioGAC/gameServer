using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.ServerUtils.Models
{
    internal class Game {

        #region ENUM
        public enum STEP { INIT, STARTED, PLAYED, CANPLAY, END };
        #endregion

        #region ATTRIBUTES
        Cell[,] cells;
        internal Player[] players;
        internal Player currentPlayer;
        Line[] lines;
        internal STEP step;
        #endregion

        #region CONSTRUCTOR

        public Game(Player[] players)
        {
            this.players = players;
            currentPlayer = players[0];
            cells = new Cell[3, 3];
            lines = new Line[8];

            InitCells();
            InitLines();
        }
        #endregion

        #region FUNCTIONS
        void InitCells()
        {
            for (int i = 0; i<3; i++)
                for(int j = 0; j<3; j++)
                    cells[i, j] = new Cell();
        }

        void InitLines()
        {
            for (int i = 0; i < 8; i++)
                    lines[i] = new Line();

            int k = 0;
            for (int i = 0; i<3; i++)
            {
                for(int j = 0; j<3; j++)
                {
                    if((i == 0 || i == 2) && j == 0)
                    {
                        lines[i == 0 ? 6 : 7].cells.Add(cells[i, j]);
                        lines[i == 0 ? 6 : 7].cells.Add(cells[1, 1]);
                        lines[i == 0 ? 6 : 7].cells.Add(cells[2 - i, 2 - j]);
                    }
                    lines[k].cells.Add(cells[i, j]);
                    lines[k + 1].cells.Add(cells[j, i]);
                }
                k += 2;
            }
        }

        internal bool Play(Client client, int[] coor)
        {
            if (step == STEP.CANPLAY && client.id == currentPlayer.client.id && cells[coor[0], coor[1]].state == Cell.STATE.VOID)
            {
                cells[coor[0], coor[1]].SetState(players[0].client.id == client.id ? Cell.STATE.CROSS : Cell.STATE.CIRCLE);
                step = STEP.INIT;
                return true;
            }
            return false;
        }
        internal void ChangeTurn()
        {
            currentPlayer = players[0].client.id == currentPlayer.client.id ? players[1] : players[0];
            step = STEP.CANPLAY;
        }

        internal KeyValuePair<bool, Cell.STATE> IsEndGame()
        {
            foreach(Line line in lines)
            {
                Cell.STATE currentState = line.IsCompleted();
                if (currentState != Cell.STATE.VOID)
                    return new KeyValuePair<bool, Cell.STATE>(true, currentState);
            }
            return new KeyValuePair<bool, Cell.STATE>(false, Cell.STATE.VOID);
        }


        #endregion

    }


}
