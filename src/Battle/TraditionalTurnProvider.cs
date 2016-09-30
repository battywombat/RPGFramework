using System.Collections.Generic;

namespace RPGFramework
{
    public class TraditionalTurnProvider : TurnProvider
    {

        List<Character> allCharacters;
        List<Character> needMoveCharacters;

        public override Battle CurrentBattle
        { 
            set
            {
                currentBattle = value;
                allCharacters.Clear();
                needMoveCharacters.Clear();
                allCharacters.AddRange(currentBattle.playerParty);
                allCharacters.AddRange(currentBattle.enemyParty);
                needMoveCharacters.AddRange(allCharacters);
            }
        }

        public TraditionalTurnProvider()
        {
            allCharacters = new List<Character>();
            needMoveCharacters = new List<Character>();
        }
        public override void doNextAction()
        {
            if (needMoveCharacters.Count == 0)
            {
                doNextMove();
            }
            else
            {
                getNextMove();
            }
            if (currentBattle.turnOrder.Count == 0)
            {
                needMoveCharacters.AddRange(allCharacters);
            }
        }

        private void doNextMove()
        {
            MoveIntent nextMove = currentBattle.turnOrder[0];
            nextMove.doMove();
            currentBattle.turnOrder.Remove(nextMove);
        }

        private void getNextMove()
        {
            Character currentChar = needMoveCharacters[0];
            MoveIntent m = currentChar.getMove(CurrentBattle);
            currentBattle.turnOrder.Add(m);
            needMoveCharacters.RemoveAt(0);
            if (needMoveCharacters.Count == 0)
            {
                currentBattle.turnOrder.Sort((m1, m2) =>  m1.MovePriority - m2.MovePriority);
            }
        }
    }
}