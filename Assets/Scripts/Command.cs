using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Command : ScriptableObject
{
	public bool isPlayerAction;


	public virtual void Do(bool isPlayerAction) {
		this.isPlayerAction = isPlayerAction;
	}

	public virtual void Undo() { }

	// Create an instance of this command and set values
	public virtual Command CreateInstance()
	{
		Command comm = (Command) CreateInstance(GetType().Name);
		comm.isPlayerAction = isPlayerAction;
		Commando.RaiseSendCommandEvent(comm);
		return comm;
	}

}
