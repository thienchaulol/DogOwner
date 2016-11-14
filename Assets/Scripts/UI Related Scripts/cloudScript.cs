using UnityEngine;
using System.Collections;

public class cloudScript : MonoBehaviour {

	//public int canvasWidth;
	public GameObject cloudSprite;
	public DogTreats treats;
	public Transform generationPoint;
	public Transform endPoint;
//	public TreatTapSpritePooler theObjectPool;
	public float spriteSpeed;
	public bool totalActiveClouds;
	public int activeCloudsMax;

	private Vector2 initialPos;
	private float cloudWidth;
	private bool atEnd = false;

	// Use this for initialization
	void Start () {
		//canvasWidth = 338;
		//activeCloudsMax = FindCloudsMax ();//calculate total number of clouds that can fit in screen
		//Debug.Log("max clouds: " + activeCloudsMax);
//		cloudWidth = cloudSprite.GetComponent<BoxCollider2D> ().size.x;	//calc cloud width
		initialPos = cloudSprite.transform.position;	//store init pos
	}
	
	// Update is called once per frame
	void Update () {

		Movement ();
		if (atEnd == true) {	//refresh cloudsprite at end
			RefreshGameObj ();
		}
	}

//	int FindCloudsMax(){
//		Debug.Log ("canvas width: " + canvasWidth + " cloudwidth: " + cloudWidth);
//		return Mathf.RoundToInt(canvasWidth / 10);
//	}

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

//	void cloudPool(){	//cloud object pool
//		GameObject newCloud = theObjectPool.GetPooledObject();	//retrieve new object
//		newCloud.SetActive (true);	//set active for use
//		newCloud.transform.position = initialPos;
//	}
}
