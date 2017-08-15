using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : Singleton<GameManager> 
{
	public static bool pause;

	[SerializeField] int enemyTypeNumber;
	[SerializeField] int weaponTypeNumber;
	[SerializeField] float mutationSpeed;
	[SerializeField] float minimalDamageModifier;

	private List<List<int>> enemyWeaponMatching;

	public delegate void SimpleEvent();
	public static event SimpleEvent Pause;
	public static event SimpleEvent ChangeScene;

	void Start()
	{
		Reset ();
	}

	public static void PauseEvent()
	{
		pause = !pause;
		Time.timeScale = 1.0f - Time.timeScale;
		if (Pause != null)
			Pause ();
	}

	public static void LoadScene(int index)
	{
		instance.Reset ();
		if(ChangeScene != null)
			ChangeScene ();
		SceneManager.LoadScene (index);
	}

	void Update ()
	{
		if (Input.GetButtonDown ("Escape")) 
		{
			PauseEvent ();
		}
	}

	public void UpdateMatching(int enemyType, int weaponType)
	{
		enemyWeaponMatching [enemyType] [weaponType]++;
	}

	public float GetMatching(int enemyType, int weapon)
	{
		return Mathf.Max (minimalDamageModifier, 1 - enemyWeaponMatching [enemyType] [weapon] / mutationSpeed);
	}

	private void ResetMatching()
	{
		enemyWeaponMatching = new List<List<int>> ();
		for (int i = 0; i < enemyTypeNumber; i++) 
		{
			List<int> dummy = new List<int> ();
			for(int j = 0; j < weaponTypeNumber; j++)
				dummy.Add(0);
			enemyWeaponMatching.Add (dummy);
		}
	}

	private void Reset()
	{
		ResetMatching ();
		pause = false;
		Time.timeScale = 1.0f;
	}
}
