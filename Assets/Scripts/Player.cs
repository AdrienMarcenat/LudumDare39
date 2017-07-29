using UnityEngine;
using UnityEngine.UI;
using System.Collections;    

public class Player : MovingObject
{
	public int totalHealth;

	public AudioClip reloadSound;
	public AudioClip attackSound;
	public AudioClip damageSound;

	public GameObject ammoPrefab;
	public Vector3 WeaponThumbnailPosition;

	public Text healthText;
	public Text ammoText;
	public Text damageText;

	private Animator animator;
	private int currentHealth;

	private Enemy currentTarget;
	private Weapon weapon;

	protected override void Start ()
	{
		animator = GetComponent<Animator>();
		weapon = GetComponent<Weapon> ();
		currentHealth = totalHealth;
		UpdateTexts ();

		base.Start ();
	}
		
	private void OnDisable ()
	{
		
	}

	private void OnEnable ()
	{
		
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
				hitEnemy.LoseHealth (weapon.damage);
			}
			else 
			{
				if (Random.Range (0, 2) >= 0.5) 
				{
					GameObject ammo = Instantiate (ammoPrefab);
					ammo.transform.position = hitEnemy.transform.position;
				}
			}
			weapon.SetAmmo (-1);
			UpdateTexts ();

			animator.SetTrigger ("playerAttack");
			SoundManager.instance.PlayMultiple (attackSound);
		}
	}
		
	private void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Exit")
			GameManager.instance.ChangeLevel ();
		else if(other.tag == "Ammo")
		{
			weapon.SetAmmo (4);
			SoundManager.instance.PlayMultiple (reloadSound);
			UpdateTexts ();
			Destroy(other.gameObject);
		}
		else if(other.tag == "Weapon")
		{
			Weapon newWeapon = other.gameObject.GetComponent<Weapon> ();
			weapon = newWeapon;
			weapon.transform.position = WeaponThumbnailPosition;
			// Gun taken sound
			UpdateTexts ();
			Destroy(other.gameObject);
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
		damageText.text = "Damage : " + weapon.damage;
		ammoText.text = "Ammo : " + weapon.currentAmmo;
		healthText.text = "Health : " + currentHealth;
	}
}