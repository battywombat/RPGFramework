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

        public TargetType tt { get { return _tt; }}

        public abstract void doEffect(Character caster, Battle b, Character target);

    }

    public class DamageEffect : MoveEffect
    {
        SingleTargetSuccessFormula _hitFormula;

        SingleTargetFormula _damageformula;

        public DamageEffect(TargetType tt, SingleTargetFormula damageFormula, SingleTargetSuccessFormula hitFormula)
        {
            _tt = tt;
            _hitFormula = hitFormula;
            _damageformula = damageFormula;
        }

        public override void doEffect(Character caster, Battle b, Character target)
        {
            if (_hitFormula(caster, target))
            {
                // do stuff here       
            }
        }
    }
}