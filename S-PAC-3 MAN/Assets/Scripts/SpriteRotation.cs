using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour {

	[SerializeField] private GameObject parent;
	private Quaternion initialRotation;
	void Start () {
		transform.position = parent.transform.position;
		transform.rotation = parent.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.identity;
	}
}
