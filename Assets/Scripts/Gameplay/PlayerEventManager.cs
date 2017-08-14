using UnityEngine;
using System.Collections;

public class PlayerEventManager : CharacterEventManager
{
	public delegate void SimpleEvent();
	public event SimpleEvent Fire;
	public event SimpleEvent SwitchGun;
	public event SimpleEvent AmmoPack;
	public event SimpleEvent HealthPack;

	public delegate void WeaponPickAction(GameObject newWeapon);
	public event WeaponPickAction WeaponPick;

	public delegate void MoveAction(float x, float y);
	public event MoveAction Move;

	protected void Update () 
	{
		if (GameManager.pause)
			return;

		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		Move (horizontal, vertical);

		if (Input.GetButton ("Fire"))
		{
			Fire ();
			UpdateUIEvent ();
		}

		if (Input.GetButtonDown ("SwitchGun"))
		{
			SwitchGun ();
			UpdateUIEvent ();
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "AmmoPack") 
		{
			AmmoPack ();
			UpdateUIEvent ();
			Destroy (other.gameObject);
		} 
		else if (other.tag == "HealthPack") 
		{
			HealthPack ();
			UpdateUIEvent ();
			Destroy (other.gameObject);
		} 
		else if (other.tag == "Weapon") 
		{
			WeaponPick (other.gameObject);
			UpdateUIEvent ();
		} 
	}
}

