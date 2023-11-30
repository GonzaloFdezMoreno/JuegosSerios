using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject weekCounter;
    public GameObject moneyCounter;

    private static GameManager Instance;

    private Text weekCounterText;
    private Text moneyCounterText;
    private int currentWeek;
    private int money;
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
    }
   
    void Start()
    {
        currentWeek = 1;
        if(weekCounter != null)weekCounterText=weekCounter.GetComponent<Text>();
        UpdateWeekCounter();
        money = 100;
        if (moneyCounter != null) moneyCounterText = moneyCounter.GetComponent<Text>();
        UpdateMoneyCounter();
    }

    void Update()
    {

    }
    public static GameManager GetInstance()
    {
        return Instance;
    }
    public void OnNextTurnButtonPressed()
    {
        currentWeek++;
        UpdateWeekCounter();
    }
    private void UpdateWeekCounter()
    {
        if (weekCounterText != null) weekCounterText.text = "Semana " + currentWeek;
    }
    public void UpdateMoney(int amount)
    {
        money += amount;
        UpdateMoneyCounter();
    }
    private void UpdateMoneyCounter()
    {
        if (moneyCounterText != null) moneyCounterText.text = "Dinero: " + money + " €";
    }
}
