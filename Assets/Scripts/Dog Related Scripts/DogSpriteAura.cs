using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DogSpriteAura : MonoBehaviour {

	public Dog1 player;	//player object
	public Sprite aura1;	//sprite object
	//want to display treat aura on dog: tiny random treats appearing around the dog while purchased
	//									 follow dog object when dog jumps
	public float displayTime;	//display aura time
	public float resetDisplayTime;	//resets display time
	public float spriteSpeed;	//levetation speed
	IList<Vector2> positions = new List<Vector2>();	//list to hold possible positions of sprite aura
	Vector2 p1;
	Vector2 p2;
	Vector3 p3;

	void Start(){
		p1 = transform.position;	//calc p1
		positions.Add(p1);	//store p1
		p2 = new Vector2(p1.x - 0.25f, p1.y - 0.25f);	//calc p2
		positions.Add(p2);	//store p2
		p3 = new Vector2 (p1.x + 0.25f, p1.y - 0.25f);	//calc p3
		positions.Add(p3);	//store p3
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

	//deals with movement of game object
	void Movement(){
		if (displayTime > 0f) {	//move sprite if displayTime > 0
			transform.Translate (Vector2.up * spriteSpeed * Time.deltaTime);	//sprite movement
		}
	}

	//resets game object for next movement
	void RefreshGameObj(){
		if (displayTime <= 0f) {	//reset display time when displayTime <= 0
			displayTime = resetDisplayTime;	//reset display time
			transform.position = positions[Random.Range(0, positions.Count)];	//choose new position from positions list
		}
	}
}