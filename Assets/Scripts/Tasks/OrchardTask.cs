using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrchardTask : Task
{
    public enum Vegetable { NONE, Tomatoes, Carrots, Peas, Lettuce };
    struct OrchardSpot
    {
        public Vegetable vegType;
        public int age;
        public int timeToBloom;
        public int timeToDie;
    }

    public Item tomato;
    public Item carrot;
    public Item pea;
    public Item lettuce;

    public Item tomatoSeeds;
    public Item carrotSeeds;
    public Item peaSeeds;
    public Item lettuceSeeds;

    public GameObject collectCostText;
    public GameObject plantTomatoCostText;
    public GameObject plantCarrotCostText;
    public GameObject plantPeaCostText;
    public GameObject plantLettuceCostText;
    public GameObject cleanOrchardCostText;

    const int orchardSpots = 4; // Numero de huecos para plantar en el huerto 

    //ArrayList spots; // Los huecos del huerto para plantar
    OrchardSpot[] spots;

    private int numTotalVegetable;
    private int numGoodVegetable;
    private int numBadVegetable;

    private int timeLifeVegetable;

    // Coste de acciones de las tareas
    const int plantActCost = 60;
    const int collectActCost = 50;
    const int cleanActCost = 20;

    // Start is called before the first frame update
    void Start()
    {
        numTotalVegetable = 0;
        numGoodVegetable = 0;
        numBadVegetable = 0;
        //spots = new ArrayList();
        spots = new OrchardSpot[orchardSpots];
        for (int i = 0; i < orchardSpots; i++)
        {
            OrchardSpot s = new OrchardSpot();
            s.vegType = Vegetable.NONE;
            s.age = 0;
            s.timeToBloom = 0;
            s.timeToDie = 0;
            //spots.Add(s);
            spots[i] = s;
        }
        UpdateCostTexts();
    }
    public override void OnNextTurn()
    {
        base.OnNextTurn();
        GameManager.Season currentSeason = GameManager.GetInstance().GetCurrentSeason();
        for (int i = 0; i < orchardSpots; i++)
        {
            if (spots[i].vegType != Vegetable.NONE)
            {
                // Las verduras plantadas que crecen en la estacion actual, crecen en este turno.
                // Las que estan plantadas y no crecen en la estacion actual mueren.
                switch (currentSeason)
                {
                    case GameManager.Season.Spring:
                        if (spots[i].vegType == Vegetable.Peas)
                            spots[i].age++;
                        else spots[i].age = spots[i].timeToDie;
                        break;
                    case GameManager.Season.Summer:
                        if (spots[i].vegType == Vegetable.Tomatoes)
                            spots[i].age++;
                        else spots[i].age = spots[i].timeToDie;
                        break;
                    case GameManager.Season.Fall:
                        if (spots[i].vegType == Vegetable.Lettuce)
                            spots[i].age++;
                        else spots[i].age = spots[i].timeToDie;
                        break;
                    case GameManager.Season.Winter:
                        if (spots[i].vegType == Vegetable.Carrots)
                            spots[i].age++;
                        else spots[i].age = spots[i].timeToDie;
                        break;
                    default:
                        break;
                }
                
                Debug.Log(spots[i].vegType + " " + spots[i].age + " semanas");
            }
        }
        UpdateCostTexts();
    }
    public void PlantTomatoes()
    {
        Plant(tomatoSeeds, Vegetable.Tomatoes);
    }
    public void PlantCarrots()
    {
        Plant(carrotSeeds, Vegetable.Carrots);
    }
    public void PlantPeas()
    {
        Plant(peaSeeds, Vegetable.Peas);
    }
    public void PlantLettuce()
    {
        Plant(lettuceSeeds, Vegetable.Lettuce);
    }
    public void Collect()
    {
        if (!CanDoTask(collectActCost)) return;
        for (int i = 0; i < orchardSpots; i++)
        {
            // Si la planta ha dado frutos y no se han pochado
            if (spots[i].vegType != Vegetable.NONE && spots[i].age >= spots[i].timeToBloom && spots[i].age < spots[i].timeToDie) 
            {
                switch (spots[i].vegType)
                {
                    case Vegetable.Tomatoes:
                        Item newItem = (Item)ScriptableObject.Instantiate(tomato);
                        newItem.amount = Random.Range(15, 25);
                        InventoryManager.GetInstance().AddItem(newItem);
                        break;
                    case Vegetable.Carrots:
                        newItem = (Item)ScriptableObject.Instantiate(carrot);
                        newItem.amount = Random.Range(15, 25);
                        InventoryManager.GetInstance().AddItem(newItem);
                        break;
                    case Vegetable.Peas:
                        newItem = (Item)ScriptableObject.Instantiate(pea);
                        newItem.amount = Random.Range(50, 80);
                        InventoryManager.GetInstance().AddItem(newItem);
                        break;
                    case Vegetable.Lettuce:
                        newItem = (Item)ScriptableObject.Instantiate(lettuce);
                        newItem.amount = Random.Range(5, 10);
                        InventoryManager.GetInstance().AddItem(newItem);
                        break;
                    default: break;
                }
                CleanSpot(i);
            }
        }
        SpendActions(collectActCost);
    }
    // Tarea para limpiar todos los huecos del huerto con verduras pochas
    public void CleanOrchard()
    {
        for (int i = 0; i < orchardSpots; i++)
        {
            if (spots[i].vegType != Vegetable.NONE && spots[i].age >= spots[i].timeToDie)
            {
                if (!CanDoTask(cleanActCost)) return;
                CleanSpot(i);
                SpendActions(cleanActCost);
            }
        }
    }
    void Plant(Item item, Vegetable type)
    {
        if (!CanDoTask(plantActCost)) return;
        int index = -1;
        //foreach (OrchardSpot i in spots)
        //{
        //    if (i.vegType == Vegetable.NONE)
        //    {
        //        index = spots.IndexOf(i);
        //        break;
        //    }
        //}
        for(int i = 0; i < orchardSpots; i++)
        {
            if (spots[i].vegType == Vegetable.NONE)
            {
                index = i;
                break;
            }
        }
        if (index == -1) return; // No hay espacio para plantar

        if (InventoryManager.GetInstance().UseItem(item, 1)) // Si tenemos semillas
        {
            spots[index].vegType = type;
            switch (type)
            {
                case Vegetable.Tomatoes:
                    spots[index].timeToBloom = 4;
                    spots[index].timeToDie = 7;
                    break;
                case Vegetable.Carrots:
                    spots[index].timeToBloom = 4;
                    spots[index].timeToDie = 7;
                    break;
                case Vegetable.Peas:
                    spots[index].timeToBloom = 4;
                    spots[index].timeToDie = 7;
                    break;
                case Vegetable.Lettuce:
                    spots[index].timeToBloom = 4;
                    spots[index].timeToDie = 7;
                    break;
                default: break;
            }
            SpendActions(plantActCost);
        }
        else // Si no mostramos el texto de que no tenemos semillas
        {
            InventoryManager.GetInstance().ShowNoSeedsText(type);
        }
    }
    void CleanSpot(int index)
    {
        spots[index].vegType = Vegetable.NONE;
        spots[index].age = 0;
        spots[index].timeToBloom = 0;
        spots[index].timeToDie = 0;
    }
    bool CanDoTask(int actCost)
    {
        int tractorDivFactor = 1;
        if (GameManager.GetInstance().IsTractorPurchased()) tractorDivFactor = 2;
        return GameManager.GetInstance().GetRemainingActions() >= actCost / tractorDivFactor;
    }
    void SpendActions(int actCost)
    {
        int tractorDivFactor = 1;
        if (GameManager.GetInstance().IsTractorPurchased()) tractorDivFactor = 2;
        GameManager.GetInstance().SpendActions(actCost / tractorDivFactor);
    }
    void UpdateCostTexts()
    {
        int tractorDivFactor = 1;
        if (GameManager.GetInstance().IsTractorPurchased()) tractorDivFactor = 2;
        collectCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + collectActCost / tractorDivFactor;
        plantTomatoCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + plantActCost / tractorDivFactor + "\nNecesita: semillas de tomate";
        plantCarrotCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + plantActCost / tractorDivFactor + "\nNecesita: semillas de zanahoria";
        plantPeaCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + plantActCost / tractorDivFactor + "\nNecesita: semillas de guisante";
        plantLettuceCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + plantActCost / tractorDivFactor + "\nNecesita: semillas de lechuga";
        cleanOrchardCostText.GetComponent<TextMeshProUGUI>().text = "Coste en acciones: " + cleanActCost / tractorDivFactor;
    }
}
