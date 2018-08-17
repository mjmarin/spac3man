using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : SpawnManager {

	[SerializeField] private GameObject enemyParent;
	[SerializeField] private float bossTime;
	private Timer timerScript;
	private bool boss = false;
	private bool bossRespawned = false;

	void Awake(){
		timerScript = Camera.main.GetComponent<Timer>();
	}

	void Update () {
		if(boss == false){
			timer -= Time.deltaTime;
			if(timer < 0 && player.GetComponent<PlayerController>().GetDeath() == false){

				/* Spawnear un fantasma */
				spawnEnemy();
				IncreaseObjectCount(true);

				/* Eliminar un fantasma */
				if(objectCount > maxObjectCount){
					IncreaseObjectCount(false);
					enemyParent.transform.GetChild(0).GetComponent<EnemyIA>().disappearGhost();
				}

				if(timerScript.GetTime() > bossTime){
					boss = true;
				}
			}
		}else{
			if(bossRespawned == false){
				/* Quitar todos los otros fantasmas */
				int enemyCount = enemyParent.transform.childCount;
				for (int i=0; i < enemyCount; i++){
					enemyParent.transform.GetChild(i).GetComponent<EnemyIA>().disappearGhost();
				}

				/* Hacerlo más grande */
				spawnable.transform.localScale = new Vector3(9,9,1);

				/* Spawn fantasma grande */
				spawnEnemy();

				/* Vuelta a normalidad -> Es el mismo recurso y se queda permanente */
				spawnable.transform.localScale = new Vector3(1,1,1);

				bossRespawned = true;
			}
		}
	}

	private void spawnEnemy(){
		ReloadTimer();
		/* Fantasmas salen con orientación al jugador */
		Vector3 dir=player.transform.position-transform.position;
		dir.Normalize();
		float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;

		GameObject obj = Instantiate(spawnable, transform.position, Quaternion.Euler(0,0,-angle), enemyParent.transform);

		SpawnIndicator(obj);
		
	}

}
