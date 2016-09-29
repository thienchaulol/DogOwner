using UnityEngine;
using System.Collections;

public class DogTreatsMultiplier : MonoBehaviour {

	public DogTreats player;
	public float costToUpgrade;
	public float newTreatsPerTap;
	public float numberOfUpgrades;
	public UnityEngine.UI.Text disp;

	void Update(){
		if (numberOfUpgrades > 0) {
			costToUpgrade = Mathf.Round(player.treatsPerTap * numberOfUpgrades * 15.25f);
		} else {
			costToUpgrade = player.treatsPerTap * 15f;
		}
		newTreatsPerTap = Mathf.Round(player.treatsPerTap * 2.15f);
		disp.text = "Tap Upgrade cost: " + costToUpgrade + "\n" + "New Treats per tap: " + newTreatsPerTap;
	}

	public void Tapped(){
		if (player.totalTreats >= costToUpgrade) {
			numberOfUpgrades += 1f;
			player.totalTreats -= costToUpgrade;
			player.treatsPerTap = newTreatsPerTap;
			costToUpgrade = Mathf.Round(costToUpgrade*1.15f);
		}
	}
}
