using UnityEngine;
using System.Collections;

public class cloudScript : MonoBehaviour {

	public GameObject cloudSprite;
	public DogTreats treats;
	public Transform generationPoint;
	public Transform endPoint;
//	public TreatTapSpritePooler theObjectPool;
	public float spriteSpeed;
	public bool clicked = false;

	private Vector2 initialPos;
	private bool atEnd = false;

	// Use this for initialization
	void Start () {
		initialPos = cloudSprite.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
//		if (cloudSprite.transform.position.x >= generationPoint.position.x) {
//			cloudPool ();	//get new cloud 
//		}
		Movement ();
		if (atEnd == true) {	//refresh cloudsprite at end
			RefreshGameObj ();
		}
	}

	void Movement(){
		//only move if the game object is active and if it's less than generation point
		if (gameObject.activeSelf && cloudSprite.transform.position.x < endPoint.position.x) {
			cloudSprite.transform.Translate (Vector2.right * spriteSpeed * Time.deltaTime);	//move cloud right
			if (cloudSprite.transform.position.x >= endPoint.position.x) {
				//if game object is equal to generation point, it is at the end
				atEnd = true;
			}
		}
	}

	void RefreshGameObj(){
		cloudSprite.transform.position = initialPos;	//refresh current cloud sprite
		//cloudSprite.SetActive (false);	//deactivate cloud until needed
		atEnd = false;
	}	

	void OnMouseDown(){
		clicked = true;
	}

//	void cloudPool(){	//cloud object pool
//		GameObject newCloud = theObjectPool.GetPooledObject();	//retrieve new object
//		newCloud.SetActive (true);	//set active for use
//		newCloud.transform.position = initialPos;
//	}
}
