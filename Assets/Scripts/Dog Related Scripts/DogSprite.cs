using UnityEngine;
using System.Collections;

public class DogSprite : MonoBehaviour {
	public Dog1 player;	//used as reference to dog game object
	public Sprite dog, dogWithClothes, dogWithHat, dogWithShoes;	//sprite for dog type
	public BG1 purchases; //BG1 object
	public float spriteJumpSpeed;	//jump speed when tapped
	public float spriteWalkSpeed;	//walking speed
	public float spriteJumpHeight;	//jump height when tapped
	Vector2 initialPos;	//store initial position
	Vector2 initialJumpPos; //position before jump
	public Transform endPoint;
	public bool jumped = false; //bool to check if is jumping
	bool click = false;	//bool to check if tapped

	void Start(){
		//store initial position
		initialPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		//"numberOfUpgrades" is for upgrading the dog (in Dog1)
		//"numOfUpgrades" is for upgrading background and clothing (in BG1)
		if (player.numberOfUpgrades > 0f) {	//display dog sprite when dog is purchased
			if (purchases.numOfUpgrades == 0) {	//no purchases in BG tab, default dog
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = dog;
				//first purchase will only change the background
			} else if(purchases.numOfUpgrades == 1) {
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = dogWithShoes;
			} else if (purchases.numOfUpgrades == 2) { //2 purchases = clothes for dog
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = dogWithClothes;
			} else if (purchases.numOfUpgrades == 3) { //3 purchases = hat for dog
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = dogWithHat;
			}
			if (click) {	//if dog is clicked on dog will jump
				//Debug.Log ("initialJP: " + initialJumpPos + "counter: " + counter);
				Jump ();
				Fall ();
			}
			if (!click) {
				Movement ();
			}
		} else {	//don't display dog sprite if not purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		}
	}

	void Movement(){
		//only move if the game object is active and if it's less than generation point
		if (gameObject.activeSelf && transform.position.x > endPoint.position.x) {
			transform.Translate (Vector2.left * spriteWalkSpeed * Time.deltaTime);	//move cloud right
			if (transform.position.x <= endPoint.position.x) {
				//if game object is equal to generation point, it is at the end
				RefreshGameObj ();
			}
		}
	}

	void RefreshGameObj(){
		Vector2 temp = new Vector2 (initialPos.x, Random.Range(initialPos.y - .5f,initialPos.y + 1f));
		transform.position = temp;	//refresh current sprite
	}

	//jumping function
	void Jump(){
		if (!jumped) {
			transform.Translate (Vector2.up * spriteJumpSpeed * Time.deltaTime);
			if (transform.position.y > initialJumpPos.y + spriteJumpHeight) {
				jumped = true;
				return;				
			}
		}
	}

	//falling function
	void Fall(){
		if (jumped) {
			transform.Translate (Vector2.down * spriteJumpSpeed * Time.deltaTime);
			if (transform.position.y <= initialJumpPos.y) {
				jumped = false;
				click = false;
				return;
			}
		}
	}

	//checks for user click. eventually changed to user tap for mobile apps
	void OnMouseDown(){
		if (player.numberOfUpgrades > 0) {
			//Debug.Log ("clicked");
			if (click == false) {
				initialJumpPos = transform.position;
			}
			click = true;
		}
	}
}
