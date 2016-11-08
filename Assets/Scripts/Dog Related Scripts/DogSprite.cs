using UnityEngine;
using System.Collections;

public class DogSprite : MonoBehaviour {
	public Dog1 player;	//used as reference to dog game object
	public Sprite dog, dogWithClothes, dogWithHat;	//sprite for dog type
	public DogAura dogAura;	//dogAura object
	public BG1 purchases; //BG1 object
	public float spriteSpeed;	//jump speed when tapped
	public float spriteJumpHeight;	//jump height when tapped
	bool click = false;	//bool to check if tapped
	public bool jumped = false;	//bool to check if is jumping
	Vector2 initialPos;	//store initial position

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
			} else if (purchases.numOfUpgrades == 2) { //2 purchases = clothes for dog
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = dogWithClothes;
			} else if (purchases.numOfUpgrades == 3) { //3 purchases = hat for dog
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = dogWithHat;
			}
			if (click) {	//if dog is clicked on dog will jump
				dogAura.enabled = false;	//disable dog aura when jumping
				Jump ();
				Fall ();
				dogAura.enabled = true;		//enable dog aura when done jumping
			}
		} else {	//don't display dog sprite if not purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		}
	}

	//jumping function
	void Jump(){
		if (!jumped) {
			transform.Translate (Vector2.up * spriteSpeed * Time.deltaTime);
			if (transform.position.y > initialPos.y + spriteJumpHeight) {
				jumped = true;
				return;				
			}
		}
	}

	//falling function
	void Fall(){
		if (jumped) {
			transform.Translate (Vector2.down * spriteSpeed * Time.deltaTime);
			if (transform.position.y <= initialPos.y) {
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
			click = true;
		}
	}
}
