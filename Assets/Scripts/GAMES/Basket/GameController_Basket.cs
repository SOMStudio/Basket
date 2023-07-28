using UnityEngine;

public class GameController_Basket : BaseGameController
{
	[Header("Main Settings")]
	[SerializeField] private GameObject[] spawnList;

	[SerializeField] private Transform[] spawnClip;
	[SerializeField] private float timeBetweenSpawn = 2.0f;
	[SerializeField] private float timeFrequencySpawn = 10;
	[SerializeField] private float timeDecreaseStep = 0.2f;
	[SerializeField] private float timeLimitBetweenSpawn = 0.5f;

	[SerializeField] private bool startGame = false;
	
	private float timePlay;
	private float betweenSpawnTime = 2.0f;
	private float lastSpawnTime;
	private float lastFrequencySpawnTime;

	private TimerClass theTimer;

	[System.NonSerialized] public static GameController_Basket Instance;

	[Header("Managers")]
	[SerializeField] private MenuManager_Basket menuManager;
	[SerializeField] private PlayerManager_Basket playerManager;
	[SerializeField] private BaseSoundController soundManager;

	private void Awake()
	{
		// activate instance
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		// init game
		InitGame();

		// create Menu
		Update_UI();

		// start game
		StartGame();
	}

	private void Update()
	{
		if (startGame && !Paused)
		{
			// global time for frequency spawn
			theTimer.UpdateTimer();
			int curTime = theTimer.GetTime();

			// time for Spawn
			timePlay += Time.deltaTime;

			// spawn Manager
			SpawnManager(timePlay);

			// delay increase spawn frequency
			SpawnFrequentManager(curTime);

			// update time Timer
			UpdateTimer_UI();
		}
	}
	
	private void InitGame()
	{
		// init menuManager ref
		if (!menuManager)
		{
			menuManager = MenuManager_Basket.Instance;
		}

		// check playerManager ref
		if (!playerManager)
		{
			playerManager = PlayerManager_Basket.Instance;
		}

		// init soundManager
		if (!soundManager)
		{
			soundManager = BaseSoundController.Instance;
		}

		// initialize a timer
		theTimer = ScriptableObject.CreateInstance<TimerClass>();
	}

	public override void StartGame()
	{
		startGame = true;

		// set can control for InputController
		playerManager.StartPlay();

		// start Timer
		theTimer.StartTimer();

		// get time
		timePlay = 0.0f;
		lastSpawnTime = timePlay;
		betweenSpawnTime = timeBetweenSpawn;
		lastFrequencySpawnTime = theTimer.GetTime();
	}

	public void RestartGame()
	{
		startGame = true;

		// reset Timer
		theTimer.ResetTimer();

		// get time
		timePlay = 0.0f;
		lastSpawnTime = timePlay;
		betweenSpawnTime = timeBetweenSpawn;
		lastFrequencySpawnTime = theTimer.GetTime();

		// clear progress
		ClearBonus();

		// player in startPos
		playerManager.SetPlayerInStartPos();
	}

	public void PauseGame()
	{
		if (Paused)
		{
			Paused = false;

			theTimer.StartTimer();
		}
		else
		{
			Paused = true;

			theTimer.StopTimer();
		}
	}

	private void SpawnManager(float time)
	{
		if (time - lastSpawnTime > betweenSpawnTime)
		{
			GameObject objDrop = GetRandomDropObject();
			Vector3 posDrop = GetRandomDropPosition();

			SpawnController.Instance.Spawn(objDrop, posDrop, Quaternion.identity);

			lastSpawnTime = time;
		}
	}

	private void SpawnFrequentManager(float time)
	{
		if (betweenSpawnTime > timeLimitBetweenSpawn)
		{
			if (time - lastFrequencySpawnTime > timeFrequencySpawn)
			{
				betweenSpawnTime -= timeDecreaseStep;

				lastFrequencySpawnTime = time;
			}
		}
	}

	private GameObject GetRandomDropObject()
	{
		return spawnList[Random.Range(0, spawnClip.Length)];
	}

	private Vector3 GetRandomDropPosition()
	{
		return new Vector3(Random.Range(spawnClip[0].position.x, spawnClip[1].position.x), spawnClip[0].position.y,
			spawnClip[0].position.z);
	}
	
	public void AddBonus(int val)
	{
		// add score in Data
		playerManager.Data.AddScore(val);

		// update score UI
		Update_UI();

		// play sound
		if (val > 0)
		{
			IncreaseScore_Sound();
		}
		else
		{
			DecreaseScore_Sound();
		}
	}

	private void ClearBonus()
	{
		// clear score in Data
		playerManager.Data.SetScore(0);

		// update score UI
		Update_UI();
	}
	
	private void Update_UI()
	{
		UpdateScore_UI();
		UpdateTimer_UI();
	}

	private void UpdateScore_UI()
	{
		menuManager.UpdateScore(playerManager.Data.GetScore());
	}

	private void UpdateTimer_UI()
	{
		menuManager.UpdateTimer(theTimer.GetFormattedTime());
	}
	
	private void IncreaseScore_Sound()
	{
		soundManager.PlaySoundByIndex(0, Vector3.zero);
	}

	private void DecreaseScore_Sound()
	{
		soundManager.PlaySoundByIndex(1, Vector3.zero);
	}
}
