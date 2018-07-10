using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMenu : MonoBehaviour {
	
	[SerializeField] private float speed;
	void Update () {
		transform.position += Vector3.up * speed;
	}
}
