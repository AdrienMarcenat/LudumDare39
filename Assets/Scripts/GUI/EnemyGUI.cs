using UnityEngine;
using System.Collections;

public class EnemyGUI : MonoBehaviour
{
	private Enemy enemy;
	private EnemyEventManager enemyEventManager;

	[SerializeField] SpriteRenderer healthBar;

	void Awake()
	{
		enemy = GetComponent<Enemy> ();
		enemyEventManager = GetComponent<EnemyEventManager> ();
		healthBar.enabled = false;
	}

	void OnEnable()
	{
		enemyEventManager.EnemySeek  += HealthBarEnable;
		enemyEventManager.UpdateUI   += UpdateUI;
		enemyEventManager.LoseHealth += UpdateUI;
	}

	void OnDisable()
	{
		enemyEventManager.EnemySeek  -= HealthBarEnable;
		enemyEventManager.UpdateUI   -= UpdateUI;
		enemyEventManager.LoseHealth -= UpdateUI;
	}

	private void HealthBarEnable()
	{
		healthBar.enabled = true;
	}

	private void UpdateUI()
	{
		float currentHealth = enemy.GetCurrentHealth ();
		float totalHealth = enemy.GetTotalHealth ();

		Vector3 scale = healthBar.transform.localScale;
		healthBar.transform.localScale = new Vector3 (currentHealth / totalHealth, scale.y, scale.z);
	}
}