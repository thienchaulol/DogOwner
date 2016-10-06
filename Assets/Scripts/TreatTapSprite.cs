using UnityEngine;
using System.Collections;

public class TreatTapSprite : MonoBehaviour {

	public Sprite dogTreat;

	public float displayTime;
	public float resetDisplayTime;
	public float spriteSpeed;

	private Vector2 initialPos;

	bool didTap = false;

	void Start(){
		initialPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			didTap = true;
			displayTime = resetDisplayTime;
			transform.position = initialPos;
		}
		Movement ();
		displayTime -= Time.deltaTime;
		if (displayTime <= 0f) {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
			displayTime = resetDisplayTime;
			transform.position = initialPos;
		}
	}

	void FixedUpdate(){
		if (didTap && displayTime > 0f) {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = dogTreat;
		}
		didTap = false;
	}

	void Movement(){
		if (this.gameObject.GetComponent<SpriteRenderer> ().sprite != null) {
			transform.Translate (Vector2.down * spriteSpeed * Time.deltaTime);
		}
	}
}
