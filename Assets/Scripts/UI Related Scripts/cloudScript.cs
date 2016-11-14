using UnityEngine;
using System.Collections;

public class cloudScript : MonoBehaviour {
	
	public Transform generationPoint;
	public Transform endPoint;
	public TreatTapSpritePooler theObjectPool;	//have 5 clouds, pick random clouds
	public float spriteSpeed;

	private Vector2 initialPos;
	private bool atEnd = false;
	private bool atGenPoint = false;

	// Use this for initialization
	void Start () {
		initialPos = transform.position;	//store init pos
	}

	// Update is called once per frame
	void Update () {
		Movement ();
		if (atEnd == true) {	//refresh cloudsprite at end
			RefreshGameObj ();
		}
		if (atGenPoint == true) {
			cloudPool ();
		}
	}

	void Movement(){
		//only move if the game object is active and if it's less than generation point
		if (gameObject.activeSelf && transform.position.x < endPoint.position.x) {
			transform.Translate (Vector2.right * spriteSpeed * Time.deltaTime);	//move cloud right
			if (transform.position.x >= endPoint.position.x) {
				//if game object is equal to generation point, it is at the end
				atEnd = true;
			}
			if (transform.position.x == generationPoint.position.x) {
				atGenPoint = true;
			}
		}
	}

	void RefreshGameObj(){
		transform.position = initialPos;	//refresh current cloud sprite
		//gameObject.SetActive (false);	//deactivate cloud until needed
		atEnd = false;
	}

	void cloudPool(){
		GameObject newCloud = theObjectPool.GetPooledObject ();
		newCloud.SetActive (true);
		newCloud.transform.position = initialPos;
		atGenPoint = false;
	}
}