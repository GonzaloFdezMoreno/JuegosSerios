using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] structures;

    [SerializeField]
    private GameObject eventFrame;
    [SerializeField]
    private GameObject tutoFrame;

    private EventProbPop evSpw;
    private TutorialNarr tuto;

    private static GameManager Instance;
    private int currentWeek;
    private int money;
    private int actions;
    private int maxActions;
    private bool tutorial = true;

    int nTuto = 0;

    bool tractorPurchased = false;

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
        money = 1000;
        UIManager.GetInstance().UpdateMoneyCounter(money);
        actions = 84;
        maxActions = 84;
        UIManager.GetInstance().UpdateRemainingActions(actions);

        tuto= tutoFrame.GetComponent<TutorialNarr>();
        tuto.showTuto(0);
        AdvanceTutorialNumber();
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
        if(currentWeek == 2)
        {
            nextTutorial(5);
            evSpw.setAppear(60);
            AdvanceTutorialNumber();
        }
    }
    public void UpdateMoney(int amount)
    {
        money += amount;
        UIManager.GetInstance().UpdateMoneyCounter(money);
        if (money > 1200)
        {
            nextTutorial(9);
        }
    }
    public int GetCurrentMoney()
    {
        return money;
    }
    public void SpendActions(int acts)
    {
        actions -= acts;
        //actions--;
        UIManager.GetInstance().UpdateRemainingActions(actions);
        //if(actions == maxActions - 1&& currentWeek % 4 == 0)
        //{
        //    evSpw.popEvent(0);
        //}
        
        if (evSpw.whenAppear > actions)
        {
            if (currentWeek == 2) { 
                evSpw.popEvent(1);
                nextTutorial(6);
                evSpw.setIsEventTutorial(true);
            }
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

    public void nextTutorial(int i)
    {
        if (tutorial) {
            if (i < 10)
            {
                tuto.showTuto(i);
            }
            else
            {
                tutorial = false;
            }
        }
    }

    public int GetonTutorialNumber()
    {
        return nTuto;
    }
    public void AdvanceTutorialNumber()
    {
        nTuto++;
    }
    public void OnTractorPurchased()
    {
        tractorPurchased = true;
    }
    public void OnTractorSale()
    {
        tractorPurchased = false;
    }
    public bool IsTractorPurchased()
    {
        return tractorPurchased;
    }
}
