using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasToolsState : BaseState
{
    public override void OnEnter()
    {
        
        layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Plane");
        base.OnEnter();
    }

    public override void MouseAction(GameObject Obj)
    {
        base.MouseAction(Obj);
        switch (LayerMask.LayerToName(Obj.layer))
        {
            case "CutFoods":
                MouseSFM.Instance.PickObj.GetComponent<BasicObj>().UseTools(Obj);
                break;
            case "Plane":
                Obj.GetComponent<BasicObj>().PutObjs();
                break;
        }
    }
}
