using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasFoodsState : BaseState
{
    public override void OnEnter()
    {
        base.OnEnter();

        layer = LayerMask.GetMask("Plane") | LayerMask.GetMask("UsefulPlane") |
                LayerMask.GetMask("Plate") | LayerMask.GetMask("trash") ;
    }

    public override void MouseAction(GameObject Obj)
    {
        Obj.GetComponent<BasicObj>().PutObjs();
    }
}
