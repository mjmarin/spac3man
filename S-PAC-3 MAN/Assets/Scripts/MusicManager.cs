using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	private static bool musicOn = true;

	void Start () {
		string resource;
		if(SceneManager.GetActiveScene().buildIndex == 0){
			resource = "Music/dreams";
		}else{
			if(Helper.DecryptInt(PlayerPrefs.GetString("modeSelected")) == 2){
				resource = "Music/gamingCircuitBeat";
			}else{
				int speed = Helper.DecryptInt(PlayerPrefs.GetString("speedSelected"));
				switch(speed){
					case 1:
						resource = "Music/2018";
					break;

					case 2:
						resource = "Music/arcadeMusicLoop";
					break;

					case 3:
						resource = "Music/arcadeWar";
					break;

					default:
						resource = "Music/2018";
					break;
				}
			}
		}
		
		GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(resource);

		if(PlayerPrefs.HasKey("musicOn") == false || Helper.DecryptInt(PlayerPrefs.GetString("musicOn")) == 1){	
			musicOn = true;
			GetComponent<AudioSource>().Play();
			PlayerPrefs.SetString("musicOn", Helper.EncryptInt(1));
		}else{
			musicOn = false;
			GetComponent<AudioSource>().Stop();
		}
		
	}

	public static bool GetMusicOn(){
		return musicOn;
	}

	/* BOTONES */
	/* Para que se ponga/quite cuando se clicken los botones de música ON/OFF */
	public void TurnOnOffMusic(){
		int boolInt;
		if(musicOn){
			musicOn = false;
			boolInt = 0;
			GetComponent<AudioSource>().Stop();
		}else{
			musicOn = true;
			boolInt = 1;
			GetComponent<AudioSource>().Play();
		}
		PlayerPrefs.SetString("musicOn", Helper.EncryptInt(boolInt));
	}
}
