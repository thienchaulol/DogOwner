using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DogAura : MonoBehaviour {

	public Dog1 player;	//player object
	public Sprite aura;	//sprite object
	public DogSprite dogSprite;	//used to remove aura when jumping to avoid glitches
	public float spriteSpeed;	//up speed
	public float auraHeight; //how high aura goes before resetting
	float initialSpriteSpeed;	//initial up speed
	IList<Vector2> positions = new List<Vector2>();	//list to hold possible positions of aura
	Vector2 p0, p1, p2;	//positions 1, 2, and 3
	float initialP0Y, initialP1Y, initialP2Y;	//initial positions
	int currPosition;	//current position

	void Start(){
		initialSpriteSpeed = spriteSpeed;	//store initial sprite speed for upgrades
		currPosition = 0;

		p0 = transform.position;	//calc p1
		initialP0Y = p0.y;
		positions.Add(p0);	//store p1

		p1 = new Vector2(p0.x - 0.25f, p0.y - 0.25f);	//calc p2
		initialP1Y = p1.y;
		positions.Add(p1);	//store p2

		p2 = new Vector2 (p0.x + 0.25f, p0.y - 0.25f);	//calc p3
		initialP2Y = p2.y;
		positions.Add(p2);	//store p3
	}

	// Update is called once per frame
	void Update () {
		if (player.numberOfUpgrades > 0f && dogSprite.jumped == false) {	//display dog sprite aura if purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = aura;
			speedIncrease ();	//function to increase aura speed
			Movement ();	//move aura
		} else if (dogSprite.jumped == true) {	//don't display dog sprite aura if dog jumped
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
			resetPosition ();
		} else {	//don't display dog sprite aura if not purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		}
	}

	void speedIncrease(){
		if (player.numberOfUpgrades == 3f) {	//increase sprite speed at certain upgrades
			spriteSpeed = initialSpriteSpeed * 2f;
		} else if (player.numberOfUpgrades == 6f) {
			spriteSpeed = initialSpriteSpeed * 2.5f;
		} else if (player.numberOfUpgrades == 9f) {
			spriteSpeed = initialSpriteSpeed * 3f;
		} else if (player.numberOfUpgrades == 10f) {
			spriteSpeed = initialSpriteSpeed * 4f;
		}
	}

	void Movement(){	//deals with movement of game object
		//pseudocode
		//move sprite up
		transform.Translate (Vector2.up * spriteSpeed * Time.deltaTime);	//sprite movement
		//check if it's past the ceiling value
		if (currPosition == 0) {
			if (transform.position.y > (initialP0Y + auraHeight)) {
				resetPosition ();
			}
		}
		if (currPosition == 1) {
			if (transform.position.y > (initialP1Y + auraHeight)) {
				resetPosition ();
			}
		}
		if (currPosition == 2) {
			if (transform.position.y > (initialP2Y + auraHeight)) {
				resetPosition ();
			}
		}
	}

	void resetPosition(){
		//	if so, reset the position to positions 1, 2, or 3
		//transform.position = positions[Random.Range(0, positions.Count)];
		if (currPosition < 2) {
			currPosition++;
			transform.position = positions [currPosition];
		} else {
			currPosition = 0;
			transform.position = positions [currPosition];
		}
	}
}