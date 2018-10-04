using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	/* Tiempo entre dos instantes consecutivos
	 de una animación */
	[SerializeField] private float requiredTime;

	/* Temporizador */
	private float time;

	/* Control del sprite a utilizar
	 en cada momento */
	private int turn;

	/* Referencia al script que controla
	 el estado del jugador */
	private PlayerController player;

	/* Referencia al componente que renderiza
	 el sprite del objeto jugador */
	private SpriteRenderer playerSprite;

	/* Conjunto de sprites que conforma 
	una animación de movimiento */
	private Sprite[] playSprites = new Sprite[4];

	/* Conjunto de sprites que conforman 
	una animación de muerte */
	private Sprite[] deathSprites = new Sprite[12];

	/* Variable que confirma si es el primer frame
	 con el objeto jugador eliminado */
	private bool wasDead;
	

	/* Inicialización de referencias */
	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
	}

	/* Inicialización de variables */
	void Start () {
		ChargeSkin(DataManager.GetSettedSkin());

		time = 0;
		turn = 0;
		wasDead = false;
	}
	
	/* Animación de movimiento y control de activación
	 de animación de muerte */
	void Update () {
		time = time + Time.deltaTime;
		if(player != null){ /* Se encuentra jugando */
			if(player.GetDeath()){
				if(wasDead == false){
					time = 0;
					turn = 0;
					wasDead = true;
				}
				if(time > requiredTime){
					DeathAnimation();
				}
			}else{
				if(time > requiredTime){
					AliveAnimation();
				}
			}
		}else{ /* Se encuentra en menú */
			if(time > requiredTime){
				AliveAnimation();
			}
		}	
	}

	/* Rotación de animación de movimiento */
	private void AliveAnimation(){
		turn = (turn + 1) % 4;
		playerSprite.sprite = playSprites[turn];
		time = 0;
	}

	/* Animación de personaje eliminado */
	private void DeathAnimation(){
		if(turn < 12){
			playerSprite.sprite = deathSprites[turn];
			turn ++;
			time = 0;
		}
	}

	/* Función interfaz para modificar el personaje */
	public void ChangeSkin(int skin){
		DataManager.SetSettedSkin(skin);
		ChargeSkin(skin);
	}

	/* Función de carga de sprites de la animación
	del personaje elegido */
	private void ChargeSkin(int skin){
		Sprite[] resourcesArray = new Sprite[3];

		resourcesArray = Resources.LoadAll<Sprite>("Sprites/Skins/skin" + skin);
		playSprites[0] = resourcesArray[2];
		playSprites[1] = resourcesArray[1];
		playSprites[2] = resourcesArray[0];
		playSprites[3] = resourcesArray[1];

		if(player != null){ /* Se encuentra jugando */
			deathSprites = Resources.LoadAll<Sprite>("Sprites/Skins/deathSkin" + skin);
		}
	}
}
