using UnityEngine;
using System.Collections;

public class TreatTapSprite2 : MonoBehaviour {
	
	public TreatTapSpritePooler theObjectPool;
	public float displayTime;
	public float resetDisplayTime;
	public float spriteSpeed;
	public bool didTap = false;
	private Vector2 initialPos;

	void Start(){
		initialPos = transform.position;
		gameObject.SetActive (false);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			didTap = true;
		}
		TreatPool ();
		Movement ();
		displayTime -= Time.deltaTime;
		RefreshGameObj ();
	}

	void Movement(){
		if (gameObject.activeSelf && displayTime > 0f) {
			transform.Translate (Vector2.down * spriteSpeed * Time.deltaTime);
		}
	}

	void RefreshGameObj(){
		if (displayTime <= 0f) {
			didTap = false;
			displayTime = resetDisplayTime;
			transform.position = initialPos;
			gameObject.SetActive (false);
		}
	}

	void TreatPool(){
		if (Input.GetKeyDown (KeyCode.Space) && didTap) {
			GameObject newTreat = theObjectPool.GetPooledObject ();
			newTreat.transform.position = initialPos;
			newTreat.SetActive (true);
		}
	}
}