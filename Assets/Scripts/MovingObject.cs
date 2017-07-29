using UnityEngine;
using System.Collections;

public abstract class MovingObject : Character
{     
	protected BoxCollider2D boxCollider;      
	protected Rigidbody2D rigidBody;

	public float smoothSpeed;

	protected virtual void Start ()
	{
		boxCollider = GetComponent <BoxCollider2D> ();
		rigidBody = GetComponent <Rigidbody2D> ();
	}

	protected void Move (float xDir, float yDir)
	{
		rigidBody.velocity = smoothSpeed * (new Vector2 (xDir, yDir).normalized);
	}
}