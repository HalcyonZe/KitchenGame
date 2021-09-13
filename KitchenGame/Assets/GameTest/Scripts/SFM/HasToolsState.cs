using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasToolsState : BaseState
{
    public override void OnEnter()
    {
        
        layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Plane");
        if(MouseSFM.Instance.PickObj.TryGetComponent<Cooking>(out Cooking cooking))
        {
            layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Plane")| 
                LayerMask.GetMask("Foods")| LayerMask.GetMask("Plate");
        }
        if (MouseSFM.Instance.PickObj.TryGetComponent<RollingPin>(out RollingPin rollingPin))
        {
            layer = LayerMask.GetMask("dough") | LayerMask.GetMask("Plane") ;
        }
        if (MouseSFM.Instance.PickObj.TryGetComponent<Shovel>(out Shovel shovel))
        {
            layer = LayerMask.GetMask("Plate") | LayerMask.GetMask("Plane");
        }
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
            case "Foods":
                MouseSFM.Instance.PickObj.GetComponent<BasicObj>().UseTools(Obj);
                break;
            case "Plate":
                MouseSFM.Instance.PickObj.GetComponent<BasicObj>().UseTools(Obj);
                break;
            case "dough":
                MouseSFM.Instance.PickObj.GetComponent<BasicObj>().UseTools(Obj);
                break;
        }
    }
}
