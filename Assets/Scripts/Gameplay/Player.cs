using UnityEngine;
using System.Collections.Generic;
using System.Collections;    

public class Player : Character
{
	[SerializeField] Vector3 weaponPosition;
	[SerializeField] Vector3 weaponRotation;
	[SerializeField] float invulnerabilityFrames;
	[SerializeField] float blinkingRate;

	private Weapon currentWeapon;
	private int currentWeaponIndex;
	private List<GameObject> weapons;

	private float invulnerabilityFramesDelay;
	private SpriteRenderer sprite;

	[HideInInspector] public PlayerEventManager playerEventManager;

	protected void Awake()
	{
		playerEventManager = GetComponent<PlayerEventManager> ();
		characterEventManager = playerEventManager;
	}

	protected void Start ()
	{
		base.Start ();
		sprite = GetComponent<SpriteRenderer> ();
		currentWeapon = GetComponentInChildren<Weapon> ();
		weapons = new List<GameObject> ();
		weapons.Add (currentWeapon.gameObject);
	}

	void OnEnable()
	{
		playerEventManager.SwitchGun  += NextGun;
		playerEventManager.Fire       += Fire;
		playerEventManager.Move       += MovePlayer;
		playerEventManager.AmmoPack   += Reload;
		playerEventManager.HealthPack += Heal;
		playerEventManager.WeaponPick += WeaponPick;
		playerEventManager.Damage     += Damage;
		playerEventManager.GameOver   += GameOver;
	}

	void OnDisable()
	{
		playerEventManager.SwitchGun  -= NextGun;
		playerEventManager.Fire       -= Fire;
		playerEventManager.Move       -= MovePlayer;
		playerEventManager.AmmoPack   -= Reload;
		playerEventManager.HealthPack -= Heal;
		playerEventManager.WeaponPick -= WeaponPick;
		playerEventManager.Damage     -= Damage;
		playerEventManager.GameOver   -= GameOver;
	}

	void MovePlayer (float x, float y)
	{
		Move (x, y);

		if (x != 0 || y != 0)
			animator.SetBool ("PlayerMove", true);
		else
			animator.SetBool ("PlayerMove", false);

		Vector3 direction = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position).normalized;
		Quaternion rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90);
		transform.rotation = rotation;
	}
		
	private void NextGun()
	{
		SwitchGun (currentWeaponIndex + 1);
	}

	private void SwitchGun(int index)
	{
		if (weapons.Count == 1)
			return;
		
		weapons [currentWeaponIndex].SetActive (false);
		currentWeaponIndex = index;
		if (currentWeaponIndex >= weapons.Count)
			currentWeaponIndex = 0;
		weapons [currentWeaponIndex].SetActive (true);
		currentWeapon = weapons [currentWeaponIndex].GetComponent<Weapon> ();
	}

	private void Fire()
	{
		currentWeapon.Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

	private void Heal()
	{
		currentHealth = totalHealth;
	}

	private void Reload()
	{
		currentWeapon.Reload ();
	}

	private void WeaponPick(GameObject newWeapon)
	{
		int weaponType = newWeapon.GetComponent<Weapon> ().GetType();

		foreach (GameObject weapon in  weapons) 
		{
			if (weapon.GetComponent<Weapon> ().GetType() == weaponType) 
			{
				Reload ();
				Destroy (newWeapon);
				return;
			}
		}

		weapons.Add (newWeapon);
		newWeapon.transform.SetParent (transform, false);
		newWeapon.transform.localPosition = weaponPosition;
		newWeapon.transform.localRotation = Quaternion.Euler (weaponRotation);
		newWeapon.GetComponent<Weapon> ().SwitchSprite ();
		newWeapon.GetComponent<BoxCollider2D> ().enabled = false;
		SwitchGun (weapons.Count - 1);
	}
		
	private void Damage(float damage, int weaponType)
	{
		if (invulnerabilityFramesDelay > 0)
			return;

		playerEventManager.LoseHealthEvent ();
		LoseHealth (damage);
		CheckIfGameOver ();
		StartCoroutine (InvulnerabilityRoutine());
	}
		
	IEnumerator InvulnerabilityRoutine()
	{
		invulnerabilityFramesDelay = invulnerabilityFrames;
		while (invulnerabilityFramesDelay > 0) 
		{
			invulnerabilityFramesDelay -= Time.deltaTime + blinkingRate;
			sprite.enabled = !sprite.enabled;
			yield return new WaitForSeconds (blinkingRate);
		}
		sprite.enabled = true;
	}

	protected void CheckIfGameOver ()
	{
		if (currentHealth <= 0)
			playerEventManager.GameOverEvent();
	}

	public Weapon GetCurrentWeapon()
	{
		return currentWeapon;
	}

	private void GameOver()
	{
		GameEventManager.GameOverEvent ();
	}
}