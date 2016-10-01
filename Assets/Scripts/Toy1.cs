using UnityEngine;
using System.Collections;

public class Toy1 : MonoBehaviour {

	public string toyName = "Squeeky Ball";
	public float toyChance = 0.01f;
	public float treatMultFactor = 2f;
	public int maxRange;
	public int receiveNumber;

	public DogTreats player;
	public UnityEngine.UI.Text disp;
	public UnityEngine.UI.Text toyNotif;

	public bool received = false;
	public bool show = false;
	bool didTap = false;
	int taps = 0;
	int tapsToDiscover = 0;

	public bool testReceive = false;
	int rndNum;
	public float numOfUpgrades = 0f;

	// Update is called once per frame
	void Update () {
		if (!received) {
			disp.text = "Toy: " + toyName + "\n" + "Receive chance every tap: " + toyChance * 100f + "%";
		} else if (received && numOfUpgrades >= 1){
			disp.text = "Toy: " + toyName + "\tLevel: " + numOfUpgrades + "\n" + "Receive chance every tap: " + toyChance * 100f + "%"
				+ "\n" + "Increases all treat tap/accumulation by: " + treatMultFactor * 100f + "%" + "\n";
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			taps += 1;
			didTap = true;
		}
	}

	void FixedUpdate(){
		rndNum = Random.Range(0,maxRange);
		if (didTap && !received) {
			//random chance to receive toy
			//Debug.Log (num);
			if (rndNum == receiveNumber) {
				Debug.Log (taps);
				received = true;
				tapsToDiscover = taps;
				show = true;
				StartCoroutine (toyNotifFunc());
				show = false;
				player.treatsMultiplier *= treatMultFactor;	//each time(after the first time) you discover a toy, the multiplier is incremented by 2%
				maxRange *= 2;	//each time you discover a toy, the chances of discovering another one is 2x more difficult
				toyChance /= 2f;
				numOfUpgrades += 1f;
				tapsToDiscover = 0;
				taps = 0;
			}
		}
		else if (didTap && received && numOfUpgrades >= 1 && show == false) {
			if (rndNum == receiveNumber) {
				show = true;
				StartCoroutine (toyNextNotifFunc());
				show = false;
				numOfUpgrades += 1f;
				player.treatsMultiplier += .2f; //each time(after the first time) you discover a toy, the multiplier is incremented by 20%
				maxRange *= 2;
				toyChance /= 2f;
				tapsToDiscover = 0;
				taps = 0;
			}
		}
		if (testReceive)
			StartCoroutine (toyNotifFunc ());
		didTap = false;
	}

	IEnumerator toyNotifFunc() {
		//Debug.Log("Before Waiting 2 seconds");
		if(show)
		toyNotif.text = "You've discovered a: " + toyName + " in " + tapsToDiscover + " taps.";
		yield return new WaitForSeconds(10);
		toyNotif.text = "";
		//Debug.Log("After Waiting 2 Seconds");
	}

	IEnumerator toyNextNotifFunc(){
		if(show)
			toyNotif.text = "You've discovered another: " + toyName + " in " + tapsToDiscover + " taps.";
		yield return new WaitForSeconds(10);
		toyNotif.text = "";
	}

}
