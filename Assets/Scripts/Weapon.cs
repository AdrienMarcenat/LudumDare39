using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public int range = 1;
	public int damage = 1;
	public int totalAmmo = int.MaxValue;
	public int currentAmmo;

	void OnEnable()
	{
		currentAmmo = totalAmmo;
	}

	public void SetAmmo(int amount)
	{
		currentAmmo += amount;
		if (currentAmmo < 0)
			currentAmmo = 0;
		if (currentAmmo > totalAmmo)
			currentAmmo = totalAmmo;
	}
}
