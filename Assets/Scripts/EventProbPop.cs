
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        StreamReader sr;
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
                evText.text = line;//"Debes pagar la luz \n\n\n\n\n -100�";
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
                    "\n El otro dia me enter� de que Paco ha decidido mudarse a otro sitio\n" +
                    "Es una pena que en este lugar cada vez haya menos gente...";*/
                //sr.Close();
                popped = true;
            }
        }
        else
        {
            if (!popped)
            {
                this.gameObject.SetActive(true);
                case1.SetActive(false);
                case2.SetActive(true);
                caseS.SetActive(false);
                sr = new StreamReader("Assets/textos/LuzText.txt");
                line = sr.ReadToEnd();
                evText.text = line;/*"Un vecino ha venido a visitarte\n\n\n\n Hola vecino, como le va?" +
                    "\n El otro dia me enter� de que Paco ha decidido mudarse a otro sitio\n" +
                    "Es una pena que en este lugar cada vez haya menos gente...";*/

                sr.Close();


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
            evText.text = "Muchas Gracias";
        }
        else
        {
            evText.text = "Que pena";
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

}


