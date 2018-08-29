using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("QuestCollection")]
public class QuestContainer{

	/* Array de todas las misiones disponibles */
	[XmlArray("Quests")]
	[XmlArrayItem("Quest")]
	public Quest[] quests = new Quest[15];

	/* Función interfaz de carga de fichero de misiones */
	public static QuestContainer Load(string path){
		TextAsset _xml = Resources.Load<TextAsset>(path);

		XmlSerializer serializer = new XmlSerializer(typeof(QuestContainer));
		
		StringReader reader = new StringReader(_xml.text);

		QuestContainer quests = serializer.Deserialize(reader) as QuestContainer;

		reader.Close();

		return quests;
	}
}
