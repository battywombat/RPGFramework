using System.Collections.Generic;

namespace RPGFramework
{
    public class Battle
    {
        public List<Character> playerParty;
        public List<Character> enemyParty;
        public List<MoveIntent> turnOrder;
        TurnProvider turnProvider;
        public Battle(Character[] playerParty, Character[] enemyParty, TurnProvider turnProvider)
        {
            this.playerParty = new List<Character>(playerParty);
            this.enemyParty = new List<Character>(enemyParty);
            this.turnProvider = turnProvider;
            this.turnProvider.CurrentBattle = this;
            turnOrder = new List<MoveIntent>();
        }

        public void start()
        {
            while (!isEnded())
            {
                this.turnProvider.doNextAction();
            }
        }
        bool isEnded()
        {
            return false;
            // throw new NotImplementedException();
        }
    }
}