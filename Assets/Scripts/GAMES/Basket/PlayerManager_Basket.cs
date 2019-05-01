using UnityEngine;

public class PlayerManager_Basket : BasePlayerManager {

	[SerializeField]
	private PlayerLeftRight_Basket playerLeftRight;

	// main event
	void Start () {
		DataManager.SetName ("Player");
		DataManager.SetScore (0);
	}

	// main logic
	public void StartPlay() {
		if (playerLeftRight) {
			playerLeftRight.CanControll (true);
		}
	}

	public void StopPlay() {
		if (playerLeftRight) {
			playerLeftRight.CanControll (false);
		}
	}

	public void SetPlayerInStartPos() {
		if (playerLeftRight) {
			playerLeftRight.SetInStartPos ();
		}
	}
}
