using System.Collections.Generic;

namespace RPGFramework
{
    public class Battle
    {
        bool _ended;
        public List<Character> PlayerParty;
        public List<Character> EnemyParty;
        public List<MoveIntent> TurnOrder;
        TurnProvider turnProvider;
        public Battle(Character[] playerParty, Character[] enemyParty, TurnProvider turnProvider)
        {
            this.PlayerParty = new List<Character>(playerParty);
            this.EnemyParty = new List<Character>(enemyParty);
            this.turnProvider = turnProvider;
            this.turnProvider.CurrentBattle = this;
            TurnOrder = new List<MoveIntent>();
            _ended = false;
        }

        public void start()
        {
            while (!Ended)
            {
                this.turnProvider.doNextAction();
            }
        }

        bool Ended
        {
            get 
            {
                return _ended;
            }
        }

        public void Escape()
        {
            _ended = true;
        }
    }
}