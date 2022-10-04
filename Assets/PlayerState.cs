using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	public static PlayerState instance;
	#region Don't Destroy
	private void Awake()
	{
        DontDestroyOnLoad(this.gameObject);
		instance = this;
    }
	#endregion

	public Deck deck;


}
