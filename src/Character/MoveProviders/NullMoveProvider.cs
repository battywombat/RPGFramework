namespace RPGFramework
{
    public class NullMoveProvider : MoveProvider
    {
        public MoveIntent GetMoveFor(Character c, Battle b)
        {
            return new NullMoveIntent();
        }
    }    
}