using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : BasicObj
{
    public override void PutObjs()
    {
        base.PutObjs();
        MouseSFM.Instance.PickObj.layer = LayerMask.NameToLayer("CutFoods");
        MouseSFM.Instance.SwitchState(MouseState.Nothing);
    }
}
