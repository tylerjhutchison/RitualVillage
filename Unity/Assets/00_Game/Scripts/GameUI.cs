﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public Image fadePlane;
	public GameObject gameOverUI;

	void Start () {
		FindObjectOfType<VillagerController> ().OnGameOver += OnGameOver;
	}

	void OnGameOver (){
		StartCoroutine (Fade (Color.clear, Color.black, 1));
		gameOverUI.SetActive (true);
	}

	IEnumerator Fade(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp (from, to, percent);
			yield return null;
		}
	}

	public void StartNewGame (){
		SceneManager.LoadScene ("Scene1");
	}

}
