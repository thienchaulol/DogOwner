using UnityEngine;
using System.Collections;

public class DogTreats : MonoBehaviour {

	//MAIN CANVAS SCRIPT
	//public BG1 currentBG;	//keeps track of current background. set "House Party" background when currentBG.numOfUpgrades >= 1
							//set "Default" background if currentBG.numOfUpgrades < 1

	public TreatTapSprite2 dogTreat;	//reference to treat tap sprite for object pooling
	public float totalTreats = 0f;	//total treats
	public float treatsPerTap = 1f;	//treats per tap
	public float playerTreatsPerSec = 0f;	//total treats per second
	public float treatsMultiplier = 1f;	//treats multiplier

	public bool showNotif = false;	//bool to show toy notification

	//public UnityEngine.UI.Text disp;

	public UnityEngine.UI.Text totalTreatsDisp;	//total treats display
	public UnityEngine.UI.Text treatsPerTapDisp;	//treats per tap display
	public UnityEngine.UI.Text treatsPerSecDisp;	//treats per sec display

	bool didTap = false;	//tap recorder

	void Start(){
		InvokeRepeating ("treatIncreaseSec", 1.0f, 1.0f);	//calls treat increase function every second
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)/* || Input.GetMouseButtonDown (0)*/) {	//records taps from user
			didTap = true;	//record tap
			dogTreat.gameObject.SetActive (true);	//for object pooling; sets game object(bone sprite) active when user taps
			dogTreat.didTap = true;	//object pooling
		}
		totalTreatsDisp.text = "Dog Treats: " + Mathf.Round (totalTreats) + "\n";	//total treats display
		treatsPerTapDisp.text = "Dog Treats per tap: " + Mathf.Round(treatsPerTap * treatsMultiplier) + "\n";	//TPT display
		treatsPerSecDisp.text = "Treats per second: " + Mathf.Round(playerTreatsPerSec * treatsMultiplier) + "\n";	//TPS display
	}

	void treatIncreaseSec(){	//treat increase function
		totalTreats += Mathf.Round(playerTreatsPerSec * treatsMultiplier);
	}

	void FixedUpdate(){
		if (didTap == true) {	//treats per tap modifier
			totalTreats += Mathf.Round(treatsPerTap * treatsMultiplier);
			didTap = false;
		}
	}
}
