using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour
{          
	private Rigidbody2D rigidBody;

	[SerializeField] float smoothSpeed;

	void Start ()
	{
		rigidBody = GetComponent <Rigidbody2D> ();
	}

	public void Move (float xDir, float yDir)
	{
		rigidBody.velocity = smoothSpeed * (new Vector2 (xDir, yDir).normalized);
	}
}