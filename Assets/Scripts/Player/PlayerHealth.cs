using UnityEngine;

public class PlayerHealth : HealthSystem
{
	[SerializeField] private float damageInterval = 1f;
	private float lastDamageTime = 0f;

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			EnemyDamage enemy = collision.gameObject.GetComponent<EnemyDamage>();
			if (enemy != null && Time.time >= lastDamageTime + damageInterval)
			{
				Damage(enemy.damage);
				lastDamageTime = Time.time;
			}
		}
	}
}

