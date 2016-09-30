using System.Collections.Generic;

namespace RPGFramework
{
    public interface MoveTreeNode
    {
        bool HasChildren();

        MoveTreeNode[] Children { get; }

        string Name { get; }

        void Add(MoveTreeNode child);
    }
    public class MoveSet : MoveTreeNode
    {
        string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        List<MoveTreeNode> _children;


        public MoveTreeNode[] Children
        {
            get
            {
                return _children.ToArray();
            }
        }

        public MoveSet(MoveTreeNode[] moves) : this("", moves) {}

        public MoveSet(string name="", MoveTreeNode[] moves=null)
        {
            _children = new List<MoveTreeNode>();
            if (moves != null)
                foreach(var move in moves)
                    Add(move);
            _name = name;
        }

        public bool HasChildren()
        {
            return _children.Count > 0;
        }

        public void Add(MoveTreeNode child)
        {
            _children.Add(child);
        }
    }
}