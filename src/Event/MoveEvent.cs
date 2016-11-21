namespace RPGFramework
{
    public class MoveEvent : GameEvent
    {
        string _onhit;
        string _onmiss;
        public string OnHit
        {
            get { return _onhit; }
        }
        public string OnMiss
        {
            get { return _onmiss; }
        }
        public MoveEvent(string message, string onhit, string onmiss) : base(message)
        {
            _onhit = onhit;
            _onmiss = onmiss;
        }
    }
}