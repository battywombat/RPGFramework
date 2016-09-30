namespace RPGFramework
{
    class DepleteableStat : Stat 
    {
        int remainingValue;
        public int RemainingValue
        {
            get
            {
                return remainingValue;
            }
            set
            {
                if (value >= baseValue)
                    remainingValue = baseValue;
                else if (value <= 0)
                    remainingValue = 0;
                else
                    remainingValue = value;
            }
        }

        public DepleteableStat(int baseValue, int remainingValue=-1) : base(baseValue)
        {
            this.remainingValue = (remainingValue < 0 ? baseValue : remainingValue);
        }
    }
}