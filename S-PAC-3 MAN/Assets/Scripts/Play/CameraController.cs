using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;

    private Vector3 offset;

	void Awake(){
		player = GameObject.FindWithTag("Player");
	}
	void Start () {
        offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		if(player!=null){
			transform.position = player.transform.position + offset;
		} 
	}
}
