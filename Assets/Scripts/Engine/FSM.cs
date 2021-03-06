﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM : MonoBehaviour
{
	public enum Action
	{
		Push,
		Pop,
		Clear,
	};

	private struct PendingChange
	{
		public Action action;
		public int stateID;

		public PendingChange(Action a, int ID = 0)
		{
			action = a;
			stateID = ID;
		}
	};

	private Stack<FSMState> stateStack;
	private List<PendingChange> pendingList;
	private Hashtable factories;


	protected virtual void Awake()
	{
		stateStack = new Stack<FSMState> ();
		pendingList = new List<PendingChange> ();
		factories = new Hashtable ();
	}

	public void RegisterState(int stateID, FSMState state)
	{
		factories.Add(stateID, state);
	}

	public void Update()
	{
		foreach(FSMState state in stateStack)
		{
			if (state.Update ())
				break;
		}
		ApplyPendingChanges ();
	}

	public void PushState(int stateID)
	{
		pendingList.Add(new PendingChange(Action.Push, stateID));
	}

	public void PopState()
	{
		if (IsEmpty())
			return;
		pendingList.Add(new PendingChange(Action.Pop));
	}

	public void ClearStates()
	{
		pendingList.Add(new PendingChange(Action.Clear));
	}

	public bool IsEmpty()
	{
		return stateStack.Count == 0;
	}

	private FSMState FindState(int stateID)
	{
		return (FSMState) factories [stateID];
	}

	private void ApplyPendingChanges()
	{
		foreach (PendingChange change in pendingList)
		{
			switch (change.action)
			{
			case Action.Push:
				FSMState pushState = FindState (change.stateID);
				pushState.Enter ();
				stateStack.Push(pushState);
				break;
			case Action.Pop:
				FSMState popState = stateStack.Pop ();
				popState.Exit();
				break;
			case Action.Clear:
				stateStack.Clear ();
				break;
			}
		}
		pendingList.Clear();
	}
}

