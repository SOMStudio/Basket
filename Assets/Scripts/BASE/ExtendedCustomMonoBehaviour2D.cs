using UnityEngine;

public class ExtendedCustomMonoBehaviour2D : MonoBehaviour 
{
	// This class is used to add some common variables to MonoBehaviour, rather than
	// constantly repeating the same declarations in every class.
	[Header("Base")]
	public Transform myTransform;
	public GameObject myGO;
	public Rigidbody2D myBody;
	
	public bool didInit;
	public bool canControl;
	
	public int id;
	
	[System.NonSerialized]
	public Vector3 tempVEC;
	
	[System.NonSerialized]
	public Transform tempTR;

	public virtual void Init() {
		// cache refs to our transform and gameObject
		if (!myTransform) {
			myTransform = transform;
		}
		if (!myGO) {
			myGO = gameObject;
		}
		if (!myBody) {
			myBody = GetComponent<Rigidbody2D> ();
		}

		didInit = true;
	}

	public virtual void SetID( int anID )
	{
		id= anID;
	}
}
