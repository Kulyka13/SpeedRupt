using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
	[SerializeField] private int damageAmount = 20;
	[SerializeField] private float lifetime = 0.3f;

	private void Start()
	{
		Destroy(gameObject, lifetime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
		if (enemy != null)
		{
			enemy.Damage(damageAmount);
		}
	}
}
