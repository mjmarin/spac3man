using System.Xml;
using System.Xml.Serialization;

public class Quest{
    [XmlAttribute("ID")]
    public int id;
    [XmlAttribute("Description")]
    public string description;
    [XmlAttribute("RequiredAmount")]
    public float requiredAmount;
    [XmlAttribute("Reward")]
    public float reward;
    [XmlAttribute("Type")]
    public int type;
}