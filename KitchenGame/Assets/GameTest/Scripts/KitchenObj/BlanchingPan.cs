using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlanchingPan : Pan
{
    public override void Cooking()
    {
        if (Hasliquid > 0 && _foodDic.Count > 0)
        {
            for (int i = 0; i < _foodDic.Count; i++)
            {
                GameObject food = _foodDic.ElementAt(i).Key;
                food.GetComponent<Foods>().m_foodState = Foods.FoodState.Blanching;
            }
        }
    }
}
