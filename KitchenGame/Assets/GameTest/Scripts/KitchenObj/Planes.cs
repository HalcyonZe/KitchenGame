using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Planes : BasicObj
{
    public override void PutObjs()
    {
        Vector3 pos = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))

        {
            pos = hit.point;
        }

    
        MouseSFM.Instance.PickObj.transform.parent = null;
        MouseSFM.Instance.PickObj.transform.DOMove(pos+new Vector3(0,0.3f,0), 0.3f).
            OnComplete(() => {
                MouseSFM.Instance.PickObj.GetComponent<Rigidbody>().isKinematic = false;
                MouseSFM.Instance.PickObj.GetComponent<Collider>().enabled = true;
                MouseSFM.Instance.PickObj = null;
                MouseSFM.Instance.SwitchState(MouseState.Nothing);
            });
        //MouseSFM.Instance.SwitchState(MouseState.Nothing);
    }

}
