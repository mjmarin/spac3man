using System.Xml;
using System.Xml.Serialization;

public class Quest{
    [XmlAttribute("ID")]
    public int id;
    [XmlAttribute("Description")]
    public string description;
    [XmlAttribute("RequiredAmount")]
    public int requiredAmount;
    [XmlAttribute("Reward")]
    public int reward;
    [XmlAttribute("Type")]
    public int type;
}