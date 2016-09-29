using UnityEngine;
using System.Collections;

public class Dog2 : MonoBehaviour {

	public string DogName = "Shitzhu";

	public DogTreats player;
	public float purchasePrice = 5000f;
	public float treatsPerSec;
	public float newTreatsPerSec;
	public float upgradeCost;
	public float numberOfUpgrades;
	bool purchased = false;
	bool maxed = false;
	public UnityEngine.UI.Text disp;

	// Update is called once per frame
	void Update () {
		//update treats once a second
		disp.text = "Dog: " + DogName + "\n" + "Price: " + purchasePrice + "\n";
		if (purchased) {
			disp.text = "Treats/Sec: " + treatsPerSec + "\n" + "Dog: " + DogName + "\n" + "Upgrade: " + Mathf.Round(purchasePrice) + "\n";
		}
		if (purchased && numberOfUpgrades >= 10) {
			disp.text = "Treats/Sec: " + treatsPerSec + "\n" + "Dog: " + DogName + "\n" + "Upgrade Maxed";
			maxed = true;
		}
	}

	public void Tapped (){
		if(maxed == false)
		if (purchased == false) {
			if (player.totalTreats >= purchasePrice) {
				newTreatsPerSec = Mathf.Round (purchasePrice / 75f);

				player.totalTreats -= purchasePrice;
				purchased = true;
				numberOfUpgrades = 1f;

				player.playerTreatsPerSec += newTreatsPerSec;

				treatsPerSec = newTreatsPerSec;

				upgradeCost = (purchasePrice * 3.15f) * numberOfUpgrades;
				purchasePrice = upgradeCost;
			}
		} else {
			if (player.totalTreats >= purchasePrice) {
				newTreatsPerSec = Mathf.Round(purchasePrice/75f);

				player.totalTreats -= purchasePrice;
				numberOfUpgrades += 1f;

				if (player.playerTreatsPerSec > 0) {
					player.playerTreatsPerSec += newTreatsPerSec;
				} else {
					player.playerTreatsPerSec = newTreatsPerSec;
				}

				treatsPerSec = Mathf.Round (purchasePrice / 75f);

				upgradeCost = (purchasePrice * 3.15f) * numberOfUpgrades;
				purchasePrice = upgradeCost;
			}
		};
	}
}
