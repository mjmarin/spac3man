using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float timePickUps;
	private PauseController scriptPause;
	private PlayUIManagement Timer;
	private bool death;
	private float screenWidth;
	private int pickUps;

	private float speedBuff;
	private bool NEOActivated;
	private int speedMode;

	void Awake(){
		GameObject canvas = GameObject.Find("Canvas");
		scriptPause = canvas.GetComponent<PauseController>();
		Timer = canvas.GetComponent<PlayUIManagement>();
	}
	void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};

		
		screenWidth = Screen.width;
		death = false;
		pickUps = 0;

		speedMode = Helper.DecryptInt(PlayerPrefs.GetString("speedSelected"));
		speedBuff = multSpeed[speedMode - 1];
		timePickUps = timePickUps / multSpeed[speedMode - 1];

		if(Helper.DecryptInt(PlayerPrefs.GetString("modeSelected")) == 1){
			NEOActivated = false;
		}else{
			NEOActivated =  true;
		}
		
	}

	/* Control de movimiento */
	void Update () {
		Movement();
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
			transform.Rotate(new Vector3(0,0,direction * rotationSpeed * speedMode));
		#endif
		transform.position += transform.up * Time.deltaTime * movementSpeed * speedBuff;
	}

	void CheckNEODeath(){
		if(Timer.GetNeoTime() <= 0 && GetDeath() == false){
			SetDeath(true);
		}
	}

	/* Colisiones con elementos. Muerte y recogida de objetos */
	void OnTriggerEnter2D(Collider2D other){
		if(GetDeath() == false){
			if(other.CompareTag("Enemy")) {
				SetDeath(true);
			}else{
				pickUps++;
				if(NEOActivated){
					Timer.EnlargeNeoTime(timePickUps);
				}
			}
		}
	}
	
	/* Establece muerte del jugador */
	private void SetDeath(bool boolean){
		if(boolean){
			int money = Helper.DecryptInt(PlayerPrefs.GetString("money"));
			PlayerPrefs.SetString("money", Helper.EncryptInt(pickUps + money));
			SetRecords();
		}
		death = boolean;
	}

	/* Evalúa si se ha roto un nuevo record */
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
			Timer.UpNewRecordText();
			PlayerPrefs.SetString(recordMode, Helper.EncryptInt(score));
		}
	}

	public int GetScore(){
		int time = Mathf.RoundToInt(Timer.GetTime());
		return GetPickUps() * 10 + time;
	}
	public int GetPickUps(){
		return pickUps;
	}

	public bool GetDeath(){
		return death;
	}
}
