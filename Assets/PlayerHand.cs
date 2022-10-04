using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHand : MonoBehaviour
{
	public List<CardHolder> frontline;
	public List<CardHolder> backline;
	public List<CardHolder> buildings;

	[SerializeField] Text goldText; 

    bool isTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerState.instance.deck.Shuffle();

        Instantiate(PlayerState.instance.deck.Draw(), transform);
        Instantiate(PlayerState.instance.deck.Draw(), transform);
        Instantiate(PlayerState.instance.deck.Draw(), transform);

        EndTurnCommand.OnEndTurn += OnEndTurn;
    }

    void OnEndTurn()
    {
        isTurn = !isTurn;

        if (isTurn)
            OnStartTurn();
    }

    void OnStartTurn()
	{
		GatherIncome();

		Instantiate(PlayerState.instance.deck.Draw(), transform);
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

		MoneyMaster.instance.AddMoney(income);
	}
}
