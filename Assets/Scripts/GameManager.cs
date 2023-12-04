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
    }
    public void UpdateMoney(int amount)
    {
        money += amount;
        UIManager.GetInstance().UpdateMoneyCounter(money);
    }

    public void UpdateRemAct()
    {
        //actions -= acts;
        actions--;
        UIManager.GetInstance().UpdateRemainingActions(actions);
        if (evSpw.whenAppear > actions)
        {
            evSpw.popEvent();
        }
    }

    public int GetCurrentWeek()
    {
        return currentWeek;
    }
}
