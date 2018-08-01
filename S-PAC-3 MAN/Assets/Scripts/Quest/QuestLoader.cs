using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoader : MonoBehaviour {

	private const string path = "Quests/QuestFile";
	private static Quest[] quests = new Quest[3];

	void Start () {
		DateTime dt = DateTime.Now;

		if(PlayerPrefs.GetInt("LastDay", 0) != dt.Day || PlayerPrefs.GetInt("LastMonth", 0) != dt.Month){
			ActualizeDate(dt.Day, dt.Month);
			ActualizeQuests();
		}
		SetActiveQuest();
	}

	private void ActualizeQuests(){
		int[] missions = new int[3];
		missions = Helper.RandomIntValues(3,0,14);

		PlayerPrefs.SetInt("Mission1", missions[0]);
		PlayerPrefs.SetInt("Mission2", missions[1]);
		PlayerPrefs.SetInt("Mission3", missions[2]);

		PlayerPrefs.SetInt("Mission1Completed", 0);
		PlayerPrefs.SetInt("Mission2Completed", 0);
		PlayerPrefs.SetInt("Mission3Completed", 0);
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

	public static Quest[] GetActiveQuests(){
		return quests;
	}  
	
}
