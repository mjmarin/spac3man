using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	/* Referencia a tipo de objeto a instanciar */
	[SerializeField] protected GameObject spawnable;

	/* Referencia a tipo de marcador a instanciar */
	[SerializeField] protected GameObject indicator;

	/* Variable tiempo de espera entre instanciaciones */
	[SerializeField] protected float spawnCooldown;

	/* Variable tiempo de espera añadido al inicio para primera instanciación */
	[SerializeField] protected float initDelay;

	/* Variable que marca el límite de objetos de ese tipo disponibles */
	[SerializeField] protected int maxObjectCount;

	/* Variable contador de objetos actuales de tipo spawnable */
	protected int objectCount;

	/* Variable temporizador */
	protected float timer;

	/* Referencia al objeto jugador */
	protected GameObject player;

	/* Variable modificadora por selección de velocidad */
	protected float speedBuff;

	/* Inicialización de variables */
	protected void Start(){		
		Random.InitState((int)System.DateTime.Now.Ticks); 

		timer = spawnCooldown + initDelay;
		player = GameObject.FindWithTag("Player");

		float[] multSpeed = {0.0f, 0.5f, 1.0f};
		speedBuff = multSpeed[DataManager.GetSelectionSS() - 1];
	}

	/* Función de reseteo de temporizador */
	protected void ReloadTimer(){
		float random = Random.Range(0, 1 * spawnCooldown / 3);
		timer=spawnCooldown - random;
	}

	/* Función de instanciación de indicador */
	protected void SpawnIndicator(GameObject obj){
		GameObject ind = Instantiate(indicator, transform.position, Quaternion.identity);
		ind.transform.parent = player.transform;
		ind.transform.position = player.transform.position;
		ind.transform.GetChild(0).transform.localPosition = new Vector3(0, Camera.main.orthographicSize * 0.9f, 0);
		ind.GetComponent<OffscreenIndicator>().SetTarget(obj);	
	}

	/* Función interfaz modificadora del número de objetos */
	public void IncreaseObjectCount(bool create){
		if(create){
			objectCount++;
		}else{
			objectCount--;
		}
	}
}
