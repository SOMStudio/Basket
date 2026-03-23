using UnityEngine;

public class PlayerManager_Basket : BasePlayerManager {

	[SerializeField]
	private PlayerLeftRight_Basket playerLeftRight;

	[System.NonSerialized]
	public static PlayerManager_Basket Instance;
	
	void Start () {
		DataManager.SetName ("Player");
		DataManager.SetScore (0);
	}

	public override void Init()
	{
		if (Instance == null)
		{
			Instance = this;

			if (!didInit) base.Init();
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void StartPlay() {
		if (playerLeftRight) {
			playerLeftRight.CanControl (true);
		}
	}

	public void StopPlay() {
		if (playerLeftRight) {
			playerLeftRight.CanControl (false);
		}
	}

	public void SetPlayerInStartPos() {
		if (playerLeftRight) {
			playerLeftRight.SetInStartPos ();
		}
	}

	public BaseUserManager Data => DataManager;
}
