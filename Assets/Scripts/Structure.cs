using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject frame;
    bool onTutorial1 = true;
    bool onTutorial2 = true;
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
            int tutoNumber = GameManager.GetInstance().GetonTutorialNumber();
            if (onTutorial1&& tutoNumber==1) { 
                GameManager.GetInstance().nextTutorial(1);
                GameManager.GetInstance().AdvanceTutorialNumber();
                onTutorial1 = false;
            }
            else if (onTutorial2&& tutoNumber==4)
            {
                GameManager.GetInstance().nextTutorial(4);
                GameManager.GetInstance().AdvanceTutorialNumber();
                onTutorial2 = false;
            }
        }
    }
    private void OnMouseOver()
    {
        if (frame != null) frame.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (frame != null) frame.SetActive(false);
    }
    public GameObject GetTaskCanvas()
    {
        return taskCanvas;
    }
}
