using UnityEngine;
using System.Collections;

public class PlayerEventManager : MonoBehaviour
{
	public delegate void SimpleEvent();
	public event SimpleEvent Fire;
	public event SimpleEvent SwitchGun;
	public event SimpleEvent AmmoPack;
	public event SimpleEvent HealthPack;
	public event SimpleEvent UpdateUI;

	public delegate void WeaponPickAction(GameObject newWeapon);
	public event WeaponPickAction WeaponPick;

	public delegate void MoveAction(float x, float y);
	public event MoveAction Move;

	void Start()
	{
		UpdateUIEvent ();
	}

	protected void Update () 
	{
		if (GameManager.pause)
			return;

		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		if(Move != null)
			Move (horizontal, vertical);

		if (Input.GetButton ("Fire"))
		{
			if (Fire != null) 
				Fire ();
			UpdateUIEvent ();
		}

		if (Input.GetButtonDown ("SwitchGun"))
		{
			if (SwitchGun != null)
				SwitchGun ();
			UpdateUIEvent ();
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "AmmoPack") 
		{
			if(AmmoPack != null)
				AmmoPack ();
			UpdateUIEvent ();
			Destroy (other.gameObject);
		} 
		else if (other.tag == "HealthPack") 
		{
			if(HealthPack != null)
				HealthPack ();
			UpdateUIEvent ();
			Destroy (other.gameObject);
		} 
		else if (other.tag == "Weapon") 
		{
			if(WeaponPick != null)
				WeaponPick (other.gameObject);
			UpdateUIEvent ();
		} 
	}

	protected void UpdateUIEvent()
	{
		if(UpdateUI != null)
			UpdateUI ();
	}
}

