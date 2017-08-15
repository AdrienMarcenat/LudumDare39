using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy 
{
	private MovingObject body;

	void Start()
	{
		body = GetComponent<MovingObject> ();
	}

	void Update()
	{
		if (isSeeking)
			MoveEnemy ();
	}

	private void MoveEnemy ()
	{
		float horizontal = target.transform.position.x - transform.position.x;
		float vertical   = target.transform.position.y - transform.position.y;

		body.Move (horizontal, vertical);
	}
}
