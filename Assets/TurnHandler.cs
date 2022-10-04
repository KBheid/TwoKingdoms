using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnHandler : MonoBehaviour
{
	public Text enemyHealthText;

	public GameObject gameEndPanel;
	public Text gameEndText;

	public void EndTurn()
	{
		int playerDamage = 0;

		CardHolder[] chs = FindObjectsOfType<CardHolder>();
		foreach (CardHolder ch in chs)
		{
			if (!ch.enemyHolder && ch.card != null)
			{
				if (ch.blockingCardHolder != null && ch.blockingCardHolder.card != null)
				{
					ch.blockingCardHolder.card.Damage(ch.card.attack);
				}
				else
				{
					playerDamage += ch.card.attack;
				}
			}
		}

		enemyHealthText.text = (10 - playerDamage).ToString();
		
		gameEndPanel.SetActive(true);
		
		if (playerDamage >= 10)
		{
			gameEndText.text = "Congratulations!\nYou have bested the puzzle.";
			if (playerDamage > 10)
				gameEndText.text += "\n You also dealt " + (playerDamage-10) + " extra damage!";
		}
		else
		{
			gameEndText.text = "You dealt " + (playerDamage) + " damage. \nYou have failed and may try again!";
		}
	}
}
