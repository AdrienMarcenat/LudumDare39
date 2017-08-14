using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MovingObject
{
	[SerializeField] protected float totalHealth;
	protected float currentHealth;
	protected Animator animator;

	[HideInInspector] public CharacterEventManager characterEventManager;

	protected void Awake()
	{
		characterEventManager = GetComponent<CharacterEventManager> ();
	}

	protected void Start()
	{
		base.Start ();
		currentHealth = totalHealth;
		animator = GetComponent<Animator> ();
	}

	public void LoseHealth(float damage)
	{
		currentHealth = Mathf.Max(0, currentHealth - damage);
	}

	public float GetCurrentHealth()
	{
		return currentHealth;
	}

	public float GetTotalHealth()
	{
		return totalHealth;
	}
}
