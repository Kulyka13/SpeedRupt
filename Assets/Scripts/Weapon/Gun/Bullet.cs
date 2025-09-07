using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}
