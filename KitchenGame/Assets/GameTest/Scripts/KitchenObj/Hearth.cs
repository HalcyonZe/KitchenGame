using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hearth : BasicObj
{
    public override void PutObjs()
    {
        MouseSFM.Instance.PickObj.transform.parent = null;

        MouseSFM.Instance.PickObj.transform.DOMove(this.transform.position+new Vector3(0,0.4f,0), 0.3f).
            OnComplete(() => {
                
                if(!MouseSFM.Instance.PickObj.TryGetComponent<casserole>(out casserole casserole))
                {
                    MouseSFM.Instance.PickObj.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                MouseSFM.Instance.PickObj.GetComponent<Rigidbody>().isKinematic = false;
                MouseSFM.Instance.PickObj.GetComponent<Collider>().enabled = true;
                //MouseSFM.Instance.PickObj.GetComponent<Pan>().m_panState=Pan.PanState.Blanching;
                MouseSFM.Instance.PickObj.GetComponent<Pan>().ChangeState(Pan.PanState.Cooking);
                MouseSFM.Instance.PickObj = null;
            });    
        MouseSFM.Instance.SwitchState(MouseState.Nothing);
    }
}
