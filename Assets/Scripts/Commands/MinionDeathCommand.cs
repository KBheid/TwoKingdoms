using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDeathCommand : Command
{
	private Card _owner;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);

		_owner._cardHolder.card = null;
		_owner.gameObject.SetActive(false);
	}

	public override void Undo()
	{
		base.Undo();

		_owner._cardHolder.card = _owner;
		_owner.gameObject.SetActive(true);
	}

	public MinionDeathCommand SetOwner(Card c)
	{
		_owner = c;

		return this;
	}
	public override Command CreateInstance()
	{
		MinionDeathCommand mdc = (MinionDeathCommand) base.CreateInstance();
		mdc._owner = _owner;

		return mdc;
	}
}
