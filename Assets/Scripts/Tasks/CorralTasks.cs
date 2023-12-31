using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CorralTasks : Task
{
    public Item eggs;
    public Item hensFeed;
    public GameObject collectEggsCostText;
    public GameObject feedCostText;
    public GameObject healCostText;

    int currentEggs;
    int hensNumber;
    const int maxHensNumber = 20;
    int sickHens; // Numero de gallinas enfermas
    bool hungry; // Si las gallinas tienen hambre

    // Turno en el que comenzaron a estar hambrientas
    int hungryStartTurn; 

    // Numero de turnos que pueden pasar las gallinas sin ser alimentadas
    // antes de enfermar
    const int maxHungryTurns = 2;

    // Turno en el que comenzaron a estar enfermas
    int sickStartTurn;

    // Numero de turnos que pueden pasar las gallinas estando enfermas
    // antes de morir
    const int maxSickTurns = 2;

    // Coste de acciones de las tareas
    const int collectEggsActCost = 10;
    const int feedActCost = 8;
    const int healActCost = 15;
    const int healMoneyCost = 50;

    // Start is called before the first frame update
    void Start()
    {
        currentEggs = 0;
        hensNumber = 5;
        sickHens = 0;
        hungry = true;
        hungryStartTurn = GameManager.GetInstance().GetCurrentWeek();
        CreateEggs();
        UpdateCostTexts();
    }

    public override void OnNextTurn()
    {
        base.OnNextTurn();
        if (!hungry)
        {
            hungry = true;
            hungryStartTurn = GameManager.GetInstance().GetCurrentWeek();
        }
        UpdateSickHens();
        UpdateHensNumber();
        CreateEggs();
        UpdateCostTexts();
    }
    void CreateEggs()
    {
        Debug.Log("N� gallinas: " + hensNumber + ", de las cuales enfermas: " + sickHens);
        currentEggs += (hensNumber - sickHens) * Random.Range(2, 7); //  De 2 a 6 huevos por gallina a la semana
    }
    public void CollectEggs()
    {
        if (currentEggs > 0 && GameManager.GetInstance().GetRemainingActions() >= collectEggsActCost) 
        {
            Item newItem = (Item)ScriptableObject.Instantiate(eggs);
            newItem.amount = currentEggs;
            InventoryManager.GetInstance().AddItem(newItem);
            currentEggs = 0;
            GameManager.GetInstance().SpendActions(collectEggsActCost);
        }
        
    }
    void UpdateSickHens()
    {
        // TO DO: hay que cambiarlo para que lo haga un evento
        // ahora aumenta el numero de gallinas enfermas en los siguientes casos:
        // - si ya hay gallinas enfermas
        // - 30% de posibilidades (simulando un evento aleatorio)
        // - si llevan mas de maxHungryTurns turnos sin comer
        if (sickHens > 0 || Random.Range(0, 10) < 3 || (hungry && GameManager.GetInstance().GetCurrentWeek() > (hungryStartTurn + maxHungryTurns))) 
        {
            if (sickHens == 0) sickStartTurn = GameManager.GetInstance().GetCurrentWeek();
            sickHens += Random.Range(1, 3);
            if (sickHens > hensNumber) sickHens = hensNumber;
        }
    }
    public void HealHens()
    {
        if (GameManager.GetInstance().GetRemainingActions() >= healActCost)
        {
            sickHens = 0;
            GameManager.GetInstance().SpendActions(healActCost);
            GameManager.GetInstance().UpdateMoney(-healMoneyCost);
        }
    }
    public void FeedHens()
    {
        if (GameManager.GetInstance().GetRemainingActions() >= feedActCost && InventoryManager.GetInstance().UseItem(hensFeed, hensNumber)) 
        {
            hungry = false;
            GameManager.GetInstance().SpendActions(feedActCost);
        }
    }
    void UpdateHensNumber()
    {
        // Si hay gallinas enfermas  
        if (sickHens > 0 && GameManager.GetInstance().GetCurrentWeek() > (sickStartTurn + maxSickTurns))
        {
            int rand = Random.Range(1, sickHens + 1);
            hensNumber -= rand;
            sickHens -= rand;
            if (hensNumber < 0) hensNumber = 0;
            if (sickHens < 0) sickHens = 0;
        }
        // Si no, pueden nacer gallinas
        else if (hensNumber > 0 && Random.Range(0, 10) < 4)  
        {
            hensNumber += 2;
            if (hensNumber > maxHensNumber) hensNumber = maxHensNumber;
        }
    }
    void UpdateCostTexts()
    {
        collectEggsCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + collectEggsActCost;
        feedCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + feedActCost;
        healCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + healActCost + "\nCoste de dinero: " + healMoneyCost;
    }
}
