using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planes : BasicObj
{
    public override void PutObjs()
    {               
        base.PutObjs();
        MouseSFM.Instance.SwitchState(MouseState.Nothing);
    }
}
