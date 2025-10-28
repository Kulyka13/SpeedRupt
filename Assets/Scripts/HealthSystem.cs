using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class HealthSystem : MonoBehaviour
{
	[SerializeField] protected bool damageable = true;
	[SerializeField] protected float healthAmount = 100f;
	[SerializeField] protected float invulnerabilityTime = 0.2f;
	[SerializeField] private Color colorInvulnerability = Color.red;
	[SerializeField] private float blinkInterval = 0.05f; // частота миготіння
	[SerializeField] private Image healthBar;
	protected bool hit;
	protected float currentHealth;

	private SpriteRenderer spriteRenderer;
	private Color originalColor;


	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalColor = spriteRenderer.color;
	}

	private void Start()
	{
		currentHealth = healthAmount;
	}
	private void Update()
	{
		if (healthBar != null)
			healthBar.fillAmount = Mathf.Clamp(currentHealth / healthAmount, 0, 1);
	}

	public void Damage(int amount)
	{
		if (damageable && !hit && currentHealth > 0)
		{
			hit = true;
			currentHealth -= amount;

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

	private IEnumerator InvulnerabilityBlink()
	{
		float elapsed = 0f;
		bool visible = true;

		while (elapsed < invulnerabilityTime)
		{
			spriteRenderer.color = visible ? colorInvulnerability : originalColor;
			visible = !visible;
			yield return new WaitForSeconds(blinkInterval);
			elapsed += blinkInterval;
		}

		spriteRenderer.color = originalColor;
		hit = false;
	}
}
