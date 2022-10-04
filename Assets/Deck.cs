using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Deck
{
	public List<GameObject> cardPrefabs;


	public void Shuffle()
	{

        for (int i = 0; i < cardPrefabs.Count; i++)
        {
            GameObject temp = cardPrefabs[i];
            int randomIndex = UnityEngine.Random.Range(i, cardPrefabs.Count);
            cardPrefabs[i] = cardPrefabs[randomIndex];
            cardPrefabs[randomIndex] = temp;
        }
    }

    public GameObject Draw()
	{
        GameObject drawnCard = cardPrefabs[0];
        cardPrefabs.RemoveAt(0);

        return drawnCard;
	}

    public void AddCardToTop(GameObject c)
	{
        cardPrefabs.Insert(0, c);
	}
}