using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Faucet : BasicObj
{
    ParticleSystem Ps;
    private bool IsOpen = false ;

    private void Start()
    {
        Ps = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        Ps.Stop();
    }

    public override void PickObjs()
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            Ps.Play();
        }
        else
        {
            Ps.Stop();
        }
    }

    public void UseFaucet()
    {
        IsOpen = true;
        Ps.Play();
        MouseSFM.Instance.PickObj.GetComponent<Pan>().SetPosY(this.transform.GetChild(0).position);

    }
}
