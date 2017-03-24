using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene ("Tutorial");
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
