using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FoodSteamer : BasicObj
{
    public bool HasLib = true;
    public override void PutObjs()
    {
        GameObject obj = MouseSFM.Instance.PickObj;

        if (obj.TryGetComponent<Lid>(out Lid lid))
        {
            obj.transform.DOMove(this.transform.GetChild(0).position, 0.3f).
                OnComplete(() =>
                {
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                    obj.GetComponent<Collider>().enabled = true;
                    obj.transform.parent = this.transform;
                    obj.transform.localEulerAngles = this.transform.GetChild(0).localEulerAngles;
                    HasLib = true;

                    MouseSFM.Instance.PickObj = null;
                });
            MouseSFM.Instance.SwitchState(MouseState.Nothing);
        }

        if (obj.TryGetComponent<dough>(out dough dough)&&!HasLib)
        {
            obj.transform.DOMove(this.transform.GetChild(0).position, 0.3f).
                OnComplete(() =>
                {
                    obj.GetComponent<Rigidbody>().isKinematic = false;
                    obj.GetComponent<Collider>().enabled = true;
                    obj.transform.parent = this.transform;

                    dough.m_state = dough.State.steamed;

                    MouseSFM.Instance.PickObj = null;
                });
            MouseSFM.Instance.SwitchState(MouseState.Nothing);
        }
    }
}
