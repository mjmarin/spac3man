using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	private Animator anim;
	private bool death;
	private float screenWidth;
	private int pickUps;
	private PauseController scriptPause;

	void Start(){
		anim = GetComponent<Animator>();
		screenWidth = Screen.width;
		death = false;
		anim.SetBool("isDead",false);
		pickUps = 0;
		scriptPause = GameObject.Find("Canvas").GetComponent<PauseController>();
	}

	void Update () {
		# if UNITY_EDITOR
			transform.Rotate(new Vector3(0,0,-Input.GetAxis("Horizontal") * rotationSpeed));		//Para probar en PC
		#else
			int direction = 0;
			if(GetDeath() == false && scriptPause.GetPaused() == false ){		// Quiero que al morir siga hacia delante pero ya no se pueda controlar
				if (Input.touchCount > 0){
					if (Input.GetTouch (0).position.x > screenWidth / 2) {					//Move Right
						direction = -1;
					}else{																	//Move Left
						direction = 1;
					}
				}
			}
			transform.Rotate(new Vector3(0,0,direction * rotationSpeed));
		#endif
		transform.position += transform.up * Time.deltaTime * movementSpeed;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Enemy")) {
			GetComponent<PlayerController>().SetDeath(true);
			anim.SetBool("isDead",true);
			int money = PlayerPrefs.GetInt("money");
			PlayerPrefs.SetInt("money", pickUps + money);
		}else{
			if(GetDeath() == false)
				pickUps++;
		}
	}

	public int GetPickUps(){
		return pickUps;
	}

	public bool GetDeath(){
		return death;
	}
	private void SetDeath(bool boolean){
		death = boolean;
	}
}
