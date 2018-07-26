using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour {

	public void DeletePlayPrefs(){
		PlayerPrefs.DeleteAll();
	}
}
