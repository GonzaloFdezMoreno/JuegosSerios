using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static private InventoryManager Instance;

    public List<Item> items;
    public Transform itemContent;

    // Inventario
    public GameObject inventoryItem;

    // Inventario de ventas
    public GameObject sellingInventoryItem;
    public Transform sellingItemContent;
    public GameObject itemName;
    public GameObject itemImage;
    public GameObject costText;

    private Item selectedItemToSell;
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
            if (i.itemName == item.itemName)
            {
                index = items.IndexOf(i);
                break;
            }
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
        // Inventario
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Item item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("Name").GetComponent<Text>();
            var itemAmount = obj.transform.Find("Amount").GetComponent<Text>();
            //var itemSprite = obj.transform.Find("Image").GetComponent<Image>();
            itemName.text = item.itemName;
            itemAmount.text = item.amount.ToString();
            //itemSprite.sprite = item.sprite;
        }

        // Inventario de ventas
        foreach (Transform item in sellingItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Item item in items)
        {
            GameObject obj = Instantiate(sellingInventoryItem, sellingItemContent);
            var itemName = obj.transform.Find("Name").GetComponent<Text>();
            var itemAmount = obj.transform.Find("Amount").GetComponent<Text>();
            //var itemSprite = obj.transform.Find("Image").GetComponent<Image>();
            itemName.text = item.itemName;
            itemAmount.text = item.amount.ToString();
            //itemSprite.sprite = item.sprite;
            obj.GetComponent<Button>().onClick.AddListener(()=> { SelectToSell(obj); });
        }
    }
    public void SelectToSell(GameObject item)
    {
        string name = item.transform.Find("Name").GetComponent<Text>().text;
        string amount = item.transform.Find("Amount").GetComponent<Text>().text;
        itemName.GetComponent<Text>().text = name;
        costText.GetComponent<Text>().text = "¿Quieres vender " + amount + " " + name.ToLower() + "?";
        foreach (Item i in items)
        {
            if (i.itemName == name)
            {
                selectedItemToSell = i;
                break;
            }
        }
        // TODO: comprobar que se muestra el sprite del objeto a vender
        itemImage.SetActive(true);
        itemImage.GetComponent<Image>().sprite = selectedItemToSell.sprite;
    }
    public void SellItem()
    {
        if (selectedItemToSell != null)
        {
            // TODO: el precio no venga cableado
            GameManager.GetInstance().UpdateMoney(selectedItemToSell.amount * 5);
            RemoveItem(selectedItemToSell);
            DeselectItemToSell();
        }
    }
    public void DeselectItemToSell()
    {
        selectedItemToSell = null;
        itemImage.SetActive(false);
        itemName.GetComponent<Text>().text = "";
        costText.GetComponent<Text>().text = "";
    }
}
