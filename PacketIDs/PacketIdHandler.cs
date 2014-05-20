using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

internal class PacketIdHandler
{
    internal static byte GetID(string field)
    {
        XElement root = XElement.Load("data/packets.xml");

        foreach (XElement elem in root.Elements("Packet"))
        {
            string name = elem.Attribute("id").Value;
            if(name == field)
                return byte.Parse(elem.Attribute("type").Value);
        }

        return 255;
    }
}

