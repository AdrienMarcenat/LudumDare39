using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] int type;
	[SerializeField] int totalAmmo = int.MaxValue;
	[SerializeField] int currentAmmo;
	[SerializeField] float ammoVelocity;
	[SerializeField] float fireRate;
	[SerializeField] string name;

	[SerializeField] Sprite thumbnail;
	[SerializeField] Sprite topDownSprite;

	[SerializeField] GameObject bulletPrefab;
	[SerializeField] AudioClip fireSound;

	private float fireDelay;

	void Start()
	{
		currentAmmo = totalAmmo;
		fireDelay = fireRate;
	}

	public void SetAmmo(int amount)
	{
		currentAmmo += amount;
		currentAmmo = Mathf.Clamp (currentAmmo, 0, totalAmmo);
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
		SoundManager.PlayMultiple (fireSound);

		// The bullet is a child of the current room so it will be deactivated when the player leave the room
		GameObject bullet = Instantiate (bulletPrefab, RoomSystem.currentRoom);
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

	public int GetType()
	{
		return type;
	}

	public int GetAmmo()
	{
		return currentAmmo;
	}

	public Sprite GetThumbnail()
	{
		return thumbnail;
	}
}
