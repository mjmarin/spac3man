using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenIndicator : MonoBehaviour {
	
	private GameObject target;
	private Camera cam;
	private GameObject player;

	void Start(){
		cam = Camera.main;
		player = GameObject.Find("Pacman");
		PointToTarget();
	}
	void Update () {
		PointToTarget();
	}

	void PointToTarget(){
		if(target == null){
			Destroy(this.gameObject);
		}else{
			Vector3 distance = target.transform.position - transform.position;
			Vector3 visible = cam.WorldToViewportPoint(target.transform.position);

			if((visible.x > 0 && visible.x < 1 && visible.y > 0 && visible.y < 1 && visible.z > 0) 
			|| player.GetComponent<PlayerController>().GetDeath()){ //Si el objeto es visible por la cámara o player muerto
				this.transform.GetChild(0).gameObject.SetActive(false);
			}else{
				this.transform.GetChild(0).gameObject.SetActive(true);
				float angle = Mathf.Atan2(distance.x,distance.y) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(-angle,Vector3.forward);
			}
		}
	}

	public void SetTarget(GameObject gameObj){
		target = gameObj;
	}
}
