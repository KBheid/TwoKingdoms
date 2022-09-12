using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHolder : CardHolder
{
	public override bool CanPlayCardOnto(Card c)
	{
		return c.playableInBuilding && card == null && c.played == false && !enemyHolder && MoneyMaster.instance.GetMoney() >= c.cost;
	}

	public override void PlayCardOnto(Card c)
	{
	}
}
