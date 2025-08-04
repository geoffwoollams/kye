 using System;
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.EventSystems;
 using UnityEngine.UI;


[RequireComponent(typeof(Selectable))]
public class MenuButtonSelector : MonoBehaviour, IPointerEnterHandler, IDeselectHandler
{
    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //outline.enabled = true;

        if (!EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //Debug.Log("eventData.selectedObject: " + eventData.selectedObject);
        //Debug.Log("EventSystem.current.currentSelectedGameObject: " + EventSystem.current.currentSelectedGameObject);
        if (eventData.selectedObject == null)
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            return;
        }

        //outline.enabled = false;
        //this.GetComponent<Selectable>().OnPointerExit(null);
    }
 }