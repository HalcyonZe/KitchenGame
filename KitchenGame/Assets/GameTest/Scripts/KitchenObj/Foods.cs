using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods : BasicObj
{
    #region 枚举属性
    public enum FoodState
    {
        normal,
        Blanching,
        boil,
        fry,
    }
    public FoodState m_foodState = FoodState.normal;
    #endregion

    #region 属性
    public string foodName;
    public FoodItem m_foodItem = new FoodItem();
    private float BlanchingTime = 0;
    private float CookingTime = 0;
    private float FryTime = 0;
    #endregion

    #region 组件
    private Material m_material;
    public List<Color> m_colors;
    //public int colorIndex = 0;
    #endregion

    private void Awake()
    {
        m_foodItem.foodName = this.foodName;
        m_material = this.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        switch (m_foodState)
        {
            case FoodState.normal:
                break;
            case FoodState.Blanching:
                FoodBlanching();
                break;
            case FoodState.boil:
                FoodBoil();
                break;
            case FoodState.fry:
                FoodFry();
                break;
        }
    }

    public void foodInit(Foods food)
    {
        m_foodItem.foodName = food.m_foodItem.foodName;
        m_foodItem.handleScoreDic = new Dictionary<string, int>(food.m_foodItem.handleScoreDic);
        foodName = food.m_foodItem.foodName;
        m_colors = food.m_colors;
        //colorIndex = food.colorIndex;
    }

    public void foodItemInit(FoodItem foodItem)
    {
        m_foodItem.foodName = foodItem.foodName;
        m_foodItem.handleScoreDic = new Dictionary<string, int>(foodItem.handleScoreDic);
        foodName = foodItem.foodName;
    }

    public override void PickObjs()
    {
        base.PickObjs();
        MouseSFM.Instance.SwitchState(MouseState.HasFoods);
        this.gameObject.layer = LayerMask.NameToLayer("Foods");
        m_foodState = FoodState.normal;
    }

    private void FoodBlanching()
    {
        if (BlanchingTime < 5)
        {
            BlanchingTime += Time.fixedDeltaTime;
            float r = Mathf.Clamp(BlanchingTime / 5, 0, 1);
            float colorG = Mathf.Lerp(1, (1 + m_colors[0].g) / 2, r);
            float colorB = Mathf.Lerp(1, (1 - m_colors[0].b) / 2, r);
            Color color = new Color(m_colors[0].r, colorG, colorB);
            m_material.SetColor("_BaseColor", color);
        }
        else
        {
            if (!m_foodItem.handleScoreDic.ContainsKey("blanching"))
            {
                m_foodItem.handleScoreDic.Add("blanching", 5);
            }
            m_foodState = FoodState.normal;
        }
    }

    private void FoodBoil()
    {
        if (CookingTime < 8)
        {
            CookingTime += Time.fixedDeltaTime;
            float r = Mathf.Clamp(CookingTime / 8, 0, 1);
            float colorG = Mathf.Lerp((1 + m_colors[0].g) / 2, m_colors[0].g, r);
            float colorB = Mathf.Lerp((1 - m_colors[0].b) / 2, m_colors[0].b, r);
            //Debug.Log(colorG);
            Color color = new Color(m_colors[0].r, colorG, colorB);
            m_material.SetColor("_BaseColor", color);
        }
        else
        {
            /*if (!m_foodItem.handleScoreDic.ContainsKey("blanching"))
            {
                m_foodItem.handleScoreDic.Add("blanching", 5);
            }*/
            m_foodState = FoodState.normal;
        }
    }

    private void FoodFry()
    {
        if (FryTime < 10)
        {
            FryTime += Time.fixedDeltaTime;
            float r = Mathf.Clamp(FryTime / 8, 0, 1);
            float colorG = Mathf.Lerp(m_colors[0].g, m_colors[1].g, r);
            float colorB = Mathf.Lerp(m_colors[0].b, m_colors[1].b, r);

            Color color = new Color(m_colors[1].r, colorG, colorB);
            m_material.SetColor("_BaseColor", color);
        }
        else
        {
            m_foodState = FoodState.normal;
        }
    }

}
