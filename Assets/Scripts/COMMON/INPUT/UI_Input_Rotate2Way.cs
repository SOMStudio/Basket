using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Input_Rotate2Way : BaseInputController, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	[Header("Rotate Clump")]
	public float rotateAngleClump = 25f;

	private Transform myTransform;
	private Vector2 startPossition;

	// main event
	void Start () {
		myTransform = transform;

		startPossition = new Vector2(myTransform.position.x, myTransform.position.y);
	}

	public void OnPointerDown (PointerEventData data) {
		
	}

	public void OnDrag(PointerEventData data) {
		Vector2 vectToPoint = data.position - startPossition;
		Vector2 posNew = new Vector2 (Mathf.Abs(vectToPoint.x), Mathf.Abs(vectToPoint.y));

		int crosX = vectToPoint.x > 0 ? 1 : -1;
		int crosY = vectToPoint.y > 0 ? 1 : -1;
		float angle = Vector2.Angle (Vector2.right, posNew);

		if (angle > rotateAngleClump) {
			angle = rotateAngleClump;
		}

		float angleRes = crosX * crosY * angle;

		// set horz
		horz = angleRes / rotateAngleClump * -1;

		// set up some boolean values left and right
		Left	= ( horz<0 );
		Right	= ( horz>0 );

		// rotate object
		myTransform.eulerAngles = new Vector3(0, 0, crosX * crosY * angle);
	}

	public void OnPointerUp (PointerEventData data) {
		myTransform.eulerAngles = Vector3.zero;

		// set horz
		horz = 0f;

		// set up some boolean values for left and right
		Left	= ( horz<0 );
		Right	= ( horz>0 );
	}
}
