using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Naturalvents : MonoBehaviour
{

    [SerializeField]
    TextAsset[] naturalTexts = new TextAsset[4];
    string line;
    public TextMeshProUGUI evText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showWindow(int type)
    {

        this.gameObject.SetActive(true);

        line = naturalTexts[type].text;
        Debug.Log(line);
        evText.text = line;
        UIManager.GetInstance().OnUICanvasOpen();

    }

  }
