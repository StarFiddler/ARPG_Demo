using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject model;//检索Player对象下挂载的模型
    public KeyboardInput pi;
    public CameraController cam;
    public float moveSpeed = 2.5f;
    public float runMultiple = 2.4f;
    public float jumpVec = 4.0f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    [SerializeField]
    private Animator ani;
    //private Animator m_ani;
    private Rigidbody _rb;
    private Vector3 _pV;//plane vector，平面移动向量
    private Vector3 thrustVec;//冲量；
    private Vector3 dP;//deltaPositon
    private CapsuleCollider _col;
    private bool lockPlaneVector = false; //bool锁定平面移动向量
    private bool targetMoveLock = false;//bool在锁定单位时解锁动作方向
    private bool canAttack;
    private string battleStyle;
    private int i = 0;//武士道风格架势变量
    void Awake()
    {
        pi = GetComponent<KeyboardInput>();//调用PlayerInput脚本
        ani = model.GetComponent<Animator>();//调用AnimatorAPI
        //m_ani = gameObject.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();//调用RidigbodyAPI
        _col = GetComponent<CapsuleCollider>();//调用胶囊碰撞
        battleStyle = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(IsGrounded() && Input.GetKeyDown(pi.keyJump))
        {
            canAttack = false;
            Jump();
        }
        if(Input.GetKeyDown(pi.keyDash))
        {
            Dash();
        }
        if(Input.GetKeyDown(pi.keyAttack1) && (IsGrounded() || canAttack))
        {
            Attack1();
        }
        if(Input.GetKeyDown(pi.keyAttack2) && (IsGrounded() || canAttack))
        {
            Attack2();
        }
        if(pi.lockon)
        {
            cam.CameraLock();
        }
        if(Input.GetKey(pi.keySowrdMaster))
        {
            battleStyle = "SwordMaster";
            BattleMode();
        }
        if(Input.GetKey(pi.keyGunslinger) && battleStyle != "Gunslinger")
        {
            battleStyle = "Gunslinger";
            BattleMode();
        }
        if(Input.GetKey(pi.keyWhip) && battleStyle != "Whip")
        {
            battleStyle = "Whip";
            BattleMode();
        }
        if(Input.GetKey(pi.keySamurai) && battleStyle != "Samurai")
        {
            battleStyle = "Samurai";
            BattleMode();
        }
        if(Input.GetKey(pi.keySheath))
        {
            battleStyle = "Sheath";
            BattleMode();
        }
        if(Resources.Load<GameObject>("Models/Weapons/RedSakura/RedSakura") == null)
        {
            print("TRUE");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float targetRunMultiple = ((pi.walk) ? 1.0f : 2.0f);
        if(cam.lockPos == false)
        {
            //动画变化速度线性插值
            ani.SetFloat("forward", pi.Dmag * Mathf.Lerp(ani.GetFloat("forward"), targetRunMultiple, 0.2f));
            //非锁定状态清空横向移动动画
            ani.SetFloat("right", 0f);
            //停止移动时方向校准
            if(pi.Dmag > 0.1f)
            {
            //角色方向变化球形线性插值
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            }
            else
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            }
            if(lockPlaneVector == false)
            {
                _pV = pi.Dmag * model.transform.forward * moveSpeed * ((pi.walk) ? 1.0f : runMultiple);//平面移动速度向量
            }
        }
        else
        {
            if(targetMoveLock == false)
            {
                model.transform.forward = pi.Dvec;
            }
            else
            {
                model.transform.forward = _pV.normalized;
                //model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            }
            //锁定状态下，动画变化速度线性插值
            Vector3 localDvec = transform.InverseTransformVector(pi.Dvec);
            ani.SetFloat("forward", localDvec.z * ((pi.walk) ? 1.0f : runMultiple));
            ani.SetFloat("right", localDvec.x * ((pi.walk) ? 1.0f : runMultiple));
            if(lockPlaneVector == false)
            {
                _pV = pi.Dvec * moveSpeed * ((pi.walk) ? 1.0f : runMultiple);
            }
        }
        _rb.position += dP;
        _rb.position += _pV * Time.fixedDeltaTime; //平面移动坐标向量
        dP = Vector3.zero;
        /*_rb.velocity = new Vector3(_pV.x, _rb.velocity.y, _pV.z) + thrustVec;
        thrustVec = Vector3.zero;*/
    }
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);//用CheckCapsule函数判断是否与指定Layer碰撞
        if(grounded == true)
        {
            ani.SetBool("IsGround", true);
            canAttack = true;
            
            /*pi.inputEnable = true;
            lockPlaneVector = false;*/
        }
        else
        {
            ani.SetBool("IsGround", false);
            /*pi.inputEnable = false;
            lockPlaneVector = true;*/
        }
        return grounded;
    }

    //检测当前动画状态机
    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        /*可读性较强Ver
        int layerIndex = ani.GetLayerIndex(layerName);
        /bool result = ani.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
        优化*/
        return ani.GetCurrentAnimatorStateInfo(ani.GetLayerIndex(layerName)).IsName(stateName);
    }

    private void Jump()
    {  
        thrustVec = new Vector3(0, jumpVec ,0);
        ani.SetTrigger("Jump");
        ani.SetBool("IsGround", false);
        _rb.velocity += thrustVec;
        thrustVec = Vector3.zero;     
    }

    private void Dash()
    {
        thrustVec = pi.Dvec * 10.0f;
        ani.SetTrigger("Dash");
        _rb.velocity += thrustVec;
        thrustVec = Vector3.zero; 
        targetMoveLock = true;
    }

    private void Attack1()
    {
        ani.SetTrigger("Attack1");
        //ani.SetBool("SwordMaster", true);
        
    }

    private void Attack2()//右键攻击触发器
    {
        if(battleStyle != "Samurai")
        {
            ani.SetTrigger("Attack2");
        }
        else//武士道风格下，右键作为架势切换键
        {
            i++ ;
            print(i);
            ani.SetInteger("SamuraiPosture", i);
            if(i == 2)
            {
                i = -1;
            }
        }
    }

    public void OnJumpEnter()
    {
        pi.inputEnable = false;
        lockPlaneVector = true;
        targetMoveLock = true;
    }

    public void OnJumpExit()
    {
        pi.inputEnable = true;
        lockPlaneVector = false;
        targetMoveLock = false;
    }

    public void OnFallEnter()
    {
        pi.inputEnable = false;
        lockPlaneVector = true;
    }

    public void OnFallExit()
    {
        pi.inputEnable = true;
        lockPlaneVector = false;
        targetMoveLock = false;
    }

    public void OnSlash1Enter()
    {
        pi.inputEnable = false;
    }

    public void OnSlash1Update()
    {
        thrustVec = model.transform.forward * ani.GetFloat("Slash1Vec");
        _rb.position += thrustVec * Time.fixedDeltaTime;
    }

    /*public void OnSwordMasterIdleEnter()
    {
        pi.inputEnable = true;
        //lockPlaneVector = false;
        lerpTarget = 1.0f;
    }

    public void OnSwordMasterIdleUpdate()
    {
        float currentWeight = ani.GetLayerWeight(ani.GetLayerIndex("SwordMasterMode"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        ani.SetLayerWeight(ani.GetLayerIndex("SwordMasterMode"), currentWeight);
        print("SM");
    }*/
    
    public void OnUpdateRootMotion(object _dP)
    {
        dP += (Vector3) _dP;
    }

    /*public void OnNormalIdleEnter()
    {
        pi.inputEnable = true;
        //lockPlaneVector = false;
        lerpTarget = 0f;
    }
     public void OnNormalIdleUpdate()
    {
        float currentWeight = ani.GetLayerWeight(ani.GetLayerIndex("SwordMasterMode"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        ani.SetLayerWeight(ani.GetLayerIndex("SwordMasterMode"), currentWeight);
    }

    public void OnGunslingerIdleEnter()
    {
        pi.inputEnable = true;
        //lockPlaneVector = false;
        lerpTarget = 1.0f;
    }

    public void OnGunslingerIdleUpdate()
    {
        float currentWeight = ani.GetLayerWeight(ani.GetLayerIndex(battleStyle));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        ani.SetLayerWeight(ani.GetLayerIndex("GunslingerMode"), currentWeight);
        print("GM");
    }*/

    public void OnNormalModeEnter()
    {
        pi.inputEnable = true;
    }

    public void OnNormalModeUpdate()//切换战斗风格时，清空当前战斗风格以外的风格权重
    {
        /*float currentWeight = ani.GetLayerWeight(ani.GetLayerIndex(battleStyle));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        ani.SetLayerWeight(ani.GetLayerIndex(battleStyle), currentWeight);*/
        //if(ani.GetBool("SwordMaster") || ani.GetBool("Gunslinger") || ani.GetBool("Whip") || ani.GetBool("Samurai"))
        if(ani.GetBool(battleStyle))
        {
            int j = ani.layerCount;
            for(int a = 0; a < ani.layerCount; a++)
            {
                if(a != ani.GetLayerIndex(battleStyle))
                {
                    ani.SetLayerWeight(a, Mathf.Lerp(ani.GetLayerWeight(a), 0, 0.1f));
                }
            }
        //ani.SetLayerWeight(ani.GetLayerIndex("SwordMasterMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SwordMasterMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("GunslingerMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("GunslingerMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("WhipMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("WhipMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("SamuraiMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SamuraiMode")), 0, 0.1f));
        //print("SM");
        }
        // if(ani.GetBool("Gunslinger"))
        // {
        // ani.SetLayerWeight(ani.GetLayerIndex("SwordMasterMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SwordMasterMode")), 0, 0.1f));
        // //ani.SetLayerWeight(ani.GetLayerIndex("GunslingerMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("GunslingerMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("WhipMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("WhipMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("SamuraiMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SamuraiMode")), 0, 0.1f));
        // //print("GS");
        // }
        // if(ani.GetBool("Whip"))
        // {
        // ani.SetLayerWeight(ani.GetLayerIndex("SwordMasterMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SwordMasterMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("GunslingerMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("GunslingerMode")), 0, 0.1f));
        // //ani.SetLayerWeight(ani.GetLayerIndex("WhipMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("WhipMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("SamuraiMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SamuraiMode")), 0, 0.1f));
        // }
        // if(ani.GetBool("Samurai"))
        // {
        // ani.SetLayerWeight(ani.GetLayerIndex("SwordMasterMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SwordMasterMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("GunslingerMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("GunslingerMode")), 0, 0.1f));
        // ani.SetLayerWeight(ani.GetLayerIndex("WhipMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("WhipMode")), 0, 0.1f));
        // //ani.SetLayerWeight(ani.GetLayerIndex("SamuraiMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SamuraiMode")), 0, 0.1f));
        // }
    }
    public void OnBattleModeEnter()
    {
        pi.inputEnable = true;
    }

    public void OnBattleModeUpdate()
    {
        //float currentWeight = ani.GetLayerWeight(ani.GetLayerIndex(battleStyle));
        //currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        float currentWeight = ani.GetLayerWeight(ani.GetLayerIndex(battleStyle));
        currentWeight = Mathf.Lerp(currentWeight, 1.0f, 0.1f);
        ani.SetLayerWeight(ani.GetLayerIndex(battleStyle), currentWeight);
        //print(battleStyle);
        //print(currentWeight);
    }

    public void OnBattleModeExit()
    {
        Sheath();
    }

    private void BattleMode()//战斗风格切换
    {
        switch(battleStyle)
        {
            case "SwordMaster":
            ani.SetBool("SwordMaster", true);
            ani.SetBool("Gunslinger", false);
            ani.SetBool("Whip", false);
            ani.SetBool("Samurai", false);
            break;
            case "Gunslinger":
            ani.SetBool("SwordMaster", false);
            ani.SetBool("Gunslinger", true);
            ani.SetBool("Whip", false);
            ani.SetBool("Samurai", false);
            break;
            case "Whip":
            ani.SetBool("SwordMaster", false);
            ani.SetBool("Gunslinger", false);
            ani.SetBool("Whip", true);
            ani.SetBool("Samurai", false);
            break;
            case "Samurai":
            ani.SetBool("SwordMaster", false);
            ani.SetBool("Gunslinger", false);
            ani.SetBool("Whip", false);
            ani.SetBool("Samurai", true);
            break;
            case "Sheath":
            ani.SetBool("SwordMaster", false);
            ani.SetBool("Gunslinger", false);
            ani.SetBool("Whip", false);
            ani.SetBool("Samurai", false);
            break;
        }
    }

    private void Sheath()//收刀后清空所有战斗图层权重
    {
        pi.inputEnable = true;
        int j = ani.layerCount;
        for(int a = 0; a < j; a++)
        {
            ani.SetLayerWeight(a, 0);
        }
        // ani.SetLayerWeight(ani.GetLayerIndex("SwordMasterMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SwordMasterMode")), 0, 1.0f));
        // ani.SetLayerWeight(ani.GetLayerIndex("GunslingerMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("GunslingerMode")), 0, 1.0f));
        // ani.SetLayerWeight(ani.GetLayerIndex("WhipMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("WhipMode")), 0, 1.0f));
        // ani.SetLayerWeight(ani.GetLayerIndex("SamuraiMode"), Mathf.Lerp(ani.GetLayerWeight(ani.GetLayerIndex("SamuraiMode")), 0, 1.0f));
    }
}
