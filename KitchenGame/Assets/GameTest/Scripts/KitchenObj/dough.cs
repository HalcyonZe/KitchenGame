using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dough : BasicObj
{
    private Material m_material;
    private int doughY = 0;
    public bool HasOil = false;

    public enum State
    {
        normal,
        steamed,
    }
    public State m_state;

    public float M_steamed = 600;
    private float steamedTime = 0;

    public List<Color> m_colors;

    private void Awake()
    {
        m_material = this.GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        if (m_state == State.steamed)
        {
            steamedUpdate();
        }
    }

    private void steamedUpdate()
    {
        if (steamedTime < M_steamed)
        {
            steamedTime += Time.fixedDeltaTime * TimeController.Instance.TimeScale;
            float r = Mathf.Clamp(steamedTime / M_steamed, 0, 1);
            float colorG = Mathf.Lerp(1, m_colors[0].g, r);
            float colorB = Mathf.Lerp(1, m_colors[0].b, r);

            Color color = new Color(m_colors[0].r, colorG, colorB);
            m_material.SetColor("_Color", color);
        }
    }

    public override void PickObjs()
    {
        base.PickObjs();
        MouseSFM.Instance.SwitchState(MouseState.HasFoods);
        this.gameObject.layer = LayerMask.NameToLayer("Foods");
        m_state = State.normal;
    }

    public void SetDoughY()
    {
        doughY++;
        if (doughY <= 3)
        {
            m_material.SetFloat("_Control", doughY * 0.3f);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag=="oil")
        {
            HasOil = true;
        }
    }

}
