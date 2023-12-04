using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Instance
    static private UIManager Instance;

    public GameObject weekCounter;
    public GameObject moneyCounter;
    public GameObject actionCounter;

    private Text weekCounterText;
    private Text moneyCounterText;
    private Text actCounterText;

    private void Awake()
    {
        // Check if the UI Manager doesn't already exist
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        if (weekCounter != null) weekCounterText = weekCounter.GetComponent<Text>();
        if (moneyCounter != null) moneyCounterText = moneyCounter.GetComponent<Text>();
        if (actionCounter != null) actCounterText = actionCounter.GetComponent<Text>();
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
    
    public void UpdateRemainingActions(int act)
    {
        if (actCounterText != null) actCounterText.text = "Act: " + act;
    }

    
}
