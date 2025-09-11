using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MeleeWeapon : MonoBehaviour
{
	[SerializeField] private int damageAmount = 20;
	private float invulnerabilityTime = .2f;
	private PlayerMovement character;
	private Rigidbody2D rb;
	private MeleeAttackManager meleeAttackManager;
	private Vector2 direction;
	private bool collided;
	private bool downwardStrike;
	//private bool hit;
	private void Start()
	{
		character = GetComponentInParent<PlayerMovement>();
		rb = GetComponentInParent<Rigidbody2D>();
		meleeAttackManager = GetComponentInParent<MeleeAttackManager>();
	}

	private void FixedUpdate()
	{
		HandleMovement();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<EnemyHealth>())
		{
			HandleCollision(collision.GetComponent<EnemyHealth>());
		}
	}

	private void HandleCollision(EnemyHealth objHealth)
	{
		// Отримуємо напрямок атаки з MeleeAttackManager
		Vector2 attackDirection = meleeAttackManager.currentAttackDirection;

		// Логіка відскоку: спрацює, якщо удар був спрямований вниз (по Y < 0)
		if (objHealth.giveUpwardForce && attackDirection.y < 0 && !character.IsGrounded)
		{
			direction = Vector2.up;
			downwardStrike = true;
			collided = true;
		}

		// Інша логіка для інших напрямків
		else if (attackDirection.y > 0 && !character.IsGrounded)
		{
			direction = Vector2.down;
			collided = true;
		}
		else
		{
			if (transform.parent.localScale.x < 0)
			{
				direction = Vector2.right;
			}
			else
			{
				direction = Vector2.left;
			}
			collided = true;
		}

		objHealth.Damage(damageAmount);
		StartCoroutine(NoLongerColliding());
	}

	private void HandleMovement()
	{
		if (collided)
		{
			if (downwardStrike)
			{
				rb.AddForce(direction * meleeAttackManager.upwardsForce);
			}
			else
			{
				rb.AddForce(direction * meleeAttackManager.defaultForce);
			}
		}
	}

	private IEnumerator NoLongerColliding()
	{
		yield return new WaitForSeconds(meleeAttackManager.movementTime);
		collided = false;
		downwardStrike = false;
	}
	/*
	private IEnumerator TurnOffHit()
	{
		yield return new WaitForSeconds(invulnerabilityTime);
		hit = false;
	}
	*/
}