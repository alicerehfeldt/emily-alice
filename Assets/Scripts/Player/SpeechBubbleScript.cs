using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleScript : MonoBehaviour {

	public Sprite hokay;
	public Sprite fuck;
	public Sprite getFucked;
	public Sprite hereItComes;
	public Sprite jesus;
	public Sprite queueBackup;
	public Sprite rope;
	public Sprite sob;
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSortingLayer(bool isAlice) {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		if (isAlice) {
			spriteRenderer.sortingLayerName = "Alice World Alice";
		} else {
			spriteRenderer.sortingLayerName = "Emily World Emily";
		}
	}

	public void Show(string name, float life = 2.0f) {
		Sprite sprite = GetSprite (name);
		if (sprite == null) {
			return;
		}
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprite;
		StartCoroutine (Die (life));
	}

	Sprite GetSprite(string name) {
		switch (name) {
		case "hokay": 
			return hokay;
		case "fuck":
			return fuck;
		case "getFucked":
			return getFucked;
		case "hereItComes":
			return hereItComes;
		case "jesus":
			return jesus;
		case "queueBackup":
			return queueBackup;
		case "rope":
			return rope;
		case "sob":
			return sob;
		}
		return null;
	}

	IEnumerator Die(float life) {
		yield return new WaitForSeconds (life);
		Destroy (gameObject);
	}
}
