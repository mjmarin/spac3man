using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemManager : SpawnManager {

	[SerializeField] private int maxItem;
	private int ItemCount;
	void Update () {
		if(GetItemCount() < maxItem && player.GetComponent<PlayerController>().GetDeath() == false ){
			timer -= Time.deltaTime;
			if(timer < 0){
				ReloadTimer();	
				SpawnIndicator(Instantiate(spawnable, transform.position, Quaternion.identity));
				IncreaseItemCount(true);
			}
			
		}
	}

	public void IncreaseItemCount(bool create){
		if(create){
			ItemCount++;
		}else{
			ItemCount--;
		}
	}

	public int GetItemCount(){
		return ItemCount;
	}
}
