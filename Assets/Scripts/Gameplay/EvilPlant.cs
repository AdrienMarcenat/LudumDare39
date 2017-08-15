using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPlant : Enemy 
{
	private Weapon weapon;

	protected void Start ()
	{
		weapon = GetComponentInChildren<Weapon>();
	}

	void Update()
	{
		if (isSeeking && weapon.Fire (target.position))
			animator.SetTrigger ("isAttacking");
	}
}
