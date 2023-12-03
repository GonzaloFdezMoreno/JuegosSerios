using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Instance
    static private InventoryManager Instance;
    public List<Item> items;
    public Transform itemContent;
    public GameObject inventoryItem;

    // Cantidad de cada item en el inventario
    int eggsAmount = 0;
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
    public static InventoryManager GetInstance()
    {
        return Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        items = new List<Item>();
        ListItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddItem(Item item)
    {
        int index = -1;
        foreach(Item i in items)
        {
            if (i.itemName == item.itemName) index = items.IndexOf(i);
        }
        if (index == -1) // Si el item no estaba ya en el inventario
        {
            items.Add(item);
        }
        else // Si estaba se suma la nueva cantidad
        {
            items[index].amount += item.amount;
        }
        ListItems();
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        ListItems();
    }
    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Item item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemAmount = obj.transform.Find("Amount").GetComponent<Text>();
            //var itemSprite = obj.transform.Find("Image").GetComponent<Image>();
            itemAmount.text = item.amount.ToString();
            //itemSprite.sprite = item.sprite;
        }
    }
}
