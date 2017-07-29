using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public float damage;
	public int weaponType;
	public float range;
	public string targetTag;

	void Update()
	{
		range -= Time.deltaTime;
		if (range < 0)
			Destroy (gameObject);
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == targetTag) 
		{
			if (targetTag == "Enemy") 
			{
				Enemy hitEnemy = other.GetComponent<Enemy> ();
				float damageModifier = GameManager.instance.GetMatching (hitEnemy.type, weaponType);
				hitEnemy.LoseHealth (damage * damageModifier);
				GameManager.instance.UpdateMatching (hitEnemy.type, weaponType);
			}
			else 
			{
				Player hitPlayer = other.GetComponent<Player> ();
				hitPlayer.LoseHealth (damage);
			}

			Destroy (gameObject);
		}
		else if(other.tag == "Wall")
			Destroy (gameObject);
	}
}
