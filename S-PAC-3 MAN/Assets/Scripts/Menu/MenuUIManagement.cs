using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;

public class MenuUIManagement : MonoBehaviour {

	/* Objectos de las distintas fases del menú principal */
	[SerializeField] private GameObject FPSCounter;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private Text moneyCounter;
	[SerializeField] private GameObject shopMenu;
	[SerializeField] private GameObject settingsMenu;
	[SerializeField] private GameObject questsMenu;
	[SerializeField] private GameObject creditsDisplay;
	[SerializeField] private GameObject instructionsDisplay1;
	[SerializeField] private GameObject instructionsDisplay2;
	[SerializeField] private GameObject instructionsDisplay3;
	[SerializeField] private GameObject recordsDisplay;
	[SerializeField] private GameObject googlePlayMenu;

	/* Variables movimiento flechas en menú principal */
	[SerializeField] private GameObject speedSelector;
	[SerializeField] private GameObject modeSelector;
	[SerializeField] private float range;
	[SerializeField] private float speed;
	private const float constantDistanceSS = 242;		// Píxeles entre las opciones de velocidad en 1920 x 1080
	private const float constantDistanceMS = 143.9f;	// Píxeles entre las opciones de modo en 1920 x 1080
	private float startPositionSS;
	private float startPositionMS;
	private int senseSS;
	private int senseMS;

	/* Variables pantalla de tienda */
	[SerializeField] private Transform ItemsPanel;
	[SerializeField] private Image descriptionImage;
	private Text buttonText;
	private Text skinNameText;
	private Text skinDescriptionText;
	private Text skinPriceText;
	private Text moneyCounterShop;
	private AnimationController animatorController;
	
	private int selectedSkin;

	/* Variables pantalla de ajustes */
	[SerializeField] private GameObject checkMusic; 
	[SerializeField] private GameObject checkSound; 
	[SerializeField] private GameObject checkFPS; 

	/* Variables pantalla de misiones */
	[SerializeField] private Text mission1Text; 
	[SerializeField] private Text mission1Reward; 
	[SerializeField] private Text mission1Complete; 
	[SerializeField] private Text mission2Text; 
	[SerializeField] private Text mission2Reward; 
	[SerializeField] private Text mission2Complete; 
	[SerializeField] private Text mission3Text; 
	[SerializeField] private Text mission3Reward; 
	[SerializeField] private Text mission3Complete; 

	/* Variables pantalla de GooglePlay */
	[SerializeField] private Button connectBt;
	private Text connectBtTxt;
	private Image connectBtImg;

	/* Variables cambio color en títulos pantalla de records */

	[SerializeField] private GameObject NormalSubtitle;
	[SerializeField] private GameObject NEOSubtitle;
	[SerializeField] private float speedColor;
	private Vector4 color = new Vector4(0.999f,0.999f,0.001f,1.0f);
	private Vector3 colorSense = new Vector3(-1,-1,1);
	private int colorTurn = 1;

	/* Variable de pantalla actual -> Uso de back nativo de Android */
	private int currentWindow = 0;

	/* Toma de referencias */
	void Awake(){
		PlayGamesPlatform.Activate();
		animatorController = player.GetComponent<AnimationController>();
		buttonText = shopMenu.GetComponentsInChildren<Text>()[0];
		moneyCounterShop = shopMenu.GetComponentsInChildren<Text>()[1];
		skinPriceText = shopMenu.GetComponentsInChildren<Text>()[3];
		skinDescriptionText = shopMenu.GetComponentsInChildren<Text>()[4];
		skinNameText = shopMenu.GetComponentsInChildren<Text>()[5];

		connectBtTxt = connectBt.GetComponentInChildren<Text>();
		connectBtImg = connectBt.GetComponentsInChildren<Image>()[1];
	}

	/* Inicialización */
	void Start(){
		currentWindow = 0;

		senseMS = 1;
		senseSS = 1;
		startPositionSS = speedSelector.transform.localPosition.y;
		startPositionMS = modeSelector.transform.localPosition.x;

		StartArrow(speedSelector, DataManager.GetSelectionSS(), 1);
		StartArrow(modeSelector, DataManager.GetSelectionMS(), -1);
		
		InitializeUI();

		ChangeConnectBt(PlayGamesPlatform.Instance.localUser.authenticated);

		
		mainMenu.SetActive(true);
		shopMenu.SetActive(false);
		settingsMenu.SetActive(false);
		creditsDisplay.SetActive(false);
		questsMenu.SetActive(false);
		instructionsDisplay1.SetActive(false);
		instructionsDisplay2.SetActive(false);
		instructionsDisplay3.SetActive(false);
		googlePlayMenu.SetActive(false);
		recordsDisplay.SetActive(false);
	}
	
