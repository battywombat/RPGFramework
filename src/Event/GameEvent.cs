namespace RPGFramework
{
    public class GameEvent
    {
        string _message;

        public string Message
        {
            get { return _message; }
        }

        public GameEvent(string message)
        {
            _message = message;
        }
    }
}