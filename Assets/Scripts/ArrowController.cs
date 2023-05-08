using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Vector3 target;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Vector3 value) 
    {
        target= value;
    }

}
