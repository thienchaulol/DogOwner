using UnityEngine;
using System.Collections;

public class DogSpriteAura : MonoBehaviour {

	public Dog1 player;	//used as reference to dog game object
	public Sprite aura;
	//aura will be three to five tiny dog bones that will appear at random
	//areas within the box collider of the dog object
	
	// Update is called once per frame
	void Update () {
		if (player.numberOfUpgrades > 0f) {	//display dog sprite aura if purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = aura;
		} else {	//don't display dog sprite aura if not purchased
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		}
	}

	void auraPlacement(){
	}
}
