using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

	/* Situa la preferencia de carga de
	 DataManager frente al resto */
	void Awake () {
		Camera.main.GetComponent<DataManager>().Init();
	}
	
}
