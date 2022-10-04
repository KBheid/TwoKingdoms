using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnCommand : Command
{
	public delegate void EndTurn();
	public static event EndTurn OnEndTurn;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);
		OnEndTurn?.Invoke();
	}

	public override Command CreateInstance()
	{
		EndTurnCommand etc = (EndTurnCommand) base.CreateInstance();

		return etc;
	}
}
