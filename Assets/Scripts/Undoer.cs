using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undoer : MonoBehaviour
{
	public void Undo()
	{
		Commando.RaiseUndoEvent();
	}
	
	public void Redo()
	{
		Commando.RaiseRedoEvent();
	}
}
