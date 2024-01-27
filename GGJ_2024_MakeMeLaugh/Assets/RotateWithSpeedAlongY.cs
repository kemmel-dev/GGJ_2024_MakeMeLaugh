using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithSpeedAlongY : MonoBehaviour
{
    private float _angle;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        _angle += (speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0,  _angle, 0));
    }
}
