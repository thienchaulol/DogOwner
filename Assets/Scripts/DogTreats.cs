using UnityEngine;
using System.Collections;

public class DogTreats : MonoBehaviour {

	public TreatTapSprite2 dogTreat;
	public float totalTreats = 0f;
	public float treatsPerTap = 1f;
	public float playerTreatsPerSec = 0f;
	public float treatsMultiplier = 1f;

	public bool showNotif = false;

	public UnityEngine.UI.Text disp;

	bool didTap = false;

	void Start(){
		InvokeRepeating ("treatIncreaseSec", 1.0f, 1.0f);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)/* || Input.GetMouseButtonDown (0)*/) {
			didTap = true;
			dogTreat.gameObject.SetActive (true);
			dogTreat.didTap = true;
		}
		disp.text = "Dog Treats: " + Mathf.Round(totalTreats) + "\n" + "Dog Treats per Tap: " + Mathf.Round(treatsPerTap * treatsMultiplier)
			+ "\n" + "Treats per second: " + Mathf.Round(playerTreatsPerSec * treatsMultiplier);
	}

	void treatIncreaseSec(){
		totalTreats += Mathf.Round(playerTreatsPerSec * treatsMultiplier);
	}

	void FixedUpdate(){
		if (didTap == true) {
			totalTreats += Mathf.Round(treatsPerTap * treatsMultiplier);
			didTap = false;
		}
	}
}
