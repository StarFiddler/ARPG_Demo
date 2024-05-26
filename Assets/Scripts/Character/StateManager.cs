using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : CharacterManagerInterface
{
    public int playerHP;
    public int enemyAttack;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void Test()
    // {
    //     print(HP);
    // }
    public void AddHP(int value)
    {
        playerHP += value;
    }
    public void ReduceHP(int value)
    {
        playerHP -= value;
    } 
}
