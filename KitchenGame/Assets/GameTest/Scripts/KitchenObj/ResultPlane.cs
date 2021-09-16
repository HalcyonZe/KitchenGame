using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResultPlane : BasicObj
{
    private List<FoodItem> FoodItems = new List<FoodItem>();


    /*private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CalcResult();
        }
    }*/

    public void CalcResult()
    {
        if (this.transform.childCount > 1)
        {
            GameObject res = this.transform.GetChild(1).gameObject;
            if (res.TryGetComponent<Plates>(out Plates plates))
            {
                plates = res.GetComponent<Plates>();
                if (plates.Foods.Count > 0)
                {
                    for (int i = 0; i < plates.Foods.Count; i++)
                    {
                        FoodItem foodItem = new FoodItem();
                        foodItem.foodName = plates.Foods[i].GetComponent<Foods>().m_foodItem.foodName;
                        foodItem.handleScoreDic = new Dictionary<string, int>(plates.Foods[i].GetComponent<Foods>().m_foodItem.handleScoreDic);
                        //FoodItems.Add(plates.Foods[i].GetComponent<Foods>().m_foodItem);
                        FoodItems.Add(foodItem);
                    }
                    ScoreController.Instance.DishCalc(FoodItems);
                    plates.Foods.Clear();
                    for(int i = 2; i < plates.gameObject.transform.childCount; i++)
                    {
                        GameObject t = plates.gameObject.transform.GetChild(i).gameObject;
                        t.transform.parent = null;
                        Destroy(t);
                    }
                }
                else
                {
                    Debug.Log("没有食物!");
                }
            }
            else
            {
                Debug.Log("没有盘子!");
            }
        }
    }

    public override void PutObjs()
    {
        GameObject Obj = MouseSFM.Instance.PickObj;
        MouseSFM.Instance.PickObj = null;

        Obj.transform.DOMove(this.transform.GetChild(0).position, 0.3f).
            OnComplete(() => {
                Obj.GetComponent<Rigidbody>().isKinematic = false;
                Obj.GetComponent<Collider>().enabled = true;
                Obj.transform.parent = this.transform;
                Obj = null;
            });
        MouseSFM.Instance.SwitchState(MouseState.Nothing);
    }
}
