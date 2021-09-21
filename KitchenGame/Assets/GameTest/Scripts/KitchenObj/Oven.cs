using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Oven : BasicObj
{
    public override void PutObjs()
    {
        GameObject obj = MouseSFM.Instance.PickObj;

        if (obj.GetComponent<BasicObj>().ObjName == "¿¾Ñ¼")
        {
            obj.transform.DOMove(this.transform.GetChild(0).position, 0.3f).
                OnComplete(() =>
                {
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                    obj.GetComponent<Collider>().enabled = true;
                    obj.transform.parent = this.transform;
                    obj.transform.localEulerAngles = this.transform.GetChild(0).localEulerAngles;

                    obj.GetComponent<Foods>().m_foodState = Foods.FoodState.bake;

                    MouseSFM.Instance.PickObj = null;
                });
            MouseSFM.Instance.SwitchState(MouseState.Nothing);
        }
    }
}
