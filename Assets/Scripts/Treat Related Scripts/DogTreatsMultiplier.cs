using UnityEngine;
using System.Collections;

public class DogTreatsMultiplier : MonoBehaviour {

	//Script variables
	public DogTreats player;	//reference to the player

	//Usual variables
	public float costToUpgrade;	//cost to upgrade
	public float newTreatsPerTap;	//new treats per tap after upgrade
	public float numberOfUpgrades;	//number of upgrades
	bool canUpgrade = true;
	//Display variables
	public UnityEngine.UI.Text dispUpgradeCost;	//upgrade Cost text display
	//public UnityEngine.UI.Text dispNewTreatsPerTap; //new treats text display

	void Update(){
		if (numberOfUpgrades > 0) {
			//increase upgrade cost based on treats per tap and number of upgrades
			costToUpgrade = Mathf.Round(player.treatsPerTap * numberOfUpgrades * 15.25f);
		} else {
			//initial cost to upgrade
			costToUpgrade = player.treatsPerTap * 15f;
		}
		newTreatsPerTap = Mathf.Round(player.treatsPerTap * 2.15f);	//treats per tap algorithm
		dispUpgradeCost.text = "Upgrade Tap: \n" + player.dispValToNiceString(costToUpgrade) + "\n";	//upgrade cost display
		//dispNewTreatsPerTap.text = "New Treats Per Tap: " + 
		//	player.dispValToNiceString(newTreatsPerTap * player.treatsMultiplier); //new treats cost display
	}

	public void Tapped(){
		if (player.totalTreats >= costToUpgrade && canUpgrade) {	//check if enough treats to upgrade
			numberOfUpgrades += 1f;	//increase number of upgrades
			player.totalTreats -= costToUpgrade;	//subtract cost from total treats
			player.treatsPerTap = newTreatsPerTap;	//set new treats per tap
			costToUpgrade = Mathf.Round(costToUpgrade*1.15f);	//increase upgrade cost
			transform.localScale = new Vector3(0.9f, 0.9f, 0.9f); //resize button when pressed
			StartCoroutine(ResizeButton (0.15f)); //resize button after short delay
		}
	}

	IEnumerator ResizeButton(float waitDelay){
		canUpgrade = false;
		yield return new WaitForSeconds (waitDelay);
		transform.localScale = new Vector3 (1, 1, 1);
		canUpgrade = true;
	}
}
