﻿using UnityEngine;
using System.Collections;

public class Dog1 : MonoBehaviour {

	//it should cost more and more treats to get relatively less improvement as times goes on.
	//near mid/end game, tapping for treats will not beat treat accumulation from dogs
	//more treats for less improvement keeps the game interesting at the start but stagnant in the end
	//have "toys" that unlock randomly per tap only when certain dogs are at certain levels to incentivise tapping
	//toys will increase interest in the stagnant end game

	public string DogName;	//type of dog
	public float purchasePrice;	//purchase price and upgrade price
	public float numberOfUpgrades;	//total number of upgrades
	public DogTreats player;	//used to reference player
	public UnityEngine.UI.Text nameDisp;	//button display
	public UnityEngine.UI.Text infoDisp;
	public UnityEngine.UI.Text info2Disp;
	float treatsPerSec;	//current treats per second from dog
	float newTreatsPerSec;	//new treats per second after upgrade
	bool maxed = false;	//can only upgrade dog 10 times

	void Update () {
		//display dog info on button
		nameDisp.text = DogName;
		infoDisp.text = "Price: " + purchasePrice + "\n";
		if (numberOfUpgrades < 10) {
			nameDisp.text = DogName;
			infoDisp.text = "Treats/Sec: " + player.dispValToNiceString (treatsPerSec);
			info2Disp.text = "Upgrade: " + player.dispValToNiceString(purchasePrice);
		}
		if (numberOfUpgrades >= 10) {
			nameDisp.text = DogName;
			infoDisp.text = "Treats/Sec: " + player.dispValToNiceString (treatsPerSec);
			info2Disp.text = "Upgrade: " + player.dispValToNiceString(purchasePrice);
			maxed = true;
		}
	}

	//purchasing an upgrade
	public void Tapped (){
		if(maxed == false)	//max # of upgrades is 10
		if (player.totalTreats >= purchasePrice) {	//algorithm to determine treats per second and upgrade price
			newTreatsPerSec = Mathf.Round (purchasePrice / 75f);
			player.totalTreats -= purchasePrice;
			numberOfUpgrades += 1f;
			player.playerTreatsPerSec += newTreatsPerSec;
			treatsPerSec += newTreatsPerSec;
			purchasePrice = (purchasePrice * 3.15f) * numberOfUpgrades;
		};
	}
}
