using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour {

	/* Referencia a objeto ligado a la posición de este*/
	[SerializeField] private GameObject parent;

	/* Variable que guarda la rotación inicial de este objeto */
	private Quaternion initialRotation;

	/* Modificación de la posición y la rotación a las del objeto
	ligado al movimiento de este */
	void Start () {
		transform.position = parent.transform.position;
		transform.rotation = parent.transform.rotation;
	}
	
	/* Mantener rotación inicial. Evitar funcionalidad gráfica
	no deseada de Unity */
	void Update () {
		transform.rotation = Quaternion.identity;
	}
}
