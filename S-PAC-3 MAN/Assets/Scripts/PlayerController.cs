using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	public bool isDead;
	private float ScreenWidth;

	void Start(){
		ScreenWidth = Screen.width;
		isDead = false;
	}

	void Update () {
		# if UNITY_EDITOR
			transform.Rotate(new Vector3(0,0,-Input.GetAxis("Horizontal") * rotationSpeed));		//Para probar en PC
		#else
			int direction = 0;
			if(isDead == false){		// Quiero que al morir siga hacia delante pero ya no se pueda controlar
				if (Input.touchCount > 0){
					if (Input.GetTouch (0).position.x > ScreenWidth / 2) {					//Move Right
						direction = -1;
					}else{																	//Move Left
						direction = 1;
					}
				}
			}
			transform.Rotate(new Vector3(0,0,direction * rotationSpeed));
		#endif
		transform.position += transform.up * Time.deltaTime * movementSpeed;
	}
}
