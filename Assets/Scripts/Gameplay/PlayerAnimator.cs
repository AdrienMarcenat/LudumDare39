using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
	private Animator animator;
	private PlayerEventManager playerEventManager;

	void Awake ()
	{
		animator = GetComponent<Animator> ();
		playerEventManager = GetComponent<PlayerEventManager> ();
	}

	void OnEnable()
	{
		playerEventManager.Move += MovePlayer;
	}

	void OnDisable()
	{
		playerEventManager.Move -= MovePlayer;
	}

	private void MovePlayer (float x, float y)
	{
		if (x != 0 || y != 0)
			animator.SetBool ("PlayerMove", true);
		else
			animator.SetBool ("PlayerMove", false);
	}
}

