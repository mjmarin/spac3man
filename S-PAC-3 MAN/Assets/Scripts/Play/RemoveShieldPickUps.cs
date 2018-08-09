using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveShieldPickUps : RemovePickUps {

	void Awake(){
		player = GameObject.FindWithTag("Player");
		spawnObject = GameObject.Find("ShieldRespawn");
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player") && player.GetComponent<PlayerController>().GetDeath() == false) {
			Destroy(this.gameObject);
		}
	}
}
