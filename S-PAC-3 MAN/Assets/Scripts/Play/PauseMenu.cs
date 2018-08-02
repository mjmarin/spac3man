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
		if(Helper.DecryptInt(PlayerPrefs.GetString("musicOn")) == 1){
			musicBool = true;
			musicBt.GetComponent<Image>().sprite = musicOn;
		}else{
			musicBool = false;
			musicBt.GetComponent<Image>().sprite = musicOff;
		}
		
		if(Helper.DecryptInt(PlayerPrefs.GetString("soundOn")) == 1){
			soundBool = true;
			soundBt.GetComponent<Image>().sprite = soundOn;
		}else{
			soundBool = false;
			soundBt.GetComponent<Image>().sprite = soundOff;
		}
	}
	
	public void ReloadBt(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MenuBt(){
		if(!player.GetComponent<PlayerController>().GetDeath())
			scriptPause.ChangeState();
		PlayerPrefs.Save();
		SceneManager.LoadScene(0);
	}

	public void QuitBt(){
		Application.Quit();
	}

	public void SoundBt(){
		if(soundBool){
			soundBt.GetComponent<Image>().sprite = soundOff;
			PlayerPrefs.SetString("soundOn", Helper.EncryptInt(0));
			soundBool =  false;
		}else{
			soundBt.GetComponent<Image>().sprite = soundOn;
			PlayerPrefs.SetString("soundOn", Helper.EncryptInt(1));
			soundBool = true;
		}
	}

	public void MusicBt(){
		if(musicBool){
			musicBt.GetComponent<Image>().sprite = musicOff;
			PlayerPrefs.SetString("musicOn", Helper.EncryptInt(0));
			musicBool = false;
		}else{
			musicBt.GetComponent<Image>().sprite = musicOn;
			PlayerPrefs.SetString("musicOn", Helper.EncryptInt(1));
			musicBool = true;
		}
	}
}
