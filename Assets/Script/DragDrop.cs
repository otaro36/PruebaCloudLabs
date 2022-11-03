using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler,IDropHandler
{
    private RectTransform m_RectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup m_CanvasGroup;
    
    public TMP_Text nameStudent;
    public float finalNote;
    public bool passed;
    public int id;

    
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
        if (finalNote>=3)
        {
            passed = true;
        }
        else
        {
            passed = false;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        m_CanvasGroup.alpha = 0.6f;
        m_CanvasGroup.blocksRaycasts=false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        m_RectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        m_CanvasGroup.alpha = 1f;
        m_CanvasGroup.blocksRaycasts = true;
        if (true)
        {

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.gameObject.name);
    }
}
