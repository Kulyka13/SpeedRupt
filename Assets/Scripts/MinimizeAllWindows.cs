using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class MinimizeTaskbarWindows : MonoBehaviour
{
    // ������� ��� ������� EnumWindows
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    // ������ ���������� ������� � user32.dll
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

    // ����� ��� ���������� �������������� ���� ���
    private IntPtr gameWindowHandle;

    private void Start()
    {
#if !UNITY_EDITOR
        // �������� ���������� ���� ���
        gameWindowHandle = GetForegroundWindow(); // � ������ ������� �� ���� ���� ������� ������ �� ��������� ���� ���
#endif
    }

    // �� ������� ���������� ���� ����, �� ������� �� ����� �������, �� �������� ����� ����
    [ContextMenu("Minimize")]
    public void MinimizeAllVisibleWindows()
    {
        EnumWindows(MinimizeVisibleWindows, IntPtr.Zero);
    }

    // ������� ��� ��������� ������� ����
    private bool MinimizeVisibleWindows(IntPtr hWnd, IntPtr lParam)
    {
        // ����������, �� � ���� �������
        if (IsWindowVisible(hWnd) && hWnd != GetShellWindow() && hWnd != gameWindowHandle)
        {
            // �������� ����� ���� (��� ������������, �� �� �� ������ ����)
            System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
            GetWindowText(hWnd, windowText, 256);

            // ���� ���� �� ����� (�����), �������� ����
            if (windowText.Length > 0)
            {
                ShowWindowAsync(hWnd, SW_MINIMIZE);
            }
        }
        return true;
    }
}
