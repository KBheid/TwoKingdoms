using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Self", menuName = "Commands/DamageSelf")]
public class SelfDamageEffect : Effect
{
	public int damage;

	public override void Do(bool isPlayerAction)
	{
		base.Do(isPlayerAction);
		_owner.Damage(damage);
	}

	public override void Undo()
	{
		base.Undo();
		_owner.health += damage;
	}

	// Create an instance of this command and set values
	override public Command CreateInstance()
	{
		SelfDamageEffect effInst = (SelfDamageEffect) base.CreateInstance();
		effInst.damage = damage;

		return effInst;
	}
}
