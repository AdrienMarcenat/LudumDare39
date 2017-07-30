using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;
	public int level = 0;
	public bool playerTurn = false;
	public float turnDelay;

	public int enemyTypeNumber;
	public int weaponTypeNumber;

	private List<Enemy> enemyList;
	private List<List<int>> enemyWeaponMatching;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		enemyList = new List<Enemy> ();
		enemyWeaponMatching = new List<List<int>> ();
		for (int i = 0; i < enemyTypeNumber; i++) 
		{
			List<int> dummy = new List<int> ();
			for(int j = 0; j < weaponTypeNumber; j++)
				dummy.Add(1);
			enemyWeaponMatching.Add (dummy);
		}
	}

	void Update()
	{
		
	}
		
	public void GameOver()
	{
		print ("Game Over");
		// TO DO : game over logic
	}

	public void ChangeLevel()
	{
		enemyList.Clear ();
		level++;
		SceneManager.LoadScene (level);
	}

	public void AddEnemyToList(Enemy enemy)
	{
		enemyList.Add (enemy);
	}

	public void RemoveEnemyFromList(Enemy enemy)
	{
		enemyList.Remove (enemy);
	}

	public void UpdateMatching(int enemyType, int weapon)
	{
		enemyWeaponMatching [enemyType] [weapon]++;
	}

	public float GetMatching(int enemyType, int weapon)
	{
		return Mathf.Min (1, 5.0f / enemyWeaponMatching [enemyType] [weapon]);
	}
}
