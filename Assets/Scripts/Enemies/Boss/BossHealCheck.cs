using UnityEngine;

public class BossHealCheck : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float healCheckRange = 18f;
	[SerializeField] private float healDelay = 4f;
	[SerializeField] private float healCooldown = 10f;
	[SerializeField] private float maxBossHP = 2000f;

	private int healAmount = 250	; 
	private BossHealth bossHealth;
	private Transform player;
	private Animator animator;

	private float timeOutOfRange = 0f;
	private float lastHealTime = -Mathf.Infinity;

	private void Start()
	{
		bossHealth = GetComponent<BossHealth>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		float dist = Vector2.Distance(transform.position, player.position);

		if (dist > healCheckRange)
			timeOutOfRange += Time.deltaTime;
		else
			timeOutOfRange = 0f;

		bool canHeal = Time.time - lastHealTime >= healCooldown;

		if (timeOutOfRange >= healDelay &&
			bossHealth.currentHealth <= (maxBossHP * 0.5f)-healAmount &&
			!IsBusy() &&
			canHeal)
		{
			animator.SetTrigger("Heal");
			lastHealTime = Time.time; 
		}
	}

	private bool IsBusy()
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

		return state.IsTag("Tongue") ||
			   state.IsTag("Spit") ||
			   state.IsTag("EnragedTongue") ||
			   state.IsTag("Special") ||
			   state.IsTag("Heal"); 
	}

	public void OnHealAnimationEvent()
	{
		bossHealth.Heal(healAmount);
	}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healCheckRange);
    }
}
