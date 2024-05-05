using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public PlayerInput pi;
    private GameObject playerHandle;
    private GameObject cameraHandle;
    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerHandle.transform.Rotate(Vector3.up, pi.MouseX * 10.0f *Time.deltaTime);
    }
}
