using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour 
{
	public float totalHealth;

	protected Animator animator;
	protected float currentHealth;

	public abstract void LoseHealth (float damage);
}
