
namespace RPGFramework
{
    class Stat
    {
        protected int baseValue;

        public int Value
        {
            get
            {
                return baseValue;
            }
        }

        public Stat(int baseValue)
        {
            this.baseValue = baseValue;
        }
    }
}