﻿using UnityEngine;
using System.Collections;

public class DogTreatsMultiplier : MonoBehaviour {

	public DogTreats player;	//reference to the player
	public float costToUpgrade;	//cost to upgrade
	public float newTreatsPerTap;	//new treats per tap after upgrade
	public float numberOfUpgrades;	//number of upgrades
	public UnityEngine.UI.Text disp;	//text display

	void Update(){
		if (numberOfUpgrades > 0) {
			//increase upgrade cost based on treats per tap and number of upgrades
			costToUpgrade = Mathf.Round(player.treatsPerTap * numberOfUpgrades * 15.25f);
		} else {
			//initial cost to upgrade
			costToUpgrade = player.treatsPerTap * 15f;
		}
		newTreatsPerTap = Mathf.Round(player.treatsPerTap * 2.15f);	//treats per tap algorithm
		disp.text = "Tap Upgrade cost: " + costToUpgrade + "\n" + "New Treats per tap: "	//text display
			+ Mathf.Round(newTreatsPerTap * player.treatsMultiplier);
	}

	public void Tapped(){
		//if(Input.GetMouseButtonDown(0))
		if (player.totalTreats >= costToUpgrade) {	//check if enough treats to upgrade
			numberOfUpgrades += 1f;	//increase number of upgrades
			player.totalTreats -= costToUpgrade;	//subtract cost from total treats
			player.treatsPerTap = newTreatsPerTap;	//set new treats per tap
			costToUpgrade = Mathf.Round(costToUpgrade*1.15f);	//increase upgrade cost
		}
	}
}
