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
	private static bool soundOn = true;

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
		
		if(PlayerPrefs.HasKey("soundOn") == false || Helper.DecryptInt(PlayerPrefs.GetString("soundOn")) == 1){	
			soundOn = true;
			PlayerPrefs.SetString("soundOn", Helper.EncryptInt(1));
		}else{
			soundOn = false;
		}
		
	}

	public static bool GetSoundOn(){
		return soundOn;
	}

	public static void SetSound(string sound){
		if(soundOn){
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
		int boolInt;

		if(soundOn){
			soundOn = false;
			boolInt = 0;
		}else{
			soundOn = true;
			boolInt = 1;
		}
		PlayerPrefs.SetString("soundOn", Helper.EncryptInt(boolInt));
	}

	public void Click(){
		if(soundOn){
			audioSource.PlayOneShot(click);
		}
	}
}
