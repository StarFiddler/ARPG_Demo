using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public GameObject model;
    public GameObject SlashEffect;
    /*public GameObject SlashEffect_9t4;
    public GameObject SlashEffect_7t6;*/
    public PlayerControl Pc;
    private Animator ani;
    private Vector3 thrustVec;
    private Rigidbody _rb;
    private WeaponManager wm;
    private float destroyTime;
    // Start is called before the first frame update
    void Awake()
    {
       Pc = GetComponentInParent<PlayerControl>(); 
       ani = model.GetComponent<Animator>();
       _rb = GetComponentInParent<Rigidbody>();
       thrustVec = Vector3.zero;
       wm = GetComponent<WeaponManager>();
       destroyTime = 1.0f;
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

    /*void EnemyJump()
    {
        thrustVec = new Vector3(0, 5 ,0);
        //ani.SetBool("IsGround", false);
        _rb.velocity += thrustVec;
        thrustVec = Vector3.zero; 
    }*/

    /*void EnemyDash()
    {
        thrustVec = new Vector3(0, 0 ,5);
        //ani.SetBool("IsGround", false);
        _rb.velocity += thrustVec;
        thrustVec = Vector3.zero; 
    }*/

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

    void WakeUpGun()
    {
        GameObject.Find("GunslingerHandle").SendMessage("WakeUpWeapon", SendMessageOptions.DontRequireReceiver);
    }
    void SheathGun()
    {
        GameObject.Find("GunslingerHandle").SendMessage("SheathWeapon", SendMessageOptions.DontRequireReceiver);
    }


    void Effect4t6()
    {
        Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(90, 0, 0));
    }
    void Effect6t4()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(90, 180, 0));
    }
    void Effect7t6()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(-65, 90, 90));
    }
    void Effect6t7()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(65, -90, -90));
    }
    void Effect9t4()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(65, 90, 0));
    }
    void Effect4t9()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(-65, -90, 0));
    }
    void Effect1t9()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(45, 90, 0));
    }
    void Effect9t1()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(-45, -90, 0));
    }
    void Effect8t2()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(180, 90, 0));
    }
    void Effect2t8()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(180, 90, 0));
    }
    void Effect7t3()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(45, -90, 0));
    }
    void Effect3t7()
    {
        GameObject.Instantiate(SlashEffect, model.transform.position + model.transform.forward * -0.5f + model.transform.up * 1.5f, SlashEffect.transform.rotation = model.transform.rotation * Quaternion.Euler(45, -90, 180));
    }
}
