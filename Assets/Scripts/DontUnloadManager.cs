using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontUnloadManager : MonoBehaviour
{

    // Use this for initialization
    public static DontUnloadManager i;

    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    
}
