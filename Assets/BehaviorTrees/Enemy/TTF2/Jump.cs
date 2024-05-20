using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Jump : Action
{
    //public Animator animator;
    //public GameObject model;
    public SharedGameObject obj;
    public Rigidbody _rb;
    //public SharedGameObject targetObj;
    //public SharedVector3 pos;
    //public SharedVector3 variable;
    private Animator ani;
    private SharedVector3 thrustVec;

    public override void OnAwake()
    {
        ani = obj.Value.GetComponentInChildren<Animator>();
        _rb = obj.Value.GetComponent<Rigidbody>();
        //thrustVec.Value = Vector3.zero;
    }
 
    public override void OnStart()
    {
        Debug.Log(_rb.velocity);
        thrustVec = new Vector3(0, 5 ,0);
        //ani.SetBool("IsGround", false);
        _rb.velocity += thrustVec.Value;
        thrustVec = Vector3.zero;
    }
    public override TaskStatus OnUpdate()
    {
    	//切换动画状态为攻击
        //obj.Value.transform.position = pos.Value;
        //_rb.velocity = pos.Value;
        //obj.Value.transform.position += _rb.velocity * Time.deltaTime;
        //obj.Value.transform.forward = targetObj.Value.transform.localPosition - obj.Value.transform.position;
        ani.SetFloat("forward", Mathf.Lerp(ani.GetFloat("forward"), 2, 0.2f));
        //variable.Value = pos.Value;
        return TaskStatus.Success;
    }

    /*public override void OnEnd(){
        return pos;
    }*/

    public override void OnReset()
    {
        obj = null;
        //pos = Vector3.zero;
        //variable = Vector3.zero;
    }
}
