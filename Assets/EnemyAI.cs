using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Deck deck;

    public List<CardHolder> frontline;
    public List<CardHolder> backline;
    public List<CardHolder> buildings;

    public List<GameObject> currentHandCardPrefabs;

	public Text goldText;

	bool isTurn = false;
	int currentGold = 0;

	private void Start()
	{
		EndTurnCommand.OnEndTurn += OnEndTurn;

		currentGold = int.Parse(goldText.text);

		deck.Shuffle();

		currentHandCardPrefabs.Add(deck.Draw());
		currentHandCardPrefabs.Add(deck.Draw());
		currentHandCardPrefabs.Add(deck.Draw());
	}

    void OnEndTurn()
	{
		isTurn = !isTurn;


		if (isTurn)
		{
			StartTurn();
		}
	}

	void StartTurn()
	{
		GatherIncome();

		// Draw a card
		currentHandCardPrefabs.Add(deck.Draw());

		bool played = false;
		Card c = null;
		foreach (GameObject go in currentHandCardPrefabs)
		{
			c = go.GetComponent<Card>();

			if (c.playableInBuilding && c.cost < currentGold)
			{
				if (PlayCard(buildings, c))
				{
					played = true;
					break;
				}
			}

			if (c.playableInFrontline && c.cost < currentGold)
			{
				if (PlayCard(frontline, c))
				{
					played = true;
					break;
				}
			}

			if (c.playableInBackline && c.cost < currentGold)
			{
				if (PlayCard(backline, c))
				{
					played = true;
					break;
				}
			}
		}
		
		if (played)
			currentHandCardPrefabs.Remove(c.gameObject);


		EndTurnCommand etc = ScriptableObject.CreateInstance("EndTurnCommand") as EndTurnCommand;
		etc.Do(true);
	}

	bool PlayCard(List<CardHolder> chs, Card c)
	{
		foreach (CardHolder ch in chs)
		{
			if (ch.card == null)
			{
				Instantiate(c).Play(ch, false);
				
				currentGold -= c.cost;
				goldText.text = currentGold.ToString();
				currentHandCardPrefabs.Remove(c.gameObject);
				return true;
			}
		}
		return false;
	}


	void GatherIncome()
	{
		int income = 0;
		foreach (FrontlineHolder flh in frontline)
		{
			if (flh.card == null)
				continue;

			if (flh.card.TryGetComponent(out GrantsIncome incomeSource))
			{
				income += incomeSource.income;
			}
		}
		foreach (BacklineHolder flh in backline)
		{
			if (flh.card == null)
				continue;

			if (flh.card.TryGetComponent(out GrantsIncome incomeSource))
			{
				income += incomeSource.income;
			}
		}
		foreach (BuildingHolder flh in buildings)
		{
			if (flh.card == null)
				continue;

			if (flh.card.TryGetComponent(out GrantsIncome incomeSource))
			{
				income += incomeSource.income;
			}
		}

		currentGold += income;
		goldText.text = currentGold.ToString();

		print(income);
	}
}
