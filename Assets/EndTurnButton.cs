using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
	public void OnClick() {
		EndTurnCommand etc = ScriptableObject.CreateInstance("EndTurnCommand") as EndTurnCommand;
		etc.Do(true);
	}
}
