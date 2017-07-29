using UnityEngine;
using System.Collections;


public class Enemy : MovingObject
{
	public int playerDamage;
	public int totalHealth;
	public int attackDelay;
	public bool attackNextMove = false;
	public int moveDelay;
	public Transform healthBar;

	private int currentAttackDelay;
	private int currentHealth;
	private int currentMoveDelay;
	private Animator animator;
	private Transform target;

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
		currentMoveDelay++;
		if (currentMoveDelay >= moveDelay)
			currentMoveDelay = 0;
		else
			return;

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
		healthBar.localScale = new Vector3 (((float)currentHealth) / totalHealth, healthBar.localScale.y, 1);
		CheckIfGameOver ();
	}
}