using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public KeyboardInput pi;
    public float hSpeed = 200.0f;
    public float vSpeed = 100.0f;
    public float cameraCatchSpeed = 0.05f;
    public Image lockPoint;//锁定UI图像
    public bool lockPos;//锁定旋转
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject camera;
    private Vector3 cameraCatch;
    private LockTarget lockTarget;//定义一个LockTarget类下的lockTarget变量，该变量有两种类型（obj和halfHeight）
    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = playerHandle.GetComponent<PlayerControl>().model;
        camera = Camera.main.gameObject;
        lockPoint.enabled = false;
        lockPos = false;
    }

    void Update()
    {
        if(lockTarget != null)
        {
            lockPoint.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            if(Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)
            {
                    lockTarget = null;
                    lockPoint.enabled = false;
                    lockPos = false;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;
            //水平镜头位移
            playerHandle.transform.Rotate(Vector3.up, pi.MouseX * hSpeed * Time.fixedDeltaTime);
            tempEulerX -= pi.MouseY * vSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp (tempEulerX, -50, 40);
            //纵向镜头位移
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
            model.transform.eulerAngles = tempModelEuler;
            //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.5f);
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
        }
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraCatch, cameraCatchSpeed);
        camera.transform.eulerAngles = transform.eulerAngles;
        //camera.transform.LookAt(cameraHandle.transform);
    }

    public void CameraLock()//视角锁定
    {
        Vector3 modelOrigin1 = playerHandle.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 0, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(1.0f, 5f, 10f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        if(cols.Length == 0)
        {
            lockTarget = null;
            lockPoint.enabled = false;
            lockPos = false;
        }
        else
        {
            foreach(var col in cols)
            {
                if(lockTarget != null && lockTarget.obj == col.gameObject)
                {
                    lockTarget = null;
                    lockPoint.enabled = false;
                    lockPos = false;
                    break;
                }
                lockTarget = new LockTarget(col.gameObject, col.bounds.extents.y);
                lockPoint.enabled = true;
                lockPos = true;
                break;
            }
        }
        
    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;
        public LockTarget(GameObject _obj, float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
        }
    }
}
