using System;
using UnityEngine;

[Serializable]
public class GyroscopeRemoute
{
    public Vector3 Gravity;
    public Vector3 Aceleration;
    public Quaternion Attitude;
}

[Serializable]
public class Vector3Data
{
    public float x;
    public float y;
    public float z;

    public Vector3 Vector
    {
        get
        {
            return new Vector3(x,y,z);
        }

        set
        {
            x = value.x;
            y = value.y;
            z = value.z;
            
        }
    }
}