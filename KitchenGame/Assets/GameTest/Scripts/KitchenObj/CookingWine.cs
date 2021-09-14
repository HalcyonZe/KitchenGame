using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CookingWine : Cooking
{
    private ParticleSystem ps;

    public enum WineType
    {
        wine,
        sauce,
        oil,
        haooil,
    }

    public WineType m_cooking;

    private void Awake()
    {
        ps = this.transform.GetChild(3).GetComponent<ParticleSystem>();
        ps.Stop();
    }

    public override void UseTools(GameObject Obj)
    {
        MouseSFM.Instance.PickObj.transform.parent = null;

        GameController.Instance.PlayerPause();

        ObjY = Obj.transform.position.y + 0.4f;

        transform.DOMove(new Vector3(Obj.transform.position.x, ObjY, Obj.transform.position.z), 0.3f).
            OnComplete(() => {


                Cursor.lockState = CursorLockMode.None;

                UseMouse = true;

            });
    }

    public override void StopCooking()
    {
        if (Input.GetMouseButtonDown(1) && UseMouse)
        {
            UseMouse = false;

            Cursor.lockState = CursorLockMode.Locked;
            PickObjs();
            GameController.Instance.PlayerPlay();
            UIController.Instance.CloseTip();
        }

    }

    private float wineTime = 0;
    public override void MouseAction()
    {
        if (Input.GetMouseButton(0))
        {
            ps.Play();

            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Foods");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                GameObject obj = hit.transform.gameObject;

                SetCooking(obj);
            }

            LayerMask layer2 = LayerMask.GetMask("Plate");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer2))
            {
                wineTime += Time.fixedDeltaTime;
                if (wineTime > 3.0f)
                {
                    //Debug.Log("hhh");
                    switch (m_cooking)
                    {
                        case WineType.wine:
                            UIController.Instance.OpenTip("�Ͼ�", 5);
                            break;
                        case WineType.sauce:
                            UIController.Instance.OpenTip("����", 5);
                            break;
                        case WineType.oil:
                            UIController.Instance.OpenTip("������", 5);
                            break;
                        case WineType.haooil:
                            UIController.Instance.OpenTip("����", 5);
                            break;
                    }
                    wineTime = 0;
                }
            }

        }
        else
        {
            ps.Stop();
        }
    }

    public void SetCooking(GameObject obj)
    {
        switch (m_cooking)
        {
            case WineType.wine:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("�Ͼ�"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("�Ͼ�", 5);
                }
                break;
            case WineType.sauce:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("����"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("����", 5);
                }
                break;
            case WineType.haooil:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("����"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("����", 5);
                }
                break;
        }
    }

}
