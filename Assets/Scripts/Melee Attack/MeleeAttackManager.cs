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
		// Перевіряємо, чи була натиснута кнопка атаки
		if (Input.GetButtonDown("MeleeAttack"))
		{
			DetermineAndTriggerMeleeAttack();
		}
	}

	private void DetermineAndTriggerMeleeAttack()
	{
		Vector2 attackDirection = Vector2.zero;

		// Отримуємо значення з правого стіка геймпада
		float rightStickX = Input.GetAxis("RightStickX");
		float rightStickY = Input.GetAxis("RightStickY");

		// Перевіряємо, чи використовується геймпад
		if (Mathf.Abs(rightStickX) > 0.1f || Mathf.Abs(rightStickY) > 0.1f)
		{
			// Якщо стік відхилений, використовуємо його напрямок
			attackDirection = new Vector2(rightStickX, rightStickY).normalized;
		}
		else
		{
			// В іншому випадку використовуємо мишу
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			attackDirection = (mouseWorldPosition - transform.position).normalized;
		}

		// --- Визначення сторони удару на основі вектора ---
		if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y))
		{
			// Горизонтальний удар
			if (attackDirection.x > 0)
			{
				// Удар вправо
				meleeAnimator.SetTrigger("ForwardMeleeSwipe");
				// Перевертаємо персонажа, якщо він дивиться вліво
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			}
			else
			{
				// Удар вліво
				meleeAnimator.SetTrigger("ForwardMeleeSwipe");
				// Перевертаємо персонажа, якщо він дивиться вправо
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