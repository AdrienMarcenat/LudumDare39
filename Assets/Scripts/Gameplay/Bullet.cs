using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	[SerializeField] float damage;
	[SerializeField] float range;
	[SerializeField] float penetration;
	[SerializeField] int weaponType;
	[SerializeField] string targetTag;

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
			Character hitCharacter = other.GetComponent<Character> ();
			hitCharacter.characterEventManager.DamageEvent (damage, weaponType);

			Destroy (gameObject, penetration);
		}
		else if(other.tag == "Wall")
			Destroy (gameObject);
	}

	void OnDisable()
	{
		Destroy (this.gameObject);
	}
}
