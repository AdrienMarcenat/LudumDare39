using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public int damage;

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Enemy") 
		{
			Enemy hitEnemy = other.GetComponent<Enemy> ();
			hitEnemy.LoseHealth (damage);
			Destroy (gameObject);
		}
		else if(other.tag == "wall")
			Destroy (gameObject);
	}
}
