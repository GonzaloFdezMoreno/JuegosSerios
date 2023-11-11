using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private Text daysCounter;
    private int currentDay;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
   
    void Start()
    {
        currentDay = 1;
        UpdateDaysCounter();
    }

    void Update()
    {

    }
    public static GameManager GetInstance()
    {
        return instance;
    }
    public void OnNextTurnButtonPressed()
    {
        currentDay++;
        UpdateDaysCounter();
    }
    private void UpdateDaysCounter()
    {
        daysCounter = GameObject.Find("DaysCounter").GetComponent<Text>();
        if (daysCounter != null) daysCounter.text = "Día " + currentDay;
    }
}
