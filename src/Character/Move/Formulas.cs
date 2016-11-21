using System;

namespace RPGFramework
{
    public class Formulas
    {
        public delegate int SingleTargetFormula(Character caster, Character target);
        public delegate int NoTargetFormula(Character caster);
        public delegate int MultipleTargetFormula(Character caster, Character[] targets);
        public delegate bool NoTargetSuccessFormula(Character caster);
        public delegate bool SingleTargetSuccessFormula(Character caster, Character target);
        public delegate bool MultipleTargetSuccessFormula(Character caster, Character[] targets);

        public static bool NeverHitFormula(Character c1, Character c2) { return false; }

        public static bool PhysHitFormula(Character c1, Character c2)
        {
            Random r = new Random();
            return c1.Speed*r.Next(1,20) > c2.Speed*r.Next(1, 20);
        }
        public static int ZeroDamageFormula(Character c1, Character c2) { return 0; }

        public static int AttackDamageFormula(Character c1, Character c2)
        {
            Random r = new Random();
            return c1.Strength*r.Next(1,3)-c1.Vitality;
        }

        public static bool EscapeFormula(Character c)
        {
            return c.Speed > new Random().Next(0, 13);
        }

        public static SingleTargetSuccessFormula[] SuccessFormulas = new SingleTargetSuccessFormula[]
        {
            NeverHitFormula,
            PhysHitFormula
        };

        public static SingleTargetFormula[] SingleTargetFormulas = new SingleTargetFormula[]
        {
            ZeroDamageFormula,
            AttackDamageFormula
        };

        public static NoTargetSuccessFormula[] NoTargetSuccessFormulas = new NoTargetSuccessFormula[]
        {
            EscapeFormula
        };

    }
}