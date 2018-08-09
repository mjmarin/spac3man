using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManagement : MonoBehaviour {

	/* Objectos de las distintas fases del menú principal */
	[SerializeField] private GameObject FPSCounter;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private Text moneyCounter;
	[SerializeField] private GameObject shopMenu;
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject questsMenu;
	[SerializeField] private GameObject creditsDisplay;
	[SerializeField] private GameObject instructionsDisplay1;
	[SerializeField] private GameObject instructionsDisplay2;
	[SerializeField] private GameObject instructionsDisplay3;
	[SerializeField] private GameObject recordsDisplay;

	/* Variables movimiento flechas en menu principal */
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
	private int selectionSS;
	private int selectionMS;

	/* Variables shop menu */
	[SerializeField] private Transform ItemsPanel;
	[SerializeField] private Image descriptionImage;
	private Text buttonText;
	private Text skinNameText;
	private Text skinDescriptionText;
	private Text skinPriceText;
	private Text moneyCounterShop;
	private AnimationController animatorController;
	
	private int selectedSkin;
	private int settedSkin;
	private int money;
	private bool[] ownedSkin = new bool[12];
	private int [] skinPrices = { 0, -20, 30, 35, 40, 50, 70, 85, 90, 100, 120, 150};

	/* Variables option menu */
	[SerializeField] private GameObject checkMusic;
	[SerializeField] private GameObject checkSound;
	[SerializeField] private GameObject checkFPS;
	private bool FPSOn;

	/* Variables quest menu */
	[SerializeField] private Text mission1Text;
	[SerializeField] private Text mission1Reward;
	[SerializeField] private Text mission1Complete;
	[SerializeField] private Text mission2Text;
	[SerializeField] private Text mission2Reward;
	[SerializeField] private Text mission2Complete;
	[SerializeField] private Text mission3Text;
	[SerializeField] private Text mission3Reward;
	[SerializeField] private Text mission3Complete;

	/* Variables cambio color records menu */

	[SerializeField] private GameObject NormalSubtitle;
	[SerializeField] private GameObject NEOSubtitle;
	[SerializeField] private float speedColor;
	private Vector4 color = new Vector4(0.999f,0.999f,0.001f,1.0f);
	private Vector3 colorSense = new Vector3(-1,-1,1);
	private int colorTurn = 1;

	void Awake(){
		animatorController = player.GetComponent<AnimationController>();
		buttonText = shopMenu.GetComponentsInChildren<Text>()[0];
		moneyCounterShop = shopMenu.GetComponentsInChildren<Text>()[1];
		skinPriceText = shopMenu.GetComponentsInChildren<Text>()[3];
		skinDescriptionText = shopMenu.GetComponentsInChildren<Text>()[4];
		skinNameText = shopMenu.GetComponentsInChildren<Text>()[5];
	}

	void Start(){
		senseMS = 1;
		senseSS = 1;
		startPositionSS = speedSelector.transform.localPosition.y;
		startPositionMS = modeSelector.transform.localPosition.x;
		
		InitializePlayerData();

		StartArrow(speedSelector, selectionSS, 1);
		StartArrow(modeSelector, selectionMS, -1);

		SetRecords();

		mainMenu.SetActive(true);
		shopMenu.SetActive(false);
		optionsMenu.SetActive(false);
		creditsDisplay.SetActive(false);
		questsMenu.SetActive(false);
		instructionsDisplay1.SetActive(false);
		instructionsDisplay2.SetActive(false);
		instructionsDisplay3.SetActive(false);
		recordsDisplay.SetActive(false);
	}
	
	void Update () {
		MoveSelectors();
		ChangeColor();
	}

	/* Inicializar Datos */
	private void InitializePlayerData(){

		/* Si no hay datos previos, inicializa a 1, sino desencripta la cadena */
		if(PlayerPrefs.HasKey("speedSelected")){
			selectionSS = Helper.DecryptInt(PlayerPrefs.GetString("speedSelected"));
		}else{
			selectionSS = 1;
			PlayerPrefs.SetString("speedSelected", Helper.EncryptInt(selectionSS));
		}
		
		
		if(PlayerPrefs.HasKey("modeSelected")){
			selectionMS = Helper.DecryptInt(PlayerPrefs.GetString("modeSelected"));
		}else{
			selectionMS = 1;
			PlayerPrefs.SetString("modeSelected", Helper.EncryptInt(selectionMS));
		}

		if(PlayerPrefs.HasKey("FPSOn") == false || Helper.DecryptInt(PlayerPrefs.GetString("FPSOn")) == 0){	
			checkFPS.SetActive(false);
			FPSOn = false;
			PlayerPrefs.SetString("FPSOn", Helper.EncryptInt(0));
		}else{
			checkFPS.SetActive(false);
			FPSOn = true;
		}

		if(PlayerPrefs.HasKey("money")){
			money = Helper.DecryptInt(PlayerPrefs.GetString("money"));
		}else{
			money = 0;
			PlayerPrefs.SetString("money", Helper.EncryptInt(0));
		}

		if(MusicManager.GetMusicOn()){	
			checkMusic.SetActive(true);
		}else{
			checkMusic.SetActive(false);
		}

		if(SoundManager.GetSoundOn()){	
			checkSound.SetActive(true);
		}else{
			checkSound.SetActive(false);
		}

		SetMoneyText(moneyCounter, money);
		SetMoneyText(moneyCounterShop, money);

		InitShop();

		if(PlayerPrefs.HasKey("settedSkin")){
			settedSkin = Helper.DecryptInt(PlayerPrefs.GetString("settedSkin"));
		}else{
			settedSkin = 0;
		}
		OnItemSelect(settedSkin);
	}

	private void SetMoneyText(Text moneyText, int money){
		moneyText.text = money.ToString();
	}

	private void InitShop(){
		int i = 0;
		foreach(Transform t in ItemsPanel){
			int currentIndex = i; 							// Para solucionar un error de OnItemSelect (Siempre seleccionaría el último integer con i)
			Button b = t.GetComponent<Button>();
			b.onClick.AddListener(() => OnItemSelect(currentIndex));
			i++;
		}

		ownedSkin[0] = true;
		for(i=1;i<12;i++){
			if(Helper.DecryptInt(PlayerPrefs.GetString("is" + i + "SkinOwned")) == 1){
				ownedSkin[i] = true;
			}else{
				ownedSkin[i] = false;
			}
		}

	}

	/* Mover las flechas de selección */

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
	private void MoveSelectors(){
		senseSS = Sense(speedSelector.transform.localPosition.y, startPositionSS, senseSS);
		speedSelector.transform.localPosition += Vector3.up * speed * senseSS;

		senseMS = Sense(modeSelector.transform.localPosition.x, startPositionMS, senseMS);
		modeSelector.transform.localPosition += Vector3.right * speed * senseMS;
	}

	private int Sense(float localPosition, float startPosition, int sense){
		if((localPosition > startPosition + range && sense == 1) || (localPosition < startPosition - range && sense == -1)){
			return -sense;
		}else{
			return sense;
		}
	}

	/* Funciones de recordsDisplay */

	/* Función para situar los records */
	private void SetRecords(){
		Text[] normalTexts = recordsDisplay.GetComponentsInChildren<Text>()[1].GetComponentsInChildren<Text>();
		Text[] NEOTexts = recordsDisplay.GetComponentsInChildren<Text>()[8].GetComponentsInChildren<Text>();

		int score;

		score = Helper.DecryptInt(PlayerPrefs.GetString("NNormalRecord"));
		if(score < 0){
			score = 0;
		}
		normalTexts[2].text = score.ToString() + " Pts";

		score = Helper.DecryptInt(PlayerPrefs.GetString("NSPRecord"));
		if(score < 0){
			score = 0;
		}
		normalTexts[4].text = score.ToString() + " Pts";

		score = Helper.DecryptInt(PlayerPrefs.GetString("NTryhardRecord"));
		if(score < 0){
			score = 0;
		}
		normalTexts[6].text = score.ToString() + " Pts";

		score = Helper.DecryptInt(PlayerPrefs.GetString("NEONormalRecord"));
		if(score < 0){
			score = 0;
		}
		NEOTexts[2].text = score.ToString() + " Pts";

		score = Helper.DecryptInt(PlayerPrefs.GetString("NEOSPRecord"));
		if(score < 0){
			score = 0;
		}
		NEOTexts[4].text = score.ToString() + " Pts";

		score = Helper.DecryptInt(PlayerPrefs.GetString("NEOTryhardRecord"));
		if(score < 0){
			score = 0;
		}
		NEOTexts[6].text = score.ToString() + " Pts";
	}

	/* Función para cambiar el color de los rótulos constantemente */
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

	/* Funciones aplicadas a botones */

	/* Click de ready! */
	public void StartPlay(){
		PlayerPrefs.Save();
		SceneManager.LoadScene(1);
	}

	/* Click de botones de tienda */

	public void ShopBt(){
		mainMenu.SetActive(false);
		shopMenu.SetActive(true);
	}

	public void ExitShopBt(){
		mainMenu.SetActive(true);
		shopMenu.SetActive(false);
	}

	public void BuySetItem(){
		if(ownedSkin[selectedSkin]){
			settedSkin = selectedSkin;
			animatorController.ChangeSkin(settedSkin);
		}else{
			if(money >= skinPrices[selectedSkin]){
				ownedSkin[selectedSkin] = true;
				money = money - skinPrices[selectedSkin];

				PlayerPrefs.SetString("is" + selectedSkin + "SkinOwned", Helper.EncryptInt(1));
				PlayerPrefs.SetString("money", Helper.EncryptInt(money));

				SetMoneyText(moneyCounter, money);
				SetMoneyText(moneyCounterShop, money);

				buttonText.text = "Set";
				skinPriceText.text = "bought";
			}
		}
	}
	private void OnItemSelect(int index){

		selectedSkin = index;

		skinNameText.text = StoryText.GetNameText(index);
		skinDescriptionText.text = StoryText.GetDescriptionText(index);

		if(ownedSkin[index]){
			buttonText.text = "Set";
			skinPriceText.text = "bought";
		}else{
			buttonText.text = "Buy";
			skinPriceText.text = skinPrices[index].ToString();
		}

		descriptionImage.sprite = shopMenu.GetComponentsInChildren<Button>()[index + 1].GetComponent<Image>().sprite;
		
	}

	/* Click de botones de selección de velocidad */
	public void SSNormal(){
		if(selectionSS != 1){
			speedSelector.transform.localPosition += Vector3.right * constantDistanceSS * (1 - selectionSS);

			selectionSS = 1;
			PlayerPrefs.SetString("speedSelected", Helper.EncryptInt(selectionSS));
		}
	}
	public void SSSpeedRacer(){
		if(selectionSS != 2){
			speedSelector.transform.localPosition += Vector3.right * constantDistanceSS * (2 - selectionSS);

			selectionSS = 2;
			PlayerPrefs.SetString("speedSelected", Helper.EncryptInt(selectionSS));
		}
	}
	public void SSTryhard(){
		if(selectionSS != 3){
			speedSelector.transform.localPosition += Vector3.right * constantDistanceSS * (3 - selectionSS);

			selectionSS = 3;
			PlayerPrefs.SetString("speedSelected", Helper.EncryptInt(selectionSS));
		}
	}

	/* Click de botones de selección de modo */
	public void MSNormal(){
		if(selectionMS != 1){
			modeSelector.transform.localPosition += Vector3.up * 143.9f;

			selectionMS = 1;
			PlayerPrefs.SetString("modeSelected", Helper.EncryptInt(selectionMS));
		}
	}
	public void MSNotEnoughOxygen(){
		if(selectionMS != 2){
			modeSelector.transform.localPosition += Vector3.up * -143.9f;

			selectionMS = 2;
			PlayerPrefs.SetString("modeSelected", Helper.EncryptInt(selectionMS));
		}
	}

	/* Click de ajustes */
	public void OptionsBt(){
		mainMenu.SetActive(false);
		player.SetActive(false);
		optionsMenu.SetActive(true);
	}

	public void ExitOptionsBt(){
		mainMenu.SetActive(true);
		player.SetActive(true);
		optionsMenu.SetActive(false);
	}

	public void MusicBt(){
		if(MusicManager.GetMusicOn()){
			checkMusic.SetActive(true);
		}else{
			checkMusic.SetActive(false);
		}
	}

	public void SoundBt(){
		if(SoundManager.GetSoundOn()){
			checkSound.SetActive(true);
		}else{
			checkSound.SetActive(false);
		}
	}

	public void FPSBt(){
		int boolInt;
		if(FPSOn){
			boolInt = 0;
			FPSOn = false;
		}else{
			boolInt = 1;
			FPSOn = true;
		}
		PlayerPrefs.SetString("FPSOn", Helper.EncryptInt(boolInt));
		
		FPSCounter.SetActive(FPSOn);
		checkFPS.SetActive(FPSOn);
	}

	public void CreditsBt(){
		optionsMenu.SetActive(false);
		creditsDisplay.SetActive(true);
	}

	public void BackCreditsBt(){
		creditsDisplay.SetActive(false);
		optionsMenu.SetActive(true);
	}

	/* Click de misiones */

	public void QuestsBt(){
		mainMenu.SetActive(false);
		player.SetActive(false);
		questsMenu.SetActive(true);

		Quest[] quests = QuestLoader.GetActiveQuests();

		mission1Text.text = "Mission: " + quests[0].description;
		mission1Reward.text = "Reward: " + quests[0].reward.ToString();
		if(QuestLoader.GetMissionCompleted(1)){
			mission1Complete.gameObject.SetActive(true);
		}else{
			mission1Complete.gameObject.SetActive(false);
		}
			
		mission2Text.text = "Mission: " + quests[1].description;
		mission2Reward.text = "Reward: " + quests[1].reward.ToString();
		if(QuestLoader.GetMissionCompleted(2)){
			mission2Complete.gameObject.SetActive(true);
		}else{
			mission2Complete.gameObject.SetActive(false);
		}
		
		mission3Text.text = "Mission: " + quests[2].description;
		mission3Reward.text = "Reward: " + quests[2].reward.ToString();
		if(QuestLoader.GetMissionCompleted(3)){
			mission3Complete.gameObject.SetActive(true);
		}else{
			mission3Complete.gameObject.SetActive(false);
		}

	}

	public void ExitQuestsBt(){
		mainMenu.SetActive(true);
		player.SetActive(true);
		questsMenu.SetActive(false);

	}

	/* Click de instrucciones */

	public void InstructionsBt(){
		mainMenu.SetActive(false);
		instructionsDisplay1.SetActive(true);
	}
	public void ExitInstructions1Bt(){
		instructionsDisplay1.SetActive(false);
		mainMenu.SetActive(true);
	}
	public void ExitInstructions2Bt(){
		instructionsDisplay2.SetActive(false);
		mainMenu.SetActive(true);
	}
	public void ExitInstructions3Bt(){
		instructionsDisplay3.SetActive(false);
		mainMenu.SetActive(true);
	}
	public void ContinueInstruction1Bt(){
		instructionsDisplay1.SetActive(false);
		instructionsDisplay2.SetActive(true);
	}
	public void ContinueInstruction2Bt(){
		instructionsDisplay2.SetActive(false);
		instructionsDisplay3.SetActive(true);
	}
	public void BackInstruction2Bt(){
		instructionsDisplay2.SetActive(false);
		instructionsDisplay1.SetActive(true);
	}
	public void BackInstruction3Bt(){
		instructionsDisplay3.SetActive(false);
		instructionsDisplay2.SetActive(true);
	}

	/* Click de GooglePlay */

	/* Click de records */
	public void RecordsBt(){
		mainMenu.SetActive(false);
		recordsDisplay.SetActive(true);
	}
	public void ExitRecordsBt(){
		recordsDisplay.SetActive(false);
		mainMenu.SetActive(true);
	}

}
