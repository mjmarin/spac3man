using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	[SerializeField] protected GameObject spawnable;
	[SerializeField] protected GameObject indicator;
	[SerializeField] protected float spawnCooldown;
	[SerializeField] protected float initDelay;
	[SerializeField] protected int maxObjectCount;
	protected int objectCount;
	protected float timer;
	protected GameObject player;
	protected float speedBuff;


	protected void Start(){		
		Random.InitState((int)System.DateTime.Now.Ticks); 

		timer = spawnCooldown + initDelay;
		player = GameObject.FindWithTag("Player");

		float[] multSpeed = {0.0f, 0.5f, 1.0f};
		speedBuff = multSpeed[DataManager.GetSelectionSS() - 1];
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

	public void IncreaseObjectCount(bool create){
		if(create){
			objectCount++;
		}else{
			objectCount--;
		}
	}
}
