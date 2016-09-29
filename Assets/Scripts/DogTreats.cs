using UnityEngine;
using System.Collections;

public class DogTreats : MonoBehaviour {

	public float totalTreats = 0f;
	public float treatsPerTap = 1f;
	public float playerTreatsPerSec;

	public UnityEngine.UI.Text disp;

	bool didTap = false;

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)/* || Input.GetMouseButtonDown (0)*/) {
			didTap = true;
		}

		disp.text = "Dog Treats: " + Mathf.Round(totalTreats) + "\n" + "Dog Treats per Tap: " + treatsPerTap 
			+ "\n" + "Total treats/second: " + playerTreatsPerSec;
	}

	void FixedUpdate(){
		if (didTap == true) {
			totalTreats += treatsPerTap;
			didTap = false;
		}
	}
}
