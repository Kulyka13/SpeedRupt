using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
	public GameObject[] weapons;
	private int currentWeaponIndex = 0;

	void Start()
	{
		SelectWeapon(currentWeaponIndex);
	}

	void Update()
	{
		// Перемикання на наступну зброю
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			currentWeaponIndex++;
			if (currentWeaponIndex >= weapons.Length)
			{
				currentWeaponIndex = 0; // Повертаємось на першу зброю
			}
			SelectWeapon(currentWeaponIndex);
		}

		// Можна також додати перемикання по цифрах, як у першому варіанті
		// наприклад, перемикач на конкретну зброю за індексом
		if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectWeapon(0); } // Меч
		if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectWeapon(1); } // RPG
	}

	void SelectWeapon(int index)
	{
		// Перевіряємо, чи індекс в межах масиву
		if (index < 0 || index >= weapons.Length)
		{
			Debug.LogError("Invalid weapon index!");
			return;
		}

		for (int i = 0; i < weapons.Length; i++)
		{
			// Активуємо обрану зброю, деактивуємо всі інші
			weapons[i].SetActive(i == index);
		}
	}
}