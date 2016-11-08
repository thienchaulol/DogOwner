using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DogSpriteAura : MonoBehaviour {

	public Dog1 player;	//player object
	public Sprite aura1;	//sprite object
	//want to display treat aura on dog: tiny random treats appearing around the dog while purchased
	//									 follow dog object when dog jumps
	public float spriteSpeed;	//up speed
	public float initialSpriteSpeed;	//initial up speed
	IList<Vector2> positions = new List<Vector2>();	//list to hold possible positions of sprite aura
	Vector2 p1;	//position 1
	Vector2 p2;	//position 2
	Vector2 p3;	//position 3
	float initP1Y;
	float initP2Y;
	float initP3Y;
	int randNum;	//stores random position number

	void Start(){
		randNum = Random.Range (0, positions.Count);	//first random number
		initialSpriteSpeed = spriteSpeed;	//store initial sprite speed for upgrades

		p1 = transform.position;	//calc p1
		initP1Y = p1.y;	//store initial p1.y for reset check
		positions.Add(p1);	//store p1

		p2 = new Vector2(p1.x - 0.25f, p1.y - 0.25f);	//calc p2
		initP2Y = p2.y;	//store initial p2.y for reset check
		positions.Add(p2);	//store p2

		p3 = new Vector2 (p1.x + 0.25f, p1.y - 0.25f);	//calc p3
		initP3Y = p3.y;	//store initial p3.y for reset check
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
		transform.Translate (Vector2.up * spriteSpeed * Time.deltaTime);	//sprite movement
		if(this.gameObject.transform.position.y >= initP1Y + .5f && randNum == 1){
			randNum = Random.Range (0, positions.Count);	//new random number
			transform.position = positions[randNum];	//choose new position from positions list
			p1.y = initP1Y;
		}
		if(this.gameObject.transform.position.y >= initP2Y + .5f && randNum == 2){
			randNum = Random.Range (0, positions.Count);	//new random number
			transform.position = positions[randNum];	//choose new position from positions list
			p2.y = initP2Y;
		}
		if(this.gameObject.transform.position.y >= initP3Y + .5f && randNum == 3){
			randNum = Random.Range (0, positions.Count);	//new random number
			transform.position = positions[randNum];	//choose new position from positions list
			p3.y = initP3Y;
		}
	}
}