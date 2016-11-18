using UnityEngine;
using System.Collections;

public class TreatTapSprite2 : MonoBehaviour {
	
	public TreatTapSpritePooler theObjectPool;	//reference to object pool script
	public float displayTime;	//display time of game object
	public float resetDisplayTime;	//reset display time of game object
	public float spriteSpeed;	//speed of game object
	public bool didTap = false;
	private Vector2 initialPos;	//initial position for moving game object (x, y)
	float rotateZ;	//used to rotate object
	public UnityEngine.GameObject bone;	//reference to bone sprite (gameobject)
	public float ceilingVal;	//height to drop bone from

	void Start(){
		initialPos = new Vector2 (Random.Range(-2f, 2f), ceilingVal);	//set initial(random) position of game object
		gameObject.transform.position = initialPos;
		gameObject.SetActive (false);	//deactivate game object until tap
		InvokeRepeating("Rotate", 0.1f, 0.1f);	//rotate every second=
	}

	void Update () {
		//if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {	//record taps
		//	didTap = true;
		//}
		if(!gameObject.name.Contains("(Clone)")){	//prevents pooling of NULL object (prevents null reference exception)
			TreatPool ();
		}
		Movement ();	//move object
		displayTime -= Time.deltaTime;	//display object for set time
		RefreshGameObj ();	//refresh game object for next tap
	}

	void Rotate(){
		if (gameObject.activeSelf && displayTime > 0f) {
			rotateZ += 20f;
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
			didTap = false;	//reset tap recorder
			displayTime = resetDisplayTime;	//reset display time
			transform.position = new Vector2 (Random.Range(-2f, 2f), ceilingVal);	//set new random position
			rotateZ = 0f;
			bone.transform.rotation = Quaternion.Euler (0, 0, rotateZ);	//reset rotation
			gameObject.SetActive (false);	//deactivate game object for next tap
		}
	}

	void TreatPool(){	//object pool of treats
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) && didTap) {	//use objects on each tap or spacebar
			GameObject newTreat = theObjectPool.GetPooledObject ();	//acquire pooled object
			//newTreat.transform.position = new Vector2 (Random.Range(-2f, 2f), 4.535f);	//give new game object a random position
			//commented out because every game object is given a random position in Start()
			newTreat.SetActive (true);	//set game object to active for use
		}
	}
}