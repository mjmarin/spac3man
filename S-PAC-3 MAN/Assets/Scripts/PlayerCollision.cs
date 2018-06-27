using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	[SerializeField] private GameObject gameOver;
	private Animator anim;
	private bool dead;
	void Start(){
		anim = GetComponent<Animator>();
		anim.SetBool("isDead",false);
		
		gameOver.SetActive(false);
	}
	void OnTriggerEnter2D(Collider2D other){
		
		if(other.CompareTag("Item")) {
			Destroy(other.gameObject);
		}else{
			if(other.CompareTag("Enemy")) {
				GetComponent<PlayerController>().isDead = true;
				anim.SetBool("isDead",true);

				gameOver.SetActive(true);
			}
		}
	}
}
