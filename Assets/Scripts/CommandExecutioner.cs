using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutioner : MonoBehaviour
{
    public static CommandExecutioner instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
