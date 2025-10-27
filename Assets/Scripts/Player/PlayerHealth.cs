using UnityEngine;

public class PlayerHealth : HealthSystem
{
	[SerializeField] private float damageInterval = 1f;
	private float lastDamageTime = 0f;
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("DamageCollider"))
		{
			EnemyDamage enemy = collision.GetComponent<EnemyDamage>();
			if (enemy == null)
				enemy = collision.GetComponentInParent<EnemyDamage>();

			if (enemy != null && Time.time >= lastDamageTime + damageInterval)
			{
				Damage(enemy.damage);
				lastDamageTime = Time.time;
			}
		}
	}

}

