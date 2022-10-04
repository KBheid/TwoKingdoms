using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Minion at Position", menuName = "Commands/SpawnMinionAtPosition")]
public class CreateMinionPositionedEffect : Effect
{
	public GameObject cardPrefab;
	
	private Card createdCard;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);

		FrontlineHolder[] flh = FindObjectsOfType<FrontlineHolder>();

		foreach (FrontlineHolder ch in flh)
		{
			if (ch.name.Contains("Mid") && ch.enemyHolder == _owner._cardHolder.enemyHolder && ch.card == null)
			{
				Card c = Instantiate(cardPrefab).GetComponent<Card>();

				c.Play(ch, false);
				ch.card = c;
				createdCard = c;

				return;
			}
		}
	}

	public override void Undo()
	{
		base.Undo();

		if (createdCard)
		{
			createdCard._cardHolder.card = null;
			Destroy(createdCard);
		}
	}

	// Create an instance of this command and set values
	override public Command CreateInstance()
	{
		CreateMinionPositionedEffect effInst = (CreateMinionPositionedEffect) base.CreateInstance();

		effInst.cardPrefab = cardPrefab;
		effInst.createdCard = createdCard;

		return effInst;
	}
}
