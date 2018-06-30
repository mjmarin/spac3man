using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField] private GameObject spawnable;
	[SerializeField] private float spawnCooldown;
	[SerializeField] private float delay;

	private float timer;
	private GameObject player;

	void Start(){
		timer = spawnCooldown + delay;
		player = GameObject.Find("Pacman");
	}


	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0){
			/* Fantasmas salen con orientación al jugador */
			Vector3 dir=player.transform.position-transform.position;
			dir.Normalize();
			float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;

			Instantiate(spawnable, transform.position, Quaternion.Euler(0,0,-angle));
			timer=spawnCooldown;
		}
	}
}
