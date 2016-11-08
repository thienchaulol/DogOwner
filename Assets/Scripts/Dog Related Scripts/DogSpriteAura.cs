using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DogSpriteAura : MonoBehaviour {

	public Dog1 player;	//player object
	public Sprite aura1;	//sprite object
	public float spriteSpeed;	//up speed
	float initialSpriteSpeed;	//initial up speed
	IList<Vector2> positions = new List<Vector2>();	//list to hold possible positions of sprite aura
	Vector2 p1;	//position 1
	Vector2 p2;	//position 2
	Vector2 p3;	//position 3
	float initialP1Y;	//initial Y position of game object
	float initialP2Y;
	float initialP3Y;
	int positionNum;

	void Start(){
		initialSpriteSpeed = spriteSpeed;	//store initial sprite speed for upgrades
		positionNum = 0;

		p1 = transform.position;	//calc p1
		initialP1Y = p1.y;
		positions.Add(p1);	//store p1

		p2 = new Vector2(p1.x - 0.25f, p1.y - 0.25f);	//calc p2
		initialP1Y = p2.y;
		positions.Add(p2);	//store p2

		p3 = new Vector2 (p1.x + 0.25f, p1.y - 0.25f);	//calc p3
		initialP1Y = p3.y;
		positions.Add(p3);	//store p3
	}

	// Update is called once per frame
	void Update () {
		if (player.numberOfUpgrades > 0f) {	//display dog sprite aura if purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = aura1;	//show aura
			speedIncrease();	//function to increase aura speed
			Movement ();	//move aura
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
		if (positionNum == 0) {
			if (transform.position.y > initialP1Y + 0.5f) {
				resetPosition ();
			}
		}
		if (positionNum == 1) {
			if (transform.position.y > initialP2Y + 0.5f) {
				resetPosition ();
			}
		}
		if (positionNum == 2) {
			if (transform.position.y > initialP3Y + 0.5f) {
				resetPosition ();
			}
		}
	}

	void resetPosition(){
		//	if so, reset the position to positions 1, 2, or 3
		//transform.position = positions[Random.Range(0, positions.Count)];
		if (positionNum < 2) {
			transform.position = positions [positionNum];
			positionNum++;
		} else {
			transform.position = positions [positionNum];
			positionNum = 0;
		}
	}
}