using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : Singleton<GameManager> 
{
	public static bool pause;

	[SerializeField] int enemyTypeNumber;
	[SerializeField] int weaponTypeNumber;

	private static GameObject pausePanel;
	private static List<List<int>> enemyWeaponMatching;

	void OnEnable()
	{
		GameEventManager.Pause     += Pause;
		GameEventManager.GameOver  += RestartLevel;
		GameEventManager.EnemyHit  += UpdateMatching;
		GameEventManager.QuitLevel += QuitLevel;
		SceneManager.sceneLoaded   += OnSceneLoaded;
	}

	private void Pause()
	{
		pause = !pause;
		pausePanel.SetActive (pause);
		Time.timeScale = 1.0f - Time.timeScale; 
	}

	private void UpdateMatching(int enemyType, int weaponType)
	{
		enemyWeaponMatching [enemyType] [weaponType]++;
	}

	public static float GetMatching(int enemyType, int weapon)
	{
		return Mathf.Min (1, 5.0f / enemyWeaponMatching [enemyType] [weapon]);
	}

	private void ResetMatching()
	{
		enemyWeaponMatching = new List<List<int>> ();
		for (int i = 0; i < enemyTypeNumber; i++) 
		{
			List<int> dummy = new List<int> ();
			for(int j = 0; j < weaponTypeNumber; j++)
				dummy.Add(1);
			enemyWeaponMatching.Add (dummy);
		}
	}

	private void RestartLevel()
	{
		GameEventManager.QuitLevelEvent ();
		SceneManager.LoadScene (1);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Reset ();
	}

	private void QuitLevel()
	{
		Reset ();
	}

	private void Reset()
	{
		ResetMatching ();
		if(pausePanel == null)
			pausePanel = GameObject.Find ("PausePanel");
		pause = false;
		Time.timeScale = 1.0f;
		pausePanel.SetActive (false);
	}
}
