using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Text titleText;
    public Text descriptionText;
    public Text costText;
    public Text attackText;
    public Text healthText;
    public Image backgroundColor;

    public Image image;
    public AudioClip hoverToPlayAudio;

    public bool hoverable = true;

    private Card _referencedCard;
    private Canvas _canvas;
    private AudioSource audioPlayer;

    private bool _dragging = false;
    private Vector3 _dragStart;
    private Vector3 _hoverStartScale;
    private Vector3 _tmpOffset = Vector3.zero;

	private void Start()
	{
        _canvas = GetComponent<Canvas>();

		#region SFX
		if (audioPlayer == null && hoverToPlayAudio != null)
        {
            audioPlayer = gameObject.AddComponent<AudioSource>();
            audioPlayer.playOnAwake = false;
            audioPlayer.clip = hoverToPlayAudio;
            audioPlayer.loop = true;
            audioPlayer.volume = 0.1f;
        }
		#endregion
	}

	private void Update()
	{
        if (_referencedCard == null)
            return;


		#region SFX
		if (hoverToPlayAudio == null || audioPlayer == null)
            return;

		if (_dragging)
		{
            if (!audioPlayer.isPlaying)
                StartCoroutine(nameof(FadeSFXIn));
		}
        else
		{
            if (audioPlayer.isPlaying)
                StartCoroutine(nameof(FadeSFXOut));
            
            // Stop immediate if played, so that we can play more sounds
            if (_referencedCard.played)
                audioPlayer.Stop();
        }
		#endregion
	}


	public void SetCard(Card c)
	{
        _referencedCard = c;
        UpdateUI();
    }

    public void UpdateUI()
	{
        titleText.text          = _referencedCard.cardName;
        descriptionText.text    = _referencedCard.cardDescription;
        costText.text           = _referencedCard.cost.ToString();
        attackText.text         = _referencedCard.attack.ToString();
        healthText.text         = _referencedCard.health.ToString();

        backgroundColor.color   = _referencedCard.backgroundColor;

        image.sprite            = _referencedCard.cardImage;
    }

	#region Drag
	public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (!_referencedCard.draggable)
            return;

        _dragging = true;

        _dragStart = transform.position;
        gameObject.transform.localScale = new Vector2(0.25f, 0.25f);
    }
    public void OnDrag(PointerEventData data)
    {
        if (!_referencedCard.draggable)
            return;
        transform.position = data.position;
        //transform.position -= new Vector3(0, GetComponent<RectTransform>().rect.height/2, 0);
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (!_referencedCard.draggable)
            return;
        _dragging = false;

        CardHolder ch = PointerOverCardHolder();
        if (ch != null)
        {
            if (ch.CanPlayCardOnto(_referencedCard))
			{
                ch.PlayCardOnto(_referencedCard);
                _referencedCard.Play(ch);
                GetComponent<RectTransform>().localScale = Vector3.one;

                _tmpOffset = Vector3.zero;
                _canvas.overrideSorting = false;
                _hoverStartScale = Vector3.one;
                return;
			}
        }

        transform.position = _dragStart;
        gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
    }

    // Get CardHolder, assuming the mouse is over one
    private CardHolder PointerOverCardHolder()
    {
        List<RaycastResult> results = GetEventSystemRaycastResults();
        for (int index = 0; index < results.Count; index++)
		{
            RaycastResult curRaysastResult = results[index];
            if (curRaysastResult.gameObject.TryGetComponent(out CardHolder ch))
			{
                return ch;
			}
        }
        return null;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }


    #endregion
    
    #region OnHover
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_dragging || !hoverable)
            return;

        _hoverStartScale = transform.localScale;
        gameObject.transform.localScale *= 2;
        _canvas.overrideSorting = true;
        _canvas.sortingOrder = 2;

        // If it would go off the screen, adjust
        Vector3[] v = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(v);
        if (v[0].y <0)
		{
            Vector3 newBottom = Vector3.zero;
            newBottom.y += 0-v[0].y;
            _tmpOffset = newBottom;
            transform.position += _tmpOffset;
		}
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_dragging || !hoverable)
            return;

        transform.localScale = _hoverStartScale;
        _canvas.overrideSorting = false;
        _canvas.sortingOrder = 0;
        transform.position -= _tmpOffset;
    }
	#endregion

	#region SFX
	IEnumerator FadeSFXIn()
    {
        audioPlayer.Play();
        audioPlayer.volume = 0;
        float fadeDuration = 1.5f;
        float fadeEnd = 0.1f;

        float currentFade = 0f;
        while (currentFade < fadeDuration)
        {
            yield return new WaitForSeconds(0.1f);
            currentFade += 0.1f;

            audioPlayer.volume = Mathf.Lerp(0f, fadeEnd, currentFade / fadeDuration);
        }
    }
    IEnumerator FadeSFXOut()
    {
        float startVol = audioPlayer.volume;
        float fadeDuration = 1.5f;
        float currentFade = 0f;

        while (currentFade < fadeDuration)
        {
            yield return new WaitForSeconds(0.1f);
            currentFade += 0.1f;

            audioPlayer.volume = Mathf.Lerp(startVol, 0f, currentFade / fadeDuration);
        }
        audioPlayer.Stop();
    }
	#endregion
}
