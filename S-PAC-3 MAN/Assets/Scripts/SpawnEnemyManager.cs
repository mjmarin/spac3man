using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : SpawnManager {

	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0 && player.GetComponent<PlayerController>().GetDeath() == false){
			ReloadTimer();
			/* Fantasmas salen con orientación al jugador */
			Vector3 dir=player.transform.position-transform.position;
			dir.Normalize();
			float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;

			GameObject obj = Instantiate(spawnable, transform.position, Quaternion.Euler(0,0,-angle));

			SpawnIndicator(obj);
		}
	}
}
