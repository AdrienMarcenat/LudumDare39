using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour 
{
	public static GameManager instance;
	public int level = 0;

	private List<Enemy> enemyList;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		enemyList = new List<Enemy> ();
	}

	public void GameOver()
	{
		print ("Game Over");
		// TO DO : game over logic
	}

	public void ChangeLevel()
	{
		level++;
		SceneManager.LoadScene (level);
	}
	public void AddEnemyToList(Enemy enemy)
	{
		enemyList.Add (enemy);
	}
}
