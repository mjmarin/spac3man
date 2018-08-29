using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMenu : MonoBehaviour {
	
	/* Velocidad a la que debe moverse el fondo del menú principal */
	[SerializeField] private float speed; 

	/* Cambio de posición cada frame en función del valor del atributo speed */
	void Update () {
		transform.position += Vector3.up * speed;
	}
}
