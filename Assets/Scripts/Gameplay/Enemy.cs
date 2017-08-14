using UnityEngine;
using System.Collections;

public class Enemy : Character
{
	[SerializeField] protected int type;
	[SerializeField] protected float playerDamage;
	[SerializeField] protected bool isSeeking;
	[SerializeField] protected AudioClip sound;

	protected Transform target;
	[HideInInspector] public EnemyEventManager enemyEventManager;

	protected void Awake()
	{
		enemyEventManager = GetComponent<EnemyEventManager> ();
		characterEventManager = enemyEventManager;
	}

	protected void Start ()
	{
		base.Start ();

		target = GameObject.FindGameObjectWithTag ("Player").transform;
		isSeeking = false;
	}

	void OnEnable()
	{
		enemyEventManager.Damage   += Damage;
		enemyEventManager.GameOver += GameOver;
	}

	void OnDisable()
	{
		enemyEventManager.Damage   -= Damage;
		enemyEventManager.GameOver -= GameOver;
	}

	public void Damage(float damage, int weaponType)
	{
		Seek();
		float damageModifier = GameManager.GetMatching (type, weaponType);
		LoseHealth (damage * damageModifier);
		characterEventManager.LoseHealthEvent ();
		GameEventManager.EnemyHitEvent (type, weaponType);
		CheckIfGameOver();
	}
		
	private void GameOver ()
	{
		animator.SetTrigger ("isDying");
		boxCollider.enabled = false;
		Destroy (gameObject, 1);
	}

	private void OnCollisionStay2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Character player = other.gameObject.GetComponent<Character> ();
			player.characterEventManager.DamageEvent (playerDamage, 0);
		}
	}

	public void Seek()
	{
		if (isSeeking)
			return;

		enemyEventManager.SeekEvent ();
		SoundManager.PlayMultiple (sound);
		isSeeking = true;
		animator.SetTrigger ("isSeeking");
	}
}