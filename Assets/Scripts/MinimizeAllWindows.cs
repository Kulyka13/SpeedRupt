using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MinimizeTaskbarWindows : MonoBehaviour
{
    // Делегат для функції EnumWindows
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    // Імпорт необхідних функцій з user32.dll
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetShellWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern void ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    private const int SW_MINIMIZE = 6;
    private const uint GW_HWNDNEXT = 2;

    // Змінна для збереження ідентифікатора вікна гри
    private IntPtr gameWindowHandle;

    private void Start()
    {
#if !UNITY_EDITOR
        // Отримуємо дескриптор вікна гри
        gameWindowHandle = GetForegroundWindow(); // В даному випадку це може бути потрібно змінити на конкретне вікно гри
#endif
    }

    // Ця функція згортатиме лише вікна, які присутні на панелі завдань, за винятком цього вікна
    [ContextMenu("Minimize")]
    public void MinimizeAllVisibleWindows()
    {
        EnumWindows(MinimizeVisibleWindows, IntPtr.Zero);
    }

    // Функція для згортання видимих вікон
    private bool MinimizeVisibleWindows(IntPtr hWnd, IntPtr lParam)
    {
        // Перевіряємо, чи є вікно видимим
        if (IsWindowVisible(hWnd) && hWnd != GetShellWindow() && hWnd != gameWindowHandle)
        {
            // Отримуємо назву вікна (щоб переконатися, що це не порожнє вікно)
            System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
            GetWindowText(hWnd, windowText, 256);

            // Якщо вікно має текст (назву), згортаємо його
            if (windowText.Length > 0)
            {
                ShowWindowAsync(hWnd, SW_MINIMIZE);
            }
        }
        return true;
    }
}
