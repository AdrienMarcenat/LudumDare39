using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour
{     
	private BoxCollider2D boxCollider;      
	private Rigidbody2D rigidBody;

	[SerializeField] float smoothSpeed;

	void Start ()
	{
		boxCollider = GetComponent <BoxCollider2D> ();
		rigidBody = GetComponent <Rigidbody2D> ();
	}

	public void Move (float xDir, float yDir)
	{
		rigidBody.velocity = smoothSpeed * (new Vector2 (xDir, yDir).normalized);
	}
}