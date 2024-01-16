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
    private const int weeksPerSeason = 12;
    private int money;
    private int actions;
    private int maxActions;
    private bool tutorial = true;
    private bool inventoryTutorial = true;
    private bool onlyOnce = false;
    private bool evTutoAppeared = false;

    int nTuto = 0;

    bool tractorPurchased = false;

    public enum Season { Spring, Summer, Fall, Winter };
    Season currentSeason;

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
        endTutorial();

        evSpw = eventFrame.GetComponent<EventProbPop>();
        evSpw.newChance(0);
        currentWeek = 0;
        UIManager.GetInstance().UpdateWeekCounter(currentWeek + 1);
        money = 1000;
        UIManager.GetInstance().UpdateMoneyCounter(money);
        actions = 84;
        maxActions = 84;
        UIManager.GetInstance().UpdateRemainingActions(actions);
        currentSeason = Season.Spring;
        UIManager.GetInstance().UpdateSeasonCounter(currentSeason);
        tuto = tutoFrame.GetComponent<TutorialNarr>();
        if (tutorial)
        {
            
            tuto.showTuto(0);
            AdvanceTutorialNumber();
        }
        else
        {
            tuto.ShowAllHidden();
        }
       
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }
    public void OnNextTurnButtonPressed()
    {
        currentWeek++;
        UIManager.GetInstance().UpdateWeekCounter(currentWeek + 1);
        actions = 84;
        UIManager.GetInstance().UpdateRemainingActions(actions);
        OnNextTurnSeasonLogic();

        foreach (GameObject obj in structures)
        {
            obj.GetComponent<Task>().OnNextTurn();
        }
        evSpw.newChance(currentWeek);
        if ((currentWeek+1) % 4 == 0 && currentWeek != 0) { 
            evSpw.popEvent(0);
            UpdateMoney(-100);
        }
        /*if (tutorial)
        {*/
            if (currentWeek + 1 == 2)
            {
                nextTutorial(5);
                evSpw.setAppear(60);
                AdvanceTutorialNumber();
            }
        //}
        
    }
    public void UpdateMoney(int amount)
    {
        money += amount;
        UIManager.GetInstance().UpdateMoneyCounter(money);
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
            Debug.Log("Salta");
            if (currentWeek+1 == 2 &&!evTutoAppeared) { 
                evSpw.popEvent(3);
                nextTutorial(6);
                evSpw.setIsEventTutorial(true);
                evTutoAppeared = true;
            }
            //else
            //{
            //    int evType = Random.Range(0, 4);
            //    //tipo random de evento (aceptar/rechazar,beneficio,informacion) 
            //    evSpw.popEvent(evType);
            //}
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
               
                if (i != 2)
                {
                    tuto.showTuto(i);
                }
                else
                {
                    if (inventoryTutorial){
                        tuto.showTuto(i);
                        inventoryTutorial = false;
                        AdvanceTutorialNumber();
                    }
                }
               
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
        if (tutorial)
        {
            nextTutorial(9);
            tutorial = false;
        }
    }
    public void OnTractorSale()
    {
        tractorPurchased = false;
    }
    public bool IsTractorPurchased()
    {
        return tractorPurchased;
    }
    void OnNextTurnSeasonLogic()
    {
        if (currentWeek % weeksPerSeason == 0)
        {
            currentSeason++;
            if (currentSeason > Season.Winter) currentSeason = Season.Spring;
            UIManager.GetInstance().UpdateSeasonCounter(currentSeason);
        }
    }
    public Season GetCurrentSeason()
    {
        return currentSeason;
    }
    public int GetWeeksPerSeason()
    {
        return weeksPerSeason;
    }


    public void endTutorial()
    {
        tutorial = false;
    }

    public bool IsOnTutorial()
    {
        return tutorial;
    }

}
