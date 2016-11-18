using UnityEngine;
using System.Collections;

public class Toy2 : MonoBehaviour {
	public DogTreats player; //user object
	public UnityEngine.UI.Text toyInfo; //button display of toy info
	public UnityEngine.UI.Text toyNotif; //toy notification when found
	public string toyName;
	public int high; //toy receive rate is 1/high
	public float treatMultFactor;

	int toyLevel;
	float toyChance;

	void Start(){
		toyLevel = 0;
		toyChance = Mathf.Round (1 / high);
	}

	// Update is called once per frame
	void Update () {
		if (toyLevel == 0) {	//receive chance info
			toyInfo.text = "Toy: " + toyName + "\n" + "Receive chance every tap: " + toyChance * 100f + "%";
		}
		if (toyLevel > 0){	//toy info after received
			toyInfo.text = "Toy: " + toyName + " \tLevel: " + toyLevel + "\n" + "Receive chance every tap: " + toyChance * 100f + "%"
				+ "\n" + "Increases all treat tap/accumulation by: " + treatMultFactor * 100f + "%" + "\n";
		}
		if (player.didTap == true) {
			if (Roll (high) == true) {
				toyMath ();
			}
		}
	}

	bool Roll(int high){
		int receive = Random.Range(0, high);
		int receive2 = Random.Range (0, high);
		//Debug.Log("receive: " + receive + "\n" + "receive2: " + receive2);
		if (receive == receive2) {
			return true;
		} else {
			return false;
		}
	}

	void toyMath(){
		player.showNotif = true; //set showNotif to true(can display message)
		player.treatsMultiplier += .2f;	//increase treat accumulation
		treatMultFactor += .2f;	//increase treat mult factor
		StartCoroutine(toyNotifFunc ()); //show toy notification
		player.showNotif = false; //set showNotif to false(can't display message)
		toyLevel++; //record toy level
		high *= 2;	//lower chance of receiving toy
		toyChance /= 2f;
	}

	IEnumerator toyNotifFunc() {
		if (player.showNotif) {
			//set notification
			toyNotif.text = "You found a: " + toyName;
		}
		yield return new WaitForSeconds(10);	//notificaion displays for 10 seconds
		toyNotif.text = "";	//remove notification
	}
}
