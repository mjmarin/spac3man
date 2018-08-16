using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {
	
	[SerializeField] private float scrollDebuff;		/* Parallax Scrolling */
	private MeshRenderer mr;
	private float speedBuff;
	void Start(){
		/* Acelerar el scrolling dependiendo de la velocidad elegida */
		float[] multSpeed = {1.0f, 1.5f, 2.0f};
		int speedMode = DataManager.GetSelectionSS();

		if(speedMode == -1){
			speedBuff = 1.0f;
		}else{
			speedBuff = multSpeed[speedMode - 1]; /* En menú será la última velocidad jugada y en partida la velocidad elegida */
		}		
																									
		mr = GetComponent<MeshRenderer>();

	}
	void Update () {
		
		mr.material.SetTextureOffset("_MainTex",((transform.position / transform.localScale.x) / scrollDebuff) * speedBuff );
		
	}
}
