using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float timePickUps;
	[SerializeField] private float invulnerableTime;

	private GameObject ShieldRespawn;
	
	private PauseController scriptPause;
	private PlayUIManagement PlayUIManagement;
	private bool shielded;
	private float timeInv;
	private bool death;
	private float screenWidth;
	private int pickUps;
	private int pickedShields;
	private float timeShield;

	private float speedBuff;
	private bool NEOActivated;
	private int speedMode;

	void Awake(){
		GameObject canvas = GameObject.Find("Canvas");
		scriptPause = canvas.GetComponent<PauseController>();
		PlayUIManagement = canvas.GetComponent<PlayUIManagement>();
		ShieldRespawn = GameObject.Find("ShieldRespawn");
	}
	void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};

		SetShield(false);
		death = false;
		screenWidth = Screen.width;
		pickUps = 0;
		pickedShields = 0;
		timeShield = 0;

		speedMode = Helper.DecryptInt(PlayerPrefs.GetString("speedSelected"));
		speedBuff = multSpeed[speedMode - 1];

		if(Helper.DecryptInt(PlayerPrefs.GetString("modeSelected")) == 1){
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
			if(GetDeath() == false && scriptPause.GetPaused() == false ){		// Quiero que al morir siga hacia delante pero ya no se pueda controlar
				if (Input.touchCount > 0){
					if (Input.GetTouch (0).position.x > screenWidth / 2) {					//Move Right
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
		if(PlayUIManagement.GetNeoTime() <= 0 && GetDeath() == false){
			SetDeath(true);
		}
	}

	/* Colisiones con elementos. Muerte y recogida de objetos */
	void OnTriggerEnter2D(Collider2D other){
		if(GetDeath() == false){
			if(other.CompareTag("Enemy")) {		/* Al chocarte con un enemigo o mueres o se quita escudo y comienza cuenta atrás de invulnerabilidad */
				if(shielded){
					SetShield(false);
					if(timeInv < 0){
						timeInv = 0;
						SetDeath(true);
					}
				}else{
					SetDeath(true);
				}	
			}else{
				if(other.CompareTag("ShieldPick")){
					SetShield(true);
				}else{
					pickUps++;
					SoundManager.SetSound("getCoin");
					if(NEOActivated){
						PlayUIManagement.EnlargeNeoTime(timePickUps);
					}
				}
			}
		}
	}
	
	/* Establece muerte del jugador */
	private void SetDeath(bool boolean){
		if(boolean){
			int money = Helper.DecryptInt(PlayerPrefs.GetString("money"));
			int reward = QuestLoader.CheckQuests(Mathf.RoundToInt(PlayUIManagement.GetTime()), pickUps, pickedShields, Mathf.RoundToInt(timeShield));
			PlayerPrefs.SetString("money", Helper.EncryptInt(pickUps + money + reward));

			if(reward > 0){
				PlayUIManagement.UpQuestRewardText(reward);
			}

			SetRecords();

			SoundManager.SetSound("death");
		}
		death = boolean;
	}

	private void SetShield(bool boolean){
		if(boolean){
			timeInv = invulnerableTime;
			pickedShields++;
			SoundManager.SetSound("getShield");
		}
		shielded = boolean;
		transform.GetChild(0).gameObject.SetActive(boolean);
	}

	private void CheckShield(){
		if(shielded){
			timeShield += Time.deltaTime;
		}else{
			if(timeInv > 0){
				timeInv = timeInv - Time.deltaTime;
				if(timeInv <= 0){
					timeInv = 0;
					ShieldRespawn.GetComponent<SpawnItemManager>().IncreaseItemCount(false);
				}
			}
		}

	}

	/* Evalúa si se ha alcanzado un nuevo record */
	private void SetRecords(){
		int bestScore, score;
		string recordMode;

		score = GetScore();

		if(NEOActivated){
			if(speedMode == 1){
				recordMode = "NEONormalRecord";
			}else if(speedMode == 2){
				recordMode = "NEOSPRecord";
			}else{
				recordMode = "NEOTryhardRecord";
			}
		}else{
			if(speedMode == 1){
				recordMode = "NNormalRecord";
			}else if(speedMode == 2){
				recordMode = "NSPRecord";
			}else{
				recordMode = "NTryhardRecord";
			}
		}

		bestScore = Helper.DecryptInt(PlayerPrefs.GetString(recordMode));

		if(score > bestScore){
			PlayUIManagement.UpNewRecordText();
			PlayerPrefs.SetString(recordMode, Helper.EncryptInt(score));
		}
	}

	public int GetScore(){
		int time = Mathf.FloorToInt(PlayUIManagement.GetTime());
		return GetPickUps() * 10 + time;
	}
	public int GetPickUps(){
		return pickUps;
	}

	public bool GetDeath(){
		return death;
	}
}
