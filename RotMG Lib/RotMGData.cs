using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RotMG_Lib
{
    public sealed class RotMGData
    {
        public static Dictionary<short, string> Items { get; set; }
        public static Dictionary<string, short> NameToId { get; set; }
        public static Dictionary<short, bool> Soulbound { get; set; }

        public RotMGData()
        {
            if (Items == null)
                Items = new Dictionary<short, string>();
            if (Soulbound == null)
                Soulbound = new Dictionary<short, bool>();
            if (NameToId == null)
                NameToId = new Dictionary<string, short>();

            XElement root = XElement.Load("data/items.xml");

            foreach (XElement elem in root.Elements("Object"))
            {
                string name = elem.Attribute("id").Value;
                string tier = String.Empty;
                if(elem.Element("Tier") != null)
                    tier = "T" + elem.Element("Tier").Value;
                short itemId = (short)Utils.FromString(elem.Attribute("type").Value);
                if (!Soulbound.ContainsKey(itemId))
                    Soulbound.Add(itemId, elem.Element("Soulbound") != null);
                if(!Items.ContainsKey(itemId))
                    Items.Add(itemId, tier == String.Empty ? name : tier + " " + name);
                if (!NameToId.ContainsKey(name))
                    NameToId.Add(name, itemId);
            }
        }
    }
}
