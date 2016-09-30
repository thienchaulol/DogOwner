using UnityEngine;
using System.Collections;

public class DogTreats : MonoBehaviour {

	public float totalTreats = 0f;
	public float treatsPerTap = 1f;
	public float playerTreatsPerSec = 0f;
	public float treatsMultiplier = 1f;

	public UnityEngine.UI.Text disp;

	bool didTap = false;

	void Start(){
		InvokeRepeating ("treatIncreaseSec", 1.0f, 1.0f);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)/* || Input.GetMouseButtonDown (0)*/) {
			didTap = true;
		}
		disp.text = "Dog Treats: " + Mathf.Round(totalTreats) + "\n" + "Dog Treats per Tap: " + treatsPerTap 
			+ "\n" + "Treats per second: " + playerTreatsPerSec;
	}

	void treatIncreaseSec(){
		totalTreats += playerTreatsPerSec * treatsMultiplier;
	}

	void FixedUpdate(){
		if (didTap == true) {
			totalTreats += treatsPerTap * treatsMultiplier;
			didTap = false;
		}
	}
}
