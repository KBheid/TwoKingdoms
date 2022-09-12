using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Destroy Adjacent", menuName = "Commands/DestroyAdjacent")]
public class DestroyAdjacentMinions : Effect
{
	public bool destroysAllies;
	public bool destroysEnemies;

	private List<Card> destroyedCards;
	private List<int> destroyedCardsHealth;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);
		destroyedCards = new List<Card>();
		destroyedCardsHealth = new List<int>();
		
		foreach (CardHolder ch in _owner._cardHolder.adjacentSpaces)
		{
			if (ch.card != null && ((ch.enemyHolder && destroysEnemies) || (!ch.enemyHolder && destroysAllies))) {
				destroyedCards.Add(ch.card);
				destroyedCardsHealth.Add(ch.card.health);
				ch.card.Die();
			}
		}
	}

	public override void Undo()
	{
		base.Undo();
		for (int i=0; i<destroyedCards.Count; i++)
		{
			destroyedCards[i].SetHealth(destroyedCardsHealth[i]);
		}
	}

	// Create an instance of this command and set values
	override public Command CreateInstance()
	{
		DestroyAdjacentMinions effInst = (DestroyAdjacentMinions)base.CreateInstance();

		effInst.destroysAllies = destroysAllies;
		effInst.destroysEnemies = destroysEnemies;

		if (destroyedCards != null)
		{
			effInst.destroyedCards = new List<Card>(destroyedCards);
			effInst.destroyedCardsHealth = new List<int>(destroyedCardsHealth);
		}

		return effInst;
	}
}
