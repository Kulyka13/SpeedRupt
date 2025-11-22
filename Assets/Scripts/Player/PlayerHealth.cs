using Unity.VisualScripting;
using UnityEngine;
public class PlayerHealth : HealthSystem
{
	[SerializeField] private float damageInterval = 1f;
	private float lastDamageTime = 0f;
    private void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("DamageCollider") || collision.CompareTag("Target"))
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
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Heal"))
		{
			Heal healObj = collision.GetComponent<Heal>();
			Heal(healObj.healAmount);
			Destroy(collision.gameObject);
        }
    }
}

