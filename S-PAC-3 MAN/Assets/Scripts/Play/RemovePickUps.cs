using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePickUps : MonoBehaviour {

	/* Variable que controla la distancia a la que eliminar un objeto */
	[SerializeField] protected float removeDistance;

	/* Variable que controla el tiempo al que un objeto es eliminado desde su instanciación */
	[SerializeField] protected float removeTime;

	/* Referencia a objeto jugador */
	protected GameObject player;

	/* Referencia a objeto spawner */
	protected GameObject spawnObject;

	/* Variable que guarda la distancia respecto al objeto jugador */
	private Vector3 distance;

	/* Variable que almacena la modificación por velocidad seleccionada */
	private float speedBuff;


	/* Inicialización de referencias */
	void Awake(){
		player = GameObject.FindWithTag("Player");
		spawnObject = GameObject.Find("PickUpRespawn");
	}

	/* Inicialización de variables */
	void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};
		speedBuff = multSpeed[DataManager.GetSelectionSS() - 1];
	}

	/* Función de comprobación de las condiciones de eliminación de objeto y eliminación del mismo */
	protected void Update () {
		if(player != null){
			distance = transform.position - player.transform.position;
			removeTime -= Time.deltaTime;
			if (removeTime < 0 || distance.magnitude > removeDistance * speedBuff){
				Destroyed(this.gameObject);
			}
		}
	}

	/* Función de gestión en colisión con el objeto  jugador. Elimina el objeto */
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player") && player.GetComponent<PlayerController>().GetDeath() == false) {
			Destroyed(this.gameObject);
		}
	}

	/* Función de añadido de moneda y destrucción de objeto */
	protected void Destroyed(GameObject obj){
		spawnObject.GetComponent<SpawnItemManager>().IncreaseObjectCount(false);
		Destroy(obj);
	}
}
