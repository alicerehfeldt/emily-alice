using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour {

	public AudioClip health;
	public AudioClip defeated;
	public AudioClip boss;
	public AudioClip victory;

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void Play(string name) {
		AudioClip clip;
		switch (name) {
		case "health":
			clip = health;
			break;
		case "defeated": 
			clip = defeated;
			break;
		case "boss":
			clip = boss;
			break;
		case "victory":
			clip = victory;
			break;
		default: 
			return;
		}

		audioSource.PlayOneShot (clip, 0.6f);
	}

}
