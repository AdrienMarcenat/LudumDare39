using UnityEngine;
using System.Collections;

public abstract class FSMState : MonoBehaviour
{
	protected FSM fsm;
	protected int ID;

	public FSMState(FSM fsm)
	{
		this.fsm = fsm;
		fsm.RegisterState (ID, this);
	}

	public abstract bool Update ();

	protected void requestStackPush(int stateID)
	{
		fsm.PushState (stateID);
	}

	protected void requestStackPop()
	{
		fsm.PopState ();
	}

	protected void requestStateClear()
	{
		fsm.ClearStates ();
	}
}

