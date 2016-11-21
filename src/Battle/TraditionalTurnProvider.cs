using System.Collections.Generic;

namespace RPGFramework
{
    public class TraditionalTurnProvider : TurnProvider
    {

        List<Character> _allCharacters;
        List<Character> _needMoveCharacters;
        Battle _currentBattle;

        public Battle CurrentBattle
        { 
            set
            {
                _currentBattle = value;
                _allCharacters.Clear();
                _needMoveCharacters.Clear();
                _allCharacters.AddRange(_currentBattle.PlayerParty);
                _allCharacters.AddRange(_currentBattle.EnemyParty);
                _needMoveCharacters.AddRange(_allCharacters);
            }
        }

        public TraditionalTurnProvider()
        {
            _allCharacters = new List<Character>();
            _needMoveCharacters = new List<Character>();
        }
        public void doNextAction()
        {
            if (_needMoveCharacters.Count == 0)
            {
                doNextMove();
            }
            else
            {
                getNextMove();
            }
            if (_currentBattle.TurnOrder.Count == 0)
            {
                _needMoveCharacters.AddRange(_allCharacters);
            }
        }

        private void doNextMove()
        {
            MoveIntent nextMove = _currentBattle.TurnOrder[0];
            nextMove.doMove();
            _currentBattle.TurnOrder.Remove(nextMove);
        }

        private void getNextMove()
        {
            Character currentChar = _needMoveCharacters[0];
            MoveIntent m = currentChar.getMove(_currentBattle);
            _currentBattle.TurnOrder.Add(m);
            _needMoveCharacters.RemoveAt(0);
            if (_needMoveCharacters.Count == 0)
            {
                _currentBattle.TurnOrder.Sort((m1, m2) =>  m1.MovePriority - m2.MovePriority);
            }
        }
    }
}