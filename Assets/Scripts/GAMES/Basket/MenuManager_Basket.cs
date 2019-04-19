using UnityEngine;
using UnityEngine.UI;

public class MenuManager_Basket : MonoBehaviour {

	public Text textTimer;
	public Text textScore;

	[System.NonSerialized]
	public static MenuManager_Basket Instance;

	private GameController_Basket gameController;

	// main event
	void Awake () {
		// activate instance
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	void Start() {
		if (!gameController) {
			gameController = GameController_Basket.Instance;
		}
	}

	void LateUpdate() {
		if (gameController) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				gameController.PauseGame ();
			}
		}
	}

	// main logic
	public void UpdateScore(int val) {
		if (textScore) {
			textScore.text = val.ToString ();
		}
	}

	public void UpdateTimer(string val) {
		if (textTimer) {
			textTimer.text = val;
		}
	}

	public void RestartGame() {
		if (gameController) {
			gameController.RestartGame ();
		}
	}
}
