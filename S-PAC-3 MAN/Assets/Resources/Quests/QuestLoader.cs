using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoader : MonoBehaviour {

	public const string path = "QuestFile";
	public static Quest[] quests;

	void Start () {
		DateTime dt = DateTime.Now;

		if(PlayerPrefs.GetInt("LastDay", 0) != dt.Day || PlayerPrefs.GetInt("LastMonth", 0) != dt.Month){
			ActualizeDate(dt.Day, dt.Month);
			ActualizeQuests();
		}
		SetActiveQuest();
	}

	private int[] RandomIntValues(int count, int minRange, int maxRange){
		List<int> usedValues = new List<int>();	
		int[] values = new int[count];
		int index = 0;

		values[index] = UnityEngine.Random.Range(minRange, maxRange);
		usedValues.Add(values[index]);

		while(usedValues.Count < count){
			index++;
			values[index] = UnityEngine.Random.Range(minRange, maxRange);
			while(usedValues.Contains(values[index])){
				values[index] = UnityEngine.Random.Range(minRange, maxRange);
			}
			usedValues.Add(values[index]);
		}
		return usedValues.ToArray();
	}

	private void ActualizeQuests(){
		int[] missions = new int[3];
		missions = RandomIntValues(3,0,14);

		PlayerPrefs.SetInt("Mission1", missions[0]);
		PlayerPrefs.SetInt("Mission2", missions[1]);
		PlayerPrefs.SetInt("Mission3", missions[2]);
	}

	private void ActualizeDate(int day, int month){
		PlayerPrefs.SetInt("LastDay", day);
		PlayerPrefs.SetInt("LastMonth", month);
	}

	private void SetActiveQuest(){
		QuestContainer qc = QuestContainer.Load(path);

		quests[0] = qc.quests[PlayerPrefs.GetInt("Mission1")];
		quests[1] = qc.quests[PlayerPrefs.GetInt("Mission2")];
		quests[2] = qc.quests[PlayerPrefs.GetInt("Mission3")];
	}
	
	public Quest[] GetActiveQuest(){
		return quests;
	}  
	
}
