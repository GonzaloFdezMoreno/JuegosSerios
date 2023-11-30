using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnTaskButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTaskButtonPressed()
    {
        GameManager.GetInstance().UpdateMoney(100);
    }
}
