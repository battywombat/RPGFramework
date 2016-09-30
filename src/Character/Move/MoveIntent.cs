using System;

namespace RPGFramework
{
    public interface MoveIntent
    {
        void doMove();
        int MovePriority { get; }
    }

    public class MoveResponse
    {
        public string[] Message;

        public MoveResponse(string[] message)
        {
            Message = message;
        }
    }

    public class NullMoveIntent : MoveIntent
    {
        public int MovePriority
        {
            get { return 0; }
        }

        public void doMove()
        {
            return;
        }
    }

    public class NoTargetIntent : MoveIntent
    {
        Character _caster;
        Move _theMove;
        public NoTargetIntent(Character caster, Move theMove)
        {
            _caster = caster;
            _theMove = theMove;

        }

        public int MovePriority
        {
            get { return _caster.Speed; }
        }

        public void doMove()
        {
            throw new NotImplementedException();
        }
    }

    public class SingleTargetIntent : MoveIntent
    {
        Character _caster;
        Move _theMove;
        Battle _context;
        Character _target;
        public int MovePriority
        {
            get { return _caster.Speed; }
        }

        public SingleTargetIntent(Battle context, Character caster, Move theMove, Character target)
        {
            _context = context;
            _caster = caster;
            _theMove = theMove;
            _target = target;
        }

        public void doMove()
        {
            _theMove.Execute(_caster, _context, _target);
        }
    }


}