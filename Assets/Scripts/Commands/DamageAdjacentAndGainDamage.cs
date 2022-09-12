using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOE Damage and Gain attack", menuName = "Commands/AOEDamageGainAttack")]
public class DamageAdjacentAndGainDamage : Effect
{
	CardHolder playedSpace;
	int gainedAttack;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);

		gainedAttack = 0;

		playedSpace = _owner._cardHolder;
		foreach (CardHolder ch in playedSpace.adjacentSpaces)
		{
			if (ch.card != null)
			{
				ch.card.Damage(1);
				gainedAttack++;
			}
		}

		_owner.SetAttack(_owner.attack + gainedAttack);
	}

	public override void Undo()
	{
		base.Undo();

		_owner.SetAttack(_owner.attack - gainedAttack);
		foreach (CardHolder ch in playedSpace.adjacentSpaces)
		{
			if (ch.card != null)
			{
				ch.card.SetHealth(ch.card.health+1);
				gainedAttack++;
			}
		}
	}

	public override Command CreateInstance()
	{
		DamageAdjacentAndGainDamage effInst = (DamageAdjacentAndGainDamage)base.CreateInstance();

		effInst.playedSpace = playedSpace;
		effInst.gainedAttack = gainedAttack;

		return effInst;
	}
}
