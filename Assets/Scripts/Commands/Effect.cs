using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect : Command
{
    protected Card _owner;

    public enum EffectType
	{
        Battlecry,
        Deathrattle,
        OnDamage
	}

    public EffectType effectType;

    public Effect SetOwner(Card c)
	{
        _owner = c;
        return this;
	}

	// Create an instance of this command and set values
	override public Command CreateInstance()
	{
		Effect effInst = (Effect) base.CreateInstance();
		effInst._owner = _owner;

		return effInst;
	}
}
