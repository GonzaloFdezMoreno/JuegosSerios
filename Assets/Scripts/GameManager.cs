using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject weekCounter;
    public GameObject moneyCounter;

    private static GameManager instance;

    private Text weekCounterText;
    private Text moneyCounterText;
    private int currentWeek;
    private int money;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }
   
    void Start()
    {
        currentWeek = 1;
        if(weekCounter != null)weekCounterText=weekCounter.GetComponent<Text>();
        UpdateWeekCounter();
        money = 100;
        if (moneyCounter != null) moneyCounterText = moneyCounter.GetComponent<Text>();
        UpdateWeekCounter();
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
