using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private GridLayoutGroup gridLayout;
    public int allStudent;
    public int passed;
    public int reprobate;
    public bool state;
    public GameObject neutral;
    public Transform passedPanel;
    public Transform reprobatePanel;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        eventData.pointerDrag.transform.SetParent(transform);
        Debug.Log(eventData.pointerDrag.gameObject.name);
        gridLayout.SetLayoutVertical();
        if (eventData.pointerDrag.gameObject.GetComponent<DragDrop>().passed!=state)
        {
            GameManager.instance.InstanciaPopUp(eventData.pointerDrag.gameObject.GetComponent<DragDrop>().id, " y esta ubicado en la casilla incorrecta",false);
        }
        if (passedPanel.childCount==passed&&reprobatePanel.childCount==reprobate)
        {
            GameManager.instance.CheckAprobateDragDrop();
        }
        if (neutral.transform.childCount==0)
        {
            GameManager.instance.ActivatePopUp();
        }

    }
}
