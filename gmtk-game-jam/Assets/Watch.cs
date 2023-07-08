using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Watch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WatchObject(GameObject gameObject)
    {
        _virtualCamera.Follow = gameObject.transform;
    }
}
