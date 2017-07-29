using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;    

public class Player : MovingObject
{
	public float totalHealth;

	public AudioClip reloadSound;
	public AudioClip attackSound;
	public AudioClip damageSound;

	public Vector3 WeaponThumbnailPosition;
	public float invulnerabiltyFrames;

	public Text healthText;
	public Text ammoText;
	public Text damageText;

	private Animator animator;
	private float currentHealth;
	private Enemy currentTarget;
	private Weapon currentWeapon;
	private int currentWeaponIndex;
	private List<Weapon> weapons;
	private LayerMask blockingLayer;

	private float invulnerabiltyFramesDelay;

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

		if (Input.GetButtonDown ("SwitchGun"))
			SwitchGun ();
		
		if(invulnerabiltyFramesDelay > 0)
			invulnerabiltyFramesDelay -= Time.deltaTime;

		UpdateTexts ();
	}
		
	private void SwitchGun()
	{
		// Sound
		currentWeaponIndex++;
		if (currentWeaponIndex >= weapons.Count)
			currentWeaponIndex = 0;
		currentWeapon = weapons [currentWeaponIndex];
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
			currentWeapon.SetAmmo (currentWeapon.totalAmmo);
			SoundManager.instance.PlayMultiple (reloadSound);
			Destroy (other.gameObject);
		} 
		else if (other.tag == "Weapon") 
		{
			Weapon newWeapon = other.gameObject.GetComponent<Weapon> ();
			weapons.Add (newWeapon);
			currentWeapon = newWeapon;
			currentWeapon.transform.position = WeaponThumbnailPosition;
			// Gun taken sound
		} 
	}

	private void OnCollisionStay2D (Collision2D other)
	{
		if (other.gameObject.tag == "Enemy" && invulnerabiltyFramesDelay <= 0)
			LoseHealth (other.gameObject.GetComponent<Enemy>().playerDamage);
	}
		
	private void CheckIfGameOver ()
	{
		if (currentHealth <= 0) 
			GameManager.instance.GameOver ();
	}

	public void LoseHealth(float damage)
	{
		SoundManager.instance.PlayMultiple (damageSound);
		currentHealth = Mathf.Max(0, currentHealth - damage);
		CheckIfGameOver ();
		invulnerabiltyFramesDelay = invulnerabiltyFrames;
	}

	private void UpdateTexts()
	{
		damageText.text = "Damage : " + currentWeapon.damage;
		ammoText.text = "Ammo : " + currentWeapon.currentAmmo;
		healthText.text = "Health : " + currentHealth;
	}
}