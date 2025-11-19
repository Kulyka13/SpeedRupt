using UnityEngine;

public class BossHealCheck : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float healCheckRange = 18f;
	[SerializeField] private float healDelay = 4f;

	[Header("Max HP Boss")]
	[SerializeField] private float maxBossHP = 4000f;

	private BossHealth bossHealth;
	private Transform player;
	private Animator animator;

	private float timeOutOfRange = 0f;

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
			timeOutOfRange = 0;

		if (timeOutOfRange >= healDelay &&
			bossHealth.currentHealth <= maxBossHP * 0.25f &&
			!IsBusy())
		{
			animator.SetTrigger("Heal");
		}
	}

	private bool IsBusy()
	{
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

		return state.IsTag("Tongue") ||
			   state.IsTag("Spit") ||
			   state.IsTag("EnragedTongue") ||
			   state.IsTag("Special");
	}
}
