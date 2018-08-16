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
	[SerializeField] private GameObject FPSCounter;
	[SerializeField] private Text counterText;
	[SerializeField] private Text timerText;
	[SerializeField] private GameObject score;
	[SerializeField] private Text scoreMoneyText;
	[SerializeField] private Text scoreTimerText;
	[SerializeField] private Text scoreValueText;
	[SerializeField] private Text addTimeText;
	[SerializeField] private GameObject newRecordText;
	[SerializeField] private GameObject questRewardText;

	
	private PlayerController playerScript;
	private PauseController pauseScript;
	
	private float time;
	private int timerMinutes;
	private int timerSeconds;

	[SerializeField] private float NEOtime;
	private int NEOtimerMinutes;
	private int NEOtimerSeconds;

	private float addTimeAlpha;
	[SerializeField] private float alphaProgression;


	private void Awake(){
		playerScript = player.GetComponent<PlayerController>();
		pauseScript = this.gameObject.GetComponent<PauseController>();
	}
	private void Start () {
		if(DataManager.GetSelectionMS() == 1){
			addTimeText.gameObject.SetActive(false);
		}else{
			addTimeText.gameObject.SetActive(true);
			addTimeText.enabled = false;
		}
		gameOverText.SetActive(false);
		pauseText.SetActive(false);
		pauseMenu.SetActive(false);
		score.SetActive(false);
		newRecordText.SetActive(false);
		questRewardText.SetActive(false);

		if(DataManager.GetFPSOn()){
			FPSCounter.SetActive(true);
		}else{
			FPSCounter.SetActive(false);
		}
		time = 0.0f;
	}
	
	private void Update(){

		SetTimer();

		counterText.text = playerScript.GetPickUps().ToString("F0");
		
		if(playerScript.GetDeath()){
			pauseBt.SetActive(false);
			gameOverText.SetActive(true);
			pauseMenu.SetActive(true);
			timerText.gameObject.SetActive(false);
			counterText.transform.parent.gameObject.SetActive(false);
			SetScore();
			score.SetActive(true);
		}else if(pauseScript.GetPaused()){
			pauseText.SetActive(true);
			pauseMenu.SetActive(true);
		}else{
			pauseText.SetActive(false);
			pauseMenu.SetActive(false);
		}

		if(addTimeText.isActiveAndEnabled){
			addTimeText.color = addTimeText.color = new Vector4(1.0f, 0.0f, 0.0f, addTimeAlpha);
			if(addTimeAlpha > 0){
				addTimeAlpha -= alphaProgression * Time.deltaTime;
			}
		}
	}

	private void SetTimer(){
		if(playerScript.GetDeath() == false){
			time += Time.deltaTime;
			timerSeconds = Mathf.FloorToInt(time);
			timerMinutes = timerSeconds / 60;

			if(DataManager.GetSelectionMS() == 2){
				if(NEOtime <= 0){
					NEOtime = 0;
				}else{
					NEOtime -= Time.deltaTime;
				}
				NEOtimerSeconds = (int)Mathf.Floor(NEOtime);
				NEOtimerMinutes = NEOtimerSeconds / 60;

				timerText.text = string.Format("{0:D2}:{1:D2}", NEOtimerMinutes, NEOtimerSeconds % 60);
			}else{
				timerText.text = string.Format("{0:D2}:{1:D2}", timerMinutes, timerSeconds % 60);
			}
		}
	}

	private void SetScore(){
		scoreMoneyText.text = counterText.text;
		scoreTimerText.text = string.Format("{0:D2}:{1:D2}", timerMinutes, timerSeconds % 60);
		scoreValueText.text = Mathf.FloorToInt(playerScript.GetActualScore()).ToString();
	}

	public void UpNewRecordText(){
		newRecordText.SetActive(true);
	}

	public void UpQuestRewardText(float reward){
		questRewardText.GetComponent<Text>().text = "Quest reward:" + reward.ToString("F0");
		questRewardText.SetActive(true);
	}

	public float GetTime(){
		return time;
	}
	public float GetNeoTime(){
		return NEOtime;
	}

	public void EnlargeNeoTime(float seconds){
		addTimeText.enabled = true;
		addTimeText.text = "+ " + Mathf.Round(seconds).ToString("") + "s";
		addTimeAlpha = 1.0f;

		NEOtime += seconds;
	}
}
