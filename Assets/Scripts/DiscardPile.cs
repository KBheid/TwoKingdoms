using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : CardHolder
{
	public override void PlayCardOnto(Card c)
	{
		c.draggable = false;
	}

	public override bool CanPlayCardOnto(Card c)
	{
		return true;
	}
}
