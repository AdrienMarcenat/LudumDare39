﻿using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
	public float moveTime = 0.1f;           
	public LayerMask blockingLayer;        

	private BoxCollider2D boxCollider;      
	private Rigidbody2D rigidBody;               
	private float inverseMoveTime;
	private bool isMoving = false;

	protected virtual void Start ()
	{
		boxCollider = GetComponent <BoxCollider2D> ();
		rigidBody = GetComponent <Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime;
	}

	protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;

		if(hit.transform == null)
		{
			if (isMoving)
				return false;
			
			StartCoroutine (SmoothMovement (end));
			return true;
		}

		return false;
	}
		
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		isMoving = true;

		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while(sqrRemainingDistance > float.Epsilon)
		{
			Vector3 newPostion = Vector3.MoveTowards(rigidBody.position, end, inverseMoveTime * Time.deltaTime);
			rigidBody.MovePosition (newPostion);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			yield return null;
		}

		isMoving = false;
	}
		
	protected virtual void AttemptMove <T> (int xDir, int yDir)
		where T : Component
	{
		RaycastHit2D hit;
		bool canMove = Move (xDir, yDir, out hit);

		if(canMove)
			return;
		
		T hitComponent = hit.transform.GetComponent <T> ();

		if(hitComponent != null)
			OnCantMove (hitComponent);
	}
		
	protected abstract void OnCantMove <T> (T component)
		where T : Component;
}