using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemManager : SpawnManager {

	/* Variable que controla la distancia aumentada a la que
	instanciar objetos consecuente a la selección de velocidad */
	private Vector3 distance;

	/* Instanciación de objetos */
	void Update () {
		if(objectCount < maxObjectCount && player.GetComponent<PlayerController>().GetDeath() == false){
			timer -= Time.deltaTime;
			if(timer < 0){
				ReloadTimer();
				distance = transform.position - player.transform.position; // Aumentar la distancia a la que aparecen con la velocidad elegida
				SpawnIndicator(Instantiate(spawnable, transform.position + distance * speedBuff, Quaternion.identity));	/* A más velocidad spawnean más lejos */
				IncreaseObjectCount(true);
			}
			
		}
	}

}
