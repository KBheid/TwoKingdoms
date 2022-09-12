using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontlineHolder : CardHolder
{

	public override bool CanPlayCardOnto(Card c)
	{
		// TODO: Check cost?
		return c.playableInFrontline && card == null && c.played == false && !enemyHolder && MoneyMaster.instance.GetMoney() >= c.cost;
	}

	public override void PlayCardOnto(Card c) { }
}
