using System.IO;
using System.Xml;
using static RPGFramework.Formulas;

namespace RPGFramework
{
    class MoveBuilder
    {
        public string Root { get; set; }

        GameEventBuilder eventBuilder;

        public MoveBuilder(string root="")
        {
            Root = root;
            eventBuilder = new GameEventBuilder();
        }
        public Move buildFromXml(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(File.OpenRead(Root+filepath));
            return buildFromXmlNode(doc.DocumentElement);
        }
        public Move buildFromXmlNode(XmlNode root)
        {
            string name = root["name"].InnerText;
            MoveEffect effect = buildEffect(root["effect"]);
            return new Move(name, effect);
        }

        private MoveEffect buildEffect(XmlNode root)
        {
            TargetType tt = (TargetType)System.Enum.Parse(typeof(TargetType), root["tt"].InnerText);
            MoveEvent ev = root["event"] == null ? null : eventBuilder.BuildMoveEventFromXml(root["event"]);
            int successformula, damageformula;
            switch(root["type"].InnerText)
            {
                case "DAMAGE":
                damageformula = int.Parse(root["formula"].InnerText);
                SingleTargetFormula fmla = Formulas.SingleTargetFormulas[damageformula];
                successformula = int.Parse(root["success"].InnerText);
                SingleTargetSuccessFormula successfmla = SuccessFormulas[successformula];
                return new DamageEffect(tt, fmla, successfmla, ev);
                case "ESCAPE":
                successformula = int.Parse(root["success"].InnerText);
                NoTargetSuccessFormula escapefmla = NoTargetSuccessFormulas[successformula];
                return new EscapeEffect(escapefmla, ev);
                default:
                return null;
            }
        }
    }
}