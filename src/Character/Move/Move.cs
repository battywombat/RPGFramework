namespace RPGFramework
{
    public class Move : MoveTreeNode
    {
        MoveEffect _effect;
        string _name;
        public string Name
        {
            get { return _name; }
        }

        public TargetType tt { get { return _effect.tt; }}

        public Move(string name, MoveEffect effect)
        {
            _name = name;
            _effect = effect;
        }

        public bool HasChildren()
        {
            return false;
        }

        public MoveTreeNode[] Children
        {
            get { return null; }
        }

        public void Add(MoveTreeNode n)
        {
            throw new System.NotSupportedException();
        }

        public void Execute(Character c, Battle b)
        {
            if (tt != TargetType.NO_TARGET)
                throw new IncorrectTargetTypeException();
            _effect.doEffect(c, b, null);
        }
        public void Execute(Character c, Battle b, Character target)
        {
            if (tt != TargetType.SINGLE_ALLY && tt != TargetType.SINGLE_OPPONENT)
                throw new IncorrectTargetTypeException();
            _effect.doEffect(c, b, target);
        }
        public void Execute(Character c, Battle b, Character[] target)
        {
            //TODO implement this
            throw new IncorrectTargetTypeException();
        }
    }

    class IncorrectTargetTypeException : System.Exception {}
}