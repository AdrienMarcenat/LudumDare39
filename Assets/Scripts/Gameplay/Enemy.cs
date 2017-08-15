using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	[SerializeField] protected int type;
	[SerializeField] protected float playerDamage;
	[SerializeField] protected bool isSeeking;
	[SerializeField] protected AudioClip sound;

	protected Transform target;
	protected Health health;
	protected Animator animator;

	public delegate void SimpleEvent();
	public event SimpleEvent EnemySeek;

	protected void Awake ()
	{
		animator = GetComponent<Animator> ();
		health = GetComponent<Health> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		isSeeking = false;
	}

	void OnEnable()
	{
		health.Damage   += Damage;
		health.GameOver += GameOver;
	}

	void OnDisable()
	{
		health.Damage   -= Damage;
		health.GameOver -= GameOver;
	}

	private void Damage(float damage, int weaponType)
	{
		Seek();
		float damageModifier = GameManager.instance.GetMatching (type, weaponType);
		health.SetDamageModifier (damageModifier);
		GameManager.instance.UpdateMatching (type, weaponType);
	}
		
	private void GameOver ()
	{
		animator.SetTrigger ("isDying");
		GetComponent<BoxCollider2D>().enabled = false;
		Destroy (gameObject, 1);
	}

	private void OnCollisionStay2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Health playerHealth = other.gameObject.GetComponent<Health> ();
			playerHealth.LoseHealth (playerDamage, 0);
		}
	}

	public void Seek()
	{
		if (isSeeking)
			return;

		if (EnemySeek != null)
			EnemySeek ();
		
		SoundManager.PlayMultiple (sound);
		isSeeking = true;
		animator.SetTrigger ("isSeeking");
	}
}