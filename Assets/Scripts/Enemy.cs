using UnityEngine;
using System.Collections;


public class Enemy : MovingObject
{
	public float playerDamage;
	public float totalHealth;
	public Transform healthBar;
	public int type;
	public bool isSeeking;

	private Animator animator;
	private Transform target;
	private float currentHealth;

	protected override void Start ()
	{
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		isSeeking = false;
		currentHealth = totalHealth;

		base.Start ();
	}
		
	void Update()
	{
		if (isSeeking)
			MoveEnemy ();
	}

	public void MoveEnemy ()
	{
		float horizontal = target.transform.position.x - transform.position.x;
		float vertical = target.transform.position.y - transform.position.y;

		Move (horizontal, vertical);
	}

	private void CheckIfGameOver ()
	{
		if (currentHealth <= 0)
		{
			GameManager.instance.RemoveEnemyFromList (this);
			animator.SetTrigger ("isDying");
			Destroy (gameObject, 1); // TO DO : animation and item drop
		}
	}

	public void LoseHealth(float damage)
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