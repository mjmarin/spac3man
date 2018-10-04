using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	/* Obtención de recursos y reproducción
	 si la música esta activada */
	void Start () {
		string resource;
		if(SceneManager.GetActiveScene().buildIndex == 0){
			resource = "Music/dreams";
		}else{
			if(DataManager.GetSelectionMS() == 2){
				resource = "Music/gamingCircuitBeat";
			}else{
				switch(DataManager.GetSelectionSS()){
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

		if(DataManager.GetMusicOn()){	
			GetComponent<AudioSource>().Play();
		}else{
			GetComponent<AudioSource>().Stop();
		}
		
	}

	/* Función asociada al evento OnClick de ajuste de música
	Activa/desactiva al clickar en los botones de ajuste de música */
	public void TurnOnOffMusic(){
		if(DataManager.GetMusicOn()){
			GetComponent<AudioSource>().Stop();
			DataManager.SetMusicOn(false);
		}else{
			GetComponent<AudioSource>().Play();
			DataManager.SetMusicOn(true);
		}
	}
}
