using UnityEngine;

[DefaultExecutionOrder(100)]
public class GunController : MonoBehaviour
{
	[SerializeField] private Animator gunAnim;
	[SerializeField] private Transform gun;
	[SerializeField] private float gunDistance = 1.5f;
	[SerializeField] private Transform aimOrigin;
	private Vector3 _initialGunLocalScale;
	private Transform Origin => aimOrigin != null ? aimOrigin : transform;

	[Header("Bullet")]
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float bulletSpeed;


	private void Awake()
	{
		if (gun == null)
		{
			Debug.LogError("[GunController] 'gun' не призначено.");
			enabled = false;
			return;
		}
		_initialGunLocalScale = gun.localScale;
	}

	private void LateUpdate()
	{
		if (!gun.gameObject.activeInHierarchy)
			return;
		// 1) Мишу в світ
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;

		// 2) Нормалізований напрямок
		Vector3 dir = (mousePos - Origin.position);
		if (dir.sqrMagnitude < 0.000001f) return;
		dir.Normalize();

		// 3) Компенсація дзеркалення від батьківського масштабу.
		// Якщо Root фліпиться через localScale.x *= -1, то дитина дзеркалиться.
		// Робимо і у зброї localScale.x з тим самим знаком, щоб у підсумку ГЛОБАЛЬНА орієнтація була нормальна.
		float parentSignX = Mathf.Sign(Origin.lossyScale.x); // < 0, якщо гравець "задзеркалений"
		gun.localScale = new Vector3(
			Mathf.Abs(_initialGunLocalScale.x) * (parentSignX < 0 ? -1f : 1f),
			Mathf.Abs(_initialGunLocalScale.y),
			_initialGunLocalScale.z
		);

		// 4) Поворот по куту до миші
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		gun.rotation = Quaternion.Euler(0f, 0f, angle);

		// 5) Позиція на відстані від "Origin"
		gun.position = Origin.position + dir * gunDistance;

		// 6) Постріл
		if (Input.GetButtonDown("Shoot"))
			Shoot(dir);
	}

	private void Shoot(Vector3 dir)
	{
		if (gunAnim != null)
			gunAnim.SetTrigger("Shoot");
		
		GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
		newBullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletSpeed;
		Destroy(newBullet, 10);

	}
}
