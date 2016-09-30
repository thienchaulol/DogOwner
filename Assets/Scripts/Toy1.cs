using UnityEngine;
using System.Collections;

public class Toy1 : MonoBehaviour {

	public string toyName = "Squeeky Ball";
	public float toyChance = 0.01f;
	public float treatIncreaseFromToy = 2f;
	public float maxRange = 0f;
	public float receiveNumber;

	public DogTreats player;
	public UnityEngine.UI.Text disp;
	public UnityEngine.UI.Text toyNotif;

	public bool received = false;
	public bool show = false;
	bool didTap = false;
	int taps = 0;

	public bool testReceive = false;
	float num;
	
	// Update is called once per frame
	void Update () {
		if (!received) {
			disp.text = "Toy: " + toyName + "\n" + "Receive chance every tap: " + toyChance * 100f + "%"
			+ "\n" + "Increases all treat tap/accumlation by: " + treatIncreaseFromToy * 100f + "%" + "\n";
		} else {
			disp.text = "Toy: " + toyName + "\n" + "Received after " + taps + " taps."
				+ "\n" + "Increases all treat tap/accumulation by: " + treatIncreaseFromToy * 100f + "%" + "\n";
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			taps += 1;
			didTap = true;
		}
	}

	void FixedUpdate(){
		num = Random.Range(0f,maxRange);
		receiveNumber = 69f;
		if (didTap && !received) {
			//random chance to receive toy
			//Debug.Log (num);
			if (num == receiveNumber) {
				Debug.Log (taps);
				received = true;
				show = true;
				StartCoroutine (toyNotifFunc());
				show = false;
				player.treatsMultiplier = treatIncreaseFromToy;
			}
		}
		if (testReceive)
			StartCoroutine (toyNotifFunc ());
		didTap = false;
	}

	IEnumerator toyNotifFunc() {
		//Debug.Log("Before Waiting 2 seconds");
		if(show)
		toyNotif.text = "You've discovered: " + toyName + " in " + taps + " taps.";
		yield return new WaitForSeconds(10);
		toyNotif.text = "";
		//Debug.Log("After Waiting 2 Seconds");
	}

}
