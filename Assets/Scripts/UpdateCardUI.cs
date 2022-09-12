using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateCardUI : MonoBehaviour
{
	public Card c;
	public CardUI c_ui;

	private void Update()
	{
		if (Application.isPlaying || !gameObject.activeSelf)
			return;

		if (c_ui != null)
		{
			c_ui.SetCard(c);
		}

		if (c._cardHolder != null)
		{
			c.played = true;
			c.transform.SetParent(c._cardHolder.transform);

			RectTransform rt = c.GetComponent<RectTransform>();

			rt.anchorMin = Vector2.one * 0.5f;
			rt.anchorMax = Vector2.one * 0.5f;
			rt.pivot = Vector2.one * 0.5f;
			rt.anchoredPosition = Vector2.zero;
			rt.localRotation = Quaternion.Euler(Vector3.zero);
		}
	}
}
