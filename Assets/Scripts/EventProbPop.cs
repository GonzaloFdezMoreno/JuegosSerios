
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class EventProbPop : MonoBehaviour
{
    
    public int appear;
    public int whenAppear;

    private bool popped = false;
    private bool poppedFix = false;

    public GameObject case1;
    public GameObject case2;
    public GameObject caseS;

    public TextMeshProUGUI evText;

    public TextAsset txtTutoEvent;
    public TextAsset txtLuzEvent;
    [SerializeField]
    TextAsset[] eventTexts = new TextAsset[10];
    [SerializeField]
    TextAsset[] narrTexts = new TextAsset[10];

    int prodVent=-1;

    public int randEvent;
    bool company=false;

    string line;
    bool tutorial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newChance(int week)
    {
        popped = false;
        poppedFix = false;
        appear = Random.Range(0, 100);

        this.gameObject.SetActive(false);
        Debug.Log("Desactiva");
        evText.text = "Evento";

        if (appear + week < 50)
        {
            whenAppear = Random.Range(0, 84);
        }
        else whenAppear = -1;
        
    }


    public void popEvent(int type)
    {
        UIManager.GetInstance().OnUICanvasClose();
        if (type == 0)
        {
            if (!poppedFix) { 
                this.gameObject.SetActive(true);
                case1.SetActive(true);
                case2.SetActive(false);
                caseS.SetActive(false);
                //sr = new StreamReader("Assets/textos/LuzText.txt");

                //line=sr.ReadToEnd();
                line = txtLuzEvent.text;
                Debug.Log(line);
                evText.text = line;//"Debes pagar la luz \n\n\n\n\n -100€";
                //sr.Close();
                poppedFix = true;
            }
            else 
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (type == 1)
        {
            if (!popped) { 
                this.gameObject.SetActive(true);
                case1.SetActive(true);
                case2.SetActive(false);
                caseS.SetActive(false);
                //sr = new StreamReader("Assets/textos/EventText1.txt");
                line = txtTutoEvent.text;
                evText.text = line;/*"Un vecino ha venido a visitarte\n\n\n\n Hola vecino, como le va?" +
                    "\n El otro dia me enteré de que Paco ha decidido mudarse a otro sitio\n" +
                    "Es una pena que en este lugar cada vez haya menos gente...";*/
                //sr.Close();
                popped = true;
            }
        }
        else if (type==2)
        {
            if (!popped)
            {
                this.gameObject.SetActive(true);
                case1.SetActive(true);
                case2.SetActive(false);
                caseS.SetActive(false);

                randEvent = Random.Range(0, 10);
                line = narrTexts[randEvent].text;

                evText.text = line;/*"Un vecino ha venido a visitarte\n\n\n\n Hola vecino, como le va?" +
                    "\n El otro dia me enteré de que Paco ha decidido mudarse a otro sitio\n" +
                    "Es una pena que en este lugar cada vez haya menos gente...";*/

                


                popped = true;
            }
        }
        else if (type == 3)
        {
            if (!popped)
            {
                this.gameObject.SetActive(true);
                case1.SetActive(false);
                case2.SetActive(false);
                caseS.SetActive(true);

                randEvent= Random.Range(1, 7);
                line = eventTexts[randEvent].text;
                prodVent = randEvent;
                //sr = new StreamReader("Assets/textos/LuzText.txt");
                //line = sr.ReadToEnd();
                evText.text = line;/*"Un vecino ha venido a visitarte\n\n\n\n Hola vecino, como le va?" +
                    "\n El otro dia me enteré de que Paco ha decidido mudarse a otro sitio\n" +
                    "Es una pena que en este lugar cada vez haya menos gente...";*/

                


                popped = true;
            }
        }

        else if (type == 4)
        {
            if (!popped)
            {
                this.gameObject.SetActive(true);
                case1.SetActive(false);
                case2.SetActive(true);
                caseS.SetActive(false);
                //Compañía
                line = eventTexts[7].text;
                
                //sr = new StreamReader("Assets/textos/LuzText.txt");
                //line = sr.ReadToEnd();
                evText.text = line;/*"Un vecino ha venido a visitarte\n\n\n\n Hola vecino, como le va?" +
                    "\n El otro dia me enteré de que Paco ha decidido mudarse a otro sitio\n" +
                    "Es una pena que en este lugar cada vez haya menos gente...";*/

                company = true;


                popped = true;
            }
        }
    }


    public void nextText(int result)
    {
        case1.SetActive(true);
        case2.SetActive(false);
        caseS.SetActive(false);

        if (result == 0)
        {
            evText.text = "Muchas gracias";
            if (company)
            {
                //deshabilitar el pasto
                GameManager.GetInstance().SellPasture();
                GameManager.GetInstance().Dealed();
                company = false;
            }
           
            
        }
        else if(result==1)
        {
            evText.text = "Que pena";
            company = false;
            
        }
        else
        {
            
            string product;
            if (prodVent == 1)
            {
                product = "Tomates";
            }
            else if (prodVent == 2)
            {
                product = "Lechugas";
            }
            else if (prodVent == 3)
            {
                product = "Huevos";
            }
            else if (prodVent == 4)
            {
                product = "Leche";
            }
            else if (prodVent == 5)
            {
                product = "Zanahorias";
            }
            else if (prodVent == 6)
            {
                product = "Guisantes";
            }
            else 
            {
                product = "Huevos";
            }
            if (InventoryManager.GetInstance().GiveItem(product, 10))
            {
                GameManager.GetInstance().UpdateMoney(200);
                evText.text = "Muchas gracias";
            }
            else
            {
                evText.text = "No tienes suficiente";
            }
            
        }

    }

    public void setAppear(int num)
    {
        whenAppear = num;
    }

    public void setIsEventTutorial(bool isTu)
    {
        tutorial = isTu;
    }

    public void postEventTutorial()
    {
        if (tutorial) { 
            GameManager.GetInstance().nextTutorial(7);
            tutorial=false;
        }
    }

    public void CloseEvent()
    {
        UIManager.GetInstance().OnUICanvasClose();
        this.gameObject.SetActive(false);
    }

}


