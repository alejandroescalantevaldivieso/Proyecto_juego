using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_pies : MonoBehaviour
{
    public control_mujer c_mujer;
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
       c_mujer.puedo_saltar = true; 
    }
    private void OnTriggerExit(Collider other)
    {
        c_mujer.puedo_saltar = false;
    }
}
