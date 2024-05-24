using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAniController : MonoBehaviour
{
    public GameObject obj;
    private Animator ani;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        ani = obj.GetComponent<Animator>();
        //rb = obj.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void AttackJudgeWake()
    {
        GameObject.Find("Hit").SendMessage("ColliderWakeUp", SendMessageOptions.DontRequireReceiver);
    }
    void AttackJudgeSleep()
    {
        GameObject.Find("Hit").SendMessage("ColliderSleep", SendMessageOptions.DontRequireReceiver);
    }
}
