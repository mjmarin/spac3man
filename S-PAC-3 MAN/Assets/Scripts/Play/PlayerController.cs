using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

/* Variables de características de la partida */
	/* Velocidad de desplazamiento del personaje */
	[SerializeField] private float movementSpeed;

	/* Velocidad de rotación del personaje */
	[SerializeField] private float rotationSpeed;

	/* Tiempo añadido por recogida de monedas en modo notEnoughOxygen */
	[SerializeField] private float pickUpsTime;

	/* Tiempo de invulnerabilidad al perder un escudo */
	[SerializeField] private float invulnerableTime;

	/* Tiempo de de efecto visual al perder un escudo */
	[SerializeField] private float turnOffShieldTime;

/* Referencias */
	/* Referencia al objeto spawner de escudos */
	private GameObject ShieldRespawn;
	
	/* Referencia al script de control temporal en partida */
	private Timer timerScript;

	/* Referencia al script de control gráfica en partida */
	private PlayUIManagement PlayUIManagement;

/* Variables de estado de la partida */	

	/* Variable que marca si el objeto jugador tiene escudo */
	private bool shielded;

	/* Variable que marca el tiempo restante de invulnerabilidad 
	del objeto jugador al perder un escudo */
	private float timeInv;

	/* Variable que señala si el objeto jugador ha sido eliminado */
	private bool death;

	/* Variable que guarda la cantidad de monedas recogidas */
	private float pickUps;

	/* Variable que guarda la cantidad de escudos recogidos */
	private int pickedShields;

	/* Variable que guarda el tiempo que el objeto jugador ha
	usado un escudo */
	private float timeShield;

	/* Variable que es utilizada para el efecto gráfico 
	de intermitencia al perder el escudo */
	private float offShieldTime;

	/* Variable que controla el ancho de la pantalla */
	private float screenWidth;

	/* Variable que contiene el valor añadido de velocidad 
	según la el modo de velocidad elegido */
	private float speedBuff;

	/* Variable que guarda si se seleccionó 
	el modo notEnoughOxygen para la partida */
	private bool NEOActivated;

	/* Variable que guarda el modo de velocdiad seleccionado */
	private int speedMode;

	/* Inicialización de referencias */
	void Awake(){
		GameObject canvas = GameObject.Find("Canvas");
		timerScript = Camera.main.GetComponent<Timer>();
		PlayUIManagement = canvas.GetComponent<PlayUIManagement>();
		ShieldRespawn = GameObject.Find("ShieldRespawn");
	}

	/* Inicialización de variables */
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

	/* Control de movimiento, control de tiempo con escudo y activación
	de la animación de destrucción del mismo. Control muerte por tiempo
	en notEnoughOxygen */
	void Update () {
		Movement();
		CheckShield();
		if(NEOActivated){
			CheckNEODeath();
		}
	}

	/* Tratamiento del entrada */
	void Movement(){
		/* Para probar en editor de Unity */
		# if UNITY_EDITOR
			transform.Rotate(new Vector3(0,0,-Input.GetAxis("Horizontal") * rotationSpeed * speedBuff));		
		#else
			int direction = 0;
			/* Al morir se pierde el control pero el objeto sigue hacia adelante */
			if(GetDeath() == false && timerScript.GetPaused() == false ){		
				if (Input.touchCount > 0){
					/* Movimiento derecha */
					if (Input.GetTouch (Input.touchCount - 1).position.x > screenWidth / 2) {					
						direction = -1;
					}else{ /* Movimiento izquierda */																		
						direction = 1;
					}
				}
			}
			transform.Rotate(new Vector3(0,0,direction * rotationSpeed * speedBuff)); /* Cambio de dirección */
		#endif

		/* Cambio de posición según la dirección actual */
		transform.position += transform.up * Time.deltaTime * movementSpeed * speedBuff;			
	}

	/* Comprueba la muerte por tiempo en notEnoughOxygen */
	void CheckNEODeath(){
		if(timerScript.GetNeoTime() <= 0 && GetDeath() == false){
			SetDeath(true);
		}
	}

	/* Colisiones con elementos. Muerte y recogida de objetos */
	void OnTriggerEnter2D(Collider2D other){
		if(GetDeath() == false){
			/* Al chocarte con un enemigo o mueres o se quita escudo y
			 comienza cuenta atrás de invulnerabilidad */
			if(other.CompareTag("Enemy")) {		
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
	
	/* Establece muerte del jugador. Guarda las monedas obtenidas.
	Comprueba las misiones superadas, envía la puntuación a los 
	marcadores online si está conectado y comprueba si se ha superado
	en record actual, en cuyo caso se guarda el nuevo record */
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

	/* Activa o desactiva el escudo y sus efectos sonoros */
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

	/* Comprueba el tiempo con escudo y activa su
	intermitencia es destruido */
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

	/* Efecto de intermitencia en escudo perdido */
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

	/* Evalúa si se ha alcanzado un nuevo record y lo guarda si es así */
	private void SetRecords(int index){
		ulong bestScore;
		float score;

		score = GetCurrentScore();

		bestScore = DataManager.GetRecords(index);

		if(Social.localUser.authenticated){
			if(Helper.CheckLongOverflow(score)){
				score =  long.MaxValue;
			}
			GooglePlayManager.SetOnlineScore(System.Convert.ToInt64(score),GooglePlayManager.GetModeID(DataManager.GetSelectionMS(), DataManager.GetSelectionSS()));
		}

		if(score > bestScore){
			PlayUIManagement.UpNewRecordText();
			DataManager.SetRecords(index, score);
		}
	}

	/* Interfaz que devuelve la puntuación actual */
	public float GetCurrentScore(){
		return GetPickUps() * 10 + Mathf.Floor(timerScript.GetTime());
	}

	/* Interfaz que devuelve las monedas recogidas */
	public float GetPickUps(){
		return pickUps;
	}

	/* Interfaz que devuelve si el personaje ha sido eliminado */
	public bool GetDeath(){
		return death;
	}
}
