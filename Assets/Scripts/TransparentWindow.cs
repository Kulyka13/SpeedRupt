using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    const int GWL_EXSTYLE = -20;
    const uint WS_EX_LAYERED = 0x00080000;  // Додає можливість прозорості
    const uint WS_EX_TRANSPARENT = 0x00000020;  // Для проклікування через фон

    const uint LWA_COLORKEY = 0x00000001;  // Прозорість на основі кольору

    private IntPtr hWnd;

    private void Start()
    {
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        // Налаштовуємо прозорий фон
        MARGINS margins = new MARGINS() { cxLeftWidth = -1, cxRightWidth = -1, cyTopHeight = -1, cyBottomHeight = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        // Стиль прозорого вікна
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        SetLayeredWindowAttributes(hWnd, 0x000000, 0, LWA_COLORKEY);

        // Встановлюємо вікно поверх інших
        SetWindowPos(hWnd, new IntPtr(-1), 0, 0, 0, 0, 0);
#endif

        Application.runInBackground = true;
    }

    private void Update()
    {
        // Перевіряємо, чи миша над UI елементами
        bool isPointerOverUI = EventSystem.current.IsPointerOverGameObject();

        // Перевіряємо, чи миша над 3D об'єктами
        bool isPointerOver3D = IsPointerOver3D();

        // Вимикаємо прокліку тільки коли миша не над UI чи 3D об'єктами
        SetClickthrough(!(isPointerOverUI || isPointerOver3D));
    }

    private void SetClickthrough(bool clickThrough)
    {
        if (clickThrough)
        {
            // Вмикаємо можливість прокліку через фон
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else
        {
            // Забороняємо проклікування через об'єкти з колайдерами або UI
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }

    // Перевіряємо, чи миша знаходиться над 3D об'єктами
    private bool IsPointerOver3D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Виконуємо Raycast
        if (Physics.Raycast(ray, out hit))
        {
            return true; // Якщо променя дійшов до об'єкта
        }

        return false; // Якщо немає жодного об'єкта під курсором
    }
}
