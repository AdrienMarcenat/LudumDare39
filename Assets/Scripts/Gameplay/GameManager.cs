using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> 
{
	public static bool pause;

	[SerializeField] Image fadeInOutImage;
	[SerializeField] float fadeSpeed;

	[SerializeField] int enemyTypeNumber;
	[SerializeField] int weaponTypeNumber;

	private static GameObject pausePanel;
	private static List<List<int>> enemyWeaponMatching;

	void Awake () 
	{
		base.Awake ();

		ResetMatching ();
		if(pausePanel == null)
			pausePanel = GameObject.Find ("PausePanel");
		pause = false;
		pausePanel.SetActive (false);
	}
		
	void Start()
	{
		StartCoroutine(FadeIn());
	}

	void OnEnable()
	{
		GameEventManager.Pause    += Pause;
		GameEventManager.GameOver += GameOver;
		GameEventManager.EnemyHit += UpdateMatching;
	}
		
	public void GameOver()
	{
		RestartLevel ();
	}

	public static void Pause()
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

	IEnumerator FadeIn()
	{
		while (fadeInOutImage.color.a > 0)
		{
			Color c = fadeInOutImage.color;
			c.a -= fadeSpeed;
			fadeInOutImage.color = c;
			yield return null;
		}
	}

	IEnumerator FadeOut()
	{
		while (fadeInOutImage.color.a < 1)
		{
			Color c = fadeInOutImage.color;
			c.a += fadeSpeed;
			fadeInOutImage.color = c;
			yield return null;
		}
	}

	private void RestartLevel()
	{
		StartCoroutine (RestartLevelRoutine());
	}

	IEnumerator RestartLevelRoutine()
	{
		while (fadeInOutImage.color.a < 1)
		{
			Color c = fadeInOutImage.color;
			c.a += fadeSpeed;
			fadeInOutImage.color = c;
			yield return null;
		}
		ResetMatching ();
		SceneManager.LoadScene (1);
		while (fadeInOutImage.color.a > 0)
		{
			Color c = fadeInOutImage.color;
			c.a -= fadeSpeed;
			fadeInOutImage.color = c;
			yield return null;
		}
	}
}
