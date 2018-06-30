using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePickUps : MonoBehaviour {

	[SerializeField] private float removeDistance;
	[SerializeField] private float removeTime;
	private GameObject player;
	
	void Start(){
		player = GameObject.Find("Pacman");
	}
	
	void Update () {
		if(player != null){
			float distance = (transform.position.x - player.transform.position.x) + 
							(transform.position.y - player.transform.position.y);
			removeTime -= Time.deltaTime;
			if (removeTime < 0 || distance > removeDistance){
				Destroy(this.gameObject);
			}
		}
	}
}
