using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    static private ShopManager Instance;

    public List<Item> items;

    public GameObject shopInventoryItem;
    public Transform shopItemContent;
    public GameObject itemName;
    public GameObject itemImage;
    public GameObject descriptionText;
    public GameObject amountText;
    public GameObject prizeText;
    public GameObject tractorPurchasedText;

    private Item selectedItemToBuy;
    private int selectedAmount;
    private int currentUnitPrize; // Precio por unidad del item seleccionado
    bool tractorPurchased = false;
    private void Awake()
    {
        // Check if the Shop Manager doesn't already exist
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
    public static ShopManager GetInstance()
    {
        return Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        selectedAmount = 1;
        currentUnitPrize = 5; // TODO QUE NO VENGA CABLEADO
        ListItems();
    }
    void ListItems()
    {
        // Inventario de ventas
        foreach (Transform item in shopItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Item item in items)
        {
            GameObject obj = Instantiate(shopInventoryItem, shopItemContent);
            var itemName = obj.transform.Find("Name").GetComponent<Text>();
            //var itemAmount = obj.transform.Find("Amount").GetComponent<Text>();
            var itemSprite = obj.transform.Find("Image").GetComponent<Image>();
            itemName.text = item.itemName;
            //itemAmount.text = item.amount.ToString();
            itemSprite.sprite = item.sprite;
            obj.GetComponent<Button>().onClick.AddListener(() => { SelectToBuy(obj); });
        }
    }
    public void SelectToBuy(GameObject item)
    {
        string name = item.transform.Find("Name").GetComponent<Text>().text;
        //string amount = item.transform.Find("Amount").GetComponent<Text>().text;
        itemName.GetComponent<TextMeshProUGUI>().text = name;
        foreach (Item i in items)
        {
            if (i.itemName == name)
            {
                selectedItemToBuy = i;
                break;
            }
        }
        // TODO: comprobar que se muestra el sprite del objeto a vender
        itemImage.SetActive(true);
        itemImage.GetComponent<Image>().sprite = selectedItemToBuy.sprite;
        descriptionText.GetComponent<TextMeshProUGUI>().text = selectedItemToBuy.description;
        amountText.SetActive(true);
        prizeText.SetActive(true);
        // TODO: el precio no venga cableado
        currentUnitPrize = selectedItemToBuy.prize;
        ResetAmountAndPrizeText();
    }
    public void BuyItem()
    {
        if (selectedItemToBuy != null)
        {
            if (GameManager.GetInstance().GetCurrentMoney() < selectedAmount * currentUnitPrize) return;
            if (selectedItemToBuy.itemName == "Tractor")
            {
                BuyTractor();
            }
            else
            {
                GameManager.GetInstance().UpdateMoney(-selectedAmount * currentUnitPrize);
                Item newItem = (Item)ScriptableObject.Instantiate(selectedItemToBuy);
                newItem.amount = selectedAmount;
                InventoryManager.GetInstance().AddItem(newItem);
            }
            DeselectItemToBuy();
        }
    }
    void BuyTractor()
    {
        if (tractorPurchased)
        {
            tractorPurchasedText.SetActive(true);
            Invoke("HideTractorPurchasedText", 2.0f);
            return;
        }
        GameManager.GetInstance().UpdateMoney(-currentUnitPrize);
        GameManager.GetInstance().OnTractorPurchased();
        tractorPurchased = true;
    }
    public void DeselectItemToBuy()
    {
        selectedItemToBuy = null;
        itemImage.SetActive(false);
        itemName.GetComponent<TextMeshProUGUI>().text = "";
        descriptionText.GetComponent<TextMeshProUGUI>().text = "";
        selectedAmount = 1;
        currentUnitPrize = 0;
        amountText.SetActive(false);
        prizeText.SetActive(false);
    }
    public void OnClickPlus1Button()
    {
        if (selectedItemToBuy.itemName == "Tractor") return;
        selectedAmount++;
        UpdateAmountText();
        UpdatePrizeText();
    }
    public void OnClickPlus10Button()
    {
        if (selectedItemToBuy.itemName == "Tractor") return;
        selectedAmount += 10;
        UpdateAmountText();
        UpdatePrizeText();
    }
    public void OnClickMinus1Button()
    {
        selectedAmount--;
        if (selectedAmount < 1) selectedAmount = 1;
        UpdateAmountText();
        UpdatePrizeText();
    }
    public void OnClickMinus10Button()
    {
        selectedAmount -= 10;
        if (selectedAmount < 1) selectedAmount = 1;
        UpdateAmountText();
        UpdatePrizeText();
    }
    void ResetAmountAndPrizeText()
    {
        selectedAmount = 1;
        UpdateAmountText();
        UpdatePrizeText();
    }
    void UpdateAmountText()
    {
        amountText.GetComponent<TextMeshProUGUI>().text = "x" + selectedAmount;
    }
    void UpdatePrizeText()
    {
        prizeText.GetComponent<TextMeshProUGUI>().text = "Total: " + currentUnitPrize * selectedAmount + "�";
    }
    void HideTractorPurchasedText()
    {
        tractorPurchasedText.SetActive(false);
    }
}
