using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

	/* Variables de audo */
	private static  AudioClip getCoin;
	private static AudioClip getShield;
	private static AudioClip shieldDown;
	private static AudioClip death;
	private AudioClip click;

	/* Referencia a componente de reproducción de audios */
	private static AudioSource audioSource;

	/* Inicialización de referencias */
	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

	/* Obtención de recursos */
	void Start () {

		click = Resources.Load<AudioClip>("Sounds/click");
		if(SceneManager.GetActiveScene().buildIndex == 1){
			getCoin = Resources.Load<AudioClip>("Sounds/getCoin");
			getShield = Resources.Load<AudioClip>("Sounds/getShield");
			shieldDown = Resources.Load<AudioClip>("Sounds/shieldDown");
			death = Resources.Load<AudioClip>("Sounds/death");
		}
	}

	/* Función interfaz para reproducir un sonido */
	public static void SetSound(string sound){
		if(DataManager.GetSoundOn()){
			AudioClip clip;
			switch(sound){
				case "getCoin":
					clip = getCoin;
				break;

				case "getShield":
					clip = getShield;
				break;

				case "shieldDown":
					clip = shieldDown;
				break;

				case "death":
					clip = death;
				break;

				default:
					clip = getCoin;
				break;
			}
			audioSource.PlayOneShot(clip);
		}	
	}

	/* Función asociada al evento OnClick de ajuste de sonido.
	Activa/desactiva al clickar en los botones de ajuste de sonido */
	public void TurnOnOffSound(){
		if(DataManager.GetSoundOn()){
			DataManager.SetSoundOn(false);
		}else{
			DataManager.SetSoundOn(true);
		}
	}

	/* Función asociada al evento OnClick de ajuste de sonido.
	Reproduce el sonido asociado al uso de botones si el sonido está activo */
	public void Click(){
		if(DataManager.GetSoundOn()){
			audioSource.PlayOneShot(click);
		}
	}
}
