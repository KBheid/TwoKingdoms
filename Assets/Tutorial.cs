using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	public List<GameObject> tutorialScreens;
	public GameObject mainScreen;
	public GameObject mainTutorialScreen;

	public void Click()
	{
		for (int i=0; i<tutorialScreens.Count-1; i++)
		{
			if (tutorialScreens[i].activeInHierarchy)
			{
				tutorialScreens[i].SetActive(false);
				tutorialScreens[i+1].SetActive(true);
				return;
			}
		}

		// If we have made it here, then the last tutorial screen is active. We can go back to the main screen.
		mainScreen.SetActive(true);
		tutorialScreens[tutorialScreens.Count - 1].SetActive(false);
		tutorialScreens[0].SetActive(true);
		mainTutorialScreen.SetActive(false);
	}
}
