using UnityEngine;
using System.Collections;

public class GameEventManager : MonoBehaviour
{
	public delegate void SimpleEvent();
	public static event SimpleEvent Pause;
	public static event SimpleEvent GameOver;
	public static event SimpleEvent QuitLevel;

	public delegate void EnemyHitAction(int enemyType, int weaponType);
	public static event EnemyHitAction EnemyHit;

	void Update ()
	{
		if (Input.GetButtonDown ("Escape")) 
		{
			Pause ();
		}
	}

	public static void GameOverEvent()
	{
		GameOver ();
	}
		
	public static void EnemyHitEvent(int enemyType, int weaponType)
	{
		EnemyHit(enemyType, weaponType);
	}

	public static void QuitLevelEvent()
	{
		QuitLevel ();
	}

	public static void PauseEvent()
	{
		Pause ();
	}
}

