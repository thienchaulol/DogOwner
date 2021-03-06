﻿using UnityEngine;
using System.Collections;

public class BG1 : MonoBehaviour {

	public DogTreats player;	//reference to player to calculate total treats
	public string dogName;	//dog
	public UnityEngine.UI.Text nameDisp;	//button display
	public UnityEngine.UI.Text infoDisp;
	public float numOfUpgrades = 0f;	//number of upgrades
	public float purchasePrice; //purchase price
                                //public Sprite BG;
    public Dog1 dog; //reference to dog's number of upgrades
	
	// Update is called once per frame
	void Update () {
		display ();	//display info on button
	}

	public void Purchased(){
		if (player.totalTreats >= purchasePrice && dog.numberOfUpgrades > 0) {	//purchase algorithm
			player.totalTreats -= purchasePrice;	//subtract purchase price from total treats
			numOfUpgrades += 1f;	//increase number of upgrades
			purchasePrice *= 5f;	//purchase price increased by 5 times per purchase
		}
	}

	void display(){
		if (numOfUpgrades == 0f) {	//display initial purchase price for background. numOfUpgrades == 1 for background
			nameDisp.text = dogName;
			infoDisp.text = "Buy shoes: " + player.dispValToNiceString(purchasePrice);
		}
		else if (numOfUpgrades == 1f) {	//display purchase price for clothes. numOfUpgrades == 2 for clothes
			nameDisp.text = dogName;
			infoDisp.text = "Buy clothes: " + player.dispValToNiceString(purchasePrice);
		}
		else if (numOfUpgrades == 2f) {	//display purchase price for hats. numOfUpgrades == 3 for hats
			nameDisp.text = dogName;
			infoDisp.text = "Buy hats: " + player.dispValToNiceString(purchasePrice);
		}
		else if (numOfUpgrades > 2f) {	//display reached max purchase, no more purchases available
			nameDisp.text = dogName;
			infoDisp.text = "All purchased";
		}
	}
}
