using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasPanState : BaseState
{
    public override void OnEnter()
    {
        base.OnEnter();

        layer = LayerMask.GetMask("Foods") | LayerMask.GetMask("Plane") |
                LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Plate");
    }

    public override void MouseAction(GameObject Obj)
    {
        base.MouseAction(Obj);
        switch (LayerMask.LayerToName(Obj.layer))
        {
            case "Foods":
                MouseSFM.Instance.PickObj.GetComponent<BasicObj>().UseTools(Obj);
                break;
            case "CutFoods":
                MouseSFM.Instance.PickObj.GetComponent<BasicObj>().UseTools(Obj);
                break;
            case "Plane":
                Obj.GetComponent<BasicObj>().PutObjs();
                break;
            case "Plate":
                MouseSFM.Instance.PickObj.GetComponent<BasicObj>().SendObjs(Obj) ;
                break;
        }
    }
}
