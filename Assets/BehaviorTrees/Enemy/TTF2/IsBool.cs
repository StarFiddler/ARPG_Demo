using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsBool : Action
{

    public SharedString boolName;
    public SharedGameObject obj;
    private Animator ani;
    // Start is called before the first frame update
    public override void OnAwake()
    {
        ani = obj.Value.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if(ani.GetBool(boolName.Value) == true)
        {
            return TaskStatus.Success;
        }
        
        return TaskStatus.Failure;
        
    }
}
