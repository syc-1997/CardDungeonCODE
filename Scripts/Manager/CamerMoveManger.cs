
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMoveManger : MonoBehaviour
{
    public static CamerMoveManger Instance;

    private Transform mainCamera;

    float moveSpeed = 0.005f;

    private void Awake()
    {
        Instance = this;
        mainCamera = gameObject.GetComponent<Transform>();

    }
    public void MoveToDestination(Vector3 CamerDestination)
    {

        mainCamera.position = Vector3.Lerp(mainCamera.position, CamerDestination, moveSpeed);        
    }
}
