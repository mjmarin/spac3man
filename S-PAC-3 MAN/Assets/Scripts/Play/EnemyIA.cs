using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour {

	[SerializeField] private float rotationSpeed;
	[SerializeField] private float movementSpeed;
	private GameObject player;
	private float speedMode;
	private bool disappearing = false;
	private float alpha = 1.0f;
	[SerializeField] private float alphaProgression;

	void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};
		player = GameObject.FindWithTag("Player");
		speedMode = multSpeed[DataManager.GetSelectionSS() - 1];
	}
	void Update () {
		if(player!=null){
			Vector3 dir=player.transform.position-transform.position;
			dir.Normalize();
			
			float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler(0,0,-angle);
			transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,rotationSpeed * speedMode);

			transform.position += transform.up * movementSpeed * Time.deltaTime * speedMode;
		}

		if(disappearing){
			this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, alpha);
			if(alpha > 0){
				alpha -= alphaProgression * Time.deltaTime;
			}else{
				Destroy(this.gameObject);
			}
		}
	}

	public void disappearGhost(){
		disappearing = true;
	}
}
