using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
	[SerializeField] private int attackDamage = 10;
	[SerializeField] int enragedAttackDamage = 10;
	[SerializeField] private float attackRange = 20;
	[SerializeField] private Vector3 attackOffset;
	[SerializeField] private LayerMask attackMask;

	public void Tongue()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().Damage(attackDamage);
		}
	}
	public void EnragedTongue()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().Damage(enragedAttackDamage);
		}
	}
	private void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;


		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(pos, attackRange);
	}

}
