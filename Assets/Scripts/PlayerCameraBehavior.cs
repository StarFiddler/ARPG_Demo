using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//将脚本放到子物体Camera上
public class PlayerCameraBehavior : MonoBehaviour {

    //Mouse direction
    private Vector2 mD;

    //The capsule parent!
    private Transform myBody;

	// Use this for initialization
	void Start () {

        myBody = this.transform.parent;
    
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mC = new Vector2
            (Input.GetAxisRaw("Mouse X"),
                Input.GetAxisRaw("Mouse Y"));

        mD += mC;
        //上下控制相机
        //绕x轴旋转，旋转大小
        this.transform.localRotation =
            Quaternion.AngleAxis(-mD.y, Vector3.right);
        //左右控制父级
        //绕y轴旋转，旋转大小
        myBody.localRotation =
            Quaternion.AngleAxis(mD.x, Vector3.up);
		
	}
}
