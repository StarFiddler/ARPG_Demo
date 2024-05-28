using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //public GameObject obj;
    public KeyboardInput pi;
    public PlayerControl pc;
    public WeaponState ws;
    public StateManager sm;
    public BattleManager bm;
    public Animator ani;
    public string hitStyle;//受击程度
    // Start is called before the first frame update
    void Awake()
    {
        if(this.gameObject.name == "Player")
        {
            pi = this.GetComponent<KeyboardInput> ();
            pc = this.GetComponent<PlayerControl> ();
            ws = Bind<WeaponState>(gameObject);
        }
        // else
        // {
            sm = Bind<StateManager>(gameObject);
            bm = Bind<BattleManager>(gameObject);
            ani = this.GetComponentInChildren<Animator> ();
            //print(this.gameObject);
        // }
        
        //hitStyle = "HeavyHit";
    }

    private T Bind<T>(GameObject obj) where T : CharacterManagerInterface
    {
        T temp;
        temp = obj.GetComponent<T>();
        if(temp == null)
        {
            temp = obj.AddComponent<T>();
        }
        temp.cm = this;
        return temp;
    }

    // Update is called once per frame
    public void Update()
    {
        //sm.Test();
    }

    public void SoftHit()
    {
        ani.SetTrigger("SoftHit");
    }
    public void NormalHit()//受击后退
    {
        ani.SetTrigger("NormalHit");
        bm.Back();
    }

    public void HeavyHit()//重击吹飞
    {
        ani.SetTrigger("HeavyHit");
        bm.BlowUp();
    }

    public void Die()
    {
        pc.Sheath();
        ani.SetTrigger("Die");
        pi.inputEnable = false;
        if(pc.cam.lockPos = true)
        {
            pc.cam.CameraLock();
        }
        pc.cam.enabled = false;
    }
    public void EnemyDie()
    {
        ani.SetBool("Die", true);
        //this.gameObject.GetComponent<Behavior>().enabled = false;
        //this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
    }

    // public void PlayerDamage(string hitStyle)
    // {
    //     sm.ReduceHP(sm.playerHP, sm.enemyAttack);
    //     print("玩家生命" + sm.playerHP);
    //     if(sm.playerHP > 0)
    //     {
    //         switch(hitStyle)
    //         {
    //             case "SoftHit":
    //             SoftHit();
    //             break;
    //             case "NormalHit":
    //             NormalHit();
    //             break;
    //             case "HeavyHit":
    //             HeavyHit();
    //             break;
    //         }
    //     }
    //     else
    //     {
    //         Die();
    //     }
    // }
    public void PlayerDamage()
    {
        sm.ReduceHP(sm.playerHP, sm.enemyAttack);
        print(sm.playerHP);
        if(sm.playerHP > 0)
        {

        }
        else
        {
            Die();
        }
    }

    public void EnemyDamage()
    {
        sm.ReduceHP(sm.enemyHP, sm.playerAttack);
        print(sm.enemyHP);
        if(sm.enemyHP > 0)
        {

        }
        else
        {
            EnemyDie();
        }
    }
}
