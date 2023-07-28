using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Input_Rotate2Way : BaseInputController, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	[Header("Rotate Clump")]
	public float rotateAngleClump = 25f;

	private Transform myTransform;
	private Vector2 startPosition;
	
	private void Start()
	{
		myTransform = transform;

		startPosition = new Vector2(myTransform.position.x, myTransform.position.y);
	}

	public void OnPointerDown(PointerEventData data)
	{

	}

	public void OnDrag(PointerEventData data)
	{
		Vector2 vectorToPoint = data.position - startPosition;
		Vector2 posNew = new Vector2(Mathf.Abs(vectorToPoint.x), Mathf.Abs(vectorToPoint.y));

		int crosX = vectorToPoint.x > 0 ? 1 : -1;
		int crosY = vectorToPoint.y > 0 ? 1 : -1;
		float angle = Vector2.Angle(Vector2.right, posNew);

		if (angle > rotateAngleClump)
		{
			angle = rotateAngleClump;
		}

		float angleRes = crosX * crosY * angle;
		
		horz = angleRes / rotateAngleClump * -1;
		
		myTransform.eulerAngles = new Vector3(0, 0, crosX * crosY * angle);
	}

	public void OnPointerUp(PointerEventData data)
	{
		myTransform.eulerAngles = Vector3.zero;
		
		horz = 0f;
	}
}
