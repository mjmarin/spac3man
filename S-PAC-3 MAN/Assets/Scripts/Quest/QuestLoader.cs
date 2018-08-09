using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoader : MonoBehaviour {

	private const string path = "Quests/QuestFile";
	private static Quest[] quests = new Quest[3];

	void Start () {
		DateTime dt = DateTime.Now;

		if(Helper.DecryptInt(PlayerPrefs.GetString("LastDay")) != dt.Day || Helper.DecryptInt(PlayerPrefs.GetString("LastMonth")) != dt.Month){
			ActualizeDate(dt.Day, dt.Month);
			ActualizeQuests();
		}
		SetActiveQuest();
	}

	private void ActualizeQuests(){
		int[] missions = new int[3];
		missions = Helper.RandomIntValues(3,0,14);

		PlayerPrefs.SetString("Mission1", Helper.EncryptInt(missions[0]));
		PlayerPrefs.SetString("Mission2", Helper.EncryptInt(missions[1]));
		PlayerPrefs.SetString("Mission3", Helper.EncryptInt(missions[2]));

		PlayerPrefs.SetString("Mission1Completed", Helper.EncryptInt(0));
		PlayerPrefs.SetString("Mission2Completed", Helper.EncryptInt(0));
		PlayerPrefs.SetString("Mission3Completed", Helper.EncryptInt(0));
	}

	private void ActualizeDate(int day, int month){
		PlayerPrefs.SetString("LastDay", Helper.EncryptInt(day));
		PlayerPrefs.SetString("LastMonth", Helper.EncryptInt(month));
	}

	private void SetActiveQuest(){
		QuestContainer qc = QuestContainer.Load(path);

		quests[0] = qc.quests[Helper.DecryptInt(PlayerPrefs.GetString("Mission1"))];
		quests[1] = qc.quests[Helper.DecryptInt(PlayerPrefs.GetString("Mission2"))];
		quests[2] = qc.quests[Helper.DecryptInt(PlayerPrefs.GetString("Mission3"))];
	}

	public static int CheckQuests(int secondsAlive, int pickedMoney, int pickedShields, int secondsShielded){
		int i = 1;
		int reward = 0;
		int parameter = 0;

		foreach (Quest quest in quests){ /* Para cada misión */
			if(Helper.DecryptInt(PlayerPrefs.GetString("Mission"+i+"Completed")) == 0){	/* Si no está completa */

				switch(quest.type){
					case 1:
						parameter = secondsAlive;
					break;

					case 2:
						parameter = pickedMoney;
					break;

					case 3:
						parameter = pickedShields;
					break;

					case 4:
						parameter = secondsShielded;
					break;

					default:
						parameter = 0;
					break;
				}

				if(parameter >= quest.requiredAmount){
					reward += quest.reward;
					PlayerPrefs.SetString("Mission"+i+"Completed", Helper.EncryptInt(1));
				}
			}
			i++;
		}

		return reward;
	}

	public static Quest[] GetActiveQuests(){
		return quests;
	}  
	
}
