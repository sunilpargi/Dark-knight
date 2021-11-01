using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float Timer = 2f;

    void Start()
    {
        Destroy(gameObject, Timer); 
    }

    
}
