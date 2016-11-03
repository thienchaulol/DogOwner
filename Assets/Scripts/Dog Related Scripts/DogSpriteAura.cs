using UnityEngine;
using System.Collections;

public class DogSpriteAura : MonoBehaviour {

	public Dog1 player;	//player object
	public Sprite aura1;	//sprite object
	//want to display treat aura on dog: tiny random treats appearing around the dog while purchased
	//									 follow dog object when dog jumps
	public float displayTime;	//display aura time
	public float resetDisplayTime;	//resets display time
	public float spriteSpeed;	//levetation speed
	private Vector2 initialPos;	//initial position of game object

	void Start(){
		initialPos = transform.position;	//store initial position
	}

	// Update is called once per frame
	void Update () {
		if (player.numberOfUpgrades > 0f) {	//display dog sprite aura if purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = aura1;	//show aura
			Movement ();	//move aura
			displayTime -= Time.deltaTime;	//countdown display time of aura
			RefreshGameObj ();	//refresh aura when display time is over
		} else {	//don't display dog sprite aura if not purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		}
	}

	//change placements of game object, duplicate as well
	void auraPlacement(){
	}

	//deals with movement of game object
	void Movement(){
		if (displayTime > 0f) {	//move sprite if displayTime > 0
			transform.Translate (Vector2.up * spriteSpeed * Time.deltaTime);	//sprite movement
		}
	}

	//resets game object for next movement
	void RefreshGameObj(){
		if (displayTime <= 0f) {	//reset display time when displayTime <= 0
			displayTime = resetDisplayTime;
			transform.position = initialPos;	//reset position
		}
	}
}
