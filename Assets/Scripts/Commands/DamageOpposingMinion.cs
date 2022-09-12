using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Opposing", menuName = "Commands/DamageOpposing")]
public class DamageOpposingMinion : Effect
{
	public int damage;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);

		Card opposingCard = _owner._cardHolder.blockingCardHolder.card;
		if (opposingCard != null)
		{
			opposingCard.Damage(damage);
		}
	}

	public override void Undo()
	{
		base.Undo(); 
		
		Card opposingCard = _owner._cardHolder.blockingCardHolder.card;
		if (opposingCard != null)
		{
			opposingCard.SetHealth(opposingCard.health + damage);
		}
	}

	// Create an instance of this command and set values
	override public Command CreateInstance()
	{
		DamageOpposingMinion effInst = (DamageOpposingMinion)base.CreateInstance();
		effInst.damage = damage;

		return effInst;
	}
}
