using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public GameObject model;
    public PlayerControl Pc;
    private Animator ani;
    private Vector3 thrustVec;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Awake()
    {
       Pc = GetComponentInParent<PlayerControl>(); 
       ani = model.GetComponent<Animator>();
       _rb = GetComponentInParent<Rigidbody>();
       thrustVec = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AnimationUp()
    {
        thrustVec = new Vector3(0, Pc.jumpVec ,0);
        ani.SetBool("IsGround", false);
        _rb.velocity += thrustVec;
        thrustVec = Vector3.zero; 
    }
}
