using UnityEngine;
using System.Collections;

public class PlayerSoundManager : MonoBehaviour
{
	private PlayerEventManager playerEventManager;

	[SerializeField] AudioClip reloadSound;
	[SerializeField] AudioClip damageSound;
	[SerializeField] AudioClip healSound;
	[SerializeField] AudioClip switchGunSound;

	void Awake()
	{
		playerEventManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>().playerEventManager;
	}

	void OnEnable()
	{
		playerEventManager.SwitchGun  += SwitchGun;
		playerEventManager.AmmoPack   += Reload;
		playerEventManager.HealthPack += Heal;
		playerEventManager.WeaponPick += SwitchGun;
		playerEventManager.LoseHealth += Damage;
	}

	void OnDisable()
	{
		playerEventManager.SwitchGun  -= SwitchGun;
		playerEventManager.AmmoPack   -= Reload;
		playerEventManager.HealthPack -= Heal;
		playerEventManager.WeaponPick -= SwitchGun;
		playerEventManager.LoseHealth -= Damage;
	}

	private void Reload()
	{
		SoundManager.PlayMultiple (reloadSound);
	}

	private void SwitchGun()
	{
		SoundManager.PlayMultiple (switchGunSound);
	}

	private void SwitchGun(GameObject newWeapon)
	{
		SoundManager.PlayMultiple (switchGunSound);
	}

	private void Heal()
	{
		SoundManager.PlayMultiple (healSound);
	}

	private void Damage()
	{
		SoundManager.PlayMultiple (damageSound);
	}
}

