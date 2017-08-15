using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour
{
	[SerializeField] GameObject[] spawnObjetcs;
	[SerializeField] AudioClip breakingSound;
	[SerializeField] Sprite brokenSprite;
	[SerializeField] Vector3 spawningLocation;
	[SerializeField] float spawningProbability;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Bullet") 
		{
			float spawningTest = Random.Range (0f, 1f);
			if (spawningTest < spawningProbability && spawnObjetcs.Length != 0) {
				int randomIndex = Random.Range (0, spawnObjetcs.Length);
				GameObject spwanObject = Instantiate (spawnObjetcs [randomIndex], transform);
				spwanObject.transform.localPosition = spawningLocation;
			}

			GetComponent<SpriteRenderer> ().sprite = brokenSprite;
			SoundManager.PlayMultiple (breakingSound);
			Destroy (this);
		}
	}
}