	void Update () {
		MoveSelectors();	/* Realizar el efecto de las flechas de selección en menú principal */
		ChangeColor();		/* Cambiar el color de los títulos de menú records */
		BackListener();		/* Respuesta al back nativo de Android */
	}

	/* Respuesta al back nativo de Android.Dependiendo de la pantalla
	en la que se encuentre irá al menú principal o se saldrá de la aplicación */
    private void BackListener(){
        if(Input.GetKeyDown(KeyCode.Escape)){	
			switch(currentWindow){
				case 0:
					Application.Quit();
				break;
				case 1:
					ExitShopBt();
				break;
				case 2:
					ExitSettingsBt();
				break;
				case 3:
					BackCreditsBt();
				break;
				case 4:
					ExitQuestsBt();
				break;
				case 5:
					ExitInstructions1Bt();
				break;
				case 6:
					ExitInstructions2Bt();
				break;
				case 7:
					ExitInstructions3Bt();
				break;
				case 8:
					ExitGooglePlayMenyBt();
				break;
				case 9:
					ExitRecordsBt();
				break;
			}
		}
    }

    /* Inicializar componentes gráficos */
    private void InitializeUI(){

		if(DataManager.GetFPSOn()){	
			checkFPS.SetActive(true);
			FPSCounter.SetActive(true);
		}else{
			checkFPS.SetActive(false);
			FPSCounter.SetActive(false);
		}

		if(DataManager.GetMusicOn()){	
			checkMusic.SetActive(true);
		}else{
			checkMusic.SetActive(false);
		}

		if(DataManager.GetSoundOn()){	
			checkSound.SetActive(true);
		}else{
			checkSound.SetActive(false);
		}

		SetMoneyText(moneyCounter, DataManager.GetMoney());
		SetMoneyText(moneyCounterShop, DataManager.GetMoney());

		InitShop();

		OnItemSelect(DataManager.GetSettedSkin());

		SetRecords();
	}

	/* Situar en el texto la cantidad de dinero actual */
	private void SetMoneyText(Text moneyText, ulong money){
		moneyText.text = money.ToString();
	}

	/* Añadir listeners al evento OnClick de los botones de tienda */
	private void InitShop(){
		int i = 0;
		foreach(Transform t in ItemsPanel){
			int currentIndex = i; 							// Para solucionar un error de OnItemSelect (Siempre seleccionaría el último integer con i)
			Button b = t.GetComponent<Button>();
			b.onClick.AddListener(() => OnItemSelect(currentIndex));
			i++;
		}
	}

	/*------------------ Mover las flechas de selección ----------------------*/

	/* Situar las flechas en la posición de la última elección
		option = 1 flecha de Speed Selector
		option = -1 flecha de Mode Selector */
	private void StartArrow(GameObject obj, int selection, int option){
		float change;
		Vector3 vect;
		if(option == 1){
			change = constantDistanceSS;
			vect = Vector3.right;
		}else{
			change = constantDistanceMS;
			vect = Vector3.up;
		}
		obj.transform.localPosition += vect * change * (selection - 1) * option;
	}

	/* Realizar el efecto de las flechas de selección en menú principal */
	private void MoveSelectors(){
		senseSS = Sense(speedSelector.transform.localPosition.y, startPositionSS, senseSS);
		speedSelector.transform.localPosition += Vector3.up * speed * senseSS;

		senseMS = Sense(modeSelector.transform.localPosition.x, startPositionMS, senseMS);
		modeSelector.transform.localPosition += Vector3.right * speed * senseMS;
	}

	/* Devuelve el sentido en el que se deben mover las flechas de selección */
	private int Sense(float localPosition, float startPosition, int sense){
		if((localPosition > startPosition + range && sense == 1) || (localPosition < startPosition - range && sense == -1)){
			return -sense;
		}else{
			return sense;
		}
	}
	/*----------------------------------------------------------------------*/

	/*---------------------- Funciones de recordsDisplay -------------------*/

