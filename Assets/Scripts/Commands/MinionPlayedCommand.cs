using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPlayedCommand : Command
{
	private Card _owner;
	private CardHolder _cardHolder;
	private Transform _hand;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);

		_owner.PlayWithoutEvent(_cardHolder);

		_hand = _owner.transform.parent;

		_owner.transform.localScale = new Vector3(1f, 1f, 1f);
		_owner.transform.SetParent(_cardHolder.transform, false);

		// Move it from hand to the proper position
		RectTransform rt = _owner.GetComponent<RectTransform>();

		rt.anchorMin = Vector2.one * 0.5f;
		rt.anchorMax = Vector2.one * 0.5f;
		rt.pivot = Vector2.one * 0.5f;
		rt.anchoredPosition = Vector2.zero;

		// Set values
		_cardHolder.card = _owner;
		_owner._cardHolder = _cardHolder;
		_owner.played = true;

		if (!_cardHolder.enemyHolder)
			MoneyMaster.instance.TakeMoney(_owner.cost);
	}

	public override void Undo()
	{
		base.Undo();

		_cardHolder.card = null;
		_owner.played = false;
		_owner.active = false;

		// Add card back to hand

		RectTransform rt = _owner.GetComponent<RectTransform>();
		rt.pivot = new Vector2(0.5f, 0f);
		_owner.transform.SetParent(_hand);
		_owner.transform.localScale = Vector2.one * 0.5f;

		if (!_cardHolder.enemyHolder)
			MoneyMaster.instance.AddMoney(_owner.cost);

		_owner.GetComponent<CardUI>().UpdateUI();
	}
	public MinionPlayedCommand SetOwner(Card c)
	{
		_owner = c;
		return this;
	}

	public MinionPlayedCommand SetCardHolder(CardHolder ch)
	{
		_cardHolder = ch;
		return this;
	}

	public override Command CreateInstance()
	{
		MinionPlayedCommand mpc = (MinionPlayedCommand) base.CreateInstance();
		mpc._owner = _owner;
		mpc._cardHolder = _cardHolder;
		mpc._hand = _hand;

		return mpc;
	}
}
