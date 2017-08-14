using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
	private Player player;
	private PlayerEventManager playerEventManager;

	[SerializeField] Image healthBar;
	[SerializeField] Text ammoText;
	[SerializeField] Image weaponThumbnail;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		playerEventManager = GetComponent<PlayerEventManager> ();
	}

	void OnEnable()
	{
		playerEventManager.UpdateUI += UpdateUI;
	}

	void OnDisable()
	{
		playerEventManager.UpdateUI -= UpdateUI;
	}

	protected void UpdateUI()
	{
		print ("bob");
		float currentHealth = player.GetCurrentHealth ();
		float totalHealth = player.GetTotalHealth ();

		Weapon currentWeapon = player.GetCurrentWeapon ();
		ammoText.text = "x" + currentWeapon.GetAmmo();
		weaponThumbnail.sprite = currentWeapon.GetThumbnail ();
		healthBar.fillAmount = currentHealth / totalHealth;
	}
}

