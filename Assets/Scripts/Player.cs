using UnityEngine;
using UnityEngine.UI;
using System.Collections;    

public class Player : MovingObject
{
	public int totalAmmo;
	public int totalDamage;
	public int totalHealth;

	public AudioClip reloadSound;
	public AudioClip attackSound;
	public AudioClip damageSound;

	public Text healthText;
	public Text ammoText;
	public Text damageText;

	private Animator animator;
	private int currentAmmo;
	private int currentDamage;
	private int currentHealth;

	private Enemy currentTarget;

	protected override void Start ()
	{
		animator = GetComponent<Animator>();
		base.Start ();
	}
		
	private void OnDisable ()
	{
		
	}

	private void OnEnable ()
	{
		currentAmmo   = totalAmmo;
		currentDamage = totalDamage;
		currentHealth = totalHealth;
		UpdateTexts ();
	}

	private void Update ()
	{
		if (!GameManager.instance.playerTurn)
			return;
		
		int horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		int vertical = (int) (Input.GetAxisRaw ("Vertical"));

		if(horizontal != 0)
			vertical = 0;
			
		if(horizontal != 0 || vertical != 0)
			AttemptMove<Enemy> (horizontal, vertical); // TO DO : Enemy class
	}

	protected override void AttemptMove <T> (int xDir, int yDir)
	{
		RaycastHit2D hit;
		bool canMove = Move (xDir, yDir, out hit);

		if(canMove)
		{
			// TO DO : audio
		}

		if (!canMove && hit.transform != null) 
		{
			T hitComponent = hit.transform.GetComponent <T> ();

			if (hitComponent != null)
				OnCantMove (hitComponent);
		}

		GameManager.instance.playerTurn = false;
	}
		
	protected override void OnCantMove <T> (T component)
	{
		Enemy hitEnemy = component as Enemy;
		if (!hitEnemy.attackNextMove) 
		{
			if (hitEnemy != currentTarget) 
			{
				currentTarget = hitEnemy;
				currentDamage = totalDamage;
				currentAmmo = Mathf.Max (0, currentAmmo - 1);
			}
			else 
			{
				currentAmmo = Mathf.Min (totalAmmo, currentAmmo + 1);
				SoundManager.instance.PlayMultiple (reloadSound);
				currentDamage = 0;
			}
			UpdateTexts ();

			hitEnemy.LoseHealth (currentDamage);
			animator.SetTrigger ("playerAttack");
			SoundManager.instance.PlayMultiple (attackSound);
		}
	}
		
	private void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Exit")
			GameManager.instance.ChangeLevel ();
		else if(other.tag == "stuff")
		{
			// TO DO : item
		}
	}
		
	private void CheckIfGameOver ()
	{
		if (currentHealth == 0) 
			GameManager.instance.GameOver ();
	}

	public void LoseHealth(int damage)
	{
		SoundManager.instance.PlayMultiple (damageSound);
		currentHealth = Mathf.Max(0, currentHealth - damage);
		UpdateTexts ();
		CheckIfGameOver ();
	}

	private void UpdateTexts()
	{
		damageText.text = "Damage : " + currentDamage;
		ammoText.text = "Ammo : " + currentAmmo;
		healthText.text = "Health : " + currentHealth;
	}
}