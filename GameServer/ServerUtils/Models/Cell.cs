namespace GameServer.ServerUtils.Models
{
    internal class Cell
    {
        #region ENUM
        public enum STATE { VOID, CROSS, CIRCLE };
        #endregion

        #region ATTRIBUTES
        internal STATE state;
        #endregion

        #region CONSTRUCTOR
        public Cell()
        {
            state = STATE.VOID;
        }
        #endregion

        #region FUNCTIONS
        internal void SetState(STATE newState)
        {
            state = newState;
        }
        #endregion

    }
}
