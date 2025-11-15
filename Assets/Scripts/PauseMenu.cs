using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private PlayerHealth player;
	[SerializeField] private string menuSceneName = "MainMenu";
	[SerializeField] private GameObject ui;
	//public GameObject pauseButton;
	[SerializeField] private SceneFader sceneFader;
	private void Update()
	{
		if (player.currentHealth <= 0)
		{
			//pauseButton.SetActive(false);
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}		
	}
	public void Toggle()
	{
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
			//pauseButton.SetActive(false);
		}
		else
		{
			Time.timeScale = 1f;
			//pauseButton.SetActive(true);
		}
	}
	public void Retry()
	{
		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}
	public void Menu()
	{
		Toggle();
		sceneFader.FadeTo(menuSceneName);
	}
}
