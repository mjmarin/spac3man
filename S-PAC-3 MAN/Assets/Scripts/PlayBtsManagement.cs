using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBtsManagement : MonoBehaviour {

	[SerializeField] private GameObject gameOverText;
	[SerializeField] private GameObject pauseText;
	[SerializeField] private GameObject pauseBt;
	[SerializeField] private GameObject restartBt;
	[SerializeField] private GameObject menuBt;
	[SerializeField] private GameObject quitBt;
	[SerializeField] private GameObject player;
	private PlayerController playerScript;
	private PauseController pauseScript;
	void Start () {
		gameOverText.SetActive(false);
		pauseText.SetActive(false);
		ActiveBts(false);
		playerScript = player.GetComponent<PlayerController>();
		pauseScript = this.gameObject.GetComponent<PauseController>();
	}
	
	void Update(){
		if(playerScript.GetDeath()){
			pauseBt.SetActive(false);
			gameOverText.SetActive(true);
			ActiveBts(true);
		}else if(pauseScript.GetPaused()){
			pauseText.SetActive(true);
			ActiveBts(true);
		}else{
			pauseText.SetActive(false);
			ActiveBts(false);
		}
	}

	void ActiveBts(bool boolean){
		restartBt.SetActive(boolean);
		menuBt.SetActive(boolean);
		quitBt.SetActive(boolean);
	}
}
