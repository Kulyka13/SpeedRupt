using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BounceEnemyAI : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f;
	private Rigidbody2D rb;
	private Vector2 direction;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		rb.freezeRotation = true;
		rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

		// Початковий напрямок — по діагоналі
		direction = new Vector2(1f, 1f).normalized;
		rb.velocity = direction * moveSpeed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.contacts.Length > 0)
		{
			Vector2 normal = collision.contacts[0].normal;
			direction = Vector2.Reflect(direction, normal).normalized;

			rb.velocity = direction * moveSpeed;

		}
	}
}
