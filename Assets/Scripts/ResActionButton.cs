using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResActionButton : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = GameManager.GetInstance();
        Button btn = GetComponent<Button>();
        //btn.onClick.AddListener(gm.UpdateRemAct);
    }

}
