using UnityEngine;
using System.Collections;

public class Toy1 : MonoBehaviour {

	public string toyName = "Squeeky Ball";
	public float toyChance = 0.01f;
	public float treatIncreaseFromToy = 2f;

	public DogTreats player;
	public UnityEngine.UI.Text disp;

	public bool received = false;
	bool didTap = false;
	int taps = 0;
	
	// Update is called once per frame
	void Update () {
		if (!received) {
			disp.text = "Toy: " + toyName + "\n" + "Receive chance per tap: " + toyChance * 100f + "%"
			+ "\n" + "Increases treat per tap by: " + treatIncreaseFromToy * 100f + "%" + "\n";
		} else {
			disp.text = "Toy: " + toyName + "\n" + "You found the toy!!"
				+ "\n" + "Increases treat per tap by: " + treatIncreaseFromToy * 100f + "%" + "\n";
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			taps += 1;
			didTap = true;
		}
	}

	void FixedUpdate(){
		if (didTap && !received) {
			//random chance to receive toy
			int num = Random.Range(0,500);
			int receiveNumber = 69;
			//Debug.Log (num);
			if (num == receiveNumber) {
				Debug.Log (taps);
				received = true;
				player.treatsMultiplier = treatIncreaseFromToy;
			}
		}
		didTap = false;
	}

}
