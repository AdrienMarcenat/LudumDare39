using UnityEngine;
using System.Collections;


public class Enemy : MovingObject
{
	public float playerDamage;
	public Transform healthBar;
	public int type;
	public bool isSeeking;

	protected Animator animator;
	protected Transform target;
	protected float currentHealth;

	protected override void Start ()
	{
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		isSeeking = false;
		currentHealth = totalHealth;

		base.Start ();
	}

	private void CheckIfGameOver ()
	{
		if (currentHealth <= 0)
		{
			GameManager.instance.RemoveEnemyFromList (this);
			animator.SetTrigger ("isDying");
			Destroy (gameObject, 1);
		}
	}

	public override void LoseHealth(float damage)
	{
		Seek ();
		currentHealth = Mathf.Max(0, currentHealth - damage);
		healthBar.localScale = new Vector3 (currentHealth / totalHealth, healthBar.localScale.y, 1);
		CheckIfGameOver ();
	}

	public void Seek()
	{
		//if(!isSeeking)
			// sound;
		isSeeking = true;
		animator.SetTrigger ("isSeeking");
		healthBar.gameObject.SetActive (true);
	}
}