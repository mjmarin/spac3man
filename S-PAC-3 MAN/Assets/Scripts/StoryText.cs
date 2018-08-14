using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryText  {

	public static string GetNameText(int index){
		string skinName;
		switch(index){
			case 0:
				skinName = "S-PAC-3 MAN";
			break;

			case 1:
				skinName = "POLITICAL CASPER";
			break;

			case 2:
				skinName = "MARGARITA RAINBOW";
			break;

			case 3:
				skinName = "PAC-KUNG FURY";
			break;

			case 4:
				skinName = "WOR-BU KBYO";
			break;

			case 5:
				skinName = "THE BLACKSMITH";
			break;

			case 6:
				skinName = "DON RAMON";
			break;

			case 7:
				skinName = "MRS COVA";
			break;

			case 8:
				skinName = "WOR-BU AN-R0K";
			break;

			case 9:
				skinName = "REBEL CD";
			break;

			case 10:
				skinName = "A-OI NKO";
			break;

			case 11:
				skinName = "LEAD ASTUR";
			break;

			default:
				skinName = "ERROR";
			break;
		}

		return ("Name: " + skinName);
	}

	public static string GetDescriptionText(int index){
		string skinDescrip;
		switch(index){
			case 0:
				skinDescrip = "His nickname comes from S: Super, PAC: Pakku, his race, 3: he is the third sexiest Pakku of all times according to Pac-Times magazine, MAN: Mononucleous Anchocobo and Not-very-cooked-rice, his favourite food, but people just call him space man. He is a super astronaut hero that studied in the PUA Hero Academy with the famous Pikoriya.";
			break;

			case 1:
				skinDescrip = "He was a political in a hot and chaotic planet. After lying about his postgrade, he was so humiliated that he wanted to go outside his home. Thus, he went to space and boasted about being the best space runner in the galaxy until his spaceship's comrades left him out. Now he must survive like a true space runner.";
			break;

			case 2:
				skinDescrip = "She used to be a sniper on eagle's army but it changed when she was sended to war. She got hard depressed cause she couldn't hit a single shot because of the jungle. Now she is ready to rise again as the best space runner of the universe...\n\nAt least that's what she put on her linkedIn.";
			break;

			case 3:
				skinDescrip = "She is a pakku who domained the kung fury path fighting against crime, even defeating Kung Fuhrer, a crazy pakku leader from the past who came back to present and tried to domain the world. Now she must left her friends back on the earth to infiltrate on a space criminal web and see what happens out there.";
			break;

			case 4:
				skinDescrip = "He is a Wor-Bu which means he is a language expert, focused on knowing all comunication ways. He thought that running for his life throughout the universe would put him in a critical situation and allow him to learn many languages faster than anyone else. He love videogames and he is making an Android one, just check it in GooglePlay: \"The Dragon Hunt: Roguelike RPG\"";
			break;

			case 5:
				skinDescrip = "He is a mythical space creature that is as vague as it is intelligent. He is also known for hating noisy things, even saying once at a planetary party 'Hey stop throwing firecrackers, they can hurt my heart, let's drink more alcohol instead of throwing firecrackers'. He risks his life as space runner  just to entertain himself.";
			break;

			case 6:
				skinDescrip = "He is a wanted space drug dealer who can transport any number of things in his magic pocket. After having a mishap in his last deal he must make a living as a space runner while looking for his partner Pokkita.";
			break;

			case 7:
				skinDescrip = "Cova is the youngest Marshall of the Royal Spaceship navy of all times, a brilliant strategist who leads his navy by musically movements. In the wake of peace, she has decided to be a space runner so as not to lose her abilities and extend the blue of the royal empire throughout the galaxy. She also loves sweet things like honey.";
			break;

			case 8:
				skinDescrip = "An is a Wor-Bu like KBYO, but he thought that they could use his intelligence to reach what he called the rockiness body level. That's this is the reason why he implanted himself robotics improvements that he called Rocket-0-Kontaminant, changing his name to An-R0k. He is running to test his implants and improve them.";
			break;

			case 9:
				skinDescrip = "He lived so peacefully until a new race arrived and dominated his planet, the evil DVDs. Now he must find allies around the galaxy to make a rebel army that takes down the dvds... Okay, okay, it's not the best character story, but what about you? You think this description part talk to you... you are so egocentric, you should have a professional look about that.";
			break;

			case 10:
				skinDescrip = "NKO is a unique robot model that use the Archic architecture, a forgotten civilization's architecture. This one, together with the Obviously Dont Limit Interface model, called the OI model, has allowed NKO to create an awareness of itself and develop its intelligence in a feline way. Now he just wants to run through the galaxy.";
			break;

			case 11:
				skinDescrip = "He was born in Astur a green and humid planet full of mountains in which a good fruit alcohol is prepared. Moreover, he was a member of the legendary military group The Lead Alliance, which achieved the supremacy of half the galaxy, thus avoiding the assaults of stellar pirates. Years has passed since then, and today he returns to his passion being a space runner.";
			break;

			default:
				skinDescrip = "ERROR";
			break;
		}

		return ("Name: " + skinDescrip);
	}
}
