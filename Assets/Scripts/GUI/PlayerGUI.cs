using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
	private WeaponManager weaponManager;
	private Health playerHealth;
	private PlayerEventManager playerEventManager;

	[SerializeField] Image healthBar;
	[SerializeField] Text  ammoText;
	[SerializeField] Image weaponThumbnail;

	void Awake()
	{
		weaponManager      = GetComponent<WeaponManager>();
		playerHealth       = GetComponent<Health> ();
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

		Weapon currentWeapon = weaponManager.GetCurrentWeapon ();
		ammoText.text = "x" + currentWeapon.GetAmmo();
		weaponThumbnail.sprite = currentWeapon.GetThumbnail ();
		healthBar.fillAmount = currentHealth / totalHealth;
	}
}

