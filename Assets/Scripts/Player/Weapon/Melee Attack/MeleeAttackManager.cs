using UnityEngine;
using System.Linq;

public class MeleeAttackManager : MonoBehaviour
{
	public float defaultForce = 300;
	public float upwardsForce = 600;
	public float movementTime = .1f;
	public Vector2 currentAttackDirection { get; private set; }

	private Animator meleeAnimator;
	private PlayerMovement character;

	private void Start()
	{
		character = GetComponent<PlayerMovement>();
		meleeAnimator = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("MeleeAttack"))
		{
			DetermineAndTriggerMeleeAttack();
		}
	}

	private void DetermineAndTriggerMeleeAttack()
	{
		Vector2 attackDirection = Vector2.zero;
		bool isGamepadConnected = Input.GetJoystickNames().Length > 0;

		if (isGamepadConnected)
		{
			float horizontalInput = Input.GetAxis("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");
			attackDirection = new Vector2(horizontalInput, verticalInput).normalized;
		}
		else
		{
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseWorldPosition.z = 0;
			attackDirection = (mouseWorldPosition - transform.position).normalized;
		}

		currentAttackDirection = attackDirection;

		character.FaceDirection(attackDirection.x);

		if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y))
		{
			meleeAnimator.SetTrigger("ForwardMeleeSwipe");
		}
		else
		{
			if (attackDirection.y > 0)
				meleeAnimator.SetTrigger("UpwardMeleeSwipe");
			else
				meleeAnimator.SetTrigger("DownwardMeleeSwipe");
		}
	}
}
