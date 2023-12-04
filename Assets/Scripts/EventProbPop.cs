
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class EventProbPop : MonoBehaviour
{
    
    public int appear;
    public int whenAppear;
    private bool popped = false;

    public Text evText;
    public GameObject evMes;

   

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


    public void popEvent()
    {
        if (!popped) { 
            this.gameObject.SetActive(true);
            evText.text = "Un vecino ha venido a visitarte\n\n\n\n Hola vecino, como le va?" +
                "\n El otro dia me enteré de que Paco ha decidido mudarse a tro sitio\n" +
                "Es una pena que en este lugar cada vez haya menos gente...";
            popped = true;
        }
    }

}
