using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class TutorialNarr : MonoBehaviour
{
    public Text evText;

    string line;
    private int i = 0;
    private string[] parrafo = new string[20];

    // Start is called before the first frame update
    void Start()
    {

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
                
            sr = new StreamReader("Assets/textos/TutoText1.txt");
            line = sr.ReadLine();
            while(line != "end")
            {
                parrafo[i] = line;
                line=sr.ReadLine();
                i++;
            }

            evText.text = parrafo[0];
            sr.Close();
            
        }
        else if (type == 1)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText2.txt");
            
            line = sr.ReadLine();
            while (line != "end")
            {
                parrafo[i] = line;
                line = sr.ReadLine();
                i++;
            }

            evText.text = parrafo[0];
            sr.Close();


        }
        else if (type == 2)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText3.txt");

            line = sr.ReadLine();
            while (line != "end")
            {
                parrafo[i] = line;
                line = sr.ReadLine();
                i++;
            }

            evText.text = parrafo[0];
            sr.Close();

        }
        i = 1;
    }


    public void nextText()
    {
        if (parrafo[i] != "exit")
        {

            evText.text=parrafo[i];
            i++;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
       

        
    }


}
