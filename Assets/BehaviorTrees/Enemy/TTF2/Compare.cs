using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Compare : Conditional
{
    public SharedFloat variable;
    public SharedFloat compareTo;

    public override TaskStatus OnUpdate()
    {
        return variable.Value <= compareTo.Value ? TaskStatus.Success : TaskStatus.Failure;
    }

    public override void OnReset()
    {
        variable = 0;
        compareTo = 0;
    }
}