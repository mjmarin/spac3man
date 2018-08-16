using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

	void Awake () {
		Camera.main.GetComponent<DataManager>().Init();
	}
	
}
