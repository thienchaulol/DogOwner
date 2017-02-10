using UnityEngine;
using System.Collections;
//using System; /*for Console.WriteLine()*/
using System.Globalization; /*for CultureInfo.InvariantCulture*/

public class DogTreats : MonoBehaviour {

	//MAIN CANVAS SCRIPT

	//Script variables
	public TreatTapSprite2 treatTapSprite;	//reference to tap sprite. used to activate sprite
	public BigBone bigBone;

	//Usual public variables
	public float totalTreats = 0f;	//total treats
	public float treatsPerTap = 1f;	//treats per tap
	public float treatsMultiplier = 1f;	//treats multiplier
	public float playerTreatsPerSec = 0f; //total treats per second
	public bool showNotif = false;	//bool to show toy notification
	public bool didTap = false;	//tap recorder
	public bool isMute; //mute button
	public int bigBoneCounter;
	int receive;
	int receive2;

	//Display variables
	public UnityEngine.UI.Text totalTreatsDisp;	//total treats display
	public UnityEngine.UI.Text treatsPerTapDisp;	//treats per tap display
	public UnityEngine.UI.Text treatsPerSecDisp;	//treats per sec display
	public UnityEngine.UI.Text treatsMultDisp;	//treats multiplier display

	//GameObject variables

	void Start(){
		InvokeRepeating ("treatIncreaseSec", 1.0f, 1.0f);	//calls treat increase function every second
		treatTapSprite.gameObject.SetActive(false); //deactivate bone sprite until tap
	}

	void Update(){
		PlayerInfoDisplay ();
	}

	public void Mute (){
		isMute = !isMute;
		AudioListener.volume =  isMute ? 0 : 1;
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

	void treatIncreaseSec(){ //treat increase function
		totalTreats += Mathf.Round(playerTreatsPerSec * treatsMultiplier);
	}

	void OnMouseUp(){
		didTap = false;
	}

	void OnMouseDown(){
		bigBoneCounter++;
		totalTreats += Mathf.Round(treatsPerTap * treatsMultiplier); //add treats
		if (!treatTapSprite.gameObject.activeSelf) {
			treatTapSprite.gameObject.SetActive (true);	//for object pooling; sets game object(bone sprite) active when user taps
		} else {
			treatTapSprite.TreatPool ();
		}
		RollForBigBone();
		didTap = true;
	}

	//Audio Fade Out
	//https://forum.unity3d.com/threads/fade-out-audio-source.335031/
	public static class AudioFadeOut {
		public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
			float startVolume = audioSource.volume;
			while (audioSource.volume > 0) {
				audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

				yield return null;
			}
			audioSource.Stop ();
			audioSource.volume = startVolume;
		}
	}

	AudioSource[] aSources;
	AudioSource audioSource1;

	void RollForBigBone(){
		receive = Random.Range(0, 5000); //1 in 5000 chance to receive a big bone
		receive2 = Random.Range (0, 5000);
		if (receive == receive2 && !bigBone.gameObject.activeSelf) {
			bigBone.gameObject.SetActive (true);
			bigBone.SpawnBigBone ();

			aSources = GetComponents <AudioSource>();

			audioSource1 = aSources[Random.Range(0,3)];
			audioSource1.Play ();
			StartCoroutine (AudioFadeOut.FadeOut (audioSource1, audioSource1.clip.length));
		} else if (bigBoneCounter >= 2000 && !bigBone.gameObject.activeSelf) { //receive big bone every 2000 taps
			bigBone.gameObject.SetActive (true);
			bigBone.SpawnBigBone ();
		}
	}

}