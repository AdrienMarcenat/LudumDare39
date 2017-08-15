using UnityEngine;
using System.Collections;

public class EnemyGUI : MonoBehaviour
{
	private Enemy enemy;
	private Health health;

	[SerializeField] SpriteRenderer healthBar;

	void Awake()
	{
		enemy = GetComponent<Enemy> ();
		health = GetComponent<Health> ();
		healthBar.enabled = false;
	}

	void OnEnable()
	{
		enemy.EnemySeek += HealthBarEnable;
		health.SimpleDamage += UpdateUI;
	}

	void OnDisable()
	{
		enemy.EnemySeek -= HealthBarEnable;
		health.SimpleDamage -= UpdateUI;
	}

	private void HealthBarEnable()
	{
		healthBar.enabled = true;
	}

	private void UpdateUI()
	{
		float currentHealth = health.GetCurrentHealth ();
		float totalHealth = health.GetTotalHealth ();

		Vector3 scale = healthBar.transform.localScale;
		healthBar.transform.localScale = new Vector3 (currentHealth / totalHealth, scale.y, scale.z);
	}
}