using System.IO;
using System.Xml;

namespace RPGFramework
{
    class MoveBuilder
    {
        public string Root { get; set; }

        public MoveBuilder(string root="")
        {
            Root = root;
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
            return new Move(name, buildEffect(root["effect"]));
        }

        private MoveEffect buildEffect(XmlNode root)
        {
            TargetType tt = (TargetType)System.Enum.Parse(typeof(TargetType), root["tt"].InnerText);
            switch(root["type"].InnerText)
            {
                case "DAMAGE":
                int dmgformula = int.Parse(root["formula"].InnerText);
                MoveEffect.SingleTargetFormula fmla = Formulas.SingleTargetFormulas[dmgformula];
                int successformula = int.Parse(root["success"].InnerText);
                MoveEffect.SingleTargetSuccessFormula successfmla = Formulas.SuccessFormulas[successformula];
                DamageEffect effect = new DamageEffect(tt, fmla, successfmla);
                return effect;
                default:
                return null;
            }
        }

    }
}