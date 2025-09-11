using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private int damageAmount = 20;
	private Rigidbody2D rb => GetComponent<Rigidbody2D>();
	private void Update() => transform.right = rb.velocity;
	/*
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Target")
		{
			Destroy(gameObject);
			Destroy(collision.gameObject);
		}
	}
	*/
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<EnemyHealth>())
		{
			HandleCollision(collision.GetComponent<EnemyHealth>());
		}
		Destroy(gameObject);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
	
	private void HandleCollision(EnemyHealth objHealth)
	{
		objHealth.Damage(damageAmount);
	}

}
