using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public KeyboardInput pi;
    public float hSpeed = 200.0f;
    public float vSpeed = 100.0f;
    public float cameraCatchSpeed = 0.05f;
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject camera;
    private Vector3 cameraCatch;
    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = playerHandle.GetComponent<PlayerControl>().model;
        camera = Camera.main.gameObject;
    }

    void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
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
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraCatch, cameraCatchSpeed);
        camera.transform.eulerAngles = transform.eulerAngles;
        //camera.transform.LookAt(cameraHandle.transform);
    }
}
