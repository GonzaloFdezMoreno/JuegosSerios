using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Instance
    static private UIManager Instance;

    public GameObject weekCounter;
    public GameObject moneyCounter;
    public GameObject actionCounter;
    public GameObject seasonCounter;

    private TextMeshProUGUI weekCounterText;
    private TextMeshProUGUI moneyCounterText;
    private TextMeshProUGUI actCounterText;
    private TextMeshProUGUI seasonCounterText;

    bool UICanvasIsOpen = false;

    private void Awake()
    {
        // Check if the UI Manager doesn't already exist
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        if (weekCounter != null) weekCounterText = weekCounter.GetComponent<TextMeshProUGUI>();
        if (moneyCounter != null) moneyCounterText = moneyCounter.GetComponent<TextMeshProUGUI>();
        if (actionCounter != null) actCounterText = actionCounter.GetComponent<TextMeshProUGUI>();
        if (seasonCounter != null) seasonCounterText = seasonCounter.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    

    public static UIManager GetInstance()
    {
        return Instance;
 
    }
    public void UpdateWeekCounter(int currentWeek)
    {
        if (weekCounterText != null) weekCounterText.text = "Semana " + currentWeek;
    }
    
    public void UpdateMoneyCounter(int money)
    {
        if (moneyCounterText != null) moneyCounterText.text = "Dinero: " + money + " €";
    }
    
    public void UpdateRemainingActions(int act)
    {
        if (actCounterText != null) actCounterText.text = "Acciones: " + act;
    }

    public void UpdateSeasonCounter(GameManager.Season season)
    {
        switch (season)
        {
            case GameManager.Season.Spring:
                if (seasonCounterText != null) seasonCounterText.text = "Primavera";
                break;
            case GameManager.Season.Summer:
                if (seasonCounterText != null) seasonCounterText.text = "Verano";
                break;
            case GameManager.Season.Fall:
                if (seasonCounterText != null) seasonCounterText.text = "Otoño";
                break;
            case GameManager.Season.Winter:
                if (seasonCounterText != null) seasonCounterText.text = "Invierno";
                break;
            default:
                break;
        }
    }

    public void OnUICanvasOpen()
    {
        UICanvasIsOpen = true;
    }
    public void OnUICanvasClose()
    {
        UICanvasIsOpen = false;
    }
    public bool GetUICanvasIsOpen()
    {
        return UICanvasIsOpen;
    }
}
