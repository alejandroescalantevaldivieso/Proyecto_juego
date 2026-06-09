using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica_global : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
