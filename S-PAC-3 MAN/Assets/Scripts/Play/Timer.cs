using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	private PlayUIManagement UIScript;
	private PlayerController playerScript;
	private bool paused;
	private float time;
	[SerializeField] private float NEOtime;
	
	void Awake(){
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		UIScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PlayUIManagement>();
	}
	void Start(){
		time = 0.0f;
		paused = false;
		Time.timeScale = 1f;
	}

	void Update(){
		UpdateTime();
	}
	private void UpdateTime(){
		if(playerScript.GetDeath() == false){
			time += Time.deltaTime;
			if(DataManager.GetSelectionMS() == 2){
				if(NEOtime <= 0){
					NEOtime = 0;
				}else{
					NEOtime -= Time.deltaTime;
				}
			}
		}
	}
	public void ChangeState(){
		if(paused){
			paused = false;
			Time.timeScale = 1f;
		}else{
			paused = true;
			Time.timeScale = 0f;
		}
	}

	public void EnlargeNeoTime(float seconds){
		NEOtime += seconds;
		UIScript.EnlargeNeoTimeText(seconds);
	}
	public bool GetPaused(){
		return paused;
	}
 
	public float GetTime(){
		return time;
	}
	public float GetNeoTime(){
		return NEOtime;
	}
}
