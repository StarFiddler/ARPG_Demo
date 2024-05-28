using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : CharacterManagerInterface
{
    public GameObject obj;
    public GameObject enemyObj;
    public CameraController cam;
    public Rigidbody rb;
    private Vector3 thrustVec;
    // Start is called before the first frame update
    void Awake()
    {
        obj = GameObject.Find("Miku");
        enemyObj = GameObject.Find("TTF2");
        cam = this.GetComponentInChildren<CameraController>();
        rb = this.GetComponent<Rigidbody>();
        thrustVec = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()//击退
    {
        if(cam.lockPos == true)
        {
            thrustVec = new Vector3(obj.transform.forward.x * -2f, 0, obj.transform.forward.z * -2f);
            rb.velocity += thrustVec;
            thrustVec = Vector3.zero;
        }
        else
        {
            thrustVec = new Vector3(enemyObj.transform.forward.x * 2f, 0, enemyObj.transform.forward.z * 2f);
            rb.velocity += thrustVec;
            thrustVec = Vector3.zero;
        }
    }
    public void BlowUp()//吹飞
    {
        if(cam.lockPos == true)
        {
            thrustVec = new Vector3(obj.transform.forward.x * -5f, 5f, obj.transform.forward.z * -5f);
            rb.velocity += thrustVec;
            thrustVec = Vector3.zero;
        }
        else
        {
            thrustVec = new Vector3(enemyObj.transform.forward.x * 5f, 5f, enemyObj.transform.forward.z * 5f);
            rb.velocity += thrustVec;
            thrustVec = Vector3.zero;
        }
    }
}
