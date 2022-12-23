using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform point;
    void Start()
    {
        // 获取物体的 Transform 组件
        Transform transform = gameObject.GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float CameraZ = Camera.main.transform.position.z;
        float RotateSpeed = 0.5f;
        //var target = Quaternion.Euler(new Vector3(0, 90, 0));
        float yRotation = transform.rotation.y;
        //Debug.Log(yRotation);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);

        if (yRotation > 0.8)
        {
            RotateSpeed = 0f;
        }
        if (CameraZ > transform.position.z - 1.2f) 
        {
            transform.RotateAround(point.transform.position, new Vector3(0, 1, 0), RotateSpeed);
        }
        
        
    }
}
