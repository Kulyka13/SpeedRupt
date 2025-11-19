using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
	[SerializeField] private int tongueDamage = 10;
	[SerializeField] private int enragedTongueDamage = 10;
	[SerializeField] private int spitDamage = 20;
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
			colInfo.GetComponent<PlayerHealth>().Damage(tongueDamage);
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
			colInfo.GetComponent<PlayerHealth>().Damage(enragedTongueDamage);
		}
	}
	public void Spit()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().Damage(spitDamage);
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
