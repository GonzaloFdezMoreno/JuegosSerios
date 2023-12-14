using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrchardTask : MonoBehaviour
{
    enum Vegetable { Tomatoes, Carrots, Peas, Lettuce, NONE };

    public Item tomatoes;

    private int numTotalVegetable;
    private int numGoodVegetable;
    private int numBadVegetable;

    private int timeLifeVegetable; 

    // Start is called before the first frame update
    void Start()
    {
        numTotalVegetable = 0;
        numGoodVegetable = 0;
        numBadVegetable = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
