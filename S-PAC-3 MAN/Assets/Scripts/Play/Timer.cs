using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	/* Referencia al script de interfaz gráfica en escena de juego */
	private PlayUIManagement UIScript;

	/* Referencia al script que controla el estado del jugador */
	private PlayerController playerScript;

	/* Variable marcador de situación de pausa */
	private bool paused;

	/* Variable temporizador */
	private float time;

	/* Variable tiempo inicial en el modo NEO */
	[SerializeField] private float NEOtime;
	
	/* Inicialización de referencias */
	void Awake(){
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		UIScript = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PlayUIManagement>();
	}

	/* Inicialización de variables */
	void Start(){
		time = 0.0f;
		paused = false;
		Time.timeScale = 1f;
	}

	/* Actualización del tiempo */
	void Update(){
		UpdateTime();
	}

	/* Actualización del tiempo de partida y tiempo NEO */
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

	/* Interfaz de entrada a estado de pausa */
	public void ChangeState(){
		if(paused){
			paused = false;
			Time.timeScale = 1f;
		}else{
			paused = true;
			Time.timeScale = 0f;
		}
	}

	/* Interfaz de modificación del tiempo NEO */
	public void EnlargeNeoTime(float seconds){
		NEOtime += seconds;
		UIScript.EnlargeNeoTimeText(seconds);
	}

	/* Interfaz de consulta sobre estado pausa */
	public bool GetPaused(){
		return paused;
	}
 
	/* Interfaz de consulta de tiempo */
	public float GetTime(){
		return time;
	}

	/* Interfaz de consulta de tiempo NEO restante */
	public float GetNeoTime(){
		return NEOtime;
	}
}
