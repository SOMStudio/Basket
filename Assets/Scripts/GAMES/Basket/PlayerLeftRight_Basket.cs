using UnityEngine;

public class PlayerLeftRight_Basket : ExtendedCustomMonoBehaviour2D {

	[Header("Main")]
	public float speedMove = 20f;

	public Transform clipingLeftPos;
	public Transform clipingRightPos;

	private Vector3 myStartPossition;

	[Header("Input Controller")]
	public UI_Input_Rotate2Way inputController;

	// main event
	void Awake() {
		base.Init ();
	}

	void Start () {
		if (!inputController || !clipingLeftPos || !clipingRightPos)
			didInit = false;

		if (myTransform) {
			myStartPossition = myTransform.position;
		}
	}

	void Update () {
		if (canControl && didInit) {
			float horMove = inputController.GetHorizontal ();
			if (Mathf.Abs (horMove) > 0) {
				Vector3 newPos = myTransform.position + new Vector3(horMove * speedMove * Time.deltaTime, 0, 0);

				newPos.x = Mathf.Clamp (newPos.x, clipingLeftPos.position.x, clipingRightPos.position.x);

				myTransform.position = newPos;
			}
		}
	}

	// main logic
	public void CanControll(bool val) {
		canControl = val;
	}

	public void SetInStartPos() {
		myTransform.position = myStartPossition;
	}
}
