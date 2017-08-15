using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	[SerializeField] protected float totalHealth;
	protected float currentHealth;
	private bool enable;
	private float damageModifier;

	public delegate void SimpleEvent();
	public event SimpleEvent GameOver;
	public event SimpleEvent SimpleDamage;

	public delegate void DamageAction(float damage, int weaponType);
	public event DamageAction Damage;

	protected void Start()
	{
		currentHealth = totalHealth;
		enable = true;
		damageModifier = 1.0f;
	}

	public void LoseHealth(float damage, int weaponType)
	{
		if (!enable)
			return;
		
		if(Damage != null)
			Damage (damage, weaponType);

		currentHealth = Mathf.Max(0, currentHealth - damageModifier*damage);

		if (SimpleDamage != null)
			SimpleDamage ();
		
		CheckIfGameOver ();
	}

	public float GetCurrentHealth()
	{
		return currentHealth;
	}

	public float GetTotalHealth()
	{
		return totalHealth;
	}

	public void Heal()
	{
		currentHealth = totalHealth;
	}

	private void CheckIfGameOver ()
	{
		if (currentHealth <= 0 && GameOver != null)
			GameOver();
	}

	public void Enable(bool enable)
	{
		this.enable = enable;
	}

	public void SetDamageModifier(float modifier)
	{
		damageModifier = modifier;
	}
}

