using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Minion", menuName = "Commands/SpawnMinion")]
public class CreateMinionEffect : Effect
{
	public int maxSpawns;
	public GameObject cardPrefab;
	
	private List<Card> createdCards;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);
		createdCards = new List<Card>();

		BacklineHolder[] blh = FindObjectsOfType<BacklineHolder>();

		foreach (BacklineHolder ch in blh)
		{
			if (!ch.enemyHolder && ch.card == null)
			{
				Card c = Instantiate(cardPrefab).GetComponent<Card>();
				c.Play(ch, false);
				ch.card = c;
				createdCards.Add(c);
			}
		}
	}

	public override void Undo()
	{
		base.Undo();
		foreach (Card c in createdCards)
		{
			c._cardHolder.card = null;
			Destroy(c.gameObject);
		}
	}

	// Create an instance of this command and set values
	override public Command CreateInstance()
	{
		CreateMinionEffect effInst = (CreateMinionEffect) base.CreateInstance();
		if (createdCards != null)
			effInst.createdCards = new List<Card>(createdCards);
		effInst.maxSpawns = maxSpawns;
		effInst.cardPrefab = cardPrefab;

		return effInst;
	}
}
