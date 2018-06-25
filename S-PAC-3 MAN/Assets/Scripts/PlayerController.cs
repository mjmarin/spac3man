using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;

	void Update () {
		transform.Rotate(new Vector3(0,0,-Input.GetAxis("Horizontal") * rotationSpeed));
		transform.position += transform.up * Time.deltaTime * movementSpeed;
	}
}
