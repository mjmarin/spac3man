using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour {

	static private int speedSelected;
	static private int modeSelected;
	static private int skinSelected;
	static private bool musicOn;
	static private bool soundOn;
	static private bool FPSOn;

	void Start(){
		speedSelected = 1;
		modeSelected = 1;
		skinSelected = 1;
		musicOn = true;
		soundOn = true;
		FPSOn = false;
	}	

	public void SetSpeedSelected(int option){
		speedSelected = option;
	}
	public int GetSpeedSelected(){
		return speedSelected;
	}

	public void SetModeSelected(int option){
		modeSelected = option;
	}
	public int GetModeSelected(){
		return modeSelected;
	}

	public void SetSkinSelected(int option){
		skinSelected = option;
	}
	public int GetSkinSelected(){
		return skinSelected;
	}

	public void SetMusicOn(bool option){
		musicOn = option;
	}
	public bool GetMusicOn(){
		return musicOn;
	}

	public void SetSoundOn(bool option){
		soundOn = option;
	}
	public bool GetSoundOn(){
		return soundOn;
	}

	public void SetFPSOn(bool option){
		FPSOn = option;
	}
	public bool GetFPSOn(){
		return FPSOn;
	}

}
