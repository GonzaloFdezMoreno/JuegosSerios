using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Clase abstracta de la que heredan los botones de las tareas
// Para evitar tener una instancia de esta clase por boton, tendremos
// una por cada canvas de tareas. Se deben definir tantos metodos onClick
// como botones de tareas tenga el canvas de la parcela.
public class TasksButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnTaskButtonPressed);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void OnTaskButtonPressed()
    {
        GameManager.GetInstance().UpdateMoney(100);
    }*/
}
