using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardAdderUI : MonoBehaviour
{
    [SerializeField] CardSelectorUI cardSelector;
    [SerializeField] Card card;
	[SerializeField] GameObject cardPrefab;

	[SerializeField] Text countText;

	private void Start()
	{
		cardSelector = transform.parent.parent.GetComponent<CardSelectorUI>();
	}

	// Copy stats
	public void SetCard(GameObject cardPrefab)
	{
		Card c = cardPrefab.GetComponent<Card>();
		this.cardPrefab = cardPrefab;

		card.attack = c.attack;
		card.cost = c.cost;
		card.health = c.health;
		card.cardDescription = c.cardDescription;
		card.cardName = c.cardName;
		card.playableInBackline = c.playableInBackline;
		card.playableInBuilding = c.playableInBuilding;
		card.playableInFrontline = c.playableInFrontline;
		card.cardImage = c.cardImage;
		card.draggable = false;
		card.backgroundColor = c.backgroundColor;

		card._cardUI.SetCard(card);

		card._cardUI.UpdateUI();
	}

    public void AddCard()
	{
		countText.text = cardSelector.AddCard(cardPrefab).ToString();
	}

    public void RemoveCard()
	{
		countText.text = cardSelector.RemoveCard(cardPrefab).ToString();
	}
}
