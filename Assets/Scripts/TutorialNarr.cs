using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class TutorialNarr : MonoBehaviour
{
    public Text evText;
    [SerializeField]
    TextAsset[] tutoTexts=new TextAsset[15];
    string line;
    private int i = 0;
    private string[] parrafo = new string[20];

    // Start is called before the first frame update
    void Start()
    {
        //tuto1 = Resources.Load("TutoText.txt") as TextAsset;
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void showTuto(int type)
    {
        
        i = 0;
        StreamReader sr;
        if (type == 0)
        {
            
            
            this.gameObject.SetActive(true);
            
            line = tutoTexts[0].text;

            parrafo = line.Split('&');
           

            evText.text = parrafo[0];
            
           
            
        }
        else if (type == 1)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[1].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        else if (type == 2)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[2].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        else if (type == 3)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[3].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        else if (type == 4)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[4].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        else if (type == 5)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[5].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        else if (type == 6)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[6].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        else if (type == 7)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[7].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        else if (type == 8)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[8].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        else if (type == 9)
        {
            this.gameObject.SetActive(true);

            line = tutoTexts[9].text;

            parrafo = line.Split('&');


            evText.text = parrafo[0];


        }
        i = 1;
        UIManager.GetInstance().OnUICanvasOpen();
    }


    public void nextText()
    {
        if (i<parrafo.Length-1)
        {

            evText.text=parrafo[i];
            i++;
        }
        else
        {
            UIManager.GetInstance().OnUICanvasClose();
            this.gameObject.SetActive(false);
        }
    }


}
