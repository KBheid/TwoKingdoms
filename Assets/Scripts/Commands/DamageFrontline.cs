using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Frontline", menuName = "Commands/DamageFrontline")]
public class DamageFrontline : Effect
{
    public bool damagesAllies;
    public bool damagesEnemies;
    public int damage;

	private List<Card> damagedCards;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);
		damagedCards = new List<Card>();

		FrontlineHolder[] flh = FindObjectsOfType<FrontlineHolder>();

		foreach (FrontlineHolder ch in flh)
		{
			if ((ch.enemyHolder && damagesEnemies) || (!ch.enemyHolder && damagesAllies) && ch.card != null)
			{
				Card c = ch.card;
				if (c == null)
					continue;

				damagedCards.Add(c);
				c.Damage(damage);
			}
		}
	}

	public override void Undo()
	{
		base.Undo();
		foreach (Card c in damagedCards) { 
			if (c != _owner)
				c.SetHealth(c.health + damage);
		}
	}

	// Create an instance of this command and set values
	override public Command CreateInstance()
	{
		DamageFrontline effInst = (DamageFrontline)base.CreateInstance();
		effInst.damage = damage;
		effInst.damagesAllies = damagesAllies;
		effInst.damagesEnemies = damagesEnemies;

		return effInst;
	}
}
