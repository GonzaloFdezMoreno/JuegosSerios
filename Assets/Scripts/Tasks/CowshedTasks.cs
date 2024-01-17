using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CowshedTasks : Task
{
    public Item milk;
    public Item fertilizer;
    public Item hay;
    public GameObject milkCostText;
    public GameObject feedCostText;
    public GameObject pastureCostText;
    public GameObject healCostText;

    public GameObject cow1;
    public GameObject cow2;
    public GameObject cow3;

    bool milked; // Si se ha ordeñado ya a las vacas en el turno actual
    int cowsNumber;
    int sickCows; // Numero de vacas enfermas
    bool hungry; // Si las vacas tienen hambre
    const int dailyMeal = 14;

    // Turno en el que comenzaron a estar hambrientas
    int hungryStartTurn;

    // Numero de turnos que pueden pasar las vacas sin ser alimentadas
    // antes de enfermar
    const int maxHungryTurns = 2;

    // Turno en el que comenzaron a estar enfermas
    int sickStartTurn;

    // Numero de turnos que pueden pasar las vacas estando enfermas
    // antes de morir
    const int maxSickTurns = 2;

    // Coste de acciones de las tareas
    const int milkActCost = 20;
    const int feedActCost = 8;
    const int pastureActCost = 15;
    const int healActCost = 30;
    const int healMoneyCost = 80;

    // Start is called before the first frame update
    void Start()
    {
        milked = false;
        cowsNumber = 3;
        sickCows = 0;
        hungry = true;
        hungryStartTurn = GameManager.GetInstance().GetCurrentWeek();
        UpdateCostTexts();
        UpdateAnim();
    }
   
    public override void OnNextTurn()
    {
        base.OnNextTurn();
        milked = false;
        if (!hungry)
        {
            hungry = true;
            hungryStartTurn = GameManager.GetInstance().GetCurrentWeek();
        }
        UpdateSickCows();
        UpdateCowsNumber();
        Debug.Log("Nº vacas: " + cowsNumber + ", de las cuales enfermas: " + sickCows);
        UpdateCostTexts();
    }
    public void MilkCows()
    {
        if (!milked && GameManager.GetInstance().GetRemainingActions() >= milkActCost)
        {
            Item newItem = (Item)ScriptableObject.Instantiate(milk);
            newItem.amount = cowsNumber * Random.Range(16, 18);
            InventoryManager.GetInstance().AddItem(newItem);
            milked = true;
            GameManager.GetInstance().SpendActions(milkActCost);
        }
    }
    public void FeedCows()
    {
        if (GameManager.GetInstance().GetRemainingActions() >= feedActCost && InventoryManager.GetInstance().UseItem(hay, cowsNumber * dailyMeal)) 
        {
            hungry = false;
            Item newItem = (Item)ScriptableObject.Instantiate(fertilizer);
            newItem.amount = cowsNumber;
            InventoryManager.GetInstance().AddItem(newItem);
            GameManager.GetInstance().SpendActions(feedActCost);
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
    public void HealCows()
    {
        if (GameManager.GetInstance().GetRemainingActions() >= healActCost)
        {
            sickCows = 0;
            GameManager.GetInstance().SpendActions(healActCost);
            GameManager.GetInstance().UpdateMoney(-healMoneyCost);
            UpdateAnim();
        }
    }
    void UpdateSickCows()
    {
        // TO DO: hay que cambiarlo para que lo haga un evento
        // ahora aumenta el numero de vacas enfermas en los siguientes casos:
        // - si ya hay vacas enfermas
        // - 30% de posibilidades (simulando un evento aleatorio)
        // - si llevan mas de maxHungryTurns turnos sin comer
        if (sickCows > 0 || Random.Range(0, 10) < 2 || (hungry && GameManager.GetInstance().GetCurrentWeek() > (hungryStartTurn + maxHungryTurns)))
        {
            if (sickCows == 0) sickStartTurn = GameManager.GetInstance().GetCurrentWeek();
            sickCows++;
            if (sickCows > cowsNumber) sickCows = cowsNumber;
            UpdateAnim();
        }
    }
    void UpdateCowsNumber()
    {
        if (sickCows > 0 && GameManager.GetInstance().GetCurrentWeek() > (sickStartTurn + maxSickTurns))
        {
            int rand = Random.Range(1, sickCows + 1);
            cowsNumber -= rand;
            sickCows -= rand;
            if (cowsNumber < 0) cowsNumber = 0;
            if (sickCows < 0) sickCows = 0;
            UpdateAnim();
        }
    }
    void UpdateCostTexts()
    {
        milkCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + milkActCost;
        feedCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + feedActCost + "\nNecesita: "+ cowsNumber * dailyMeal+" de heno";
        pastureCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + pastureActCost + "\nNecesita: tener un terreno de pasto" ;
        healCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + healActCost + "\nCoste de dinero: " + healMoneyCost;
    }
    void UpdateAnim()
    {
        if (cowsNumber == 0)
        {
            cow1.SetActive(false);
            cow2.SetActive(false);
            cow3.SetActive(false);
        }
        else if (cowsNumber == 1)
        {
            cow1.SetActive(true);
            cow2.SetActive(false);
            cow3.SetActive(false);

            cow1.GetComponent<Animator>().Play("VacaNormal");
        }
        else if (cowsNumber == 2)
        {
            cow1.SetActive(true);
            cow2.SetActive(true);
            cow3.SetActive(false);

            cow1.GetComponent<Animator>().Play("VacaNormal");
            cow1.GetComponent<Animator>().Play("VacaNormal");
        }
        else if (cowsNumber == 3) 
        {
            cow1.SetActive(true);
            cow2.SetActive(true);
            cow3.SetActive(true);

            cow1.GetComponent<Animator>().Play("VacaNormal");
            cow1.GetComponent<Animator>().Play("VacaNormal");
            cow1.GetComponent<Animator>().Play("VacaNormal");
        }

        if (sickCows == 1) cow1.GetComponent<Animator>().Play("VacaEnferma");
        else if (sickCows == 2)
        {
            cow1.GetComponent<Animator>().Play("VacaEnferma");
            cow2.GetComponent<Animator>().Play("VacaEnferma");
        }
        else if (sickCows == 3)
        {
            cow1.GetComponent<Animator>().Play("VacaEnferma");
            cow2.GetComponent<Animator>().Play("VacaEnferma");
            cow3.GetComponent<Animator>().Play("VacaEnferma");
        }
    }
}
