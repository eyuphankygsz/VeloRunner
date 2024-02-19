using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float _zOffset;
    [SerializeField] Transform _target;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x, transform.position.y, _target.position.z - _zOffset
            ); 
    }
}
