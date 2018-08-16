using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoader : MonoBehaviour {

	private const string path = "Quests/QuestFile";
	private static Quest[] activeQuests = new Quest[3];

	void Start () {
		DateTime dt = DateTime.Now;

		if(DataManager.GetLastDay() != dt.Day || DataManager.GetLastMonth() != dt.Month){
			ActualizeDate(dt.Day, dt.Month);
			ActualizeQuests();
		}
		SetActiveQuest();
	}

	private void ActualizeQuests(){
		int[] missions = new int[3];
		missions = Helper.RandomIntValues(3,0,14);

		DataManager.SetActiveMissionsIndex(0, missions[0]);
		DataManager.SetActiveMissionsIndex(1, missions[1]);
		DataManager.SetActiveMissionsIndex(2, missions[2]);

		DataManager.SetMissionsCompleted(0, false);
		DataManager.SetMissionsCompleted(1, false);
		DataManager.SetMissionsCompleted(2, false);

	}

	private void ActualizeDate(int day, int month){
		DataManager.SetLastDay(day);
		DataManager.SetLastMonth(month);
	}

	private void SetActiveQuest(){
		QuestContainer qc = QuestContainer.Load(path);

		activeQuests[0] = qc.quests[DataManager.GetActiveMissionsIndex(0)];
		activeQuests[1] = qc.quests[DataManager.GetActiveMissionsIndex(1)];
		activeQuests[2] = qc.quests[DataManager.GetActiveMissionsIndex(2)];

	}

	public static float CheckQuests(float secondsAlive, float pickedMoney, float pickedShields, float secondsShielded){
		int i = 0;
		float reward = 0;
		float parameter = 0;

		foreach (Quest quest in activeQuests){ /* Para cada misión */
			if(DataManager.GetMissionCompleted(i) == false){ /* Si no está completa */

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
					DataManager.SetMissionsCompleted(i,true);
				}
			}
			i++;
		}
		return reward;
	}

	public static Quest[] GetActiveQuests(){
		return activeQuests;
	}  
	
}
