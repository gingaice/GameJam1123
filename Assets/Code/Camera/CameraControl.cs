using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    private CinemachineConfiner2D confiner;

    // Start is called before the first frame update
    void Start()
    {
        confiner = camera.GetComponent<CinemachineConfiner2D>();
        confiner.InvalidateCache();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
