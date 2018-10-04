using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour {

	/*  Variables de desplazamiento de enemigo */

	/* Velocidad de rotación del enemigo */
	[SerializeField] private float rotationSpeed;

	/* Velocidad de desplazamiento del enemigo */
	[SerializeField] private float movementSpeed;

	/* Referencia al objeto jugador */
	private GameObject player;

	/* Modo de velocidad elegido para la partida */
	private float speedMode;

	/* Variables de eliminación de enemigo */

	/* Variable que controla si el enemigo se encuentra en proceso de desaparición */
	private bool disappearing = false;

	/* Variable que guarda la visibilidad del sprite del enemigo */
	private float alpha = 1.0f;

	/* Variable que controla la velocidad de disminución de visibilidad del sprite del enemigo */
	[SerializeField] private float alphaProgression;

	/* Inicialización de referencias y bonificación de velocidad por modo de velocidad elegido */
	void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};
		player = GameObject.FindWithTag("Player");
		speedMode = multSpeed[DataManager.GetSelectionSS() - 1];
	}

	/* Calcula el vector director entre el objeto jugador y el enemigo
	y cambia su orientación progresivamente con el fin de tener la dirección
	y sentido del vector. Luego modifica su posición en función de su 
	orientación actual. Además lleva a cabo la desaparición y destrucción
	del enemigo al ser marcado */
	void Update () {
		if(player!=null){
			Vector3 dir=player.transform.position-transform.position;
			dir.Normalize();
			
			float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler(0,0,-angle);
			transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,rotationSpeed * speedMode);

			transform.position += transform.up * movementSpeed * Time.deltaTime * speedMode;
		}

		if(disappearing){
			this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, alpha);
			if(alpha > 0){
				alpha -= alphaProgression * Time.deltaTime;
			}else{
				Destroy(this.gameObject);
			}
		}
	}

	/* Función interfaz que marca al fantasma para eleminarse */
	public void disappearGhost(){
		disappearing = true;
	}
}
