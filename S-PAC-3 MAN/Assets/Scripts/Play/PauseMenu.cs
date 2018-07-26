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
	private bool musicBool;
	private bool soundBool;

	private void Awake(){
		scriptPause = this.gameObject.GetComponent<PauseController>();
	}

	private void Start(){
		if(PlayerPrefs.GetInt("musicOn") == 1){
			musicBool = true;
			musicBt.GetComponent<Image>().sprite = musicOn;
		}else{
			musicBool = false;
			musicBt.GetComponent<Image>().sprite = musicOff;
		}
			
		if(PlayerPrefs.GetInt("soundOn") == 1){
			soundBool = true;
			soundBt.GetComponent<Image>().sprite = soundOn;
		}else{
			soundBool = false;
			soundBt.GetComponent<Image>().sprite = soundOff;
		}
	}
	
	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu(){
		if(!player.GetComponent<PlayerController>().GetDeath())
			scriptPause.ChangeState();
		PlayerPrefs.Save();
		SceneManager.LoadScene(0);
	}

	public void Quit(){
		Application.Quit();
	}

	public void SoundClick(){
		if(soundBool){
			soundBt.GetComponent<Image>().sprite = soundOff;
			PlayerPrefs.SetInt("soundOn", 0);
			soundBool =  false;
		}else{
			soundBt.GetComponent<Image>().sprite = soundOn;
			PlayerPrefs.SetInt("soundOn", 1);
			soundBool = true;
		}
	}

	public void MusicClick(){
		if(musicBool){
			musicBt.GetComponent<Image>().sprite = musicOff;
			PlayerPrefs.SetInt("musicOn", 0);
			musicBool = false;
		}else{
			musicBt.GetComponent<Image>().sprite = musicOn;
			PlayerPrefs.SetInt("musicOn", 1);
			musicBool = true;
		}
	}
}
