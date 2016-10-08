using UnityEngine;
using System.Collections;

public class DogSprite : MonoBehaviour {

	public Dog1 player;

	public Sprite dog;
	public float spriteSpeed;
	public float spriteJumpHeight;

	bool didTap = false;
	Vector2 initialPos;

	void Start(){
		initialPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (player.numberOfUpgrades > 0f) {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = dog;
			if (Input.GetKeyDown (KeyCode.Space)) {
				didTap = true;
			}
			Jump ();
			Fall ();
		} else {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		}
	}

	bool jumped = false;
	void Jump(){
		if (!jumped) {
			if (didTap) {
				transform.Translate (Vector2.up * spriteSpeed * Time.deltaTime);
				if (transform.position.y > initialPos.y + spriteJumpHeight) {
					jumped = true;
					didTap = false;
					return;
				}
			}
		}
	}

	void Fall(){
		if (jumped) {
			transform.Translate (Vector2.down * spriteSpeed * Time.deltaTime);
			if (transform.position.y <= initialPos.y) {
				jumped = false;
				return;
			}
		}
	}

}
