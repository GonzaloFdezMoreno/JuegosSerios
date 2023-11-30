
using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class EventProbPop : MonoBehaviour
{
    
    public int appear;
    public int whenAppear;
    private bool popped = false;

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

        if (appear + week < 50)
        {
            whenAppear = Random.Range(0, 84);
        }
        else whenAppear = -1;
    }


    public void popEvent()
    {
        if (!popped) { 
            Debug.Log("Hello vecino " + appear + " " + whenAppear);
            popped = true;
        }
    }

}
