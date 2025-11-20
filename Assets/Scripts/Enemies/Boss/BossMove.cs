using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : StateMachineBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
	[SerializeField] private string[] attackTypes;
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;
    private string triggerName;
    private Transform player;
    private Rigidbody2D rb;
    private BossFlipping bossFlipping;
	private BossHealth bossHealth;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		if (playerObj != null)
			player = playerObj.transform;

		rb = animator.GetComponent<Rigidbody2D>();
		bossFlipping = animator.GetComponent<BossFlipping>();
		bossHealth = animator.GetComponent<BossHealth>();
        lastAttackTime = -Mathf.Infinity;
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		bossFlipping.LookAtPlayer();

		Vector2 target = new Vector2(player.position.x, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
		rb.MovePosition(newPos);
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            string triggerName = attackTypes[Random.Range(0, attackTypes.Length)];
            animator.SetTrigger(triggerName);

            lastAttackTime = Time.time;
        }
    }


	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){}
}
