using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenIndicator : MonoBehaviour {
	
	/* Referencia al objeto que apunta */
	private GameObject target;

	/* Referencia a la cámara */
	private Camera cam;

	/* Referencia al jugador */
	private GameObject player;

	/* Inicialización de referencias y posicionamiento en la dirección 
	y sentido del objeto jugador al objeto apuntado */
	void Start(){
		cam = Camera.main;
		player = GameObject.FindWithTag("Player");
		PointToTarget();
	}

	void Update () {
		PointToTarget();
	}

	/* Gestión de la destrucción de la flecha si el objeto al que apunta ha sido destruido.
	Modificación de la dirección y posición de la flecha.
	Descativación de la flecha si el objeto apuntado es visible en la cámara. */
	void PointToTarget(){
		if(target == null){
			Destroy(this.gameObject);
		}else{
			Vector3 distance = target.transform.position - transform.position;
			Vector3 visible = cam.WorldToViewportPoint(target.transform.position);

			if((visible.x > 0 && visible.x < 1 && visible.y > 0 && visible.y < 1 && visible.z > 0) 
			|| player.GetComponent<PlayerController>().GetDeath()){ //Si el objeto es visible por la cámara o jugador muerto
				this.transform.GetChild(0).gameObject.SetActive(false);
			}else{
				this.transform.GetChild(0).gameObject.SetActive(true);
				float angle = Mathf.Atan2(distance.x,distance.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(-angle,Vector3.forward);
			}
		}
	}

	/* Interfaz para definir el objeto apuntado */
	public void SetTarget(GameObject gameObj){
		target = gameObj;
	}
}
