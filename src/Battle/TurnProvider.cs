
namespace RPGFramework
{
    public interface TurnProvider
    {
        Battle CurrentBattle { set; }

        void doNextAction();
    }
}
    