using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
	public string menuSceneName = "MainMenu";
	public SceneFader sceneFader;
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI finalTimeText;
    private void OnEnable()
    {
        timer.timerEnable = false;

        float time = timer.GetElapsedTime();
        int m = Mathf.FloorToInt(time / 60f);
        int s = Mathf.FloorToInt(time % 60f);

        finalTimeText.text = $"{m:00}:{s:00}";
    }
    public void Retry()
	{
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}
	public void Menu()
	{
		sceneFader.FadeTo(menuSceneName);
	}
}
