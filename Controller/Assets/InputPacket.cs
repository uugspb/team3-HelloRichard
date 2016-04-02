using System;

[Serializable]
public class InputPacket
{
    public TouchesRemoute Touches = new TouchesRemoute();
    public GyroscopeRemoute Gyroscope = new GyroscopeRemoute();

    public void Clear()
    {
        Touches.Touches.Clear();
    }
}