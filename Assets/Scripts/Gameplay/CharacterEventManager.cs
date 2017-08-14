using UnityEngine;
using System.Collections;

public class CharacterEventManager : MonoBehaviour
{
	public delegate void SimpleEvent();
	public event SimpleEvent GameOver;
	public event SimpleEvent LoseHealth;
	public event SimpleEvent UpdateUI;

	public delegate void DamageAction(float damage, int weaponType);
	public event DamageAction Damage;

	public void GameOverEvent()
	{
		GameOver ();
	}

	public void DamageEvent(float damage, int weaponType)
	{
		Damage (damage, weaponType);
	}

	public void LoseHealthEvent()
	{
		LoseHealth ();
		UpdateUI ();
	}

	public void UpdateUIEvent()
	{
		UpdateUI ();
	}
}

