using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastureTasks : Task
{
    bool available;
    // Start is called before the first frame update
    void Start()
    {
        available = true;   
    }

    
    public bool IsAvailable()
    {
        return available;
    }
    public void Sell()
    {
        available = false;
        // TODO añadir dinero y cambiar sprite
        //GameManager.GetInstance().UpdateMoney()
    }
}
