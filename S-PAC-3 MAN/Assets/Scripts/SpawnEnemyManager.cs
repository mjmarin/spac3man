using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour {

	[SerializeField] private GameObject spawnable;
	[SerializeField] private GameObject indicator;
	[SerializeField] private float spawnCooldown;
	[SerializeField] private float InitDelay;

	private float timer;
	private GameObject player;

	void Start(){
		Random.InitState((int)System.DateTime.Now.Ticks); 
		timer = spawnCooldown + InitDelay;
		player = GameObject.Find("Pacman");
	}


	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0){
			float random = Random.Range(0,2 * spawnCooldown / 3);
			/* Fantasmas salen con orientación al jugador */
			Vector3 dir=player.transform.position-transform.position;
			dir.Normalize();
			float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;

			GameObject obj = Instantiate(spawnable, transform.position, Quaternion.Euler(0,0,-angle));

			GameObject ind = Instantiate(indicator, transform.position, Quaternion.identity);
			ind.transform.parent = player.transform;
			ind.transform.position = player.transform.position;
			ind.transform.GetChild(0).transform.localPosition = new Vector3(0, Camera.main.orthographicSize * 0.9f, 0);
			ind.GetComponent<OffscreenIndicator>().target = obj;

			timer=spawnCooldown - random;
		}
	}
}
