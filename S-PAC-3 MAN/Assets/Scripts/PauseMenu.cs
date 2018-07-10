using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public void Reload(){
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu(){
		SceneManager.LoadSceneAsync(0);
	}

	public void Quit(){
		Application.Quit();
	}
}
