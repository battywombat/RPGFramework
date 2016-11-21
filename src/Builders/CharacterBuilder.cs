using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace RPGFramework
{
    class CharacterBuilder
    {
        public string diroot;

        MoveBuilder mbuilder;

        public MoveBuilder MoveBuilder
        {
            get
            {
                if(mbuilder == null)
                    mbuilder = new MoveBuilder("");
                return mbuilder;
            }
            set
            {
                mbuilder = value;
            }
        }

        public CharacterBuilder(string diroot="")
        {
            this.diroot = diroot;
        }

        public Character buildFromXml(string filepath, MoveProvider provider=null)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(File.OpenRead(diroot == null ? filepath : (diroot+filepath)));
            return buildFromXmlNode(doc.DocumentElement, provider);
        }

        public Character buildFromXmlNode(XmlNode root, MoveProvider provider=null)
        {
            provider = provider == null ? new NullMoveProvider() : provider;
            string name = root["name"].InnerText;
            MoveSet moveroot = XmlbuildMoveSet(root["moves"], name);
            Dictionary<string, int> stats = new Dictionary<string, int>();
            stats["speed"] = int.Parse(root["speed"].InnerText);
            stats["strength"] = int.Parse(root["strength"].InnerText);
            stats["vitality"] = int.Parse(root["vitality"].InnerText);
            stats["hp"] = int.Parse(root["hp"].InnerText);
            return new Character(provider, moveroot, name, stats);
        }

        MoveSet XmlbuildMoveSet(XmlNode root, string rootname="")
        {
            var rootset = new MoveSet(rootname);
            foreach(XmlNode node in root.ChildNodes)
            {
                if (node.Name == "moves" || node.Name == "movset")
                {
                    var text = node.Attributes["name"].InnerText;
                    var newroot = XmlbuildMoveSet(node, text);
                    rootset.Add(newroot);
                }
                else
                {
                    var newmove = MoveBuilder.buildFromXml(node.InnerText);
                    rootset.Add(newmove);
                }
            }
            return rootset;
        }
    }
}