using UnityEngine;
using System.Collections;

public class Dog1 : MonoBehaviour {

	//it should cost more and more treats to get relatively less improvement as times goes on.
	//near mid/end game, tapping for treats will not beat treat accumulation from dogs
	//more treats for less improvement keeps the game interesting at the start but stagnant in the end
	//have "toys" that unlock randomly per tap only when certain dogs are at certain levels to incentivise tapping
	//toys will increase interest in the stagnant end game

	public string DogName;
	public float purchasePrice;

	public DogTreats player;
	public float treatsPerSec;
	public float newTreatsPerSec;
	public float numberOfUpgrades;

	bool maxed = false;

	public UnityEngine.UI.Text disp;

	void Update () {
		disp.text = "Dog: " + DogName + "\n" + "Price: " + purchasePrice + "\n";
		if (numberOfUpgrades < 10) {
			disp.text = "Treats/Sec: " + treatsPerSec + "\n" + "Dog: " + DogName + "\n" + "Upgrade: " + Mathf.Round(purchasePrice) + "\n";
		}
		if (numberOfUpgrades >= 10) {
			disp.text = "Treats/Sec: " + treatsPerSec + "\n" + "Dog: " + DogName + "\n" + "Upgrade Maxed";
			maxed = true;
		}
	}

	public void Tapped (){
		if(maxed == false)
		if (player.totalTreats >= purchasePrice) {
			newTreatsPerSec = Mathf.Round (purchasePrice / 75f);
			player.totalTreats -= purchasePrice;
			numberOfUpgrades += 1f;
			player.playerTreatsPerSec += newTreatsPerSec;
			treatsPerSec += newTreatsPerSec;
			purchasePrice = (purchasePrice * 3.15f) * numberOfUpgrades;
		};
	}
}
