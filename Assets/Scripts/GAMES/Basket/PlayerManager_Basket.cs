using UnityEngine;

public class PlayerManager_Basket : BasePlayerManager {

	[SerializeField]
	private PlayerLeftRight_Basket playerLeftRight;

	[System.NonSerialized]
	public static PlayerManager_Basket Instance;

	// main event
	void Start () {
		DataManager.SetName ("Player");
		DataManager.SetScore (0);
	}

	// main logic
	public override void Init ()
	{
		// activate instance
		if (Instance == null) {
			Instance = this;

			if (!didInit)
				base.Init ();
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

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

	public BaseUserManager Data {
		get { return DataManager; }
	}
}
