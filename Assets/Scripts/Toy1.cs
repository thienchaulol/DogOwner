using UnityEngine;
using System.Collections;

public class Toy1 : MonoBehaviour {

	public string toyName = "Squeeky Ball";
	public float toyChance = 0.095f;
	public float treatIncreaseFromToy = 1.1f;

	public DogTreats player;
	public UnityEngine.UI.Text disp;

	bool received = false;
	bool didTap = false;
	
	// Update is called once per frame
	void Update () {
		disp.text = "Toy: " + toyName + "\n" + "Receive chance per tap: " + toyChance + "%" 
			+ "\n" + "Increases all treat accumulation by: " + treatIncreaseFromToy + "%" + "\n";
		if (Input.GetKeyDown (KeyCode.Space)) {
			didTap = true;
			received = true;
		}
		if (didTap == true) {
			//random chance to receive toy

		}
	}

	public void Tapped(){
		if (received) {
			player.treatsPerTap *= treatIncreaseFromToy;
			player.playerTreatsPerSec *= treatIncreaseFromToy;
		}
	}
}
