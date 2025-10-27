using UnityEngine;

public class SlowDown : MonoBehaviour
{
	[SerializeField] private float decelerationForce = 0.5f;
	[SerializeField] private float switchCooldown = 0.5f;
	[SerializeField] private bool isSlowing = false;
	private float nextSwitchTime = 0f;

	void Update()
	{
		if (Input.GetButtonDown("SlowDown") && Time.time >= nextSwitchTime)
		{
			if (isSlowing)
			{
				isSlowing = false;
				Time.timeScale = 1f;
			}
			else
			{
				SlowDownWorld();
			}

			nextSwitchTime = Time.time + switchCooldown;
		}
	}

	private void SlowDownWorld()
	{
		isSlowing = true;
		Time.timeScale = decelerationForce;
	}
}
