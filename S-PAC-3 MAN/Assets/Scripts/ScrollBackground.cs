using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {
	
	[SerializeField] private float scrollDebuff;		/* Parallax Scrolling */
	private MeshRenderer mr;
	void Start(){
		
		mr = GetComponent<MeshRenderer>();

	}
	void Update () {
		
		mr.material.SetTextureOffset("_MainTex",(transform.position / transform.localScale.x)/scrollDebuff );
		
	}
}
