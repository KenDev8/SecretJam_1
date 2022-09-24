using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;

public class Betty : MonoBehaviour, ICollector
{
    //_________________ Signelton Pattern _________________//
    public static Betty _instance;
    public static Betty Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Betty>();
            }
            return _instance;
        }
    }
    //_________________ Signelton Pattern _________________//

    public int bonesCount = 0;

    public void Collect()
    {
        bonesCount += 1;
        print(bonesCount);
    }
}
