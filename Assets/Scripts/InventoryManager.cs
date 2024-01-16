using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    static private InventoryManager Instance;

    private List<Item> items;
    public Transform itemContent;

    // Items que hay inicialmente en el inventario
    //public Item hensFeed;
    public Item[] startingItems;
    public int[] startingItemsAmount;

    // Inventario
    public GameObject inventoryItem;

    // Inventario de ventas
    public GameObject sellingInventoryItem;
    public Transform sellingItemContent;
    public GameObject itemName;
    public GameObject itemImage;
    public GameObject costText;

    private Item selectedItemToSell;

    // Textos para las tareas del huerto
    public GameObject noTomatoSeedsText;
    public GameObject noCarrotSeedsText;
    public GameObject noPeaSeedsText;
    public GameObject noLettuceSeedsText;
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
        CreateStartingItems();
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
            var itemAmount = obj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            var itemSprite = obj.transform.Find("Image").GetComponent<Image>();
            itemName.text = item.itemName;
            itemAmount.text = item.amount.ToString();
            itemSprite.sprite = item.sprite;
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
            var itemAmount = obj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            var itemSprite = obj.transform.Find("Image").GetComponent<Image>();
            itemName.text = item.itemName;
            itemAmount.text = item.amount.ToString();
            itemSprite.sprite = item.sprite;
            obj.GetComponent<Button>().onClick.AddListener(()=> { SelectToSell(obj); });
        }
    }
    public void SelectToSell(GameObject item)
    {
        string name = item.transform.Find("Name").GetComponent<Text>().text;
        string amount = item.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text;
        foreach (Item i in items)
        {
            if (i.itemName == name)
            {
                selectedItemToSell = i;
                break;
            }
        }
        itemName.GetComponent<TextMeshProUGUI>().text = name;
        costText.GetComponent<TextMeshProUGUI>().text = "¿Quieres vender " + amount + " " + name.ToLower() + " por " + selectedItemToSell.amount * selectedItemToSell.prize + "€?";
        // TODO: comprobar que se muestra el sprite del objeto a vender
        itemImage.SetActive(true);
        itemImage.GetComponent<Image>().sprite = selectedItemToSell.sprite;
    }
    public void SellItem()
    {
        if (selectedItemToSell != null)
        {
            // TODO: el precio no venga cableado
            GameManager.GetInstance().UpdateMoney(selectedItemToSell.amount * selectedItemToSell.prize);
            RemoveItem(selectedItemToSell);
            DeselectItemToSell();
        }
    }
    public void DeselectItemToSell()
    {
        selectedItemToSell = null;
        itemImage.SetActive(false);
        itemName.GetComponent<TextMeshProUGUI>().text = "";
        costText.GetComponent<TextMeshProUGUI>().text = "";
    }
    public bool UseItem(Item item, int amount)
    {
        int availableAmount = 0;
        string itemName = item.itemName;
        Item itemInInventory = null;
        foreach (Item i in items)
        {
            if (i.itemName == itemName)
            {
                itemInInventory = i;
                availableAmount = i.amount;
                break;
            }
        }
        if (availableAmount < amount) return false;
        int newAmount = availableAmount - amount;
        if (newAmount > 0)
        {
            itemInInventory.amount = newAmount;
            ListItems();
        }
        else RemoveItem(itemInInventory);

        return true;
    }
    public void ShowNoSeedsText(OrchardTask.Vegetable type)
    {
        switch (type)
        {
            case OrchardTask.Vegetable.Tomatoes:
                noTomatoSeedsText.SetActive(true);
                break;
            case OrchardTask.Vegetable.Carrots:
                noCarrotSeedsText.SetActive(true);
                break;
            case OrchardTask.Vegetable.Peas:
                noPeaSeedsText.SetActive(true);
                break;
            case OrchardTask.Vegetable.Lettuce:
                noLettuceSeedsText.SetActive(true);
                break;
            default: break;
        }
        StartCoroutine(WaitAndInvoke(2.0f, type));
    }
    private IEnumerator WaitAndInvoke(float delay, OrchardTask.Vegetable type)
    {
        yield return new WaitForSeconds(delay);
        HideNoSeedsText(type);
    }
    void HideNoSeedsText(OrchardTask.Vegetable type)
    {
        switch (type)
        {
            case OrchardTask.Vegetable.Tomatoes:
                noTomatoSeedsText.SetActive(false);
                break;
            case OrchardTask.Vegetable.Carrots:
                noCarrotSeedsText.SetActive(false);
                break;
            case OrchardTask.Vegetable.Peas:
                noPeaSeedsText.SetActive(false);
                break;
            case OrchardTask.Vegetable.Lettuce:
                noLettuceSeedsText.SetActive(false);
                break;
            default: break;
        }
    }
    private void CreateStartingItems()
    {
        items = new List<Item>();
        int index = 0;
        foreach(Item i in startingItems)
        {
            Item newItem = (Item)ScriptableObject.Instantiate(i);
            newItem.amount = startingItemsAmount[index];
            AddItem(newItem);
            index++;
        }
        ListItems();
    }

    public bool GiveItem(string itemname,int amount)
    {
        int index = -1;
        foreach (Item i in items)
        {
            if (i.itemName == itemname)
            {
                index = items.IndexOf(i);
                break;
            }
        }
        if (index != -1)
        {
            if (items[index].amount > amount)
            {
                items[index].amount -= amount;
                return true;
            }
        }
        return false;
        
    }
}
