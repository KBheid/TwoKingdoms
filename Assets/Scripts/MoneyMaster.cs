using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMaster : MonoBehaviour
{
	public Text moneyUI;
	private int coins = 15;

	public static MoneyMaster instance;
	private void Awake()
	{
		instance = this;
	}

	public void AddMoney(int money)
	{
		coins += money;
		moneyUI.text = coins.ToString();
	}

	public void TakeMoney(int money)
	{
		coins -= money;
		moneyUI.text = coins.ToString();
	}

	public int GetMoney()
	{
		return coins;
	}
}
