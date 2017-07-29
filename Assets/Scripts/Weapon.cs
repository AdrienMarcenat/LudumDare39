using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public float range = 1;
	public float damage = 1;
	public int totalAmmo = int.MaxValue;
	public int currentAmmo;
	public float ammoVelocity;
	public float fireRate;
	public float knockBack;
	public int type;
	public string name;
	public Sprite thumbnail;

	public GameObject bulletPrefab;
	public AudioClip fireSound;

	private float fireDelay;

	void Start()
	{
		currentAmmo = totalAmmo;
		fireDelay = fireRate;
	}

	public void SetAmmo(int amount)
	{
		currentAmmo += amount;
		if (currentAmmo < 0)
			currentAmmo = 0;
		if (currentAmmo > totalAmmo)
			currentAmmo = totalAmmo;
	}

	void Update()
	{
		fireDelay += Time.deltaTime;
	}

	public void Fire(Vector3 direction, Vector3 position)
	{
		if (currentAmmo == 0 || fireDelay < fireRate)
			return;

		fireDelay = 0;
		SetAmmo (-1);
		SoundManager.instance.PlayMultiple (fireSound);

		GameObject bullet = Instantiate (bulletPrefab);
		bullet.transform.position = position;
		direction.z = 0;
		bullet.GetComponent<Rigidbody2D> ().velocity = ammoVelocity*((direction - position).normalized);
		Bullet bulletScript = bullet.AddComponent<Bullet> ();
		bulletScript.damage = damage;
		bulletScript.weaponType = type;
	}

	public void Reload()
	{
		currentAmmo = totalAmmo;
	}
}
