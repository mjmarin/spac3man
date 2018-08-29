using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveShieldPickUps : RemovePickUps {

	/* Inicialización de referencias */
	void Awake(){
		player = GameObject.FindWithTag("Player");
		spawnObject = GameObject.Find("ShieldRespawn");
	}

	/* Función de gestión en colisión con el objeto  jugador. Elimina el objeto */
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player") && player.GetComponent<PlayerController>().GetDeath() == false) {
			Destroy(this.gameObject);
		}
	}
}
