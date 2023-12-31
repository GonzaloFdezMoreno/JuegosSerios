using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject frame;
    bool onTutorial = true;
    //bool onTutorial2 = true;
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
            if (onTutorial&& tutoNumber==1) { 
                GameManager.GetInstance().nextTutorial(1);
                GameManager.GetInstance().AdvanceTutorialNumber();
                onTutorial = false;
            }
            else if (onTutorial && tutoNumber == 3)
            {
                GameManager.GetInstance().nextTutorial(3);
                GameManager.GetInstance().AdvanceTutorialNumber();
                onTutorial = false;
            }
            else if (onTutorial&& tutoNumber==4)
            {
                GameManager.GetInstance().nextTutorial(4);
                GameManager.GetInstance().AdvanceTutorialNumber();
                onTutorial = false;
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
