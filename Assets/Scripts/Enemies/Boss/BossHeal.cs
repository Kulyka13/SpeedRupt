using UnityEngine;

public class BossHealAction : MonoBehaviour
{
	[SerializeField] private int healAmount = 300;

	private BossHealth bossHealth;

	private void Start()
	{
		bossHealth = GetComponent<BossHealth>();
	}

	public void HealBoss()
	{
		bossHealth.currentHealth = Mathf.Min(
			bossHealth.currentHealth + healAmount,
			bossHealth.healthAmount
		);
	}
}
