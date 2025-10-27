using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class GunController : MonoBehaviour
{
	[SerializeField] private Animator gunAnim;
	[SerializeField] private Transform gun;
	[SerializeField] private float gunDistance = 1.5f;
	[SerializeField] private Transform aimOrigin;
	[SerializeField] private float coolDown;
	private Vector3 _initialGunLocalScale;
	private Transform Origin => aimOrigin != null ? aimOrigin : transform;
	private bool canShoot = true;
	[Header("Bullet")]
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float bulletSpeed;

	[SerializeField] private float stickDeadzone = 0.2f;
	[SerializeField] private float mouseMoveThreshold = 0.1f; // чутливість до руху мишки

	private enum InputMode { Mouse, Gamepad }
	private InputMode currentInput = InputMode.Mouse; // за замовчуванням мишка

	private Vector3 lastMousePos;
	private Vector3 lastDir = Vector3.right; // запасний напрямок, якщо нічого не рухається

	private void Awake()
	{
		if (gun == null)
		{
			Debug.LogError("[GunController] 'gun' не призначено.");
			enabled = false;
			return;
		}
		_initialGunLocalScale = gun.localScale;
		lastMousePos = Input.mousePosition;
	}

	private void LateUpdate()
	{
		if (!gun.gameObject.activeInHierarchy)
			return;

		Vector3 dir = Vector3.zero;

		// --- 1) Читаємо стік ---
		float stickX = Input.GetAxis("RightStickX");
		float stickY = Input.GetAxis("RightStickY");

		// Міняємо місцями + інвертуємо вертикаль
		Vector2 stickInput = new Vector2(stickY, -stickX);

		if (stickInput.magnitude > stickDeadzone)
		{
			currentInput = InputMode.Gamepad;
			dir = stickInput.normalized;
			lastDir = dir;
		}

		else
		{
			// Перевіряємо, чи рухалась мишка
			Vector3 mouseDelta = Input.mousePosition - lastMousePos;
			if (mouseDelta.magnitude > mouseMoveThreshold)
			{
				currentInput = InputMode.Mouse;
			}
			lastMousePos = Input.mousePosition;

			if (currentInput == InputMode.Mouse)
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z = 0;
				dir = (mousePos - Origin.position);
				if (dir.sqrMagnitude < 0.000001f) return;
				dir.Normalize();
				lastDir = dir;
			}
			else if (currentInput == InputMode.Gamepad)
			{
				// якщо стік відпущений — тримаємо останній напрямок
				dir = lastDir;
			}
		}

		// --- 2) Компенсація дзеркалення ---
		float parentSignX = Mathf.Sign(Origin.lossyScale.x);
		gun.localScale = new Vector3(
			Mathf.Abs(_initialGunLocalScale.x) * (parentSignX < 0 ? -1f : 1f),
			Mathf.Abs(_initialGunLocalScale.y),
			_initialGunLocalScale.z
		);

		// --- 3) Поворот зброї по вектору ---
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		gun.rotation = Quaternion.Euler(0f, 0f, angle);
		gun.position = Origin.position + dir * gunDistance;

		// --- 4) Позиція зброї на відстані від Origin ---
		gun.position = Origin.position + dir * gunDistance;


		// --- 5) Постріл ---
		if (Input.GetButton("Shoot") && canShoot)
		{
			Shoot(dir);
		}
	}

	private void Shoot(Vector3 dir)
	{
		if (gunAnim != null)
			gunAnim.SetTrigger("Shoot");

		GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
		newBullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletSpeed;
		Destroy(newBullet, 10);

		canShoot = false;              
		StartCoroutine(ShootCooldown());
	}
	private IEnumerator ShootCooldown()
	{
		yield return new WaitForSeconds(coolDown);
		canShoot = true;
	}
}
