using UnityEngine;

[AddComponentMenu("SOMStudio/Basket/Drop Object Manager")]
public class DropObjectManager : MonoBehaviour
{
	[SerializeField] private int bonus = 0;

	private void Start()
	{
		Destroy(gameObject, 2);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			GameController.Instance.AddBonus(bonus);

			Destroy(gameObject);
		}
	}
}
