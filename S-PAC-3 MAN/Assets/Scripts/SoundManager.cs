using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

	private static  AudioClip getCoin;
	private static AudioClip getShield;
	private static AudioClip death;
	private AudioClip click;
	private static AudioSource audioSource;

	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}
	void Start () {

		click = Resources.Load<AudioClip>("Sounds/click");
		if(SceneManager.GetActiveScene().buildIndex == 1){
			getCoin = Resources.Load<AudioClip>("Sounds/getCoin");
			getShield = Resources.Load<AudioClip>("Sounds/getShield");
			death = Resources.Load<AudioClip>("Sounds/death");
		}
	}

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

	/* BOTONES */
	/* Para que se ponga/quite cuando se clicken los botones de música ON/OFF */
	public void TurnOnOffSound(){
		if(DataManager.GetSoundOn()){
			DataManager.SetSoundOn(false);
		}else{
			DataManager.SetSoundOn(true);
		}
	}

	public void Click(){
		if(DataManager.GetSoundOn()){
			audioSource.PlayOneShot(click);
		}
	}
}
