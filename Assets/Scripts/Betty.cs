using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Betty : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
