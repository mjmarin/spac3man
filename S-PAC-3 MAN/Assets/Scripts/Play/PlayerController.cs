using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float pickUpsTime;
	[SerializeField] private float invulnerableTime;
	[SerializeField] private float turnOffShieldTime;

	private GameObject ShieldRespawn;
	
	private Timer timerScript;
	private PlayUIManagement PlayUIManagement;
	private bool shielded;
	private float timeInv;
	private bool death;
	private float screenWidth;
	private float pickUps;
	private int pickedShields;
	private float timeShield;
	private float offShieldTime;

	private float speedBuff;
	private bool NEOActivated;
	private int speedMode;

	void Awake(){
		GameObject canvas = GameObject.Find("Canvas");
		timerScript = Camera.main.GetComponent<Timer>();
		PlayUIManagement = canvas.GetComponent<PlayUIManagement>();
		ShieldRespawn = GameObject.Find("ShieldRespawn");
	}
	void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};

		shielded = false;
		death = false;
		screenWidth = Screen.width;
		pickUps = 0;
		pickedShields = 0;
		timeShield = 0;

		speedMode = DataManager.GetSelectionSS();
		speedBuff = multSpeed[speedMode - 1];

		if(DataManager.GetSelectionMS() == 1){
			NEOActivated = false;
		}else{
			NEOActivated =  true;
		}
		
	}

	/* Control de movimiento */
	void Update () {
		Movement();
		CheckShield();
		if(NEOActivated){
			CheckNEODeath();
		}
	}

	void Movement(){
		# if UNITY_EDITOR
			transform.Rotate(new Vector3(0,0,-Input.GetAxis("Horizontal") * rotationSpeed * speedBuff));		//Para probar en PC
		#else
			int direction = 0;
			if(GetDeath() == false && timerScript.GetPaused() == false ){		// Quiero que al morir siga hacia delante pero ya no se pueda controlar
				if (Input.touchCount > 0){
					if (Input.GetTouch (Input.touchCount - 1).position.x > screenWidth / 2) {					//Move Right
						direction = -1;
					}else{																	//Move Left
						direction = 1;
					}
				}
			}
			transform.Rotate(new Vector3(0,0,direction * rotationSpeed * speedBuff));
		#endif
		transform.position += transform.up * Time.deltaTime * movementSpeed * speedBuff;
	}

	void CheckNEODeath(){
		if(timerScript.GetNeoTime() <= 0 && GetDeath() == false){
			SetDeath(true);
		}
	}

	/* Colisiones con elementos. Muerte y recogida de objetos */
	void OnTriggerEnter2D(Collider2D other){
		if(GetDeath() == false){
			if(other.CompareTag("Enemy")) {		/* Al chocarte con un enemigo o mueres o se quita escudo y comienza cuenta atrás de invulnerabilidad */
				if(shielded){
					SetShield(false);
				}else{
					if(timeInv <= 0){
						SetDeath(true);
					}
				}	
			}else{
				if(other.CompareTag("ShieldPick")){
					SetShield(true);
				}else{
					pickUps++;
					SoundManager.SetSound("getCoin");
					if(NEOActivated){
						timerScript.EnlargeNeoTime(pickUpsTime);
					}
				}
			}
		}
	}
	
	/* Establece muerte del jugador */
	private void SetDeath(bool boolean){
		if(boolean){
			transform.GetChild(0).gameObject.SetActive(false);
			float money = System.Convert.ToSingle(DataManager.GetMoney());

			float reward = QuestLoader.CheckQuests(timerScript.GetTime(), pickUps, pickedShields, timeShield);
			DataManager.SetMoney(pickUps + money + reward);

			if(reward > 0){
				PlayUIManagement.UpQuestRewardText(reward);
			}

			int modeIndex = DataManager.GetSelectionSS() - 1 + (DataManager.GetSelectionMS() - 1) * 3;

			SetRecords(modeIndex);

			SoundManager.SetSound("death");
		}
		death = boolean;
	}

	private void SetShield(bool boolean){
		if(boolean){
			offShieldTime = turnOffShieldTime;
			timeInv = invulnerableTime;
			pickedShields++;
			SoundManager.SetSound("getShield");
			transform.GetChild(0).gameObject.SetActive(true);
		}else{
			SoundManager.SetSound("shieldDown");
		}
		shielded = boolean;
		
	}

	private void CheckShield(){
		if(shielded){
			timeShield += Time.deltaTime;
		}else{
			if(timeInv > 0){
				VisualLostedShield(true);
				timeInv = timeInv - Time.deltaTime;
				if(timeInv <= 0){
					timeInv = 0;
					VisualLostedShield(false);
					ShieldRespawn.GetComponent<SpawnItemManager>().IncreaseObjectCount(false);
				}
			}
		}

	}
	private void VisualLostedShield(bool stillActive){
		if(stillActive){
			offShieldTime -= Time.deltaTime;
			if(offShieldTime < 0){
				offShieldTime = turnOffShieldTime;
				transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
			}
		}else{
			transform.GetChild(0).gameObject.SetActive(false);
		}
	}

	/* Evalúa si se ha alcanzado un nuevo record */
	private void SetRecords(int index){
		ulong bestScore;
		float score;

		score = GetCurrentScore();

		bestScore = DataManager.GetRecords(index);

		if(score > bestScore){
			PlayUIManagement.UpNewRecordText();
			DataManager.SetRecords(index, score);
		}
	}

	public float GetCurrentScore(){
		return GetPickUps() * 10 + Mathf.Floor(timerScript.GetTime());
	}
	public float GetPickUps(){
		return pickUps;
	}

	public bool GetDeath(){
		return death;
	}
}
