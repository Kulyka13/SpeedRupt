using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    private void Awake()
    {
        Instance = this;
    }

[SerializeField] private TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    [HideInInspector] public bool timerEnable = true;
    private void Update()
    {
        if (timerEnable)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    public float GetElapsedTime() => elapsedTime;
}
