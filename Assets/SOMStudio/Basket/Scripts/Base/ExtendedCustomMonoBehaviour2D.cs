using UnityEngine;

namespace SOMStudio.Basket.Scripts.Base
{
	public class ExtendedCustomMonoBehaviour2D : MonoBehaviour
	{
		[Header("Base")]
		[SerializeField] protected bool didInit;
		[SerializeField] protected bool canControl;

		protected int id;

		protected Transform myTransform;
		protected GameObject myGameObject;
		protected Rigidbody2D myBody;

		private void Start()
		{
			Init();
		}

		protected virtual void Init()
		{
			if (!myTransform)
			{
				myTransform = transform;
			}

			if (!myGameObject)
			{
				myGameObject = gameObject;
			}

			if (!myBody)
			{
				myBody = GetComponent<Rigidbody2D>();
			}

			didInit = true;
		}

		public virtual void SetID(int anID)
		{
			id = anID;
		}
	}
}
