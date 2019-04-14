using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 

    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    void LateUpdate()
    {
        //float z =transform.position.z;
        //transform.position =new Vector3(Mathf.SmoothStep(transform.position.x, player.transform.position.x, .15f);,0,z);      
        transform.position = player.transform.position + offset;
    }
}
