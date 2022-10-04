using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectorUI : MonoBehaviour
{
	public List<GameObject> selectableCardPrefabs;
	public GameObject CardSelectionUIPrefab;
	public PlayerState playerState;

	public Transform cardHolder;

	List<GameObject> currentCards;


	private void Start()
	{
		currentCards = new List<GameObject>();

		// Instantiate and set all cards
		foreach (GameObject c in selectableCardPrefabs)
		{
			CardAdderUI cardAdder = Instantiate(CardSelectionUIPrefab, cardHolder).GetComponent<CardAdderUI>();
			cardAdder.SetCard(c);
		}
	}

	public void SetDeck(List<GameObject> deck)
	{
		currentCards = deck;
	}
	public int AddCard(GameObject c)
	{
		currentCards.Add(c);
		return currentCards.FindAll( card => card == c ).Count;
	}
	public int RemoveCard(GameObject c)
	{
		currentCards.Remove(c);
		return currentCards.FindAll(card => card == c).Count;
	}

	public void Close()
	{
		playerState.deck = new Deck();
		playerState.deck.cardPrefabs = currentCards;

		gameObject.SetActive(false);
	}
}
