using System.Collections;
using UnityEngine;

public class BossHealth : EnemyHealth
{
	[Header("Boss")]
	public int enrageHealth;
	[SerializeField] private GameObject winUI;
	[SerializeField] private GameObject[] objectsToDisable;

	public override void Damage(int amount)
	{
		if (damageable && !hit && currentHealth > 0)
		{
			hit = true;
			currentHealth -= amount;
            if (healthBar != null)
                healthBar.fillAmount = Mathf.Clamp(currentHealth / healthAmount, 0, 1);
            if (currentHealth < enrageHealth)
			{
				GetComponent<Animator>().SetBool("IsEnraged", true);
			}

			if (currentHealth <= 0)
			{
				currentHealth = 0;
				winUI.SetActive(true);

                foreach (var obj in objectsToDisable)
                    if (obj != null) obj.SetActive(false);

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

		*/
	//}
}
