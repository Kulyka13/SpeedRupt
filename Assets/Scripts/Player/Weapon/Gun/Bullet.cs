using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private int damageAmount = 20;
	[SerializeField] private GameObject explosionPrefab;
	[SerializeField] private float explosionRadius = 3f;
	[SerializeField] private float explosionForce = 15f;

	private Rigidbody2D rb => GetComponent<Rigidbody2D>();

	private void Update()
	{
		transform.right = rb.velocity;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Explode();
	}

	private void Explode()
	{
		// —творюЇмо в≥зуальний ефект вибуху
		GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(explosionInstance, 0.5f);

		// «находимо вс≥ об'Їкти в рад≥ус≥ вибуху
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

		foreach (Collider2D hit in hits)
		{
			// якщо це ворог Ч отримуЇ урон
			EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
			if (enemy != null)
			{
				enemy.Damage(damageAmount);
			}

			// якщо це гравець Ч отримуЇ вибухову силу (rocket jump)
			if (hit.CompareTag("Player"))
			{
				Rigidbody2D playerRb = hit.attachedRigidbody;
				if (playerRb != null)
				{
					Vector2 direction = (hit.transform.position - transform.position).normalized;
					float distance = Vector2.Distance(transform.position, hit.transform.position);
					float forceMultiplier = Mathf.Clamp01(1f - distance / explosionRadius); // ближче Ч сильн≥ше
					playerRb.AddForce(direction * explosionForce * forceMultiplier, ForceMode2D.Impulse);
				}
			}
		}

		Destroy(gameObject);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
