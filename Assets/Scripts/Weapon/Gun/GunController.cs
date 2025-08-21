using UnityEngine;

// ���������, �� ��� ������ ���������� ϲ��� ������� ���������� ����
[DefaultExecutionOrder(100)]
public class GunController : MonoBehaviour
{
	[SerializeField] private Animator gunAnim;
	[SerializeField] private Transform gun;
	[SerializeField] private float gunDistance = 1.5f;

	// (������'������) ���� �����, ��� ����� ����� ������� �� � this.transform, � � ������ ����� (��������� "HandPivot")
	[SerializeField] private Transform aimOrigin;

	private Vector3 _initialGunLocalScale;

	private Transform Origin => aimOrigin != null ? aimOrigin : transform;

	private void Awake()
	{
		if (gun == null)
		{
			Debug.LogError("[GunController] 'gun' �� ����������.");
			enabled = false;
			return;
		}
		_initialGunLocalScale = gun.localScale;
	}

	private void LateUpdate()
	{
		// 1) ���� � ���
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0;

		// 2) ������������� ��������
		Vector3 dir = (mousePos - Origin.position);
		if (dir.sqrMagnitude < 0.000001f) return;
		dir.Normalize();

		// 3) ����������� ����������� �� ������������ ��������.
		// ���� Root �������� ����� localScale.x *= -1, �� ������ ������������.
		// ������ � � ���� localScale.x � ��� ����� ������, ��� � ������� ��������� �������� ���� ���������.
		float parentSignX = Mathf.Sign(Origin.lossyScale.x); // < 0, ���� ������� "�������������"
		gun.localScale = new Vector3(
			Mathf.Abs(_initialGunLocalScale.x) * (parentSignX < 0 ? -1f : 1f),
			Mathf.Abs(_initialGunLocalScale.y),
			_initialGunLocalScale.z
		);

		// 4) ������� �� ���� �� ����
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		gun.rotation = Quaternion.Euler(0f, 0f, angle);

		// 5) ������� �� ������ �� "Origin"
		gun.position = Origin.position + dir * gunDistance;

		// 6) ������
		if (Input.GetButtonDown("Shoot"))
			Shoot();
	}

	private void Shoot()
	{
		if (gunAnim != null)
			gunAnim.SetTrigger("Shoot");
	}
}
