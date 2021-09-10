using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dough : BasicObj
{
    private Material m_material;
    private int doughY = 0;

    private void Awake()
    {
        m_material = this.GetComponent<Renderer>().material;
    }

    public override void PickObjs()
    {
        base.PickObjs();
        MouseSFM.Instance.SwitchState(MouseState.HasFoods);
        this.gameObject.layer = LayerMask.NameToLayer("Foods");
    }

    public void SetDoughY()
    {
        doughY++;
        if (doughY <= 3)
        {
            m_material.SetFloat("_Control", doughY * 0.3f);
        }
    }

}
