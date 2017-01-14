using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BigBone : MonoBehaviour {
	//AHH! This script does almost the same thing as TreatTapSprite2.cs
	//Can't figure out how to make the Big Bone work inside there ;_;

	public DogTreats player;
	public Transform endpoint;

	bool spawned = false;
	bool notMoving = false;

	public float spriteSpeed;	//speed of game object
	public float ceilingVal;	//height to drop bone from

	AudioSource[] aSources;
	AudioSource audioSource1;
		//wanted it to be public so it can be played in DogTreats.cs so the sound isn't cut off when the bone is claimed
		//**a sound managing script would be nice so objects that can be claimed before the end of it's sound clip is played won't
		//**be cut off prematurely.

	void Awake(){	//putting this portion of code in Awake() instead of Start() prevents null reference exception
		//because otherwise audioSource1 will reference the disabled varibale, aSources, when audioSource1
		//is called in OnEnable(). This is occurs because aSources is nothing when "gameObject.SetActive(false)" is called
		//in RefreshGameObj()
		//AudioSource type array of AudioSource components attached to gameObject
		aSources = GetComponents <AudioSource>();
		//**To randomize sounds, could have one AudioSource object that is randomly initialized to
		//**one of the elements in the AudioSource array, aSources, on every tap.
		//audioSource1 = aSources[Random.Range(0,2)];
	}

	void OnEnable(){ //this function is called when an object becomes enabled and active
		audioSource1 = aSources[Random.Range(0,2)];
		audioSource1.Play ();
	}

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (spawned && (gameObject.transform.position.y > endpoint.position.y)) {
			Movement (); //move object
		} else {
			notMoving = true;
		}
	}

	void Movement(){
		transform.Translate (Vector2.down * spriteSpeed * Time.deltaTime);
	}

	void Refresh(){
		transform.position = new Vector2 (Random.Range(-1.7f, 1.756f), ceilingVal);	//set new random position
		spawned = false;
		notMoving = false;
		player.bigBoneCounter = 0; //reset pity timer if a bigbone is retrieved
		gameObject.SetActive (false);	//deactivate game object for next tap
	}

	public void SpawnBigBone(){
		spawned = true;
	}

	void OnMouseDown(){
		if (notMoving && !audioSource1.isPlaying) {
			player.totalTreats += player.totalTreats + player.treatsPerTap * 100;
			Refresh ();
		}
	}
}
