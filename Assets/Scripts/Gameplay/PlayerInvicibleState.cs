using UnityEngine;
using System.Collections;

public class PlayerInvicibleState : FSMState
{
	private Health health;
	private SpriteRenderer sprite;
	[SerializeField] float invulnerabilityFrames;
	[SerializeField] float blinkingRate;
	private float invulnerabilityFramesDelay;

	protected override void Awake()
	{
		ID = (int)PlayerStates.ID.Invincible;
		base.Awake ();

		health = GetComponent<Health> ();
		sprite = GetComponent<SpriteRenderer> ();
	}

	public override void Enter ()
	{
		health.Enable(false);
		StartCoroutine (InvulnerabilityRoutine());
	}

	public override void Exit ()
	{
		sprite.enabled = true;
		health.Enable(true);
	}

	IEnumerator InvulnerabilityRoutine()
	{
		invulnerabilityFramesDelay = invulnerabilityFrames;
		while (invulnerabilityFramesDelay > 0) 
		{
			invulnerabilityFramesDelay -= Time.deltaTime + blinkingRate;
			sprite.enabled = !sprite.enabled;
			yield return new WaitForSeconds (blinkingRate);
		}
		requestStackPop ();
	}
}

