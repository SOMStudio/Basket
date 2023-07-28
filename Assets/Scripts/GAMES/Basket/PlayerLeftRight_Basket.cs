using UnityEngine;

public class PlayerLeftRight_Basket : ExtendedCustomMonoBehaviour2D
{
	[Header("Main")]
	[SerializeField] private float speedMove = 20f;

	[SerializeField] private Transform clippingLeftPos;
	[SerializeField] private Transform clippingRightPos;

	private Vector3 myStartPosition;

	[Header("Input Controller")]
	[SerializeField] private UI_Input_Rotate2Way inputController;
	
	private void Awake()
	{
		base.Init();
	}

	private void Start()
	{
		if (!inputController || !clippingLeftPos || !clippingRightPos)
			didInit = false;

		if (myTransform)
		{
			myStartPosition = myTransform.position;
		}
	}

	private void Update()
	{
		if (canControl && didInit)
		{
			float horMove = inputController.GetHorizontal();
			if (Mathf.Abs(horMove) > 0)
			{
				Vector3 newPos = myTransform.position + new Vector3(horMove * speedMove * Time.deltaTime, 0, 0);

				newPos.x = Mathf.Clamp(newPos.x, clippingLeftPos.position.x, clippingRightPos.position.x);

				myTransform.position = newPos;
			}
		}
	}
	
	public void CanControl(bool val)
	{
		canControl = val;
	}

	public void SetInStartPos()
	{
		myTransform.position = myStartPosition;
	}
}
