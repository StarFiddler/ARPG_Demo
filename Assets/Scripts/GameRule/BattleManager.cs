using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponet(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour
{
    public GameObject obj;
    public GameObject targetObj;
    public Animator targetAni;
    public Rigidbody targetRb;
    private Vector3 thrustVec;
    void Awake()
    {
       targetRb = targetObj.GetComponent<Rigidbody>();
       targetAni = targetObj.GetComponentInChildren<Animator>();
       thrustVec = Vector3.zero;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.name == "Player")
        {
            thrustVec = new Vector3(targetObj.transform.forward.x * -5f, 5f, targetObj.transform.forward.z * -5f);
            targetRb.velocity += thrustVec;
            targetAni.SetTrigger("Hit3");
            // Debug.Log("Player detected - attack!");
            thrustVec = Vector3.zero;
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
