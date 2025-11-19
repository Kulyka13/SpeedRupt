using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
	[Header("Boss")]
	[SerializeField] private int enrageHealth;
	public new void Damage(int amount)
	{
		if (damageable && !hit && currentHealth > 0)
		{
			hit = true;
			currentHealth -= amount;
			if (currentHealth < enrageHealth)
			{
				GetComponent<Animator>().SetBool("IsEnraged", true);
			}
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				gameObject.SetActive(false);
			}
			else
			{
				StartCoroutine(InvulnerabilityBlink());
			}
		}
	}
}
