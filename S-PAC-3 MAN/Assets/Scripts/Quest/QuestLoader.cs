﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoader : MonoBehaviour {

	/* Variable path de fichero */
	private const string path = "Quests/QuestFile";

	/* Variable con las misiones activas */
	private static Quest[] activeQuests = new Quest[3];

	/* Función de actualización e inicialización de misiones */
	void Start () {
		DateTime dt = DateTime.Now;

		if(DataManager.GetLastDay() != dt.Day || DataManager.GetLastMonth() != dt.Month){
			UpdateDate(dt.Day, dt.Month);
			UpdateQuests();
		}
		SetActiveQuest();
	}

	/* Actualización de misiones */
	private void UpdateQuests(){
		int[] missions = new int[3];
		missions = Helper.RandomIntValues(3,0,14);

		DataManager.SetActiveMissionsIndex(0, missions[0]);
		DataManager.SetActiveMissionsIndex(1, missions[1]);
		DataManager.SetActiveMissionsIndex(2, missions[2]);

		DataManager.SetMissionsCompleted(0, false);
		DataManager.SetMissionsCompleted(1, false);
		DataManager.SetMissionsCompleted(2, false);

	}

	/* Actualización de última fecha de acceso */
	private void UpdateDate(int day, int month){
		DataManager.SetLastDay(day);
		DataManager.SetLastMonth(month);
	}

	/* Inicialización de misiones activas */
	private void SetActiveQuest(){
		QuestContainer qc = QuestContainer.Load(path);

		activeQuests[0] = qc.quests[DataManager.GetActiveMissionsIndex(0)];
		activeQuests[1] = qc.quests[DataManager.GetActiveMissionsIndex(1)];
		activeQuests[2] = qc.quests[DataManager.GetActiveMissionsIndex(2)];

	}

	/* Función interfaz de comprobación de compleción de misiones */
	public static float CheckQuests(float secondsAlive, float pickedMoney, float pickedShields, float secondsShielded){
		int i = 0;
		float reward = 0;
		float parameter = 0;

		/* Para cada misión */
		foreach (Quest quest in activeQuests){ 
			/* Si no está completa */
			if(DataManager.GetMissionCompleted(i) == false){ 

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

				if(Mathf.RoundToInt(parameter) >= Mathf.RoundToInt(quest.requiredAmount)){
					reward += quest.reward;
					DataManager.SetMissionsCompleted(i,true);
				}
			}
			i++;
		}
		return reward;
	}

	/* Función interfaz de consulta de misiones activas */
	public static Quest[] GetActiveQuests(){
		return activeQuests;
	}  
	
}
