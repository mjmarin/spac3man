using System.Xml;
using System.Xml.Serialization;

public class Quest{

    /* Variable identificadora de misión */
    [XmlAttribute("ID")]
    public int id;

    /* Variable descriptiva de misión */
    [XmlAttribute("Description")]
    public string description;

    /* Variable cantidad requerida de misión */
    [XmlAttribute("RequiredAmount")]
    public float requiredAmount;

    /* Variable recompensa de misión */
    [XmlAttribute("Reward")]
    public float reward;

    /* Variable tipo de misión */
    [XmlAttribute("Type")]
    public int type;
}