using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : CharacterManagerInterface
{
    public float HP = 100.0f;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        print(HP);
    }
}
