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
        else if (type == 3)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText4.txt");

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
        else if (type == 4)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText5.txt");

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
        else if (type == 5)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText6.txt");

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
        else if (type == 6)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText7.txt");

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
        else if (type == 7)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText8.txt");

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
        else if (type == 8)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText9.txt");

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
        else if (type == 9)
        {
            this.gameObject.SetActive(true);
            sr = new StreamReader("Assets/textos/TutoText10.txt");

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