	/* Función para escribir las puntuaciones más altas en los textos correspondientes */
	private void SetRecords(){
		Text[] normalTexts = recordsDisplay.GetComponentsInChildren<Text>()[1].GetComponentsInChildren<Text>();
		Text[] NEOTexts = recordsDisplay.GetComponentsInChildren<Text>()[8].GetComponentsInChildren<Text>();

		normalTexts[2].text = DataManager.GetRecords(0).ToString() + " Pts";

		normalTexts[4].text = DataManager.GetRecords(1).ToString() + " Pts";

		normalTexts[6].text = DataManager.GetRecords(2).ToString() + " Pts";

		NEOTexts[2].text = DataManager.GetRecords(3).ToString() + " Pts";

		NEOTexts[4].text = DataManager.GetRecords(4).ToString() + " Pts";

		NEOTexts[6].text = DataManager.GetRecords(5).ToString() + " Pts";
	}

	/* Función para cambiar el color de los rótulos de records constantemente */
	private void ChangeColor(){

		if(color[colorTurn] > 1.0f){
			color[colorTurn] = 1.0f;
			colorSense[colorTurn] = -colorSense[colorTurn];
			colorTurn = (colorTurn + 1) % 3;
		}else if(color[colorTurn] < 0.0f){
			color[colorTurn] = 0.0f;
			colorSense[colorTurn] = -colorSense[colorTurn];
			colorTurn = (colorTurn + 1) % 3;
		}

		NormalSubtitle.GetComponent<Image>().color = color;
		NEOSubtitle.GetComponent<Image>().color = color;

		color[colorTurn] = color[colorTurn] + speedColor * colorSense[colorTurn];
	}

	/*----------------------------------------------------------------------*/

	/*---------------------- Funciones aplicadas a botones -----------------*/

	/* Carga la escena de juego */
	public void StartPlay(){
		SceneManager.LoadScene(1);
	}

	/* Inicio click de botones de tienda */

	/* Abre la pantalla de tienda */
	public void ShopBt(){
		mainMenu.SetActive(false);
		shopMenu.SetActive(true);
		currentWindow = 1;
	}

	/* Cierra la pantalla de tienda */
	public void ExitShopBt(){
		mainMenu.SetActive(true);
		shopMenu.SetActive(false);
		currentWindow = 0;
	}

	/* Compra o utiliza un personaje de la tienda */
	public void BuySetItem(){
		if(DataManager.GetOwnedSkin(selectedSkin)){
			animatorController.ChangeSkin(selectedSkin);
		}else{
			float price;
			price = DataManager.GetSkinPrices(selectedSkin);
			if(DataManager.GetMoney() >= price){
				
				DataManager.SetOwnedSkin(selectedSkin, true);
				DataManager.SetMoney(DataManager.GetMoney() - price);

				SetMoneyText(moneyCounter, DataManager.GetMoney());
				SetMoneyText(moneyCounterShop, DataManager.GetMoney());

				buttonText.text = "Set";
				skinPriceText.text = "Bought";

				if(Social.localUser.authenticated){
					GooglePlayManager.CheckAchievements();
				}
			}
		}
	}

	/* Situa la descripción del personaje seleccionado */
	private void OnItemSelect(int index){

		selectedSkin = index;

		skinNameText.text = StoryText.GetNameText(index);
		skinDescriptionText.text = StoryText.GetDescriptionText(index);

		if(DataManager.GetOwnedSkin(index)){
			buttonText.text = "Set";
			skinPriceText.text = "bought";
		}else{
			buttonText.text = "Buy";
			skinPriceText.text = DataManager.GetSkinPrices(index).ToString();
		}

		descriptionImage.sprite = shopMenu.GetComponentsInChildren<Button>()[index + 1].GetComponent<Image>().sprite;
		
	}
	/* Fin click de botones de tienda */

	/* Inicio click de botones de selección de velocidad */

	/* Modifica la selección de velocidad a normal y sitúa la flecha de selección encima del botón normal */
	public void SSNormal(){
		if(DataManager.GetSelectionSS() != 1){
			speedSelector.transform.localPosition += Vector3.right * constantDistanceSS * (1 - DataManager.GetSelectionSS());
			DataManager.SetSelectionSS(1);
		}
	}

	/* Modifica la selección de velocidad a speedRacer y sitúa la flecha de selección encima del botón speedRacer */
	public void SSSpeedRacer(){
		if(DataManager.GetSelectionSS() != 2){
			speedSelector.transform.localPosition += Vector3.right * constantDistanceSS * (2 - DataManager.GetSelectionSS());
			DataManager.SetSelectionSS(2);
		}
	}

