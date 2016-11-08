using UnityEngine;
using System.Collections;

public class BG1 : MonoBehaviour {

	public DogTreats player;	//reference to player to calculate total treats
	public string BGName;	//background name
	public UnityEngine.UI.Text disp;	//button display
	public float numOfUpgrades = 0f;	//number of upgrades
	public float purchasePrice;	//purchase price
	
	// Update is called once per frame
	void Update () {
		display ();	//display background info
	}

	public void Purchased(){
		if (player.totalTreats >= purchasePrice) {	//purchase algorithm
			player.totalTreats -= purchasePrice;	//subtract purchase price from total treats
			numOfUpgrades += 1f;	//increase number of upgrades
			purchasePrice *= 5f;	//purchase price increased by 5 times per purchase
		}
	}

	void display(){
		if (numOfUpgrades == 0f) {	//display initial purchase price for background. numOfUpgrades == 1 for background
			disp.text = BGName + "\n" + "Purchase price: " + purchasePrice;
		}
		else if (numOfUpgrades == 1f) {	//display purchase price for clothes. numOfUpgrades == 2 for clothes
			disp.text = BGName + "\n" + "Buy clothes: " + purchasePrice;
		}
		else if (numOfUpgrades == 2f) {	//display purchase price for hats. numOfUpgrades == 3 for hats
			disp.text = BGName + "\n" + "Buy hats: " + purchasePrice;
		}
		else if (numOfUpgrades > 2f) {	//display reached max purchase, no more purchases available
			disp.text = BGName + "\n" + "All accessories purchased!";
		}
	}
}
