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
        if(taskCanvas != null)
        {
            taskCanvas.SetActive(true);
        }
    }
    public GameObject GetTaskCanvas()
    {
        return taskCanvas;
    }
}
