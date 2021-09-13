using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hearth : BasicObj
{
    public override void PutObjs()
    {
        MouseSFM.Instance.SwitchState(MouseState.Nothing);

        GameObject my_Obj = MouseSFM.Instance.PickObj;
        MouseSFM.Instance.PickObj.transform.parent = null;
        MouseSFM.Instance.PickObj = null;

        my_Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        my_Obj.transform.DOMove(this.transform.position+new Vector3(0,0.4f,0), 0.3f).
            OnComplete(() => {
                if (my_Obj.TryGetComponent<casserole>(out casserole casserole))
                {
                    my_Obj.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                my_Obj.GetComponent<Rigidbody>().isKinematic = false;
                my_Obj.GetComponent<Collider>().enabled = true;

                my_Obj.GetComponent<Pan>().ChangeState(Pan.PanState.Cooking);
                my_Obj = null;
            });    
        
    }
}
