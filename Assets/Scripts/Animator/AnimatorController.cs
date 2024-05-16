using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public GameObject model;
    public GameObject SlashEffect;
    public PlayerControl Pc;
    private Animator ani;
    private Vector3 thrustVec;
    private Rigidbody _rb;
    private WeaponManager wm;
    // Start is called before the first frame update
    void Awake()
    {
       Pc = GetComponentInParent<PlayerControl>(); 
       ani = model.GetComponent<Animator>();
       _rb = GetComponentInParent<Rigidbody>();
       thrustVec = Vector3.zero;
       wm = GetComponent<WeaponManager>();
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

    void DrawBlade()
    {
        GameObject.Find("WeaponHandle").SendMessage("SwitchWeapon", SendMessageOptions.DontRequireReceiver);
    }

    void WakeUpWeapon()
    {
        GameObject.Find("PropsHandle").SendMessage("WakeUpWeapon", SendMessageOptions.DontRequireReceiver);
    }

    void SheathWeapon()
    {
        GameObject.Find("PropsHandle").SendMessage("SheathWeapon", SendMessageOptions.DontRequireReceiver);
    }

    void Effect()
    {
        GameObject.Instantiate(SlashEffect, _rb.position + new Vector3(0,1,0), Quaternion.Euler(90, 0, 0));
    }
}
