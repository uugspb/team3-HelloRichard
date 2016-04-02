using System;
using System.Collections.Generic;

[Serializable]
public class TouchesRemoute
{
    public List<TouchRemoute> Touches = new List<TouchRemoute>();

    public int CountTouches
    {
        get { return Touches.Count; }
    }
}