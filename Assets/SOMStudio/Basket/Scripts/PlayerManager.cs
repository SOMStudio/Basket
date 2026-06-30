using UnityEngine;

[AddComponentMenu("SOMStudio/Basket/Player Manager")]
public class PlayerManager : BasePlayerManager {

	[SerializeField]
	private PlayerLeftRight playerLeftRight;

	[System.NonSerialized]
	public static PlayerManager Instance;
	
	void Start () {
		dataManager.SetName ("Player");
		dataManager.SetScore (0);
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

	public BaseUserManager Data => dataManager;
}
