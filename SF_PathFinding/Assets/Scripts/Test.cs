using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int k = 3;
        Add(ref k);
        Debug.Log(k);
    }

    void Add(ref Int32 _k)
    {
        _k++;
    }
    
}
