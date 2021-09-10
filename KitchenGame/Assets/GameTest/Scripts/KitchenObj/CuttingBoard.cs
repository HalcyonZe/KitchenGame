using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : BasicObj
{
    public override void PutObjs()
    {
        base.PutObjs();
        MouseSFM.Instance.PickObj.layer = LayerMask.NameToLayer("CutFoods");
        if(MouseSFM.Instance.PickObj.TryGetComponent<dough>(out dough dough))
        {
            MouseSFM.Instance.PickObj.layer = LayerMask.NameToLayer("dough");
        }
        MouseSFM.Instance.SwitchState(MouseState.Nothing);
    }
}
