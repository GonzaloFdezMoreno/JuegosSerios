using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Instance
    static public UIManager Instance;

    private void Awake()
    {
        // Check if the UI Manager doesn't already exist
        if (Instance == null)
        {
            Debug.Log("UI Manager Instanced");
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogError("Another UI Manager Instanced");
            DestroyImmediate(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
