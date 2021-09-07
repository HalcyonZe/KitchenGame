using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Faucet : BasicObj
{
    ParticleSystem Ps;
    //public FaucetWater fw;
    private bool IsOpen = false ;
    private AudioSource m_audioSource;

    private void Start()
    {
        Ps = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        Ps.Stop();
        m_audioSource = this.GetComponent<AudioSource>();
        //fw = this.transform.GetChild(1).GetComponent<FaucetWater>();
        //fw.SetState(IsOpen);
    }

    public override void PickObjs()
    {
        IsOpen = !IsOpen;
        //fw.SetState(IsOpen);
        if (IsOpen)
        {
            Ps.Play();
            m_audioSource.Play();
        }
        else
        {
            Ps.Stop();
            m_audioSource.Stop();
        }
    }

    public void UseFaucet()
    {
        IsOpen = true;
        //fw.SetState(IsOpen);
        Ps.Play();
        m_audioSource.Play();
        MouseSFM.Instance.PickObj.GetComponent<Pan>().SetPosY(this.transform.GetChild(0).position);

    }
}
