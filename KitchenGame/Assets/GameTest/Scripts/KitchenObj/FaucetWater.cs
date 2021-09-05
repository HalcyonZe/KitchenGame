using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetWater : MonoBehaviour
{
    ParticleSystem Ps;
    private bool IsOpen = false;
    private float waterTime = 0;

    private void Awake()
    {
        Ps = this.GetComponent<ParticleSystem>();
    }

    public void SetState(bool isOpen)
    {
        IsOpen = isOpen;
        if (IsOpen)
        {
            Ps.Play();
        }
        else
        {
            Ps.Stop();
            waterTime = 0;
        }
    }

    

    private void OnParticleCollision(GameObject other)
    {
        
        if (other.TryGetComponent<Pan>(out Pan pan)/*&&IsOpen*/)
        {
            Debug.Log("hhh");
            //waterTime += Time.fixedDeltaTime;
            //pan.SetWaterHeight(waterTime);
        }
    }
}
