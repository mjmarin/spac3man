using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePickUps : MonoBehaviour {

	[SerializeField] protected float removeTime;
	protected GameObject player;
	protected GameObject spawnObject;

	void Awake(){
		player = GameObject.FindWithTag("Player");
		spawnObject = GameObject.Find("PickUpRespawn");
	}

	protected void Update () {
		if(player != null){
			removeTime -= Time.deltaTime;
			if (removeTime < 0){
				Destroyed(this.gameObject);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player") && player.GetComponent<PlayerController>().GetDeath() == false) {
			Destroyed(this.gameObject);
		}
	}

	protected void Destroyed(GameObject obj){
		spawnObject.GetComponent<SpawnItemManager>().IncreaseItemCount(false);
		Destroy(obj);
	}
}
