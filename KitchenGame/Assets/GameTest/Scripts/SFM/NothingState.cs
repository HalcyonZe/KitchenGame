using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingState : BaseState
{
    public override void OnEnter()
    {
        base.OnEnter();

        layer = LayerMask.GetMask("Foods") | LayerMask.GetMask("Tools") |
                LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Plate")|
                LayerMask.GetMask("Faucet") | LayerMask.GetMask("dough")|
                LayerMask.GetMask("Bell")| LayerMask.GetMask("posters");
    }

    public override void MouseAction(GameObject Obj)
    {
        Obj.GetComponent<BasicObj>().PickObjs();
    }

}
