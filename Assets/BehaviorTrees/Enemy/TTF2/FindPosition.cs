using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FindPosition : Action
{
    //public Animator animator;
    public SharedGameObject obj;
    public SharedVector3 pos;
    public SharedVector3 variable;
 
    public override TaskStatus OnUpdate()
    {
    	//切换动画状态为攻击
        pos = obj.Value.transform.position;
        variable.Value = pos.Value;
        return TaskStatus.Success;
    }

    /*public override void OnEnd(){
        return pos;
    }*/

    public override void OnReset()
    {
        obj = null;
        pos = Vector3.zero;
        variable = Vector3.zero;
    }
}
