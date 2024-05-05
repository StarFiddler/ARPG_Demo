using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

public void DoRun() {
    this.gameObject.GetComponent<Animator>().SetBool("BoolRun", true);
    //TODO 也可以同时修改transform.position来实际调整距离
}

public void Stop() {
    this.gameObject.GetComponent<Animator>().SetBool("BoolRun", false);
}
}
