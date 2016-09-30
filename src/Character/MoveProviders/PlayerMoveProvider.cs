namespace RPGFramework
{
    public class PlayerMoveProvider : MoveProvider
    {
        public MoveIntent GetMoveFor(Character c, Battle b)
        {
            Move selectedMove = null;
            MoveIntent ret = null;
            while (ret == null)
            {
                selectedMove = Frontend.Instance.selectMove(c);
                if (selectedMove != null)
                    ret = generateIntent(c, selectedMove, b);
            }
            return ret;
        }

        public Character selectSingleAlly(Character c, Move m, Battle b)
        {
           Character[] selection =  b.enemyParty.Contains(c) ? b.enemyParty.ToArray() : b.playerParty.ToArray();
            return Frontend.Instance.selectCharacter(selection);
        }

        public Character selectSingleOpponent(Character c, Move m, Battle b)
        {
            Character[] selection = b.enemyParty.Contains(c) ? b.playerParty.ToArray() : b.enemyParty.ToArray();
            return Frontend.Instance.selectCharacter(selection);
        }

        private MoveIntent generateIntent(Character c, Move m, Battle b) 
        {
            Character target;
            switch(m.tt)
            {
                case TargetType.NO_TARGET:
                return new NoTargetIntent(c, m);
                case TargetType.SINGLE_ALLY:
                target = selectSingleAlly(c, m, b);
                return (target == null ? null : new SingleTargetIntent(b, c, m, target));
                case TargetType.SINGLE_OPPONENT:
                target = selectSingleOpponent(c, m, b);
                return (target == null ? null : new SingleTargetIntent(b, c, m, target));
                default:
                return new NullMoveIntent();
            }
        }
    }
}