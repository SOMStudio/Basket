using UnityEngine;

[AddComponentMenu("SOMStudio/Basket/Game Controller")]
public class GameController : BaseGameController
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

	[System.NonSerialized] public static GameController Instance;

	[Header("Managers")]
	[SerializeField] private MenuManager menuManager;
	[SerializeField] private PlayerManager playerManager;
	[SerializeField] private BaseSoundController soundManager;

	private void Awake()
	{
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
		InitGame();
		
		Update_UI();
		
		StartGame();
	}

	private void Update()
	{
		if (startGame && !Paused)
		{
			theTimer.UpdateTimer();
			int curTime = theTimer.GetTime();
			
			timePlay += Time.deltaTime;
			
			SpawnManager(timePlay);
			
			SpawnFrequentManager(curTime);
			
			UpdateTimer_UI();
		}
	}
	
	private void InitGame()
	{
		if (!menuManager)
		{
			menuManager = MenuManager.Instance;
		}
		
		if (!playerManager)
		{
			playerManager = PlayerManager.Instance;
		}
		
		if (!soundManager)
		{
			soundManager = BaseSoundController.Instance;
		}
		
		theTimer = new TimerClass();
	}

	public override void StartGame()
	{
		startGame = true;
		
		playerManager.StartPlay();
		
		theTimer.StartTimer();
		
		timePlay = 0.0f;
		lastSpawnTime = timePlay;
		betweenSpawnTime = timeBetweenSpawn;
		lastFrequencySpawnTime = theTimer.GetTime();
	}

	public void RestartGame()
	{
		startGame = true;
		
		theTimer.ResetTimer();
		
		timePlay = 0.0f;
		lastSpawnTime = timePlay;
		betweenSpawnTime = timeBetweenSpawn;
		lastFrequencySpawnTime = theTimer.GetTime();
		
		ClearBonus();
		
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
		playerManager.Data.AddScore(val);
		
		Update_UI();
		
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
		playerManager.Data.SetScore(0);
		
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
