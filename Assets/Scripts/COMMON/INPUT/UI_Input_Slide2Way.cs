using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Input_Slide2Way : BaseInputController, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	[Header("Slide Clump")]
	public float slideClump = 100f;

	private Transform myTransform;
	private Vector2 shiftClick;
	private Vector2 startPossition;

	// main event
	void Start () {
		myTransform = transform;

		startPossition = new Vector2(myTransform.position.x, myTransform.position.y);
	}

	public void OnPointerDown (PointerEventData data) {
		shiftClick = data.position - startPossition;
	}

	public void OnDrag(PointerEventData data) {
		float distanceToPoint = Mathf.Clamp (data.position.x - startPossition.x, -slideClump, slideClump);

		// set horz
		horz = distanceToPoint / slideClump;

		// rotate object
		myTransform.position = new Vector3 (startPossition.x + distanceToPoint, myTransform.position.y, myTransform.position.z);
	}

	public void OnPointerUp (PointerEventData data) {
		myTransform.position = new Vector3(startPossition.x, myTransform.position.y, myTransform.position.z);

		// set horz
		horz = 0f;
	}
}
