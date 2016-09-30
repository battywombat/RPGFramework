
namespace RPGFramework
{
    public abstract class TurnProvider 
    {

        public virtual Battle CurrentBattle
        {
            get
            {
                return currentBattle;
            }
            set
            {
                currentBattle = value;
            }
        }

        public abstract void doNextAction();

        protected Battle currentBattle;

    }
}
    