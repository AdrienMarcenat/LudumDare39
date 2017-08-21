using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
	[SerializeField] protected Vector3 weaponPosition;
	[SerializeField] protected Vector3 weaponRotation;

	private Weapon currentWeapon;
	private int currentWeaponIndex;
	private List<GameObject> weapons;

	void Awake ()
	{
		currentWeapon = GetComponentInChildren<Weapon> ();
		weapons = new List<GameObject> ();
		weapons.Add (currentWeapon.gameObject);
	}

	public void NextGun()
	{
		SwitchGun (currentWeaponIndex + 1);
	}

	public void SwitchGun(int index)
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

	public void Fire()
	{
		currentWeapon.Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}
		
	public void Reload()
	{
		currentWeapon.Reload ();
	}

	public void WeaponPick(GameObject newWeapon)
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

	public Weapon GetCurrentWeapon()
	{
		return currentWeapon;
	}
}

