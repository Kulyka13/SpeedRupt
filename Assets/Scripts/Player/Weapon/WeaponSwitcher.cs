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
        if (Input.GetButtonDown("R2"))
        {
            NextWeapon();
        }

        if (Input.GetButtonDown("L2"))
        {
            PreviousWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)           
            NextWeapon();

        if (scroll < 0f)            
            PreviousWeapon();
    }

    void NextWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex >= weapons.Length)
            currentWeaponIndex = 0;

        SelectWeapon(currentWeaponIndex);
    }

    void PreviousWeapon()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
            currentWeaponIndex = weapons.Length - 1;

        SelectWeapon(currentWeaponIndex);
    }

    void SelectWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
        {
            Debug.LogError("Invalid weapon index!");
            return;
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }
    }
}
