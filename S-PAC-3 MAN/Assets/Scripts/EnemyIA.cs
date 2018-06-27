using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour {

	[SerializeField] private GameObject player;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float movementSpeed;
	
	// Update is called once per frame
	void Update () {
		if(player!=null){
			Vector3 dir=player.transform.position-transform.position;
			dir.Normalize();
			
			float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler(0,0,-angle);
			transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,rotationSpeed);

			transform.position += transform.up * movementSpeed * Time.deltaTime;
		}
	}
}
