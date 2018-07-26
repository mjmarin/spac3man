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

	public void Start(){
		Random.InitState((int)System.DateTime.Now.Ticks); 
		timer = spawnCooldown + initDelay;
		player = GameObject.Find("Pacman");
	}
	public void ReloadTimer(){
		float random = Random.Range(0, 2 * spawnCooldown / 3);
		timer=spawnCooldown - random;
	}
	public void SpawnIndicator(GameObject obj){
		GameObject ind = Instantiate(indicator, transform.position, Quaternion.identity);
		ind.transform.parent = player.transform;
		ind.transform.position = player.transform.position;
		ind.transform.GetChild(0).transform.localPosition = new Vector3(0, Camera.main.orthographicSize * 0.9f, 0);
		ind.GetComponent<OffscreenIndicator>().SetTarget(obj);	
	}
}
