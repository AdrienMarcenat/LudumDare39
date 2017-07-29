using UnityEngine;
using System.Collections;


public class Enemy : MovingObject
{
	public int playerDamage;
	public int totalHealth;
	public int attackDelay;
	public bool attackNextMove = false;

	private int currentAttackDelay;
	private int currentHealth;
	private Animator animator;
	private Transform target;
	private bool skipMove;

	protected override void Start ()
	{
		GameManager.instance.AddEnemyToList (this);
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		currentHealth = totalHealth;
		currentAttackDelay = 0;

		base.Start ();
	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		if(skipMove)
		{
			skipMove = false;
			return;
		}

		base.AttemptMove <T> (xDir, yDir);

		currentAttackDelay++;
		if (currentAttackDelay >= attackDelay)
		{
			currentAttackDelay = 0;
			attackNextMove = true;
		} 
		else
			attackNextMove = false;
		animator.SetBool ("enemyAttackNextMove", attackNextMove);

		skipMove = true;
	}
		
	public void MoveEnemy ()
	{
		int xDir = 0;
		int yDir = 0;

		if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			yDir = target.position.y > transform.position.y ? 1 : -1;
		else
			xDir = target.position.x > transform.position.x ? 1 : -1;

		AttemptMove <Player> (xDir, yDir);
	}
		
	protected override void OnCantMove <T> (T component)
	{
		if (attackNextMove) 
		{
			print ("enemy attack");
			Player hitPlayer = component as Player;
			hitPlayer.LoseHealth (playerDamage);
			animator.SetTrigger ("enemyAttack");
		}
	}

	private void CheckIfGameOver ()
	{
		if (currentHealth == 0)
		{
			GameManager.instance.RemoveEnemyFromList (this);
			Destroy (gameObject); // TO DO : animation and item drop
		}
	}

	public void LoseHealth(int damage)
	{
		currentHealth = Mathf.Max(0, currentHealth - damage);
		print ("enemy " + currentHealth);
		CheckIfGameOver ();
	}
}