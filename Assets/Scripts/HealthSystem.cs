using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
	public float healthAmount = 100f;
	public float currentHealth;
	[SerializeField] protected bool damageable = true;
	[SerializeField] protected float invulnerabilityTime = 0.2f;
	[SerializeField] private Color colorInvulnerability = Color.red;
	[SerializeField] private float blinkInterval = 0.05f; // частота миготіння
	[SerializeField] private Image healthBar;
	protected bool hit;

	private SpriteRenderer spriteRenderer;
	private Color originalColor;


	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
			originalColor = spriteRenderer.color;
	}


	private void Start()
	{
		currentHealth = healthAmount;
	}
	public virtual void Damage(int amount)
	{
		if (damageable && !hit && currentHealth > 0)
		{
			hit = true;
			currentHealth -= amount;
			if (healthBar != null)
				healthBar.fillAmount = Mathf.Clamp(currentHealth / healthAmount, 0, 1);
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

	protected IEnumerator InvulnerabilityBlink()
	{
		float elapsed = 0f;
		bool visible = true;

		while (elapsed < invulnerabilityTime)
		{
			if (spriteRenderer != null)
			{
				spriteRenderer.color = visible ? colorInvulnerability : originalColor;
			}

			visible = !visible;
			yield return new WaitForSeconds(blinkInterval);
			elapsed += blinkInterval;
		}

		spriteRenderer.color = originalColor;
		hit = false;
	}
}
