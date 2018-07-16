using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	[SerializeField] private GameObject player;
	private PauseController scriptPause;
	void Start(){
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
}
