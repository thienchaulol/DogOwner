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
		if (notMoving) {
			player.totalTreats += player.totalTreats + player.treatsPerTap * 100;
			Refresh ();
		}
	}
}
