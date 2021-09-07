using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasToolsState : BaseState
{
    public override void OnEnter()
    {
        
        layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Plane");
        if(/*MouseSFM.Instance.PickObj.tag!="Knife"*/MouseSFM.Instance.PickObj.TryGetComponent<Cooking>(out Cooking cooking))
        {
            layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Plane")| LayerMask.GetMask("Foods")| LayerMask.GetMask("Plate");
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
        }
    }
}
