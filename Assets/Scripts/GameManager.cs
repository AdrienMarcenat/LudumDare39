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
		if (playerTurn || enemiesMoving)
			return;

		StartCoroutine (MoveEnemies ());
	}

	IEnumerator MoveEnemies()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds(turnDelay);

		if(enemyList.Count == 0)
			yield return new WaitForSeconds(turnDelay);
		
		for (int i = 0; i < enemyList.Count; i++)
		{
			enemyList[i].MoveEnemy ();
			yield return new WaitForSeconds(enemyList[i].moveTime);
		}

		playerTurn = true;
		enemiesMoving = false;
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
}
