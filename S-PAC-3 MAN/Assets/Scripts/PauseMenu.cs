using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	[SerializeField] private Sprite soundOn;
	[SerializeField] private Sprite soundOff;
	[SerializeField] private Button soundBt;
	[SerializeField] private Sprite musicOn;
	[SerializeField] private Sprite musicOff;
	[SerializeField] private Button musicBt;
	[SerializeField] private GameObject player;
	private PauseController scriptPause;

	private void Awake(){
		scriptPause = this.gameObject.GetComponent<PauseController>();
	}
	
	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu(){
		if(!player.GetComponent<PlayerController>().GetDeath())
			scriptPause.ChangeState();
		SceneManager.LoadScene(0);
	}

	public void Quit(){
		Application.Quit();
	}

	public void SoundClick(){
		if(PlayerPrefs.GetInt("soundOn") == 1){
			soundBt.GetComponent<Image>().sprite = soundOff;
			PlayerPrefs.SetInt("soundOn", 0);
		}else{
			soundBt.GetComponent<Image>().sprite = soundOn;
			PlayerPrefs.SetInt("soundOn", 1);
		}
	}

	public void MusicClick(){
		if(PlayerPrefs.GetInt("musicOn") == 1){
			musicBt.GetComponent<Image>().sprite = musicOff;
			PlayerPrefs.SetInt("musicOn", 0);
		}else{
			musicBt.GetComponent<Image>().sprite = musicOn;
			PlayerPrefs.SetInt("musicOn", 0);
		}
	}
}
