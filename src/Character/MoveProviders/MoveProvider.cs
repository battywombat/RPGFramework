namespace RPGFramework
{
    public interface MoveProvider 
    {
        MoveIntent GetMoveFor(Character c, Battle b);
    }
}