using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_pies : MonoBehaviour
{
    public Control_mujer control_mujer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
       control_mujer.puedo_saltar = true; 
    }
    private void OnTriggerExit(Collider other)
    {
        control_mujer.puedo_saltar = false;
    }
}
