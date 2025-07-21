using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MeleeAttackManager : MonoBehaviour
{
	public float defaultForce = 300;
	public float upwardsForce = 600;
	public float movementTime = .1f;
	private bool meleeAttack;
	private Animator meleeAnimator;
	private Animator anim;
	private PlayerMovement character;
	private void Start()
	{
		anim = GetComponent<Animator>();
		character = GetComponent<PlayerMovement>();
		meleeAnimator = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
	}
	private void Update()
	{
		CheckInput();
	}
	private void CheckInput()
	{
		if (Input.GetButton("MeleeAttack"))
		{
			meleeAttack = true;
		}
		else
		{
			meleeAttack = false;
		}
		if (meleeAttack && Input.GetAxis("Vertical") > 0)
		{
			//anim.SetTrigger("UpwardMelee");
			meleeAnimator.SetTrigger("UpwardMeleeSwipe");
		}
		if (meleeAttack && Input.GetAxis("Vertical") < 0 && !character.IsGrounded)
		{
			//anim.SetTrigger("DownwardMelee");
			meleeAnimator.SetTrigger("DownwardMeleeSwipe");
		}
		if ((meleeAttack && Input.GetAxis("Vertical") == 0) || meleeAttack && (Input.GetAxis("Vertical") < 0 && character.IsGrounded))
		{
			//anim.SetTrigger("ForwardMelee");
			meleeAnimator.SetTrigger("ForwardMeleeSwipe");
		}
	}
}
