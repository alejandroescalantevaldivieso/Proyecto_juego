using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sonar_boton : MonoBehaviour, IPointerEnterHandler
{
    
    public AudioSource sonido;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        sonido.Play();
    }

}
