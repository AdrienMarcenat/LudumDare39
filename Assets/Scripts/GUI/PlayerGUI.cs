using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
	private Player player;
	private Health playerHealth;
	private PlayerEventManager playerEventManager;

	[SerializeField] Image healthBar;
	[SerializeField] Text  ammoText;
	[SerializeField] Image weaponThumbnail;

	void Awake()
	{
		player = GetComponent<Player>();
		playerHealth = GetComponent<Health> ();
		playerEventManager = GetComponent<PlayerEventManager> ();
	}

	void Start()
	{
		UpdateUI ();
	}

	void OnEnable()
	{
		playerHealth.SimpleDamage   += UpdateUI;
		playerEventManager.UpdateUI += UpdateUI;
	}

	void OnDisable()
	{
		playerHealth.SimpleDamage   -= UpdateUI;
		playerEventManager.UpdateUI -= UpdateUI;
	}

	protected void UpdateUI()
	{
		float currentHealth = playerHealth.GetCurrentHealth ();
		float totalHealth = playerHealth.GetTotalHealth ();

		Weapon currentWeapon = player.GetCurrentWeapon ();
		ammoText.text = "x" + currentWeapon.GetAmmo();
		weaponThumbnail.sprite = currentWeapon.GetThumbnail ();
		healthBar.fillAmount = currentHealth / totalHealth;
	}
}

