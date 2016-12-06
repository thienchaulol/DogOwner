using UnityEngine;
using System.Collections;

public class TreatTapSprite2 : MonoBehaviour {

	//Script variables
	public DogTreats player;
	public TreatTapSpritePooler theObjectPool;	//reference to object pool script

	//Usual public variables
	public float displayTime;	//display time of game object
	public float resetDisplayTime;	//reset display time of game object
	public float spriteSpeed;	//speed of game object
	public float ceilingVal;	//height to drop bone from
	private Vector2 initialPos;	//initial position for moving game object (x, y)

	//Usual not-public variables
	float rotateZ;	//used to rotate object

	//Display variables

	//GameObject variables
	public UnityEngine.GameObject bone;	//reference to bone sprite (gameobject)

	void Start(){
		initialPos = new Vector2 (Random.Range(-2f, 2f), ceilingVal);	//set initial(random) position of game object
		gameObject.transform.position = initialPos; //store initial position of game object
		gameObject.SetActive (false);	//deactivate game object until OnMouseDown()
		//**This script as well as DogTreats.cs both keep track of the user taps
		//TODO:**Could possibly put this into DogTreats.cs to reduce overhead of keeping track of taps.
		//can't do that because DogTreats.cs re-activates the gameObject this script is attached to.
		InvokeRepeating("Rotate", 0.1f, 0.1f);	//rotate every second
	}

	void Update () {
		if(Input.GetMouseButtonDown(0) && !gameObject.name.Contains("(Clone)")){	//cloned objects cannot clone themselves
			//BUG: sometimes TWO sprites appear on tap.
			TreatPool ();
		}
		Movement (); //move object
		displayTime -= Time.deltaTime;	//display object for set time
		RefreshGameObj ();	//refresh game object for next tap
	}

	void Rotate(){
		if (gameObject.activeSelf && displayTime > 0f) {
			rotateZ += 40f;
			bone.transform.rotation = Quaternion.Euler (0, 0, rotateZ);	//set rotation
		}
	}

	void Movement(){
		//only move if the game object is active and if it is able to be displayed
		if (gameObject.activeSelf && displayTime > 0f) {
			transform.Translate (Vector2.down * spriteSpeed * Time.deltaTime);
		}
	}

	void RefreshGameObj(){
		if (displayTime <= 0f) {
			displayTime = resetDisplayTime;	//reset display time
			transform.position = initialPos;	//set new random position
			rotateZ = 0f;
			bone.transform.rotation = Quaternion.Euler (0, 0, rotateZ);	//reset rotation
			gameObject.SetActive (false);	//deactivate game object for next tap
		}
	}

	void TreatPool(){	//object pool of treats
		GameObject newTreat = theObjectPool.GetPooledObject ();	//acquire pooled object
		newTreat.SetActive (true);	//set game object to active for use
	}
}