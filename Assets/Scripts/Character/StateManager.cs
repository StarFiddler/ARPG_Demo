using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : CharacterManagerInterface
{
    public int playerHP;
    public int playerAttack;
    public int enemyHP;
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
    public void AddHP(int HP, int Heal)
    {

        HP += Heal;
    }
    public void ReducePlayerHP()
    {
        playerHP -= enemyAttack;
    } 
    public void ReduceHP(int HP, int Attack)
    {
        print("HP:" + HP);
        print("Attack:" + Attack);
        HP -= Attack;
        print(HP);
        //return HP;
    } 
}
