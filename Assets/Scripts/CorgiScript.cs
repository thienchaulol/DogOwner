using UnityEngine;
using System.Collections;

public class CorgiScript : MonoBehaviour {

	//it should cost more and more treats to get relatively less improvement as times goes on.
	//near mid/end game, tapping for treats will not beat treat accumulation from dogs
		//more treats for less improvement keeps the game interesting at the start but stagnant in the end
			//have "toys" that unlock randomly per tap only when certain dogs are at certain levels to incentivise tapping
				//toys will increase interest in the stagnant end game
	public string dogName = "Corgi";
	static float hierarchyConstant = 3;
	static float baseTreats = 50f;

	public DogTreats player;
	public UnityEngine.UI.Text disp;
	public float purchasePrice = Mathf.Round(((hierarchyConstant - 1f) * 1.5f + 1f) * 50f);
	public float treatIncreasePerSec = Mathf.Round((hierarchyConstant - .5f) * baseTreats);
	public float numberOfUpgrades = 0f;
	public float upgradePrice;
	bool purchased = false;

	// Update is called once per frame
	void Update () {
		if (numberOfUpgrades > 0) {
			upgradePrice = Mathf.Round(purchasePrice * Mathf.Pow(numberOfUpgrades, ((hierarchyConstant - 1f) * .25f) + 1.75f));
		} else {
			upgradePrice = 2f * purchasePrice;
		}
		if (purchased) {//increase treats per second, not per frame.
			disp.text = dogName + "\n" + "Treats per second: " + treatIncreasePerSec + "\n" + "Upgrade price: " + upgradePrice;
		} else {
			disp.text = dogName + "\n" + "Treats per second: " + treatIncreasePerSec + "\n" + "Purchase price: " + purchasePrice;
		}
	}

	public void Purchase(){
		if (player.totalTreats >= purchasePrice && purchased == false) {
			purchased = true;
			player.totalTreats -= purchasePrice;
			numberOfUpgrades += 1f;
		}
		if (player.totalTreats >= upgradePrice && purchased == true) {
			player.totalTreats -= upgradePrice;
			treatIncreasePerSec = Mathf.Round (treatIncreasePerSec * (((hierarchyConstant - 1f) * .25f) + 1.45f));
			numberOfUpgrades += 1f;
		}
	}
}
