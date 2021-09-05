using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class casserole : Pan
{
    public override void Cooking()
    {
        //Debug.Log(_foodDic.Count);
        if (HasWater && _foodDic.Count > 0)
        {
            for (int i = 0; i < _foodDic.Count; i++)
            {
                GameObject food = _foodDic.ElementAt(i).Key;
                food.GetComponent<Foods>().m_foodState = Foods.FoodState.cook;
            }
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        if (m_panState == PanState.HoldWater && other.gameObject.tag == "Faucet")
        {
            HasWater = true;
            WaterHeight += Time.fixedDeltaTime;

            float WH = Mathf.Clamp(WaterHeight / 8, 0, 1);
            float Wscale = Mathf.Lerp(0.01f, 0.1f, WH);
            _water.SetActive(true);
            _water.transform.localScale = new Vector3(Wscale, 1, Wscale);
            _water.transform.localPosition = new Vector3(0, Mathf.Lerp(0.031f, 0.035f, WH), 0);

            _boiling.transform.GetChild(0).localScale = _water.transform.localScale;
            _boiling.transform.GetChild(0).localPosition = _water.transform.localPosition;

        }
    }

}
