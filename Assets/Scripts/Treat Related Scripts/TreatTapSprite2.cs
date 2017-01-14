using UnityEngine;
using System.Collections;

public class TreatTapSprite2 : MonoBehaviour {

	//Script variables
	//public DogTreats player;
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

	AudioSource[] aSources;
	AudioSource audioSource1;

	void Awake(){	//putting this portion of code in Awake() instead of Start() prevents null reference exception
				 	//because otherwise audioSource1 will reference the disabled varibale, aSources, when audioSource1
					//is called in OnEnable(). This is occurs because aSources is nothing when "gameObject.SetActive(false)" is called
					//in RefreshGameObj()
		//AudioSource type array of AudioSource components attached to gameObject
		aSources = GetComponents <AudioSource>();
		//**To randomize sounds, could have one AudioSource object that is randomly initialized to
		//**one of the elements in the AudioSource array, aSources, on every tap.
		audioSource1 = aSources[Random.Range(0,14)];
	}

	void OnEnable(){ //this function is called when an object becomes enabled and active
		audioSource1.Play ();
	}

	void Start(){
		//every clone will have the same "random" position it's given at the beginning
		//move initialPos to RefreshGameObj() as well
		initialPos = new Vector2 (Random.Range(-2f, 2f), ceilingVal);	//set initial(random) position of game object
		gameObject.transform.position = initialPos; //store initial position of game object
		//gameObject.SetActive (false);	//deactivate game object until OnMouseDown()
		//**This script as well as DogTreats.cs both keep track of the user taps
		//TODO:**Could possibly put this into DogTreats.cs to reduce overhead of keeping track of taps.
		//can't do that because DogTreats.cs re-activates the gameObject this script is attached to.
		InvokeRepeating("Rotate", 0.1f, 0.1f);	//rotate every second
	}

	void Update () {
		//if(Input.GetMouseButtonDown(0) && !gameObject.name.Contains("(Clone)")){	//cloned objects cannot clone themselves
			//BUG: sometimes TWO sprites appear on tap.
		//	TreatPool ();
		//}
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
			initialPos = new Vector2 (Random.Range(-2f, 2f), ceilingVal); //set new random initial position
			transform.position = initialPos;	//set new random position
			audioSource1 = aSources[Random.Range(0,4)]; //set new random dog sound
			rotateZ = 0f;
			bone.transform.rotation = Quaternion.Euler (0, 0, rotateZ);	//reset rotation
			gameObject.SetActive (false);	//deactivate game object for next tap
		}
	}

	public void TreatPool(){	//object pool of treats
		GameObject newTreat = theObjectPool.GetPooledObject ();	//acquire pooled object
		newTreat.SetActive (true);	//set game object to active for use
	}
}