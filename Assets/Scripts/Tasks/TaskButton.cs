using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaskButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject costText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (costText != null) costText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (costText != null) costText.SetActive(false);
    }

}
