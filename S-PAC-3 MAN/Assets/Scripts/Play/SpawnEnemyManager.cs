using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : SpawnManager {

	/* Referencia a que incluye a todos los objetos enemigos */
	[SerializeField] private GameObject enemyParent;

	/* Variable que guarda el tiempo necesario para que aparezca el jefe enemigo */
	[SerializeField] private float bossTime;

	/* Referencia a script de tiempo */
	private Timer timerScript;

	/* Variables de jefe */
	private bool boss = false;
	private bool bossRespawned = false;

	/* Inicialización de referencias */
	void Awake(){
		timerScript = Camera.main.GetComponent<Timer>();
	}

	/* Se encarga de instanciar enemigos */
	void Update () {
		if(boss == false){
			ghostRespawn();
			if(timerScript.GetTime() > bossTime){
				boss = true;
			}
		}else{
			if(bossRespawned == false){
				/* Quitar todos los otros fantasmas */
				int enemyCount = enemyParent.transform.childCount;
				for (int i=0; i < enemyCount; i++){
					enemyParent.transform.GetChild(i).GetComponent<EnemyIA>().disappearGhost();
				}

				/* Hacerlo más grande */
				spawnable.transform.localScale = new Vector3(4,4,1);

				/* Spawn fantasma grande */
				spawnEnemy();

				/* Vuelta a normalidad -> Es el mismo recurso y se queda permanente */
				spawnable.transform.localScale = new Vector3(1,1,1);

				bossRespawned = true;

				if(Social.localUser.authenticated){
					GooglePlayManager.SetOnlineAchievement("CgkInJLw2NEXEAIQEg");
				}
			}else{
				ghostRespawn();
			}
		}
	}

	/* Función que gestiona la instanciación de un enemigo */
	private void spawnEnemy(){
		ReloadTimer();
		/* Fantasmas salen con orientación al jugador */
		Vector3 dir=player.transform.position-transform.position;
		dir.Normalize();
		float angle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg;

		GameObject obj = Instantiate(spawnable, transform.position, Quaternion.Euler(0,0,-angle), enemyParent.transform);

		SpawnIndicator(obj);
		
	}

	/* Función que gestiona realiza las acciones consecuentes a la instanciación de un enemigo */
	private void ghostRespawn(){
		timer -= Time.deltaTime;
		if(timer < 0 && player.GetComponent<PlayerController>().GetDeath() == false){

			/* Spawnear un fantasma */
			spawnEnemy();
			IncreaseObjectCount(true);

			/* Eliminar un fantasma */
			if(objectCount > maxObjectCount){
				IncreaseObjectCount(false);
				if(bossRespawned){
					enemyParent.transform.GetChild(1).GetComponent<EnemyIA>().disappearGhost();
				}else{
					enemyParent.transform.GetChild(0).GetComponent<EnemyIA>().disappearGhost();
				}
			}
		}
	}
}
