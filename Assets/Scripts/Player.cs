using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;    

public class Player : MovingObject
{
	public AudioClip reloadSound;
	public AudioClip damageSound;
	public AudioClip healSound;

	public Vector3 weaponPosition;
	public Vector3 weaponRotation;
	public float invulnerabiltyFrames;

	public Image healthBar;
	public Text ammoText;
	public Image weaponThumbnail;


	private Enemy currentTarget;
	private Weapon currentWeapon;
	private int currentWeaponIndex;
	private List<GameObject> weapons;
	private LayerMask blockingLayer;

	private float invulnerabiltyFramesDelay;

	protected override void Start ()
	{
		animator = GetComponent<Animator>();
		currentWeapon = GetComponentInChildren<Weapon> ();
		weapons = new List<GameObject> ();
		weapons.Add (currentWeapon.gameObject);
		currentHealth = totalHealth;
		UpdateUI ();

		base.Start ();
	}

	private void Update ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		if (horizontal != 0 || vertical != 0)
			animator.SetBool ("PlayerMove", true);
		else
			animator.SetBool ("PlayerMove", false);
		
		Vector3 direction = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position).normalized;
		Quaternion rotation = Quaternion.Euler( 0, 0, Mathf.Atan2 ( direction.y, direction.x ) * Mathf.Rad2Deg + 90 );
		transform.rotation = rotation;

		Move (horizontal, vertical);

		if (Input.GetButton ("Fire"))
			Fire ();

		if (Input.GetButtonDown ("SwitchGun"))
			SwitchGun ();
		
		if(invulnerabiltyFramesDelay > 0)
			invulnerabiltyFramesDelay -= Time.deltaTime;

		UpdateUI ();
	}
		
	private void SwitchGun()
	{
		// Sound
		weapons [currentWeaponIndex].SetActive (false);
		currentWeaponIndex++;
		if (currentWeaponIndex >= weapons.Count)
			currentWeaponIndex = 0;
		weapons [currentWeaponIndex].SetActive (true);
		currentWeapon = weapons [currentWeaponIndex].GetComponent<Weapon> ();
	}

	private void Fire()
	{
		currentWeapon.Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}
		
	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Exit")
			GameManager.instance.ChangeLevel ();
		else if (other.tag == "AmmoPack") 
		{
			currentWeapon.Reload ();
			SoundManager.instance.PlayMultiple (reloadSound);
			Destroy (other.gameObject);
		} 
		else if (other.tag == "HealthPack") 
		{
			currentHealth = Mathf.Max (totalHealth, currentHealth + 50);
			SoundManager.instance.PlayMultiple (healSound);
			Destroy (other.gameObject);
		} 
		else if (other.tag == "Weapon") 
		{
			GameObject newWeapon = other.gameObject;
			int weaponType = newWeapon.GetComponent<Weapon> ().type;
			bool alreadyInWeapons = false;

			foreach (GameObject weapon in  weapons) 
			{
				if (weapon.GetComponent<Weapon> ().type == weaponType) 
				{
					weapon.GetComponent<Weapon> ().Reload ();
					alreadyInWeapons = true;
					SoundManager.instance.PlayMultiple (reloadSound);
					Destroy (newWeapon);
				}
			}

			if (!alreadyInWeapons)
			{
				weapons.Add (newWeapon);
				newWeapon.transform.SetParent (transform, false);
				newWeapon.transform.localPosition = weaponPosition;
				newWeapon.transform.localRotation = Quaternion.Euler (weaponRotation);
				newWeapon.GetComponent<Weapon> ().SwitchSprite ();
				newWeapon.GetComponent<BoxCollider2D> ().enabled = false;
				SwitchGun ();
			}
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

	public override void LoseHealth(float damage)
	{
		SoundManager.instance.PlayMultiple (damageSound);
		currentHealth = Mathf.Max(0, currentHealth - damage);
		CheckIfGameOver ();
		invulnerabiltyFramesDelay = invulnerabiltyFrames;
	}

	private void UpdateUI()
	{
		ammoText.text = "x" + currentWeapon.currentAmmo;
		healthBar.fillAmount = currentHealth / totalHealth;
		weaponThumbnail.sprite = currentWeapon.thumbnail;
	}
}