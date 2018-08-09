using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemManager : SpawnManager {

	[SerializeField] private int maxItem;
	private int itemCount;
	void Update () {
		if(itemCount < maxItem && player.GetComponent<PlayerController>().GetDeath() == false){
			timer -= Time.deltaTime;
			if(timer < 0){
				ReloadTimer();	
				SpawnIndicator(Instantiate(spawnable, transform.position * speedBuff, Quaternion.identity));	/* A más velocidad spawnean más lejos */
				IncreaseItemCount(true);
			}
			
		}
	}

	public void IncreaseItemCount(bool create){
		if(create){
			itemCount++;
		}else{
			itemCount--;
		}
	}

}
