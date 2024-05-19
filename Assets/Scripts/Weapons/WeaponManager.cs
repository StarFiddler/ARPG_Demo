using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public PlayerControl pc;
    public Animator ani;
    public int selectedWeapon = 1;
    //public CharacterManager cm;
    // Start is called before the first frame update
    void Awake()
    {
        pc = GetComponentInParent<PlayerControl>(); 
        ani = GetComponentInParent<Animator>();
    }

    void Start()
    {
        SwitchWeapon();
        //SheathWeapon();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchWeapon()
    {
        int i = 0;
        selectedWeapon = ani.GetLayerIndex(pc.battleStyle) - 1;
        //print(selectedWeapon);
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void WakeUpWeapon()
    {
        foreach(Transform weapon in transform)
        {
            weapon.gameObject.SetActive(true);
        }
    }
    public void SheathWeapon()
    {
        foreach(Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
    }
}
