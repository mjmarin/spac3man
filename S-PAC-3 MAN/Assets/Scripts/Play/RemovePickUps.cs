using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePickUps : MonoBehaviour {

	[SerializeField] protected float removeDistance;
	[SerializeField] protected float removeTime;
	protected GameObject player;
	protected GameObject spawnObject;
	private Vector3 distance;
	private float speedBuff;

	void Awake(){
		player = GameObject.FindWithTag("Player");
		spawnObject = GameObject.Find("PickUpRespawn");
	}

	void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};
		speedBuff = multSpeed[DataManager.GetSelectionSS() - 1];
	}

	protected void Update () {
		if(player != null){
			distance = transform.position - player.transform.position;
			removeTime -= Time.deltaTime;
			if (removeTime < 0 || distance.magnitude > removeDistance * speedBuff){
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
		spawnObject.GetComponent<SpawnItemManager>().IncreaseObjectCount(false);
		Destroy(obj);
	}
}
