using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField] protected GameObject spawnable;
	[SerializeField] protected GameObject indicator;
	[SerializeField] protected float spawnCooldown;
	[SerializeField] protected float initDelay;
	protected float timer;
	protected GameObject player;
	protected float speedBuff;

	protected void Start(){
		float[] multSpeed = {1.0f, 1.5f, 2.0f};
		speedBuff = multSpeed[Helper.DecryptInt(PlayerPrefs.GetString("speedSelected")) - 1];
		
		Random.InitState((int)System.DateTime.Now.Ticks); 

		timer = spawnCooldown + initDelay;
		player = GameObject.FindWithTag("Player");
	}
	protected void ReloadTimer(){
		float random = Random.Range(0, 1 * spawnCooldown / 3);
		timer=spawnCooldown - random;
	}
	protected void SpawnIndicator(GameObject obj){
		GameObject ind = Instantiate(indicator, transform.position, Quaternion.identity);
		ind.transform.parent = player.transform;
		ind.transform.position = player.transform.position;
		ind.transform.GetChild(0).transform.localPosition = new Vector3(0, Camera.main.orthographicSize * 0.9f, 0);
		ind.GetComponent<OffscreenIndicator>().SetTarget(obj);	
	}
}
