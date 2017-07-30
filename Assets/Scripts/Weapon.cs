using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public int totalAmmo = int.MaxValue;
	public int currentAmmo;
	public float ammoVelocity;
	public float fireRate;
	public float knockBack;
	public int type;
	public string name;

	public Sprite thumbnail;
	public Sprite topDownSprite;

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

	public bool Fire(Vector3 direction)
	{
		if (currentAmmo == 0 || fireDelay < fireRate)
			return false;

		fireDelay = 0;
		SetAmmo (-1);
		SoundManager.instance.PlayMultiple (fireSound);

		GameObject bullet = Instantiate (bulletPrefab);
		bullet.transform.position = transform.position;
		direction.z = 0;
		bullet.GetComponent<Rigidbody2D> ().velocity = ammoVelocity*((direction - transform.position).normalized);

		return true;
	}

	public void Reload()
	{
		currentAmmo = totalAmmo;
	}

	public void SwitchSprite()
	{
		GetComponent<SpriteRenderer> ().sprite = topDownSprite;
	}
}
