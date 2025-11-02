using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoLimits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (transform.position + transform.forward * 1000) );
    }
}
