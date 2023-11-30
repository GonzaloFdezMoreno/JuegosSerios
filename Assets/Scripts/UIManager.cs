using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Instance
    static private UIManager Instance;

    public GameObject weekCounter;
    public GameObject moneyCounter;
    public GameObject ActionsCounter;

    private Text weekCounterText;
    private Text moneyCounterText;
    private Text actionsCounterText;

    private void Awake()
    {
        // Check if the UI Manager doesn't already exist
        if (Instance == null)
        {
            Debug.Log("UI Manager Instanced");
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("Another UI Manager Instanced");
            DestroyImmediate(this);
        }

        if (weekCounter != null) weekCounterText = weekCounter.GetComponent<Text>();
        if (moneyCounter != null) moneyCounterText = moneyCounter.GetComponent<Text>();
        if (ActionsCounter != null) actionsCounterText = ActionsCounter.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    public void UpdateRemActions(int remAct)
    {

        if (actionsCounterText != null) actionsCounterText.text = "Act: " + remAct;


    }

}
