using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BasicObj : MonoBehaviour
{
    public virtual void UseTools(GameObject Obj)
    {

    }

    public virtual void PickObjs()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.DOMove(MouseSFM.Instance.transform.GetChild(0).position, 0.1f).
                        OnComplete(() => {
                            
                            this.transform.parent = MouseSFM.Instance.transform;                            
                        }); 
        MouseSFM.Instance.PickObj = this.gameObject;       
    }

    public virtual void PutObjs()
    {
        MouseSFM.Instance.PickObj.transform.parent = null;
        MouseSFM.Instance.PickObj.transform.DOMove(this.transform.GetChild(0).position, 0.3f).
            OnComplete(() => {
                MouseSFM.Instance.PickObj.GetComponent<Rigidbody>().isKinematic = false;
                MouseSFM.Instance.PickObj.GetComponent<Collider>().enabled = true;
                MouseSFM.Instance.PickObj = null;
            });
    }

    public virtual void SendObjs(GameObject Obj)
    {

    }
}
