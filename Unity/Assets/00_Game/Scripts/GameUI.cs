using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public Image fadePlane;
	public GameObject gameOverUI;
	public GameObject introBanner;
	public RectTransform healthBar;

	VillagerController controller;

	void Start () {

		controller = FindObjectOfType<VillagerController> ();
		controller.OnGameOver += OnGameOver;
		controller.OnStartGame += OnStartGame;
	}

	void OnStartGame (){
		StartCoroutine (Flash (Color.black, Color.clear, 1));
	}

	void Update (){
		//health percent is the inverse of x/60  (60 - x) / 60
		float healthPercent = (60f - controller.totalLackOfFaith) /60f;
		healthBar.localScale = new Vector3 (healthPercent, 1, 1);	
	}

	IEnumerator Flash(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;

		introBanner.SetActive (true);

		while (percent < 1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp (from, to, percent);
			yield return null;
		}
		introBanner.SetActive (false);

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
