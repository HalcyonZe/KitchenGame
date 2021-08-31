using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasPanState : BaseState
{
    public override void OnEnter()
    {
        base.OnEnter();

        layer = LayerMask.GetMask("Foods") | LayerMask.GetMask("Plane") |
                LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Plate") |
                LayerMask.GetMask("Faucet") | LayerMask.GetMask("Hearth");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (MouseSFM.Instance.PickObj != null)
        {
            MouseSFM.Instance.PickObj.transform.localRotation = Quaternion.Euler(0, -90, MouseSFM.Instance.transform.localEulerAngles.x);
        }
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
            case "Faucet":
                Obj.GetComponent<Faucet>().UseFaucet();
                break;
            case "Hearth":
                Obj.GetComponent<BasicObj>().PutObjs();
                break;
        }
    }
}
