﻿using UnityEngine;
using System.Collections;

public class Toy1 : MonoBehaviour {

	public string toyName = "Squeeky Ball";
	public float toyChance = 0.01f;
	public float treatMultFactor;
	public int maxRange;
	public int receiveNumber;

	public DogTreats player;
	public UnityEngine.UI.Text disp;
	public UnityEngine.UI.Text toyNotif;

	public bool received = false;
	public bool show = false;
	bool didTap = false;
	int taps = 0;

	public bool testReceive = false;
	int rndNum;
	public float numOfUpgrades = 0f;

	// Update is called once per frame
	void Update () {
		if (numOfUpgrades == 0) {
			disp.text = "Toy: " + toyName + "\n" + "Receive chance every tap: " + toyChance * 100f + "%";
		} else if (numOfUpgrades > 0){
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
		if (didTap) {
			if (rndNum == receiveNumber) {
				if (numOfUpgrades > 0) {
					show = true;
					StartCoroutine (toyNextNotifFunc());
					show = false;
					player.treatsMultiplier += .2f;
					treatMultFactor += .2f;
				} else {
					show = true;
					StartCoroutine (toyNotifFunc());
					show = false;
					player.treatsMultiplier *= treatMultFactor;
				}
				maxRange *= 2;
				toyChance /= 2f;
				numOfUpgrades += 1f;
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
		toyNotif.text = "You've discovered a: " + toyName + " in " + taps + " taps.";
		yield return new WaitForSeconds(10);
		toyNotif.text = "";
		//Debug.Log("After Waiting 2 Seconds");
	}

	IEnumerator toyNextNotifFunc(){
		if(show)
			toyNotif.text = "You've discovered another: " + toyName + " in " + taps+ " taps.";
		yield return new WaitForSeconds(10);
		toyNotif.text = "";
	}

}
