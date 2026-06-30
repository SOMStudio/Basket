using UnityEngine;
using UnityEngine.UI;

namespace SOMStudio.Basket.Scripts.Utility
{
	[AddComponentMenu("SOMStudio/Basket/Utility/FPS Counter")]
	public class FpsCounter : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField] private Text fpsText;
		[SerializeField] private Text minFpsText;
		[SerializeField] private Text maxFpsText;

		private const float FPSMeasurePeriod = 0.5f;
		private int mFpsAccumulator;
		private float mFpsNextPeriod;
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

				if (fpsText != null)
				{
					fpsText.text = $"FPS:{mCurrentFps}";
					minFpsText.text = $"minFPS:{minFPS}";
					maxFpsText.text = $"maxFPS:{maxFPS}";
				}
			}
		}
	}
}