	/* Modifica la selección de velocidad a tryhard y sitúa la flecha de selección encima del botón tryhard */
	public void SSTryhard(){
		if(DataManager.GetSelectionSS() != 3){
			speedSelector.transform.localPosition += Vector3.right * constantDistanceSS * (3 - DataManager.GetSelectionSS());
			DataManager.SetSelectionSS(3);
		}
	}
	/* Fin click de botones de selección de velocidad */

	/* Inicio click de botones de selección de modo */

	/* Modifica la selección de modo a normal y sitúa la flecha de selección encima del botón de modo normal */
	public void MSNormal(){
		if(DataManager.GetSelectionMS() != 1){
			modeSelector.transform.localPosition += Vector3.up * constantDistanceMS;
			DataManager.SetSelectionMS(1);
		}
	}

	/* Modifica la selección de modo a notEnoughOxygen y sitúa la flecha de selección encima del botón de modo notEnoughOxygen */
	public void MSNotEnoughOxygen(){
		if(DataManager.GetSelectionMS() != 2){
			modeSelector.transform.localPosition += Vector3.up * constantDistanceMS * -1;
			DataManager.SetSelectionMS(2);
		}
	}

	/* Inicio click de botones de selección de modo */

	/* Inicio click de ajustes */

	/* Abre la pantalla de ajustes */
	public void SettingsBt(){
		mainMenu.SetActive(false);
		player.SetActive(false);
		settingsMenu.SetActive(true);
		currentWindow = 2;
	}

	/* Cierra la pantalla de ajustes */
	public void ExitSettingsBt(){
		mainMenu.SetActive(true);
		player.SetActive(true);
		settingsMenu.SetActive(false);
		currentWindow = 0;
	}

	/* Activa/desactiva la música y añade/quita el checkmark */
	public void MusicBt(){
		if(DataManager.GetMusicOn()){
			checkMusic.SetActive(true);
		}else{
			checkMusic.SetActive(false);
		}
	}

	/* Activa/desactiva los efectos de sonido y añade/quita el checkmark */
	public void SoundBt(){
		if(DataManager.GetSoundOn()){
			checkSound.SetActive(true);
		}else{
			checkSound.SetActive(false);
		}
	}

	/* Activa/desactiva el amrcador de frames por segundo y añade/quita el checkmark */
	public void FPSBt(){
		bool FPSOn;
		if(DataManager.GetFPSOn()){
			FPSOn = false;
		}else{
			FPSOn =  true;
		}
		DataManager.SetFPSOn(FPSOn);

		FPSCounter.SetActive(FPSOn);
		checkFPS.SetActive(FPSOn);
	}

	/* Abre la pantalla de créditos */
	public void CreditsBt(){
		settingsMenu.SetActive(false);
		creditsDisplay.SetActive(true);
		currentWindow = 3;
	}

	/* Cierra la pantalla de créditos */
	public void BackCreditsBt(){
		creditsDisplay.SetActive(false);
		settingsMenu.SetActive(true);
		currentWindow = 2;
	}

	/* Fin click de ajustes */

	/* Inicio click de misiones */

	/* Abre la pantalla de misiones, carga las misiones activas y sitúa su descripción en los textos de la pantalla */
	public void QuestsBt(){
		mainMenu.SetActive(false);
		player.SetActive(false);
		questsMenu.SetActive(true);
		currentWindow = 4;

		Quest[] quests = QuestLoader.GetActiveQuests();

		mission1Text.text = "Mission: " + quests[0].description;
		mission1Reward.text = "Reward: " + quests[0].reward.ToString();
		if(DataManager.GetMissionCompleted(0)){
			mission1Complete.gameObject.SetActive(true);
		}else{
			mission1Complete.gameObject.SetActive(false);
		}
			
		mission2Text.text = "Mission: " + quests[1].description;
		mission2Reward.text = "Reward: " + quests[1].reward.ToString();
		if(DataManager.GetMissionCompleted(1)){
			mission2Complete.gameObject.SetActive(true);
		}else{
			mission2Complete.gameObject.SetActive(false);
		}
		
		mission3Text.text = "Mission: " + quests[2].description;
		mission3Reward.text = "Reward: " + quests[2].reward.ToString();
		if(DataManager.GetMissionCompleted(2)){
			mission3Complete.gameObject.SetActive(true);
		}else{
			mission3Complete.gameObject.SetActive(false);
		}

	}

