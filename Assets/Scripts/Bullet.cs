using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public float damage;
	public int weaponType;
	public float range;

	void Update()
	{
		range -= Time.deltaTime;
		if (range < 0)
			Destroy (gameObject);
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Enemy") 
		{
			Enemy hitEnemy = other.GetComponent<Enemy> ();
			float damageModifier = GameManager.instance.GetMatching (hitEnemy.type, weaponType);
			hitEnemy.LoseHealth (damage * damageModifier);
			GameManager.instance.UpdateMatching (hitEnemy.type, weaponType);
			Destroy (gameObject);
		}
		else if(other.tag == "Wall")
			Destroy (gameObject);
	}
}
