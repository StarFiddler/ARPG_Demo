using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponet(typeof(CapsuleCollider))]
public class ColliderManager : MonoBehaviour
{
    // public GameObject obj;
    //public GameObject targetObj;
    // public Animator targetAni;
    // public Rigidbody targetRb;
    // private Vector3 thrustVec;
    // private StateManager sm;
    private CharacterManager cm;
    void Awake()
    {
    //    targetRb = targetObj.GetComponent<Rigidbody>();
    //    targetAni = targetObj.GetComponentInChildren<Animator>();
    //    thrustVec = Vector3.zero;
    //    sm = targetObj.GetComponent<StateManager>();
        //cm = targetObj.GetComponent<CharacterManager>();
    }

    void Update()
    {
        //print("敌人速度" + obj.GetComponent<Rigidbody>().velocity);
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.name == "Player")
        {
            cm = col.GetComponent<CharacterManager>();
            // thrustVec = new Vector3(targetObj.transform.forward.x * -5f, 5f, targetObj.transform.forward.z * -5f);
            // targetRb.velocity += thrustVec;
            //targetObj.transform.position += thrustVec * Time.deltaTime;
            //targetAni.SetTrigger("Hit3");
            cm.PlayerDamage();
            //sm.ReduceHP(sm.enemyAttack);
            //Debug.Log("Player detected - attack!");
            // thrustVec = Vector3.zero;
        }
        if(col.name == "EnemyHandle")
        {
            cm = col.GetComponent<CharacterManager>();
            cm.EnemyDamage();
        }
    }

    void ColliderWakeUp()
    {
        transform.GetComponent<Collider>().enabled = true;
    }
    void ColliderSleep()
    {
        transform.GetComponent<Collider>().enabled = false;
    }
}
