using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject model;//检索Player对象下挂载的模型
    public KeyboardInput pi;
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
    private bool canAttack;
    private float lerpTarget;
    void Awake()
    {
        pi = GetComponent<KeyboardInput>();//调用PlayerInput脚本
        ani = model.GetComponent<Animator>();//调用AnimatorAPI
        //m_ani = gameObject.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();//调用RidigbodyAPI
        _col = GetComponent<CapsuleCollider>();//调用胶囊碰撞
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
        if(Input.GetKeyDown(pi.keyAttack1) && IsGrounded() && canAttack)
        {
            Attack();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float targetRunMultiple = ((pi.walk) ? 1.0f : 2.0f);
        //动画变化速度线性插值
        ani.SetFloat("forward", pi.Dmag * Mathf.Lerp(ani.GetFloat("forward"), targetRunMultiple, 0.2f));
        //停止移动时方向校准
        if(pi.Dmag > 0.1f)
        {
            //角色方向变化球形线性插值
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
        }
        if(lockPlaneVector == false)
        {
            _pV = pi.Dmag * model.transform.forward * moveSpeed * ((pi.walk) ? 1.0f : runMultiple);//平面移动速度向量
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
    }

    private void Attack()
    {
        ani.SetTrigger("Slash");
    }

    public void OnJumpEnter()
    {
        pi.inputEnable = false;
        lockPlaneVector = true;
    }

    public void OnJumpExit()
    {
        pi.inputEnable = true;
        lockPlaneVector = false;
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
    }

    public void OnSlash1Enter()
    {
        pi.inputEnable = false;
        //lockPlaneVector = true;
        lerpTarget = 1.0f;
    }

    public void OnSlash1Update()
    {
        thrustVec = model.transform.forward * ani.GetFloat("Slash1Vec");
        float currentWeight = ani.GetLayerWeight(ani.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        ani.SetLayerWeight(ani.GetLayerIndex("Attack"), currentWeight);
        _rb.position += thrustVec * Time.fixedDeltaTime;
    }

    public void OnSwordIdleEnter()
    {
        pi.inputEnable = true;
        //lockPlaneVector = false;
        lerpTarget = 0f;
    }

    public void OnSwordIdleUpdate()
    {
        float currentWeight = ani.GetLayerWeight(ani.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.1f);
        ani.SetLayerWeight(ani.GetLayerIndex("Attack"), currentWeight);
    }
    
    public void OnUpdateRootMotion(object _dP)
    {
        dP += (Vector3) _dP;
    }
}
