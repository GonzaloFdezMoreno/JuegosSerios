using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    private int currentWeek;
    private int money;


    
    public GameObject eventMngr;
   

    private EventProbPop nEvent;
    private int remAct;

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

        if (eventMngr != null) nEvent = eventMngr.GetComponent<EventProbPop>();
        nEvent.newChance(0);
        remAct = 84;
        currentWeek = 1;
        UIManager.GetInstance().UpdateWeekCounter(currentWeek);
        money = 100;
        UIManager.GetInstance().UpdateMoneyCounter(money);
    }

    void Update()
    {
        if (nEvent.whenAppear >= remAct)
        {
            nEvent.popEvent();
        }
    }
    public static GameManager GetInstance()
    {
        return Instance;
    }
    public void OnNextTurnButtonPressed()
    {
        currentWeek++;
        remAct = 84;
        UIManager.GetInstance().UpdateWeekCounter(currentWeek);
        nEvent.newChance(currentWeek - 1);
    }
    public void UpdateMoney(int amount)
    {
        money += amount;
        UIManager.GetInstance().UpdateMoneyCounter(money);
    }

    public void DestActPressed()
    {
        remAct--;

        UIManager.GetInstance().UpdateRemActions(remAct);
    }
}
