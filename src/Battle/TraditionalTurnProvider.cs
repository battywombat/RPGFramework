using System.Collections.Generic;

namespace RPGFramework
{
    public class TraditionalTurnProvider : TurnProvider
    {

        List<Character> _allCharacters;
        List<Character> _needMoveCharacters;

        public override Battle CurrentBattle
        { 
            set
            {
                currentBattle = value;
                _allCharacters.Clear();
                _needMoveCharacters.Clear();
                _allCharacters.AddRange(currentBattle.PlayerParty);
                _allCharacters.AddRange(currentBattle.EnemyParty);
                _needMoveCharacters.AddRange(_allCharacters);
            }
        }

        public TraditionalTurnProvider()
        {
            _allCharacters = new List<Character>();
            _needMoveCharacters = new List<Character>();
        }
        public override void doNextAction()
        {
            if (_needMoveCharacters.Count == 0)
            {
                doNextMove();
            }
            else
            {
                getNextMove();
            }
            if (currentBattle.TurnOrder.Count == 0)
            {
                _needMoveCharacters.AddRange(_allCharacters);
            }
        }

        private void doNextMove()
        {
            MoveIntent nextMove = currentBattle.TurnOrder[0];
            nextMove.doMove();
            currentBattle.TurnOrder.Remove(nextMove);
        }

        private void getNextMove()
        {
            Character currentChar = _needMoveCharacters[0];
            MoveIntent m = currentChar.getMove(CurrentBattle);
            currentBattle.TurnOrder.Add(m);
            _needMoveCharacters.RemoveAt(0);
            if (_needMoveCharacters.Count == 0)
            {
                currentBattle.TurnOrder.Sort((m1, m2) =>  m1.MovePriority - m2.MovePriority);
            }
        }
    }
}