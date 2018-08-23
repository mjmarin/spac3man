using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GooglePlayManager : MonoBehaviour {

	static public void SetOnlineScore(long score, string leaderboardID){
		Social.ReportScore(score, leaderboardID, null);
	}

	static public void SetOnlineAchievement(string achivementID){
		Social.ReportProgress(achivementID, 100.0, null);
	}

	static public void CheckAchievements(){
		int cont = 0;
		if(Social.localUser.authenticated){
			for(int i = 1; i < 12; i++){
				if(DataManager.GetOwnedSkin(i) == true){
					SetOnlineAchievement(GetSkinID(i));
					cont++;
				}
			}

			if(cont == 11){
				SetOnlineAchievement("CgkInJLw2NEXEAIQEQ");
			}
		}
	}


	static public string GetSkinID(int selectedSkin){
		string ID;
		switch(selectedSkin){
			case 1:
				ID = "CgkInJLw2NEXEAIQBw";
			break;

			case 2:
				ID = "CgkInJLw2NEXEAIQDQ";
			break;

			case 3:
				ID = "CgkInJLw2NEXEAIQDw";
			break;

			case 4:
				ID = "CgkInJLw2NEXEAIQDA";
			break;

			case 5:
				ID = "CgkInJLw2NEXEAIQEw";
			break;

			case 6:
				ID = "CgkInJLw2NEXEAIQEA";
			break;

			case 7:
				ID = "CgkInJLw2NEXEAIQDg";
			break;

			case 8:
				ID = "CgkInJLw2NEXEAIQCA";
			break;

			case 9:
				ID = "CgkInJLw2NEXEAIQCw";
			break;

			case 10:
				ID = "CgkInJLw2NEXEAIQCQ";
			break;

			case 11:
				ID = "CgkInJLw2NEXEAIQCg";
			break;

			default:
			 	ID = "ERROR";
			break;
		}
		return ID;
	}

	static public string GetModeID(int selectedMode, int selectedSpeed){
		string ID;
		if(selectedMode == 1){
			if(selectedSpeed == 1){
				ID = "CgkInJLw2NEXEAIQAQ";
			}else if(selectedSpeed == 2){
				ID = "CgkInJLw2NEXEAIQAg";
			}else{
				ID = "CgkInJLw2NEXEAIQAw";
			}
		}else{
			if(selectedSpeed == 1){
				ID = "CgkInJLw2NEXEAIQBA";
			}else if(selectedSpeed == 2){
				ID = "CgkInJLw2NEXEAIQBQ";
			}else{
				ID = "CgkInJLw2NEXEAIQBg";		
			}
		}
		return ID;
	}
}
