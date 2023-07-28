using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Utility/FPS counter")]
public class FPSCounter : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private Text m_Text;
	[SerializeField] private Text min_Text;
	[SerializeField] private Text max_Text;

	private const float FPSMeasurePeriod = 0.5f;
	private int mFpsAccumulator = 0;
	private float mFpsNextPeriod = 0;
	private int mCurrentFps;
	private int minFPS = -1;
	private int maxFPS = -1;
	
	private void Start()
	{
		mFpsNextPeriod = Time.realtimeSinceStartup + FPSMeasurePeriod;
	}

	private void Update()
	{
		mFpsAccumulator++;
		if (Time.realtimeSinceStartup > mFpsNextPeriod)
		{
			mCurrentFps = (int)(mFpsAccumulator / FPSMeasurePeriod);

			if (Time.realtimeSinceStartup > 20)
			{
				if (minFPS == -1)
				{
					minFPS = mCurrentFps;
					maxFPS = mCurrentFps;
				}
				else
				{
					if (minFPS > mCurrentFps)
						minFPS = mCurrentFps;
					if (maxFPS < mCurrentFps)
						maxFPS = mCurrentFps;
				}
			}

			mFpsAccumulator = 0;
			mFpsNextPeriod += FPSMeasurePeriod;

			if (m_Text != null)
			{
				m_Text.text = $"FPS:{mCurrentFps}";
				min_Text.text = $"minFPS:{minFPS}";
				max_Text.text = $"maxFPS:{maxFPS}";
			}
		}
	}
}
