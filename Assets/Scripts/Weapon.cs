﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public int range = 1;
	public int damage = 1;
	public int totalAmmo = int.MaxValue;
	public int currentAmmo;
	public float ammoVelocity;
	public float fireRate;
	public float knockBack;

	public GameObject bulletPrefab;

	private float fireDelay;

	void OnEnable()
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

		GameObject bullet = Instantiate (bulletPrefab);
		bullet.transform.position = position;
		bullet.GetComponent<Rigidbody2D> ().velocity = ammoVelocity*((direction - position).normalized);
		Bullet bulletScript = bullet.AddComponent<Bullet> ();
		bulletScript.damage = damage;
	}
}
