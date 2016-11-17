using UnityEngine;
using System.Collections;
//using System; /*for Console.WriteLine()*/
using System.Globalization; /*for CultureInfo.InvariantCulture*/

public class DogTreats : MonoBehaviour {

	//MAIN CANVAS SCRIPT
	//public BG1 currentBG;	//keeps track of current background. set "House Party" background when currentBG.numOfUpgrades >= 1
							//set "Default" background if currentBG.numOfUpgrades < 1

	public TreatTapSprite2 treatTapSprite;	//reference to treat tap sprite for object pooling
	public float totalTreats = 0f;	//total treats
	public float treatsPerTap = 1f;	//treats per tap
	public float playerTreatsPerSec = 0f;	//total treats per second
	public float treatsMultiplier = 1f;	//treats multiplier

	public bool showNotif = false;	//bool to show toy notification

	//public UnityEngine.UI.Text disp;

	public UnityEngine.UI.Text totalTreatsDisp;	//total treats display
	public UnityEngine.UI.Text treatsPerTapDisp;	//treats per tap display
	public UnityEngine.UI.Text treatsPerSecDisp;	//treats per sec display
	public UnityEngine.UI.Text treatsMultDisp;	//treats multiplier display

	public bool didTap = false;	//tap recorder

	void Start(){
		InvokeRepeating ("treatIncreaseSec", 1.0f, 1.0f);	//calls treat increase function every second
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {	//records taps from user
			didTap = true;	//record tap
			treatTapSprite.gameObject.SetActive (true);	//for object pooling; sets game object(bone sprite) active when user taps
			treatTapSprite.didTap = true;	//object pooling
		}
	//	if (cloud.atEnd == true) {
	//		cloud.gameObject.SetActive (true);
	//	}
		PlayerInfoDisplay ();
	}

	void PlayerInfoDisplay(){
		//in hindsight, i could've made a function that took in a "UnityEngine.UI.Text" type
		//and a "float" type that would do the four if statements instead of making four of them..
		//i'll do that now.
		//total treats display
		dispVal(totalTreatsDisp, totalTreats);
		//TPT display
		treatsPerTapDisp.text = "Treats Per Tap: " + dispValToNiceString(treatsPerTap * treatsMultiplier);
		//TPS display
		treatsPerSecDisp.text = "Treats Per Sec: " + dispValToNiceString(playerTreatsPerSec * treatsMultiplier);
		//multiplier display
		treatsMultDisp.text = "Treats Multiplier: " + dispValToNiceString(treatsMultiplier * 100) + "%";
	}

	//http://stackoverflow.com/questions/11731996/string-format-numbers-thousands-123k-millions-123m-billions-123b
	//https://en.wikipedia.org/wiki/Names_of_large_numbers
	public void dispVal(UnityEngine.UI.Text x, float value){ //can only display a single value in given "Text" game object
		if (value == 0) {
			x.text = value.ToString();
		}
		if (value > 0 && value < 100000) {
			x.text = value.ToString ("#,#", CultureInfo.InvariantCulture);
		} else if (value >= 100000 && value < 1000000) {
			x.text = value.ToString ("0,,#", CultureInfo.InvariantCulture);	//hundred thousand
		} else if (value >= 1000000 && value < 1000000000) {
			x.text = value.ToString ("0,,.#M", CultureInfo.InvariantCulture);	//million
		} else if (value >= 1000000000 && value < 1000000000000) {
			x.text = value.ToString ("0,,,.#B", CultureInfo.InvariantCulture);	//billion
		} else if (value >= 1000000000000 && value < 1000000000000000) {
			x.text = value.ToString ("0,,,,.#T", CultureInfo.InvariantCulture);	//trillion
		}
	}

	public string dispValToNiceString(float value){	//returns "formatted" string of value up to trillion
		if (value == 0) {
			return value.ToString();
		}
		if (value > 0 && value < 100000) {
			return value.ToString ("#,#", CultureInfo.InvariantCulture);	//thousand
		} else if (value >= 100000 && value < 1000000) {
			return value.ToString ("0,,#", CultureInfo.InvariantCulture);	//hundred thousand
		} else if (value >= 1000000 && value < 1000000000) {
			return value.ToString ("0,,.#M", CultureInfo.InvariantCulture);	//million
		} else if (value >= 1000000000 && value < 1000000000000) {
			return value.ToString ("0,,,.#B", CultureInfo.InvariantCulture);	//billion
		} else if (value >= 1000000000000 && value < 1000000000000000) {
			return value.ToString ("0,,,,.#T", CultureInfo.InvariantCulture);	//trillion
		}
		return value.ToString();
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
