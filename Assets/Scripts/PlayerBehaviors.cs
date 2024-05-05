using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviors : MonoBehaviour
{
     
  /*
  public float moveSpeed = 10f;
  public float rotateSpeed = 75f;
  public float jumpVelocity = 5f;
  public float distanceToGround = 0.1f;
  public LayerMask groundLayer;
  public GameObject bullet;
  public float bulletSpeed = 100f;

  private float vInput;
  private float hInput;
  */
  public float moveSpeed = 6.0f;
  public float jumpSpeed = 8.0f;
  public float gravity = 20.0f;

    
  //md is move Direction
  private Vector3 mD = Vector3.zero;
  // Start is called before the first frame update
  private Rigidbody _rb; //刚体类型变量
  private CapsuleCollider _col; //胶囊体碰撞类型变量
  private GameBehavior _gameManager; //游戏行为脚本类型变量
  private Animator _ani; //动画状态机行为类型变量
  void Start()
  {
    //变量初始化
    _rb = GetComponent<Rigidbody>();
    _col = GetComponent<CapsuleCollider>();
    _ani = GetComponent<Animator>();
    _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
  }
  // Update is called once per frame
  void Update()
  {
    CharacterController controller = GetComponent<CharacterController>();
    //CharacterController组件中自带检测是否在地面方法
    if(controller.isGrounded)
    {
      //范围（-1,1）
      float h = Input.GetAxis("Horizontal");
      //范围（-1,1）
      float v = Input.GetAxis("Vertical");
			//获取单位向量
      mD = new Vector3(h, 0, v);
      //从自身坐标到世界坐标变换方向。
      mD = transform.TransformDirection(mD);
      //单位向量乘速度
      mD *= moveSpeed;
      //跳跃检测
      if (Input.GetButton("Jump"))
      {
        mD.y = jumpSpeed;

        _ani.SetTrigger("JumpTrigger");
      }       
    }
    mD.y -= gravity * Time.deltaTime;
    //CharacterController组件移动方法
    controller.Move(mD * Time.deltaTime);
    if(Input.GetAxis("Vertical") > 0) //移动状态判定
    {
      _ani.SetBool("BoolRun", true);
      if(Input.GetKey(KeyCode.LeftShift))
      {
        moveSpeed = 3.0f;
        _ani.SetBool("BoolWalk", true);
      }
      else
      {
        _ani.SetBool("BoolWalk", false);
        moveSpeed = 6.0f;
      }
    }
    else
    {
      _ani.SetBool("BoolRun", false);
      _ani.SetBool("BoolWalk", false);
    }
    
    /*
    vInput = Input.GetAxis("Vertical") * moveSpeed; //移动速度
    hInput = Input.GetAxis("Horizontal") * rotateSpeed; //摄像机角度转动速度
    if(IsGrounded() && Input.GetKeyDown(KeyCode.Space)) //跳跃判定
    {
      _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
      _ani.SetTrigger("JumpTrigger");
    }
    if (Input.GetMouseButtonDown(1)) //拔枪判定
    {
      _ani.SetBool("ShootMode", true);
    }
    if (IsShoot() && Input.GetMouseButtonUp(1)) //收枪判定
    {
      _ani.SetBool("ShootMode", false);
    }
    if (IsShoot() && Input.GetMouseButtonDown(0)) //射击攻击判定
    {
      GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
      Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
      bulletRB.velocity = this.transform.forward * bulletSpeed;
    }
      
    if(Input.GetAxis("Vertical") > 0) //移动状态判定
    {
      _ani.SetBool("BoolRun", true);
      if(Input.GetKey(KeyCode.LeftShift))
      {
        moveSpeed = 3;
        _ani.SetBool("BoolWalk", true);
      }
      else
      {
        _ani.SetBool("BoolWalk", false);
        moveSpeed = 10;
      }
    }
    else
    {
      _ani.SetBool("BoolRun", false);
      _ani.SetBool("BoolWalk", false);
    }
    */
       
    
    /*
    this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
    this.transform.Translate(Vector3.up * hInput * Time.deltaTime); 
    */
  }
  void FixedUpdate()
  {
    /*
    Vector3 rotation = Vector3.up * hInput;
    Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
    _rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
    _rb.MoveRotation(_rb.rotation * angleRot);
    */
  }

  /*
  private bool IsGrounded()
  {
    Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
    bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
    return grounded;
  }
  */

  void OnCollisionEnter(Collision collision)
  {
    if(collision.gameObject.name == "Enemy")
    {
      _gameManager.HP -=1;
    }
  }

  private bool IsShoot()
  {
    bool Shoot = _ani.GetBool("ShootMode");
    return Shoot;
  }
}
