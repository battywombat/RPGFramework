using System.Collections.Generic;

namespace RPGFramework
{
    public class Character
    {
        
        string _name;
        MoveProvider _mprovider;
        public MoveSet _mset;
        DepleteableStat _hp;
        Stat _strength;
        Stat _vitality;
        Stat _speed;

        public int Strength
        {
            get
            {
                return _strength.Value;
            }
        }
        public int Vitality
        {
            get
            {
                return _vitality.Value;
            }
        }
        public int Speed
        {
            get
            {
                return _speed.Value;
            }
        }

        public int RemainingHP
        {
            get
            {
                return _hp.RemainingValue;
            }
            set
            {
                _hp.RemainingValue = value;
            }
        }

        public int HP
        {
            get
            {
                return _hp.Value;
            }
        }
        public string Name
        {
            get
            {
                return _name != null ? _name : ToString();
            }
        }

        public MoveProvider MProvider
        {
            get { return _mprovider; }
        }

        public Character(MoveProvider mprovider, MoveSet mset, string name, Dictionary<string, int> stats)
        {
            _mprovider = mprovider;
            _mset = mset;
            _hp = new DepleteableStat(stats["hp"]);
            _strength = new Stat(stats["strength"]);
            _speed = new Stat(stats["speed"]);
            _vitality = new Stat(stats["vitality"]);
            _name = name;
        }

        public MoveIntent getMove(Battle b)
        {
            return _mprovider.GetMoveFor(this, b);
        }
    }
}