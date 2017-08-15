using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour
{
	[SerializeField] GameObject pausePanel;

	void Start()
	{
		pausePanel.SetActive (false);
	}

	void OnEnable ()
	{
		GameManager.Pause += Pause;
		GameManager.ChangeScene += Pause;
	}

	void OnDisable ()
	{
		GameManager.Pause -= Pause;
		GameManager.ChangeScene -= Pause;
	}

	void Pause ()
	{
		pausePanel.SetActive (GameManager.pause);
	}
}

