using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRotation : MonoBehaviour {

	/* Variable que marca la velocidad de rotación */
	[SerializeField] private float speed;

	/* Referencia al objeto jugador */
	private GameObject player;

	/* Inicialización de referencias */
	void Start(){
		player = GameObject.FindWithTag("Player");
	}

	/* Rotación alrededor del jugador */
	void Update () {
		transform.RotateAround(player.transform.position, Vector3.forward, speed * Time.deltaTime);
	}
}