	/* Cierra la pantalla de misiones */
	public void ExitQuestsBt(){
		mainMenu.SetActive(true);
		player.SetActive(true);
		questsMenu.SetActive(false);
		currentWindow = 0;

	}
	/* Fin click de misiones */

	/* Inicio click de instrucciones */

	/* Abre la pantalla de instrucciones */
	public void InstructionsBt(){
		mainMenu.SetActive(false);
		instructionsDisplay1.SetActive(true);
		currentWindow = 5;
	}

	/* Cierra la pantalla de instrucciones */
	public void ExitInstructions1Bt(){
		instructionsDisplay1.SetActive(false);
		mainMenu.SetActive(true);
		currentWindow = 0;
	}

	/* Cierra la pantalla de instrucciones */
	public void ExitInstructions2Bt(){
		instructionsDisplay2.SetActive(false);
		mainMenu.SetActive(true);
		currentWindow = 0;
	}

	/* Cierra la pantalla de instrucciones */
	public void ExitInstructions3Bt(){
		instructionsDisplay3.SetActive(false);
		mainMenu.SetActive(true);
		currentWindow = 0;
	}

	/* Pasa a la siguiente pantalla de instrucciones */
	public void ContinueInstruction1Bt(){
		instructionsDisplay1.SetActive(false);
		instructionsDisplay2.SetActive(true);
		currentWindow = 6;
	}

	/* Pasa a la siguiente pantalla de instrucciones */
	public void ContinueInstruction2Bt(){
		instructionsDisplay2.SetActive(false);
		instructionsDisplay3.SetActive(true);
		currentWindow = 7;
	}

	/* Vuelve a la anterior pantalla de instrucciones */
	public void BackInstruction2Bt(){
		instructionsDisplay2.SetActive(false);
		instructionsDisplay1.SetActive(true);
		currentWindow = 5;
	}

	/* Vuelve a la anterior pantalla de instrucciones */
	public void BackInstruction3Bt(){
		instructionsDisplay3.SetActive(false);
		instructionsDisplay2.SetActive(true);
		currentWindow = 6;
	}
	/* Fin click de instrucciones */

	/* Inicio click de GooglePlay */

	/* Abre la pantalla de GooglePlay */
	public void GooglePlayMenuBt(){
		googlePlayMenu.SetActive(true);
		mainMenu.SetActive(false);
		currentWindow = 8;
	}

	/* Cierra la pantalla de GooglePlay */
	public void ExitGooglePlayMenyBt(){
		googlePlayMenu.SetActive(false);
		mainMenu.SetActive(true);
		currentWindow = 0;
	}

	/* Realiza la conexión con GooglePlay */
	public void ConnectBt(){
		if(Social.localUser.authenticated){
			PlayGamesPlatform.Instance.SignOut();
			ChangeConnectBt(false);
		}else{
			Social.localUser.Authenticate((bool success) => 
			{
				if(Social.localUser.authenticated){
					ChangeConnectBt(true);
					GooglePlayManager.CheckAchievements();
				}
			});
		}
	}

	/* Modifica el teto del botón de conexión así como 
	su marca dependiendo si el usuario esta conectado o no  */
	private void ChangeConnectBt(bool authenticated){
		if(authenticated){
			connectBtTxt.text = "Disconnect";
			connectBtImg.color = new Color32(0,255,0,255);
		}else{
			connectBtTxt.text = "Connect";
			connectBtImg.color = new Color32(255,0,0,255);
		}
	}

	/* Despliega la interfaz de usuario de GooglePlay para los logros */
	public void AchivementsBt(){
		if(Social.localUser.authenticated){
			Social.ShowAchievementsUI();
		}
	}

	/* Despliega la interfaz de usuario de GooglePlay para los marcadores online */
	public void LeaderboardsBt(){
		if(Social.localUser.authenticated){
			Social.ShowLeaderboardUI();
		}
	}
	/* Fin click de GooglePlay */

	/* Inicio click de records */

	/* Abre la pantalla de records locales */
	public void RecordsBt(){
		mainMenu.SetActive(false);
		recordsDisplay.SetActive(true);
		currentWindow = 9;
	}

	/* Cierra la pantalla de records locales */
	public void ExitRecordsBt(){
		recordsDisplay.SetActive(false);
		mainMenu.SetActive(true);
		currentWindow = 0;
	}
	/* Fin click de records */

}
