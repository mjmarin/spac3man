using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

	/* Constante con el índice máximo de personajes */
	private const int maxSkinIndex = 11;

	/* Constante con el índice máximo de misiones activas */
	private const int maxMissionIndex = 2;

	/* Variable que regula si ya ha sido inicializado o no */
	static private bool initialized = false;

	/* Variables de selección de velocidad y modo */
	static private int selectionSS = 1;
	static private int selectionMS = 1;

	/* Variables de ajustes */

	/* Guarda la activación/desactivación del cálculo y muestra de los frame por segundo */
	static private bool FPSOn = false;

	/* Guarda la activación/desactivación de la reproducción de las pistas de música */
	static private bool musicOn = true;

	/* Guarda la activación/desactivación de la reproducción de los efectos de sonido */
	static private bool soundOn = true;

	/* Variables de tienda y skins */

	/* Dinero del jugador */
	static private ulong money = 0;

	/* Personaje seleccionado por el jugador */
	static private int settedSkin = 0;

	/* Control del desbloqueo de personajes */
	static private bool[] ownedSkin = new bool[12];

	/* Precio de los personajes */
	static private int [] skinPrices = { 0, -20, 30, 35, 40, 50, 70, 85, 90, 100, 120, 150};

	/* Guardado de los records del usuario */
	static private ulong [] records = { 0, 0, 0, 0, 0, 0};

	/* Variables de misiones */

	/* Índice de misiones activas */
	static private int[] activeMissionsIndex = { 0, 1, 2};

	/* Misiones compleción de misiones activas */
	static private bool[] missionsCompleted = { false, false, false};

	/* Variables de día de último día de entrada */

	/* Último día de entrada */
	static private int lastDay = 1;

	/* Último mes de entrada */
	static private int lastMonth = 1;

	/* Inicialización de datos */
	public void Init(){
		if(initialized == false){
			initialized = true;

			/* MODOS */
			if(PlayerPrefs.HasKey("speedSelected")){
				selectionSS = Helper.DecryptInt(PlayerPrefs.GetString("speedSelected"));
			}else{
				selectionSS = 1;
			}

			if(PlayerPrefs.HasKey("modeSelected")){
				selectionMS = Helper.DecryptInt(PlayerPrefs.GetString("modeSelected"));
			}else{
				selectionMS = 1;
			}

			/* AJUSTES */
			if(PlayerPrefs.HasKey("FPSOn")){
				FPSOn = Helper.DecryptBool(PlayerPrefs.GetString("FPSOn"));
			}else{
				FPSOn = false;
			}

			if(PlayerPrefs.HasKey("musicOn")){
				musicOn = Helper.DecryptBool(PlayerPrefs.GetString("musicOn"));
			}else{
				musicOn = true;
			}

			if(PlayerPrefs.HasKey("soundOn")){
				soundOn = Helper.DecryptBool(PlayerPrefs.GetString("soundOn"));
			}else{
				soundOn = true;
			}

			/* TIENDA */
			if(PlayerPrefs.HasKey("money")){
				money = System.Convert.ToUInt64(Helper.DecryptFloat(PlayerPrefs.GetString("money")));
			}else{
				money = 0;
			}

			if(PlayerPrefs.HasKey("settedSkin")){
				settedSkin = Helper.DecryptInt(PlayerPrefs.GetString("settedSkin"));
			}else{
				settedSkin = 0;
			}
			
			ownedSkin[0] = true;
			for(int i=1;i<maxSkinIndex;i++){
				if(Helper.DecryptBool(PlayerPrefs.GetString("is" + i + "SkinOwned"))){
					ownedSkin[i] = true;
				}else{
					ownedSkin[i] = false;
				}
			}

			/* RECORDS */
			if(PlayerPrefs.HasKey("NNormalRecord")){
				records[0] = System.Convert.ToUInt64(Helper.DecryptFloat(PlayerPrefs.GetString("NNormalRecord")));
			}else{
				records[0] = 0;
			}

			if(PlayerPrefs.HasKey("NSPRecord")){
				records[1] = System.Convert.ToUInt64(Helper.DecryptFloat(PlayerPrefs.GetString("NSPRecord")));
			}else{
				records[1] = 0;
			}

			if(PlayerPrefs.HasKey("NTryhardRecord")){
				records[2] = System.Convert.ToUInt64(Helper.DecryptFloat(PlayerPrefs.GetString("NTryhardRecord")));
			}else{
				records[2] = 0;
			}

			if(PlayerPrefs.HasKey("NEONormalRecord")){
				records[3] = System.Convert.ToUInt64(Helper.DecryptFloat(PlayerPrefs.GetString("NEONormalRecord")));
			}else{
				records[3] = 0;
			}

			if(PlayerPrefs.HasKey("NEOSPRecord")){
				records[4] = System.Convert.ToUInt64(Helper.DecryptFloat(PlayerPrefs.GetString("NEOSPRecord")));
			}else{
				records[4] = 0;
			}

			if(PlayerPrefs.HasKey("NEOTryhardRecord")){
				records[5] = System.Convert.ToUInt64(Helper.DecryptFloat(PlayerPrefs.GetString("NEOTryhardRecord")));
			}else{
				records[5] = 0;
			}

			/* MISIONES */
			for(int i=0;i<maxMissionIndex + 1;i++){
				if(PlayerPrefs.HasKey("Mission" + i)){
					activeMissionsIndex[i] = Helper.DecryptInt(PlayerPrefs.GetString("Mission" + i));
				}else{
					activeMissionsIndex[i] = i;
				}
			}

			for(int i=0;i<maxMissionIndex + 1;i++){
				if(PlayerPrefs.HasKey("Mission" + i + "Completed")){
					missionsCompleted[i] = Helper.DecryptBool(PlayerPrefs.GetString("Mission" + i + "Completed"));
				}else{
					missionsCompleted[i] = false;
				}
			}

			/* FECHA */
			if(PlayerPrefs.HasKey("LastDay")){
				lastDay = Helper.DecryptInt(PlayerPrefs.GetString("LastDay"));
			}else{
				lastDay = 0;
			}

			if(PlayerPrefs.HasKey("LastMonth")){
				lastMonth = Helper.DecryptInt(PlayerPrefs.GetString("LastMonth"));
			}else{
				lastMonth = 0;
			}
		}

	}

	/* FUNCIONES INTERFAZ DE CONSULTA */
	/* SELECCIÓN */
	static public int GetSelectionSS(){return selectionSS;}
	static public int GetSelectionMS(){return selectionMS;}
	/* AJUSTES */
	static public bool GetFPSOn(){return FPSOn;}
	static public bool GetMusicOn(){return musicOn;}
	static public bool GetSoundOn(){return soundOn;}
	/* TIENDA Y SKINS */
	static public ulong GetMoney(){return money;}
	static public int GetSettedSkin(){return settedSkin;}
	static public bool GetOwnedSkin(int selection){
		if(selection < 0 || selection > maxSkinIndex){
			return false;
		}else{
			return ownedSkin[selection];
		}
	}
	static public int GetSkinPrices(int selection){
		if(selection < 0 || selection > maxSkinIndex){
			return int.MaxValue;
		}else{
			return skinPrices[selection];
		}
	}
	/* RECORDS */
	static public ulong GetRecords(int index){
		if(index > -1 && index < 6){
			return records[index];
		}else{
			return 0;
		}
	}
	/* MISIONES */
	public static int GetActiveMissionsIndex(int selection){
		if(selection < 0 || selection > maxMissionIndex){
			return -1;
		}else{
			return activeMissionsIndex[selection];
		}
	}
	public static bool GetMissionCompleted(int selection){
		if(selection < 0 || selection > maxMissionIndex){
			return false;
		}else{
			return missionsCompleted[selection];
		}
	}

	/* FECHA */
	public static int GetLastDay(){return lastDay;}
	public static int GetLastMonth(){return lastMonth;}

	/* FUNCIONES INTERFAZ DE MODIFICACIÓN */
	static public void SetSelectionSS(int selection){
		if(selection < 1 || selection > 3){
			selectionSS = 1;
		}else{
			selectionSS = selection;
		}
		PlayerPrefs.SetString("speedSelected", Helper.EncryptInt(selectionSS));
	}
	static public void SetSelectionMS(int selection){
		if(selection < 1 || selection > 2){
			selectionMS = 1;
		}else{
			selectionMS = selection;
		}
		PlayerPrefs.SetString("modeSelected", Helper.EncryptInt(selectionMS));
	}
	static public void SetFPSOn(bool option){
		FPSOn = option;
		PlayerPrefs.SetString("FPSOn", Helper.EncryptBool(FPSOn));
	}
	static public void SetMusicOn(bool option){
		musicOn = option;
		PlayerPrefs.SetString("musicOn", Helper.EncryptBool(musicOn));
	}
	static public void SetSoundOn(bool option){
		soundOn = option;
		PlayerPrefs.SetString("soundOn", Helper.EncryptBool(soundOn));
	}
	static public void SetMoney(float newMoney){
		if(Helper.CheckUlongOverflow(newMoney)){
			money =  ulong.MaxValue;
		}else if(newMoney > 0){
			money = System.Convert.ToUInt64(newMoney);
		}else{
			money = 0;
		}
		PlayerPrefs.SetString("money", Helper.EncryptFloat(System.Convert.ToSingle(money)));
	}
	static public void SetSettedSkin(int selection){
		if(selection < 0 || selection > maxSkinIndex){
			settedSkin = 0;
		}else{
			settedSkin = selection;
		}
		PlayerPrefs.SetString("settedSkin", Helper.EncryptInt(settedSkin));
	}
	static public void SetOwnedSkin(int selection, bool boolean){
		if(selection > -1 && selection < maxSkinIndex + 1){
			ownedSkin[selection] = boolean;
			PlayerPrefs.SetString("is" + selection + "SkinOwned", Helper.EncryptBool(boolean));
		}
	}
	/* RECORDS */
	static public void SetRecords(int index, float newRecord){
		string stringMode;
		if(index > -1 && index < 6){
			switch(index){
				case 0:
					stringMode = "NNormalRecord";
				break;

				case 1:
					stringMode = "NSPRecord";
				break;

				case 2:
					stringMode = "NTryhardRecord";
				break;

				case 3:
					stringMode = "NEONormalRecord";
				break;

				case 4:
					stringMode = "NEOSPRecord";
				break;

				case 5:
					stringMode = "NEOTryhardRecord";
				break;

				default:
					stringMode = "ERROR";
				break;
			}

			if(Helper.CheckUlongOverflow(newRecord)){
				records[index] =  ulong.MaxValue;
			}else if(newRecord > records[index]){
				records[index] = System.Convert.ToUInt64(newRecord);
			}else{
				records[index] = 0;
			}
			PlayerPrefs.SetString(stringMode, Helper.EncryptFloat(System.Convert.ToSingle(records[index])));
		}
	}
	/* MISIONES */
	static public void SetActiveMissionsIndex(int selection, int index){
		if(selection > -1 && selection < maxMissionIndex + 1){
			activeMissionsIndex[selection] = index;
			PlayerPrefs.SetString("Mission" + selection, Helper.EncryptInt(index));
		}
	}

	static public void SetMissionsCompleted(int selection, bool completed){
		if(selection > -1 && selection < maxMissionIndex + 1){
			missionsCompleted[selection] = completed;
			PlayerPrefs.SetString("Mission" + selection + "Completed", Helper.EncryptBool(completed));
		}
	}

	/* FECHA */
	static public void SetLastDay(int day){
		if(day > -1 && day < 32){
			lastDay = day;
			PlayerPrefs.SetString("LastDay", Helper.EncryptInt(lastDay));
		}
	}

	static public void SetLastMonth(int month){
		if(month > -1 && month < 13){
			lastMonth = month;
			PlayerPrefs.SetString("LastMonth", Helper.EncryptInt(lastMonth));
		}
	}
}
