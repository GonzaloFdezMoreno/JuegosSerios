using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] structures;

    [SerializeField]
    private GameObject eventFrame;

    private EventProbPop evSpw;

    private static GameManager Instance;
    private int currentWeek;
    private int money;
    private int actions;
    private int maxActions;
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
    }
   
    void Start()
    {

        evSpw = eventFrame.GetComponent<EventProbPop>();
        evSpw.newChance(0);
        currentWeek = 1;
        UIManager.GetInstance().UpdateWeekCounter(currentWeek);
        money = 100;
        UIManager.GetInstance().UpdateMoneyCounter(money);
        actions = 84;
        maxActions = 84;
        UIManager.GetInstance().UpdateRemainingActions(actions);

        
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
        UIManager.GetInstance().UpdateWeekCounter(currentWeek);
        actions = 84;
        UIManager.GetInstance().UpdateRemainingActions(actions);
        
        foreach (GameObject obj in structures)
        {
            obj.GetComponent<Task>().OnNextTurn();
        }
        evSpw.newChance(currentWeek);
        if (currentWeek % 4 == 0 && currentWeek != 0) { 
            evSpw.popEvent(0);
            UpdateMoney(-100);
        }
    }
    public void UpdateMoney(int amount)
    {
        money += amount;
        UIManager.GetInstance().UpdateMoneyCounter(money);
    }

    public void UpdateRemAct(int acts)
    {
        actions -= acts;
        //actions--;
        UIManager.GetInstance().UpdateRemainingActions(actions);
        if(actions == maxActions - 1&& currentWeek % 4 == 0)
        {
            evSpw.popEvent(0);
        }
        if (evSpw.whenAppear > actions)
        {
            evSpw.popEvent(1);
        }
    }

    public int GetCurrentWeek()
    {
        return currentWeek;
    }
    public int GetRemainingActions()
    {
        return actions;
    }
    public bool PastureAvailable()
    {
        foreach (GameObject obj in structures)
        {
            if (obj.GetComponent<PastureTasks>() != null && obj.GetComponent<PastureTasks>().IsAvailable())
                return true;
                
        }
        return false;
    }
}
