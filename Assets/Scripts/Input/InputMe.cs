using UnityEngine;
using System.Collections;

public class InputMe : InputBase
{
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            InputPacket.Touches.Touches.Clear();
            for (int i = 0; i < Input.touchCount; i++)
            {
                var tch = Input.GetTouch(i);
                InputPacket.Touches.Touches.Add(new TouchRemoute()
                {
                    TouchId = tch.fingerId,
                    Position = tch.position
                });
            }
        }

        if (Input.gyro.enabled)
        {
            InputPacket.Gyroscope.Aceleration = Input.gyro.userAcceleration;
            InputPacket.Gyroscope.Gravity = Input.gyro.gravity;
            InputPacket.Gyroscope.Attitude = Input.gyro.attitude;
        }
    }
}