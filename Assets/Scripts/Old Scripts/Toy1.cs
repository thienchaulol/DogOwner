using UnityEngine;
using System.Collections;

public class Toy1 : MonoBehaviour {

	public string toyName;	//name of toy
	public float toyChance = 0.01f;	//chance to receive toy
	public float treatMultFactor;	//treat increase factor
	public int maxRange;	//used to change toy discovery chance
	public DogTreats player;	//reference to player
	public UnityEngine.UI.Text disp;	//button display of toy info
	public UnityEngine.UI.Text toyNotif;	//toy notification when found
	public bool received = false;	//bool for toy discovery
	bool didTap = false;	//bool to check user tap

	public bool testReceive = false;	//for testing
	int receiveNumber;	//used to change toy discovery chance
	int rndNum;	//used to determine toy discovery chance
	public float numOfUpgrades = 0f;	//tracks how many times toys are upgraded


	// Update is called once per frame
	void Update () {
		if (numOfUpgrades == 0) {	//receive chance info
			disp.text = "Toy: " + toyName + "\n" + "Receive chance every tap: " + toyChance * 100f + "%";
		}
		if (numOfUpgrades > 0){	//toy info after received
			disp.text = "Toy: " + toyName + " \tLevel: " + numOfUpgrades + "\n" + "Receive chance every tap: " + toyChance * 100f + "%"
				+ "\n" + "Increases all treat tap/accumulation by: " + treatMultFactor * 100f + "%" + "\n";
		}
		if (player.didTap == true) {	//records taps
			didTap = true;
			rndNum = Random.Range(0,maxRange);	//get random number
			receiveNumber = Random.Range(0, maxRange); //get random number
			Debug.Log("rndNum: " + rndNum + "\n" + "Receive Num: " + receiveNumber);
		}
	}

	void FixedUpdate(){
		if (didTap && !player.showNotif) {
			if (rndNum == receiveNumber) {
				if (numOfUpgrades > 0) {
					player.showNotif = true;	//set showNotif to true(can display message)
					player.treatsMultiplier += .2f;	//increase treat accumulation
					treatMultFactor += .2f;	//increase treat mult factor
					StartCoroutine (toyNextNotifFunc());	//display notification message
					player.showNotif = false;	//set showNotif to false(can't display message)
				} else {
					player.showNotif = true;	//set showNotif to true(can display message)
					player.treatsMultiplier *= treatMultFactor;	//increase treat accumulation
					StartCoroutine (toyNotifFunc());	//display notification message
					player.showNotif = false;	//set showNotif to false(can't display message)
				}
				maxRange *= 2;	//lower chance of receiving toy
				toyChance /= 2f;
				numOfUpgrades += 1f;	//record number of upgrades
			}
		}
		if (testReceive)	//test toy display
			StartCoroutine (toyNotifFunc ());
		didTap = false;
	}

	IEnumerator toyNotifFunc() {
		if(player.showNotif)
			//set notification
			toyNotif.text = "You found a: " + toyName/* + " in " + taps + " taps." + "\n"
				+ "Treats per tap and Treats per second increased to: " + treatMultFactor * 100f + "%"*/;
		yield return new WaitForSeconds(10);	//notificaion displays for 10 seconds
		toyNotif.text = "";	//remove notification
	}

	IEnumerator toyNextNotifFunc(){
		if (player.showNotif)
			//set notification
			toyNotif.text = "You found another: " + toyName/* + " in " + taps + " taps." + "\n"
			+ "Treats per tap and Treats per second increased to: "  + treatMultFactor * 100f + "%"*/;
		yield return new WaitForSeconds(10);	//notificaion displays for 10 seconds
		toyNotif.text = "";
	}

}
