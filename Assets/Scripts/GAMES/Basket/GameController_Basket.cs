using UnityEngine;

public class GameController_Basket : BaseGameController {

	public GameObject[] spawnList;
	public Transform[] spawnClip;
	public float timeBetweaneSpawn = 2.0f; // time betweane spawn
	public float timeFrequenceSpawn = 10; // time after decreace spawn time
	public float timeDecreaceStep = 0.2f; // step decreace spawn
	public float timeLimitBetweaneSpawn = 0.5f; // limit betweane spawn

	[SerializeField]
	private bool startGame = false;

	// spawn settings
	private float timePlay = 0.0f;
	private float betweaneSpawnTime = 2.0f;
	private float lastSpawnTime = 0.0f;
	private float lastFrequenceSpawnTime = 0.0f;

	private TimerClass theTimer;

	[System.NonSerialized]
	public static GameController_Basket Instance;

	[Header("managers")]
	public MenuManager_Basket menuManager;
	public PlayerManager_Basket playerManager;
	public BaseUserManager playerData;
	public BaseSoundController soundManager;

	// main event
	void Awake () {
		// activate instance
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	void Start () {
		// init game
		InitGame ();

		// crear Menu
		Update_UI ();

		// start game
		StartGame ();
	}

	void Update () {
		if (startGame && !Paused) {
			// global time for frequence spawn
			theTimer.UpdateTimer ();
			int curTime = theTimer.GetTime ();

			// time for Spawn
			timePlay += Time.deltaTime;

			// spawn Manager
			SpawnManager (timePlay);

			// delay increase spawn frequence
			SpawnFrequentManager (curTime);

			// update time Timer
			UpdateTimer_UI ();
		}
	}

	// main logic
	private void InitGame () {
		// init nemuManager ref
		if (!menuManager) {
			menuManager = MenuManager_Basket.Instance;
		}

		// chack playerManager ref
		if (playerManager) {
			// set ref playerData
			playerData = playerManager.DataManager;

			// set can controll for InputControlle
			playerManager.StartPlay ();
		}

		// init soundManager
		if (!soundManager) {
			soundManager = BaseSoundController.Instance;
		}

		// initialize a timer
		theTimer = ScriptableObject.CreateInstance<TimerClass>();
	}

	public override void StartGame() {
		startGame = true;

		// start Timer
		theTimer.StartTimer ();

		// get time
		timePlay = 0.0f;
		lastSpawnTime = timePlay;
		betweaneSpawnTime = timeBetweaneSpawn;
		lastFrequenceSpawnTime = theTimer.GetTime ();
	}

	public void RestartGame() {
		startGame = true;

		// reset Timer
		theTimer.ResetTimer();

		// get time
		timePlay = 0.0f;
		lastSpawnTime = timePlay;
		betweaneSpawnTime = timeBetweaneSpawn;
		lastFrequenceSpawnTime = theTimer.GetTime ();

		// clear progress
		ClearBonus();

		// player in startPos
		playerManager.SetPlayerInStartPos ();
	}

	public void PauseGame() {
		if (Paused) {
			Paused = false;

			theTimer.StartTimer ();
		} else {
			Paused = true;

			theTimer.StopTimer ();
		}
	}

	private void SpawnManager(float time) {
		if (time - lastSpawnTime > betweaneSpawnTime) {
			GameObject objDrop = GetRandomDropObject ();
			Vector3 posDrop = GetRandomDropPossition ();

			SpawnController.Instance.Spawn (objDrop, posDrop, Quaternion.identity);

			lastSpawnTime = time;
		}
	}

	private void SpawnFrequentManager(float time) {
		if (betweaneSpawnTime > timeLimitBetweaneSpawn) {
			if (time - lastFrequenceSpawnTime > timeFrequenceSpawn) {
				betweaneSpawnTime -= timeDecreaceStep;

				lastFrequenceSpawnTime = time;
			}
		}
	}

	private GameObject GetRandomDropObject() {
		return spawnList [Random.Range (0, spawnClip.Length)];
	}

	private Vector3 GetRandomDropPossition() {
		return new Vector3 (Random.Range (spawnClip[0].position.x, spawnClip[1].position.x), spawnClip[0].position.y, spawnClip[0].position.z);
	}

	// Player Data
	public void AddBonus(int val) {
		// add score in Data
		playerData.AddScore (val);

		// update score UI
		Update_UI ();

		// play sound
		if (val > 0) {
			IncreaseScore_Sound ();
		} else {
			DecreaseScore_Sound ();
		}
	}

	private void ClearBonus() {
		// clear score in Data
		playerData.SetScore (0);

		// update score UI
		Update_UI ();
	}

	// menu Manager
	public void Update_UI() {
		UpdateScore_UI();
		UpdateTimer_UI();
	}

	private void UpdateScore_UI() {
		menuManager.UpdateScore (playerData.GetScore ());
	}

	private void UpdateTimer_UI() {
		menuManager.UpdateTimer (theTimer.GetFormattedTime ());
	}

	// sound Manager
	private void IncreaseScore_Sound() {
		soundManager.PlaySoundByIndex (0, Vector3.zero);
	}

	private void DecreaseScore_Sound() {
		soundManager.PlaySoundByIndex (1, Vector3.zero);
	}
}
