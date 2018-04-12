using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play_button()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void instructions_button()
    {
        SceneManager.LoadScene("Instructions", LoadSceneMode.Single);
    }
}
