using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_global : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
