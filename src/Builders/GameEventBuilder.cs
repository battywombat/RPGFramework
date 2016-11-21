using System.Xml;

namespace RPGFramework
{
    public class GameEventBuilder
    {
        string _root;

        public GameEventBuilder(string root="")
        {
            _root = root;
        }

        public MoveEvent BuildMoveEventFromXml(XmlNode node)
        {
            string message = node["message"].InnerText;
            string onhit = node["onhit"].InnerText;
            string onmiss = node["onmiss"].InnerText;
            return new MoveEvent(message, onhit, onmiss);
        }
    }
}