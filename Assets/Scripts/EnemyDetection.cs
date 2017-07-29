using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour 
{
	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
			GetComponentInParent<Enemy>().isSeeking = true;
	}
}
