using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawCommand : Command
{
	GameObject _oldParent;
	GameObject _newParent;
	Card _card;

	public CardDrawCommand SetCard(Card c)
	{
		_card = c;
		return this;
	}

	public CardDrawCommand SetParents(GameObject newParent, GameObject oldParent)
	{
		_newParent = newParent;
		_oldParent = oldParent;
		return this;
	}

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);

		_card.transform.parent = _newParent.transform;
	}

	public override void Undo()
	{
		base.Undo();

		_card.transform.parent = _oldParent.transform;
	}

	public override Command CreateInstance()
	{
		CardDrawCommand cdc = (CardDrawCommand) base.CreateInstance();

		cdc._oldParent = _oldParent;
		cdc._newParent = _newParent;
		cdc._card = _card;

		return cdc;
	}
}
