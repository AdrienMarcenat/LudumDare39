using UnityEngine;
using System.Collections;

public class EndLevelPanel : MonoBehaviour
{
	[SerializeField] GameObject endLevelPanel;

	void Start()
	{
		endLevelPanel.SetActive(false);
	}

	void OnEnable ()
	{
		EndLevelTrigger.EndLevel += EndLevel;
		GameManager.ChangeScene  += ChangeScene;
	}

	void OnDisable ()
	{
		EndLevelTrigger.EndLevel -= EndLevel;
		GameManager.ChangeScene  -= ChangeScene;
	}

	private void EndLevel ()
	{
		GameManager.pause = true;
		Time.timeScale = 0f;
		endLevelPanel.SetActive(true);
	}

	private void ChangeScene()
	{
		endLevelPanel.SetActive(false);
	}
}

