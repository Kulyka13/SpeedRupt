using System.Collections;
using UnityEngine;

public class BossHealth : EnemyHealth
{
	[Header("Boss")]
	public int enrageHealth;

	public override void Damage(int amount)
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
	/*
	public void Heal(int amount)
	{
		currentHealth += amount;
		if (currentHealth > healthAmount) currentHealth = healthAmount;

		// Оновлюємо health bar якщо він є
		/*
		if (GetComponent<HealthSystem>() != null && GetComponent<HealthSystem>().healthBar != null)
		{
			GetComponent<HealthSystem>().healthBar.fillAmount = Mathf.Clamp(currentHealth / healthAmount, 0, 1);
		}
		*/
	//}
}
