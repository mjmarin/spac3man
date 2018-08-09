using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	[SerializeField] private float requiredTime;
	private float time;
	private int turn;
	private PlayerController player;
	private SpriteRenderer playerSprite;
	private Sprite[] playSprites = new Sprite[4];
	private Sprite[] deathSprites = new Sprite[12];
	private int settedSkin;
	private bool wasDead;
	

	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
	}
	void Start () {

		if(PlayerPrefs.HasKey("settedSkin")){
			settedSkin = Helper.DecryptInt(PlayerPrefs.GetString("settedSkin"));
		}else{
			settedSkin = 0;
		}

		ChargeSkins(settedSkin);

		time = 0;
		turn = 0;
		wasDead = false;
	}
	
	void Update () {
		time = time + Time.deltaTime;
		if(player != null){ /* Se encuentra jugando */
			if(player.GetDeath()){
				if(wasDead == false){
					time = 0;
					turn = 0;
					wasDead = true;
				}
				if(time > requiredTime){
					Dead();
				}
			}else{
				if(time > requiredTime){
					Alive();
				}
			}
		}else{ /* Se encuentra en menú */
			if(time > requiredTime){
				Alive();
			}
		}	
	}

	private void Alive(){
		turn = (turn + 1) % 4;
		playerSprite.sprite = playSprites[turn];
		time = 0;
	}

	private void Dead(){
		if(turn < 12){
			playerSprite.sprite = deathSprites[turn];
			turn ++;
			time = 0;
		}
	}

	public void ChangeSkin(int settedSkin){
		PlayerPrefs.SetString("settedSkin", Helper.EncryptInt(settedSkin));
		
		ChargeSkins(settedSkin);
	}

	private void ChargeSkins(int settedSkin){
		Sprite[] resourcesArray = new Sprite[3];

		resourcesArray = Resources.LoadAll<Sprite>("Sprites/Skins/skin" + settedSkin);
		playSprites[0] = resourcesArray[2];
		playSprites[1] = resourcesArray[1];
		playSprites[2] = resourcesArray[0];
		playSprites[3] = resourcesArray[1];

		if(player != null){ /* Se encuentra jugando */
			deathSprites = Resources.LoadAll<Sprite>("Sprites/Skins/deathSkin" + settedSkin);
		}
	}
}
