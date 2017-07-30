using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;

	public Image fadeInOutImage;
	public float fadeSpeed;

	public int enemyTypeNumber;
	public int weaponTypeNumber;

	private List<List<int>> enemyWeaponMatching;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		ResetMatching ();
	}
		
	void Start()
	{
		StartCoroutine(FadeIn());
	}
		
	public void GameOver()
	{
		RestartLevel ();
	}

	public void UpdateMatching(int enemyType, int weapon)
	{
		enemyWeaponMatching [enemyType] [weapon]++;
	}

	public float GetMatching(int enemyType, int weapon)
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

	public void RestartLevel()
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
