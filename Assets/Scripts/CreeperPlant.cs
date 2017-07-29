using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperPlant : Enemy 
{
	void Update()
	{
		if (isSeeking)
			MoveEnemy ();
	}

	public void MoveEnemy ()
	{
		float horizontal = target.transform.position.x - transform.position.x;
		float vertical = target.transform.position.y - transform.position.y;

		Move (horizontal, vertical);
	}
}
