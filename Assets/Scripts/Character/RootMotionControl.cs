using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{
    private Animator ani;
    void Awake()
    {
        ani = GetComponent<Animator>();
    }

    void OnAnimatorMove()
    {
        SendMessageUpwards("OnUpdateRootMotion", (object)ani.deltaPosition);
    }
}
