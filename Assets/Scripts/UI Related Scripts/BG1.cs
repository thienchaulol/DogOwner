using UnityEngine;
using System.Collections;

public class BG1 : MonoBehaviour {

	public DogTreats player;
	public string BGName;
	public UnityEngine.UI.Text disp;
	public float numOfUpgrades = 0f;
	public float purchasePrice;
	
	// Update is called once per frame
	void Update () {
		display ();
	}

	public void Purchased(){
		if (player.totalTreats >= purchasePrice) {
			player.totalTreats -= purchasePrice;
			numOfUpgrades += 1f;
			purchasePrice *= 5f;
		}
	}

	void display(){
		if (numOfUpgrades == 0f) {
			disp.text = BGName + "\n" + "Purchase price: " + purchasePrice;
		}
		else if (numOfUpgrades == 1f) {
			disp.text = BGName + "\n" + "Buy clothes: " + purchasePrice;
		}
		else if (numOfUpgrades == 2f) {
			disp.text = BGName + "\n" + "Buy hats: " + purchasePrice;
		}
		else if (numOfUpgrades > 2f) {
			disp.text = BGName + "\n" + "All accessories purchased!";
		}
	}
}
