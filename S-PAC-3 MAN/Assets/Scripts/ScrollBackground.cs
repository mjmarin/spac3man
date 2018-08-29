using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {
	
	/* Velocidad de scrolling para realizar Parallax Scrolling */
	[SerializeField] private float scrollDebuff;

	/* Referencia al componente de renderizado gráfico */
	private MeshRenderer mr;

	/* Modificación por velocidad elegida*/
	private float speedBuff;

	/* Inicialización de variables */
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

	/* Scrolling del fondo */
	void Update () {
		
		mr.material.SetTextureOffset("_MainTex",((transform.position / transform.localScale.x) / scrollDebuff) * speedBuff );
		
	}
}
