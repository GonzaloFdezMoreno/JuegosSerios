using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] structures;

    private static GameManager Instance;
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
        UIManager.GetInstance().UpdateWeekCounter(currentWeek);
        money = 100;
        UIManager.GetInstance().UpdateMoneyCounter(money);
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
        foreach (GameObject obj in structures)
        {
            obj.GetComponent<Task>().OnNextTurn();
        }
    }
    public void UpdateMoney(int amount)
    {
        money += amount;
        UIManager.GetInstance().UpdateMoneyCounter(money);
    }
    public int GetCurrentWeek()
    {
        return currentWeek;
    }
}
