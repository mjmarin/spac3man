using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRotation : MonoBehaviour {

	[SerializeField] private float speed;
	private GameObject player;

	void Start(){
		player = GameObject.Find("Pacman");
	}
	void Update () {
		transform.RotateAround(player.transform.position, Vector3.forward, speed * Time.deltaTime);
	}
}
