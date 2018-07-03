using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenIndicator : MonoBehaviour {
	
	public GameObject target;
	private Camera cam;
	private GameObject player;
	private GameObject child;

	void Start(){
		cam = Camera.main;
		player = GameObject.Find("Pacman");
	}
	void Update () {
		if(target!=null){
			Vector3 distance = target.transform.position - transform.position;
			Vector3 visible = cam.WorldToViewportPoint(target.transform.position);

			if((visible.x > 0 && visible.x < 1 && visible.y > 0 && visible.y < 1 && visible.z > 0) 
			|| player.GetComponent<PlayerController>().isDead == true){ //Si el objeto es visible por la cámara o player muerto
				SetChildrenActive(false);
			}else{
				SetChildrenActive(true);
				float angle = Mathf.Atan2(distance.x,distance.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(-angle,Vector3.forward);
			}
		}
	}

	void SetChildrenActive(bool boolean){
		foreach (Transform children in transform){
			children.gameObject.SetActive(boolean);
		}
	}
}
