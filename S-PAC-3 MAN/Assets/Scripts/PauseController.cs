using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

	[SerializeField] private Sprite play;
	[SerializeField] private Sprite pause;
	[SerializeField] private GameObject pauseBt;
	private bool paused;
	
	void Start(){
		paused = false;
		Time.timeScale = 1f;
	}
	public void ChangeState(){
		if(paused){
			paused = false;
			pauseBt.GetComponent<Image>().sprite = pause;
			Time.timeScale = 1f;
		}else{
			paused = true;
			pauseBt.GetComponent<Image>().sprite = play;
			Time.timeScale = 0f;
		}
	}
	public bool GetPaused(){
		return paused;
	}
}
