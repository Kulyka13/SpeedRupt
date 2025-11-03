using System.Collections;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
	[Header("Enemy")]
	[SerializeField] private Animator animator;
	public bool giveUpwardForce = true;
	private bool isDead = false;

	private void Awake()
	{
		if (animator == null)
			animator = GetComponent<Animator>();
	}

	public new void Damage(int amount)
	{
		if (isDead) return;

		base.Damage(amount);

		if (currentHealth > 0)
		{
			if (animator != null)
				animator.SetTrigger("Hurt");
		}
		else
		{
			if (animator != null)
				animator.SetTrigger("Death");

			isDead = true;
			StartCoroutine(HandleDeath());
		}
	}

	private IEnumerator HandleDeath()
	{
		yield return new WaitForSeconds(0.5f); 
		gameObject.SetActive(false);
	}
}
