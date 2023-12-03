using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorralTasksButtons : MonoBehaviour
{
    public GameObject corral;
    // Start is called before the first frame update
    void Start()
    {
        Transform son1 = transform.Find("CollectEggsButton");
        if (son1 != null)
        {
            Button btn = son1.GetComponentInChildren<Button>();
            btn.onClick.AddListener(OnTask1ButtonPressed);
        }
        Transform son2 = transform.Find("FeedButton");
        if (son2 != null)
        {
            Button btn = son2.GetComponentInChildren<Button>();
            btn.onClick.AddListener(OnTask2ButtonPressed);
        }
        Transform son3 = transform.Find("HealButton");
        if (son3 != null)
        {
            Button btn = son3.GetComponentInChildren<Button>();
            btn.onClick.AddListener(OnTask3ButtonPressed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTask1ButtonPressed()
    {
        if (corral != null) corral.GetComponent<CorralTasks>().CollectEggs();
    }
    public void OnTask2ButtonPressed()
    {
        if (corral != null) corral.GetComponent<CorralTasks>().FeedHens();
    }
    public void OnTask3ButtonPressed()
    {
        if (corral != null) corral.GetComponent<CorralTasks>().HealHens();
    }
}
