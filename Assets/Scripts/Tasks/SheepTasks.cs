using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SheepTasks : Task
{
    public Item cheese;
    public Item wool;
    public GameObject cheeseProdCostText;
    public GameObject shearCostText;
    public GameObject pastureCostText;
    public GameObject healCostText;
    public GameObject cannotShearText;

    bool cheeseProduced; // Si se ha producido queso en el turno actual
    bool canShear; // Si se puede esquilar en el turno actual
    int sheepNumber;
    int sickSheeps; // Numero de ovejas enfermas
    bool hungry; // Si las ovejas tienen hambre
    int lastShearWeek; // Semana en la que se esquilo por ultima vez
    int weeksToShear; // Semanas que deben pasar para volver a esquilar

    // Turno en el que comenzaron a estar hambrientas
    int hungryStartTurn;

    // Numero de turnos que pueden pasar las ovejas sin ser alimentadas
    // antes de enfermar
    const int maxHungryTurns = 2;

    // Turno en el que comenzaron a estar enfermas
    int sickStartTurn;

    // Numero de turnos que pueden pasar las ovejas estando enfermas
    // antes de morir
    const int maxSickTurns = 2;

    // Coste de acciones de las tareas
    const int cheeseProdActCost = 20;
    const int sheerActCost = 15;
    const int pastureActCost = 20;
    const int healActCost = 20;
    const int healMoneyCost = 50;

    // Start is called before the first frame update
    void Start()
    {
        cheeseProduced = false;
        canShear = true;
        lastShearWeek = 0;

        // Se puede esquilar una vez cada dos estaciones
        weeksToShear = GameManager.GetInstance().GetWeeksPerSeason() * 2;

        sheepNumber = 3;
        sickSheeps = 0;
        hungry = true;
        hungryStartTurn = GameManager.GetInstance().GetCurrentWeek();
        UpdateCostTexts();
    }

    public override void OnNextTurn()
    {
        base.OnNextTurn();
        cheeseProduced = false;
        if (!hungry)
        {
            hungry = true;
            hungryStartTurn = GameManager.GetInstance().GetCurrentWeek();
        }
        if (!canShear && GameManager.GetInstance().GetCurrentWeek() >= lastShearWeek + weeksToShear) 
        {
            canShear = true;
        }
        UpdateSickSheeps();
        UpdateSheepsNumber();
        Debug.Log("Nº ovejas: " + sheepNumber + ", de las cuales enfermas: " + sickSheeps);
        UpdateCostTexts();
    }
    public void ProduceCheese()
    {
        if (!cheeseProduced && GameManager.GetInstance().GetRemainingActions() >= cheeseProdActCost)
        {
            Item newItem = (Item)ScriptableObject.Instantiate(cheese);
            newItem.amount = sheepNumber;
            InventoryManager.GetInstance().AddItem(newItem);
            cheeseProduced = true;
            GameManager.GetInstance().SpendActions(cheeseProdActCost);
        }
    }
    public void ShearSheeps()
    {
        if (GameManager.GetInstance().GetRemainingActions() >= sheerActCost * sheepNumber) 
        {
            if(canShear)
            {
                Item newItem = (Item)ScriptableObject.Instantiate(wool);
                newItem.amount = sheepNumber;
                InventoryManager.GetInstance().AddItem(newItem);
                canShear = false;
                lastShearWeek = GameManager.GetInstance().GetCurrentWeek();
                GameManager.GetInstance().SpendActions(sheerActCost * sheepNumber);
            }
            else
            {
                cannotShearText.SetActive(true);
                Invoke("HideCannotShearText", 2.0f);
            }
        }
    }

    public void Pasture()
    {
        if (GameManager.GetInstance().PastureAvailable() && GameManager.GetInstance().GetRemainingActions() >= pastureActCost)
        {
            hungry = false;
            GameManager.GetInstance().SpendActions(pastureActCost);
            Debug.Log("A pastar!");
        }
    }
    public void HealSheeps()
    {
        if (GameManager.GetInstance().GetRemainingActions() >= healActCost)
        {
            sickSheeps = 0;
            GameManager.GetInstance().SpendActions(healActCost);
            GameManager.GetInstance().UpdateMoney(-healMoneyCost);
        }
    }
    void UpdateSickSheeps()
    {
        // TO DO: hay que cambiarlo para que lo haga un evento
        // ahora aumenta el numero de ovejas enfermas en los siguientes casos:
        // - si ya hay ovejas enfermas
        // - 30% de posibilidades (simulando un evento aleatorio)
        // - si llevan mas de maxHungryTurns turnos sin comer
        if (sickSheeps > 0 || Random.Range(0, 10) < 2 || (hungry && GameManager.GetInstance().GetCurrentWeek() > (hungryStartTurn + maxHungryTurns)))
        {
            if (sickSheeps == 0) sickStartTurn = GameManager.GetInstance().GetCurrentWeek();
            sickSheeps++;
            if (sickSheeps > sheepNumber) sickSheeps = sheepNumber;
        }
    }
    void UpdateSheepsNumber()
    {
        if (sickSheeps > 0 && GameManager.GetInstance().GetCurrentWeek() > (sickStartTurn + maxSickTurns))
        {
            int rand = Random.Range(1, sickSheeps + 1);
            sheepNumber -= rand;
            sickSheeps -= rand;
            if (sheepNumber < 0) sheepNumber = 0;
            if (sickSheeps < 0) sickSheeps = 0;
        }
    }
    void HideCannotShearText()
    {
        cannotShearText.SetActive(false);
    }
    void UpdateCostTexts()
    {
        cheeseProdCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + cheeseProdActCost;
        shearCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + sheerActCost * sheepNumber;
        pastureCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + pastureActCost + "\nNecesita: tener un terreno de pasto";
        healCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + healActCost + "\nCoste de dinero: " + healMoneyCost;
    }
}
