
namespace RPGFramework
{
    interface GameFrontend
    {
        /// <summary>
        /// Select a single move from a character's possible moves.
        /// </summary>
        /// <param name="c">The character we select the moves from</param>
        /// <returns></returns>
        Move selectMove(Character c);

        ///<summary>
        /// Select a single character from an array.
        /// </summary>
        /// <returns>A selected character, or null if not character is to be selected</returns>
        Character selectCharacter(Character[] characters);

        /// <summary>
        /// Display the given move on the interface.
        /// </summary>
        /// <param name="m">The move intent carrying all information about the move</param>
        void ExecuteMove(MoveIntent m);
    }
    class Frontend
    {
        public static GameFrontend Instance
        {
            get
            {
                if (frontend == null)
                {
                    frontend = new CLIFrontend();
                }
                return frontend;
            }
            set 
            {
                frontend = value;
            }
        }

        private static GameFrontend frontend = null;
    }
}