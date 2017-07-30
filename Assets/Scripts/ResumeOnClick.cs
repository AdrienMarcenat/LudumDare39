using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeOnClick : MonoBehaviour 
{
	public GameObject pausePanel;
	private Player player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	public void Resume()
	{
		Time.timeScale = 1.0f;
		player.pause = false;;
		pausePanel.SetActive (false);
	}
}
