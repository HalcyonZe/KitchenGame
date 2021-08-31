using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cooking : BasicObj
{
    public enum CookingTools
    {
        salt,
        pepper,
    }

    public CookingTools m_cooking;

    private bool UseMouse = false;
    private float ObjY = 0;

    private void FixedUpdate()
    {
        
        if (UseMouse)
        {
            ObjCooking();
            StopCooking();
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public override void PickObjs()
    {
        
        this.GetComponent<Collider>().enabled = false;

        base.PickObjs();


        MouseSFM.Instance.SwitchState(MouseState.HasTools);
    }

    public override void UseTools(GameObject Obj)
    {
        MouseSFM.Instance.PickObj.transform.parent = null;

        GameController.Instance.PlayerPause();

        ObjY = Obj.transform.position.y + 0.15f;

        transform.DOMove(new Vector3(Obj.transform.position.x, ObjY, Obj.transform.position.z), 0.3f).
            OnComplete(() => {
                

                    Cursor.lockState = CursorLockMode.None;

                    UseMouse = true;
                
            });
    }

    private void ObjCooking()
    {
        this.transform.eulerAngles = new Vector3(180, 0, 0);
        // Û±Í“∆∂Ø
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.position = worldPos;
        transform.position = new Vector3(worldPos.x, ObjY, worldPos.z);


        if (Input.GetMouseButtonDown(0))
        {
            transform.DOShakePosition(0.5f,new Vector3(0,0.2f,0));
            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Foods");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                //Debug.Log("hhh");
                GameObject obj = hit.transform.gameObject;
                SetCooking(obj);
            }
        }

    }

    private void SetCooking(GameObject obj)
    {
        switch(m_cooking)
        {
            case CookingTools.salt:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("salt"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("salt", 5);
                }
                break;
            case CookingTools.pepper:
                if (!obj.GetComponent<Foods>().m_foodItem.handleScoreDic.ContainsKey("pepper"))
                {
                    obj.GetComponent<Foods>().m_foodItem.handleScoreDic.Add("pepper", 5);
                }
                break;
        }
    }

    private void StopCooking()
    {
        if (Input.GetMouseButtonDown(1) && UseMouse)
        {
            UseMouse = false;

            Cursor.lockState = CursorLockMode.Locked;

            PickObjs();
            GameController.Instance.PlayerPlay();
        }
    }

}
