using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUIManagement : MonoBehaviour {
	[SerializeField] private GameObject gameOverText;
	[SerializeField] private GameObject pauseText;
	[SerializeField] private GameObject pauseBt;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject player;
	[SerializeField] private Text counterText;
	[SerializeField] private Text timerText;
	private PlayerController playerScript;
	private PauseController pauseScript;
	private float time;
	private int timerMinutes;
	private int timerSeconds;
	void Start () {
		gameOverText.SetActive(false);
		pauseText.SetActive(false);
		pauseMenu.SetActive(false);

		playerScript = player.GetComponent<PlayerController>();
		pauseScript = this.gameObject.GetComponent<PauseController>();

		time = 0;
	}
	
	void Update(){

		SetTimer();

		counterText.text = playerScript.GetPickUps().ToString();
		
		if(playerScript.GetDeath()){
			pauseBt.SetActive(false);
			gameOverText.SetActive(true);
			pauseMenu.SetActive(true);
		}else if(pauseScript.GetPaused()){
			pauseText.SetActive(true);
			pauseMenu.SetActive(true);
		}else{
			pauseText.SetActive(false);
			pauseMenu.SetActive(false);
		}
	}

	void SetTimer(){
		if(playerScript.GetDeath() == false){
			time += Time.deltaTime;
			timerSeconds = (int)Mathf.Floor(time);
			timerMinutes = timerSeconds / 60;

			timerText.text = string.Format("{0:D2}:{1:D2}", timerMinutes, timerSeconds % 60);
		}
	}
}
