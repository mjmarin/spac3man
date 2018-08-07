using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePickUps : MonoBehaviour {

	[SerializeField] private float removeDistance;
	[SerializeField] private float removeTime;
	private GameObject player;
	private GameObject spawnObject;

	void Awake(){
		player = GameObject.FindWithTag("Player");
		spawnObject = GameObject.Find("ItemRespawn");
	}

	void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};
		removeDistance = removeDistance * multSpeed[Helper.DecryptInt(PlayerPrefs.GetString("speedSelected")) - 1];
	}

	void Update () {
		if(player != null){
			float distance = (transform.position.x - player.transform.position.x) + 
							(transform.position.y - player.transform.position.y);
			removeTime -= Time.deltaTime;
			if (removeTime < 0 || distance > removeDistance){
				Destroyed(this.gameObject);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player") && player.GetComponent<PlayerController>().GetDeath() == false) {
			Destroyed(this.gameObject);
		}
	}

	private void Destroyed(GameObject obj){
		spawnObject.GetComponent<SpawnItemManager>().IncreaseItemCount(false);
		Destroy(obj);
	}
}
