using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;    

public class Player : MovingObject
{
	public int totalHealth;

	public AudioClip reloadSound;
	public AudioClip attackSound;
	public AudioClip damageSound;

	public Vector3 WeaponThumbnailPosition;

	public Text healthText;
	public Text ammoText;
	public Text damageText;

	private Animator animator;
	private int currentHealth;
	private Enemy currentTarget;
	private Weapon currentWeapon;
	private List<Weapon> weapons;
	private LayerMask blockingLayer;

	protected override void Start ()
	{
		animator = GetComponent<Animator>();
		currentWeapon = GetComponent<Weapon> ();
		weapons = new List<Weapon> ();
		weapons.Add (currentWeapon);
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
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Move (horizontal, vertical);

		if (Input.GetButton ("Fire"))
			Fire ();
	}
		
	private void Fire()
	{
		currentWeapon.Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
	}
		
	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Exit")
			GameManager.instance.ChangeLevel ();
		else if (other.tag == "Ammo") 
		{
			currentWeapon.SetAmmo (4);
			SoundManager.instance.PlayMultiple (reloadSound);
			UpdateTexts ();
			Destroy (other.gameObject);
		} 
		else if (other.tag == "Weapon") 
		{
			Weapon newWeapon = other.gameObject.GetComponent<Weapon> ();
			weapons.Add (newWeapon);
			currentWeapon = newWeapon;
			currentWeapon.transform.position = WeaponThumbnailPosition;
			// Gun taken sound
			UpdateTexts ();
			Destroy (other.gameObject);
		} 
		else if (other.tag == "Enemy")
			LoseHealth (other.GetComponent<Enemy>().playerDamage);
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
		damageText.text = "Damage : " + currentWeapon.damage;
		ammoText.text = "Ammo : " + currentWeapon.currentAmmo;
		healthText.text = "Health : " + currentHealth;
	}
}