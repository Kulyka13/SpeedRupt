using UnityEngine;

public class Bullet : MonoBehaviour
{
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
		// Створюємо вибух
		GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

		// Відкидання гравця у радіусі вибуху
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
		foreach (Collider2D hit in hits)
		{
			if (hit.CompareTag("Player"))
			{
				Rigidbody2D playerRb = hit.attachedRigidbody;
				if (playerRb != null)
				{
					Vector2 direction = (hit.transform.position - transform.position).normalized;
					float distance = Vector2.Distance(transform.position, hit.transform.position);
					float forceMultiplier = Mathf.Clamp01(1f - distance / explosionRadius);
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
