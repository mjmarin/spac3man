using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	[SerializeField] private Sprite soundOnSprite;
	[SerializeField] private Sprite soundOffSprite;
	[SerializeField] private Button soundBt;
	[SerializeField] private Sprite musicOnSprite;
	[SerializeField] private Sprite musicOffSprite;
	[SerializeField] private Button musicBt;
	[SerializeField] private GameObject player;
	private PauseController scriptPause;

	private void Awake(){
		scriptPause = this.gameObject.GetComponent<PauseController>();
	}

	private void Start(){
		if(DataManager.GetMusicOn()){
			musicBt.GetComponent<Image>().sprite = musicOnSprite;
		}else{
			musicBt.GetComponent<Image>().sprite = musicOffSprite;
		}
		
		if(DataManager.GetSoundOn()){
			soundBt.GetComponent<Image>().sprite = soundOnSprite;
		}else{
			soundBt.GetComponent<Image>().sprite = soundOffSprite;
		}
	}
	
	public void ReloadBt(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MenuBt(){
		if(!player.GetComponent<PlayerController>().GetDeath())
			scriptPause.ChangeState();
		SceneManager.LoadScene(0);
	}

	public void QuitBt(){
		Application.Quit();
	}

	public void SoundBt(){
		if(DataManager.GetSoundOn()){
			soundBt.GetComponent<Image>().sprite = soundOffSprite;
		}else{
			soundBt.GetComponent<Image>().sprite = soundOnSprite;
		}
	}

	public void MusicBt(){
		if(DataManager.GetMusicOn()){
			musicBt.GetComponent<Image>().sprite = musicOffSprite;
		}else{
			musicBt.GetComponent<Image>().sprite = musicOnSprite;
		}
	}
}
