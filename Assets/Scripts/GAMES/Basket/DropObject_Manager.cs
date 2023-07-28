using UnityEngine;

public class DropObject_Manager : MonoBehaviour
{
	[SerializeField] private int bonus = 0;

	private void Start()
	{
		Destroy(gameObject, 2);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			GameController_Basket.Instance.AddBonus(bonus);

			Destroy(gameObject);
		}
	}
}
