namespace RPGFramework
{
    public enum TargetType
    {
        NO_TARGET,
        SINGLE_OPPONENT,
        SINGLE_ALLY,
        ALL_OPPONENTS,
        ALL_ALLIES,
        ALL
    }
    public abstract class MoveEffect
    {
        public delegate int SingleTargetFormula(Character caster, Character target);
        public delegate int NoTargetFormula(Character caster);
        public delegate int MultipleTargetFormula(Character caster, Character[] targets);
        public delegate bool NoTargetSuccessFormula(Character caster);
        public delegate bool SingleTargetSuccessFormula(Character caster, Character target);
        public delegate bool MultipleTargetSuccessFormula(Character caster, Character[] targets);

        protected TargetType _tt;

        protected MoveEvent _response;

        public TargetType tt { get { return _tt; }}

        public abstract void doEffect(Character caster, Battle b, Character target);

    }

    public class DamageEffect : MoveEffect
    {
        SingleTargetSuccessFormula _hitFormula;

        SingleTargetFormula _damageformula;

        public DamageEffect(TargetType tt, SingleTargetFormula damageFormula, SingleTargetSuccessFormula hitFormula, MoveEvent response)
        {
            _response = response;
            _tt = tt;
            _hitFormula = hitFormula;
            _damageformula = damageFormula;
            _response = response;
        }

        public override void doEffect(Character caster, Battle b, Character target)
        {
            if (_hitFormula(caster, target))
            {
                int damage = _damageformula(caster, target);
                target.RemainingHP -= damage;
                Frontend.Instance.RequestMoveEventDisplay(_response, caster, target, true, damage);
            }
            else 
            {
                Frontend.Instance.RequestMoveEventDisplay(_response, caster, target, false , 0);
            }
        }
    }


    public class EscapeEffect : MoveEffect
    {
        NoTargetSuccessFormula _success;

        public EscapeEffect(NoTargetSuccessFormula success, MoveEvent response)
        {
            _success = success;
            _response = response;
        }
        public override void doEffect(Character caster, Battle b, Character target)
        {
            if (_success(caster))
            {
                Frontend.Instance.RequestMoveEventDisplay(_response, caster, null, true, 0);
                b.Escape();
            }
            else 
            {
                Frontend.Instance.RequestMoveEventDisplay(_response, caster, null, false, 0);
            }
        }
    }
}