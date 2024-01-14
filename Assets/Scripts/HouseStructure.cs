using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseStructure : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject shopCanvas;
    public GameObject frame;
    public GameObject confirmButtons;
    public GameObject noTractorText;
    bool onTutorial = true;
    //bool onTutorial2 = true;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnMouseDown()
    {
        Debug.Log(onTutorial);
        if (!onTutorial && taskCanvas != null && !UIManager.GetInstance().GetUICanvasIsOpen()) 
        {
            taskCanvas.SetActive(true);
            UIManager.GetInstance().OnUICanvasOpen();
            return;
        }
        if (shopCanvas != null && !UIManager.GetInstance().GetUICanvasIsOpen())
        {
            shopCanvas.SetActive(true);
            UIManager.GetInstance().OnUICanvasOpen();
            int tutoNumber = GameManager.GetInstance().GetonTutorialNumber();
            Debug.Log(tutoNumber);
            if (onTutorial && tutoNumber == 1)
            {
                GameManager.GetInstance().nextTutorial(1);
                GameManager.GetInstance().AdvanceTutorialNumber();
                onTutorial = false;
            }
            else if (onTutorial && tutoNumber == 3)
            {
                GameManager.GetInstance().nextTutorial(3);
                GameManager.GetInstance().AdvanceTutorialNumber();
                onTutorial = false;
            }
            else if (onTutorial && tutoNumber == 4)
            {
                GameManager.GetInstance().nextTutorial(4);
                GameManager.GetInstance().AdvanceTutorialNumber();
                onTutorial = false;
            }
        }
    }
    private void OnMouseOver()
    {
        if (frame != null) frame.SetActive(true);
    }
    private void OnMouseExit()
    {
        if (frame != null) frame.SetActive(false);
    }
    public GameObject GetTaskCanvas()
    {
        return taskCanvas;
    }
    public void OpenShop()
    {
        taskCanvas.SetActive(false);
        shopCanvas.SetActive(true);
    }
    public void ShowConfirmButtons()
    {
        if (!GameManager.GetInstance().IsTractorPurchased())
        {
            noTractorText.SetActive(true);
            Invoke("HideNoTractorText", 2.0f);
            return;
        }
        confirmButtons.SetActive(true);
    }
    public void HideConfirmButtons()
    {
        confirmButtons.SetActive(false);
    }
    public void SellTractor()
    {
        GameManager.GetInstance().OnTractorSale();
        GameManager.GetInstance().UpdateMoney(600);
        HideConfirmButtons();
    }
    void HideNoTractorText()
    {
        noTractorText.SetActive(false);
    }
}
