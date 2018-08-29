using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	/* Referencia al objeto jugador */
    private GameObject player;

	/* Vector distancia entre la cámara y el objeto jugador */
    private Vector3 offset;

	/* Inicialización de referencias */
	void Awake(){
		player = GameObject.FindWithTag("Player");
	}

	/* Cálculo del vector diferencia */
	void Start () {
        offset = transform.position - player.transform.position;
	}

	/* Modificación de la camara: seguimiento del objeto jugador */
	void LateUpdate () {
		if(player!=null){
			transform.position = player.transform.position + offset;
		} 
	}
}
