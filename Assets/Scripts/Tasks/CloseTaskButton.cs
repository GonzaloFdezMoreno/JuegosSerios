using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTaskButton : MonoBehaviour
{
    public GameObject taskCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnCloseButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCloseButtonPressed()
    {
        if (taskCanvas != null)
        {
            taskCanvas.SetActive(false);
        }
    }
}
