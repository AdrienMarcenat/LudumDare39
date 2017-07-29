using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilPlant : Enemy 
{
	private Weapon weapon;

	protected override void Start ()
	{
		weapon = GetComponentInChildren<Weapon>();
		base.Start ();
	}

	void Update()
	{
		if (isSeeking)
			if (weapon.Fire (target.position))
				animator.SetTrigger ("isAttacking");
	}
}
