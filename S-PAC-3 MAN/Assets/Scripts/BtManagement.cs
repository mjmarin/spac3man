using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtManagement : MonoBehaviour {

	[SerializeField] private GameObject gameOver;
	[SerializeField] private GameObject restartBt;
	[SerializeField] private GameObject player;
	void Start () {
		gameOver.SetActive(false);
		restartBt.SetActive(false);
	}
	
	void Update(){
		if(player.GetComponent<PlayerController>().isDead == true){
			gameOver.SetActive(true);
			restartBt.SetActive(true);
		}
	}
}
