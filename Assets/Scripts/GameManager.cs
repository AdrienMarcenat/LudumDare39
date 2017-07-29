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

	private List<Enemy> enemyList;
	private bool enemiesMoving = false;
	private List<List<int>> enemyWeaponMatching;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		enemyList = new List<Enemy> ();
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
		enemiesMoving = false;
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
}
