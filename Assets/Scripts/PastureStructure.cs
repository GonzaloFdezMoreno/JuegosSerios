using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastureStructure : MonoBehaviour
{
    public GameObject frame;
    // Start is called before the first frame update
    void Start()
    {

    }


    
    private void OnMouseOver()
    {
        if (frame != null && !GameManager.GetInstance().IsPaused() && gameObject.GetComponent<PastureTasks>().IsAvailable()) frame.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (frame != null) frame.SetActive(false);
    }
    
}
