using UnityEngine;
using System.Collections;

public class PlayerNormalState : FSMState
{
	private WeaponManager weaponManager;
	private PlayerEventManager playerEventManager;
	private Health health;
	private MovingObject body;
	private Animator animator;

	protected override void Awake()
	{
		ID = (int)PlayerStates.ID.Normal;
		base.Awake ();
	
		playerEventManager = GetComponent<PlayerEventManager> ();
		health             = GetComponent<Health>();
		body               = GetComponent<MovingObject> ();
		weaponManager      = GetComponent<WeaponManager> ();
		animator           = GetComponent<Animator> ();
	}

	public override void Enter ()
	{
		playerEventManager.SwitchGun  += weaponManager.NextGun;
		playerEventManager.Fire       += weaponManager.Fire;
		playerEventManager.Move       += MovePlayer;
		playerEventManager.AmmoPack   += weaponManager.Reload;
		playerEventManager.HealthPack += health.Heal;
		playerEventManager.WeaponPick += weaponManager.WeaponPick;
		health.SimpleDamage           += Damage;
		health.GameOver               += GameOver;
	}

	public override void Exit ()
	{
		playerEventManager.SwitchGun  -= weaponManager.NextGun;
		playerEventManager.Fire       -= weaponManager.Fire;
		playerEventManager.Move       -= MovePlayer;
		playerEventManager.AmmoPack   -= weaponManager.Reload;
		playerEventManager.HealthPack -= health.Heal;
		playerEventManager.WeaponPick -= weaponManager.WeaponPick;
		health.SimpleDamage           -= Damage;
		health.GameOver               -= GameOver;
	}

	private void MovePlayer (float x, float y)
	{
		body.Move (x, y);

		if (x != 0 || y != 0)
			animator.SetBool ("PlayerMove", true);
		else
			animator.SetBool ("PlayerMove", false);

		Vector3 direction = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position).normalized;
		Quaternion rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90);
		transform.rotation = rotation;
	}

	private void GameOver()
	{
		GameManager.LoadScene (1);
	}

	private void Damage()
	{
		requestStackPush ((int) PlayerStates.ID.Invincible);
	}
}

