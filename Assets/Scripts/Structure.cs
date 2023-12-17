using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public GameObject taskCanvas;
    public bool onTutorial = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnMouseDown()
    {
        if (taskCanvas != null && !UIManager.GetInstance().GetUICanvasIsOpen()) 
        {
            taskCanvas.SetActive(true);
            UIManager.GetInstance().OnUICanvasOpen();
            if (onTutorial) { 
                GameManager.GetInstance().nextTutorial(1);
                onTutorial = false;
            }
        }
    }
    public GameObject GetTaskCanvas()
    {
        return taskCanvas;
    }
}
