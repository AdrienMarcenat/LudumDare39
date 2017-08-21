﻿using UnityEngine;
using System.Collections;

namespace PlayerStates
{
	public enum ID
	{
		Normal = 0,
		Invincible = 1,
	}
}

public class PlayerFSM : FSM
{
	protected void Awake()
	{
		base.Awake ();
		PushState ((int) PlayerStates.ID.Normal);
	}
}
