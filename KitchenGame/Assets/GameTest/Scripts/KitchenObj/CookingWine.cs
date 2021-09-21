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
        honey,
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
                            UIController.Instance.OpenTip("¡œæ∆", 5);
                            break;
                        case WineType.sauce:
                            UIController.Instance.OpenTip("Ω¥”Õ", 5);
                            break;
                        case WineType.oil:
                            UIController.Instance.OpenTip("ª®…˙”Õ", 5);
                            break;
                        case WineType.haooil:
                            UIController.Instance.OpenTip("∫ƒ”Õ", 5);
                            break;
                        case WineType.honey:
                            UIController.Instance.OpenTip("∑‰√€", 5);
                            break;
                    }
                    wineTime = 0;
                }
            }

            RaycastHit hit2;
            LayerMask layer3 = LayerMask.GetMask("soup");
            if (Physics.Raycast(ray, out hit2, Mathf.Infinity, layer3))
            {
                GameObject obj2 = hit2.transform.GetChild(1).gameObject;
                if (obj2.TryGetComponent<Soup>(out Soup soup))
                {
                    obj2.GetComponent<Soup>().AddMat(this.ObjName);
                }
                else
                {
                    Debug.Log("Fuck!!!");
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
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("¡œæ∆"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("¡œæ∆", 5);
                }
                break;
            case WineType.sauce:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("Ω¥”Õ"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("Ω¥”Õ", 5);
                }
                break;
            case WineType.haooil:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("∫ƒ”Õ"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("∫ƒ”Õ", 5);
                }
                break;
            case WineType.honey:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("∑‰√€"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("∑‰√€", 5);
                }
                break;
        }
    }

}
