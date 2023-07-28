using UnityEngine;
using UnityEngine.UI;

public class MenuManager_Basket : MonoBehaviour
{
	[Header("Game Settings")]
	[SerializeField] private Text textTimer;

	[SerializeField] private Text textScore;

	[Header("Game Controller Ref")]
	[SerializeField] private GameController_Basket gameController;

	[System.NonSerialized] public static MenuManager_Basket Instance;

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
		if (!gameController)
		{
			gameController = GameController_Basket.Instance;
		}
	}

	private void LateUpdate()
	{
		if (gameController)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				gameController.PauseGame();
			}
		}
	}
	
	public void UpdateScore(int val)
	{
		if (textScore)
		{
			textScore.text = val.ToString();
		}
	}

	public void UpdateTimer(string val)
	{
		if (textTimer)
		{
			textTimer.text = val;
		}
	}

	public void RestartGame()
	{
		if (gameController)
		{
			gameController.RestartGame();
		}
	}
}
