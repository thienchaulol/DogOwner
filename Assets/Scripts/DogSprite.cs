using UnityEngine;
using System.Collections;

public class DogSprite : MonoBehaviour {

	public Dog1 player;

	public Sprite dog;
	public float spriteSpeed;
	
	// Update is called once per frame
	void Update () {
		if (player.numberOfUpgrades > 0) {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = dog;
			if (Input.GetKeyDown (KeyCode.Space)) {
				Jump ();
			}
		} else {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		}
	}

	void Jump(){
		//transform.Translate(Vector2.up * spriteSpeed * Time.deltaTime);
	}
}
