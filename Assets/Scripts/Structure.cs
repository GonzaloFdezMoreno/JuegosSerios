using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public GameObject taskCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (taskCanvas != null && !UIManager.GetInstance().GetUICanvasIsOpen()) 
        {
            taskCanvas.SetActive(true);
            UIManager.GetInstance().OnUICanvasOpen();
            GameManager.GetInstance().nextTutorial(1);
        }
    }
    public GameObject GetTaskCanvas()
    {
        return taskCanvas;
    }
}
