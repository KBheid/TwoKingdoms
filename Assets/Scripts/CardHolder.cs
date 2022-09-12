using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardHolder : MonoBehaviour
{
	public bool enemyHolder;
	public Card card;
	public List<CardHolder> adjacentSpaces;
	public CardHolder blockingCardHolder;

	public abstract void PlayCardOnto(Card c);
	public abstract bool CanPlayCardOnto(Card c);
}
