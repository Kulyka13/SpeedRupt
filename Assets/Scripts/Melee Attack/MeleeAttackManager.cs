using UnityEngine;

public class MeleeAttackManager : MonoBehaviour
{
	public float defaultForce = 300;
	public float upwardsForce = 600;
	public float movementTime = .1f;

	private Animator meleeAnimator;
	private PlayerMovement character;

	private void Start()
	{
		character = GetComponent<PlayerMovement>();
		meleeAnimator = GetComponentInChildren<MeleeWeapon>().gameObject.GetComponent<Animator>();
	}

	private void Update()
	{
		// ����������, �� ���� ��������� ������ �����
		if (Input.GetButtonDown("MeleeAttack"))
		{
			DetermineAndTriggerMeleeAttack();
		}
	}

	private void DetermineAndTriggerMeleeAttack()
	{
		Vector2 attackDirection = Vector2.zero;

		// �������� �������� � ������� ���� ��������
		float rightStickX = Input.GetAxis("RightStickX");
		float rightStickY = Input.GetAxis("RightStickY");

		// ����������, �� ��������������� �������
		if (Mathf.Abs(rightStickX) > 0.1f || Mathf.Abs(rightStickY) > 0.1f)
		{
			// ���� ��� ���������, ������������� ���� ��������
			attackDirection = new Vector2(rightStickX, rightStickY).normalized;
		}
		else
		{
			// � ������ ������� ������������� ����
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			attackDirection = (mouseWorldPosition - transform.position).normalized;
		}

		// --- ���������� ������� ����� �� ����� ������� ---
		if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y))
		{
			// �������������� ����
			if (attackDirection.x > 0)
			{
				// ���� ������
				meleeAnimator.SetTrigger("ForwardMeleeSwipe");
				// ����������� ���������, ���� �� �������� ����
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
			else
			{
				// ���� ����
				meleeAnimator.SetTrigger("ForwardMeleeSwipe");
				// ����������� ���������, ���� �� �������� ������
				transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
		}
		else
		{
			if (attackDirection.y > 0)
			{
				meleeAnimator.SetTrigger("UpwardMeleeSwipe");
			}
			else
			{
				meleeAnimator.SetTrigger("DownwardMeleeSwipe");
			}
		}
	}
}