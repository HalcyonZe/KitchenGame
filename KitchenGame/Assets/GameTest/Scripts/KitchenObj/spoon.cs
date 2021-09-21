using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class spoon : BasicObj
{
    public override void PickObjs()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.DOMove(MouseSFM.Instance.transform.GetChild(0).position, 0.1f).
                        OnComplete(() => {

                            this.transform.parent = MouseSFM.Instance.transform;
                            MouseSFM.Instance.SwitchState(MouseState.HasTools);
                        });
        MouseSFM.Instance.PickObj = this.gameObject;

    }

    public override void UseTools(GameObject Obj)
    {
        Obj.GetComponent<Soup>().MakeSoup();

    }
}
