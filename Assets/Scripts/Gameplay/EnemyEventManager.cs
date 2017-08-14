using UnityEngine;
using System.Collections;

public class EnemyEventManager : CharacterEventManager
{
	public event SimpleEvent EnemySeek;

	public void SeekEvent()
	{
		EnemySeek ();
	}
}

