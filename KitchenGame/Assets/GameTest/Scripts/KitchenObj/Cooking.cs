using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cooking : BasicObj
{
    /*public enum CookingTools
    {
        salt,
        pepper,
    }

    public CookingTools m_cooking;*/

    protected bool UseMouse = false;
    protected float ObjY = 0;
    //private GameObject cap;
    //private ParticleSystem ps;

    /*private void Awake()
    {
        cap = this.transform.GetChild(0).gameObject;
        //ps = this.transform.GetChild(4).GetComponent<ParticleSystem>();
    }*/


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
        //cap.SetActive(false);

        /*MouseSFM.Instance.PickObj.transform.parent = null;

        GameController.Instance.PlayerPause();

        ObjY = Obj.transform.position.y + 0.3f;

        transform.DOMove(new Vector3(Obj.transform.position.x, ObjY, Obj.transform.position.z), 0.3f).
            OnComplete(() => {
                

                    Cursor.lockState = CursorLockMode.None;

                    UseMouse = true;
                
            });*/
    }

    private void ObjCooking()
    {
        this.transform.eulerAngles = new Vector3(160, 0, 0);
        // Û±Í“∆∂Ø
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.position = worldPos;
        transform.position = new Vector3(worldPos.x, ObjY, worldPos.z);


        MouseAction();

    }
    
    public virtual void StopCooking()
    {
        if (Input.GetMouseButtonDown(1) && UseMouse)
        {
            UseMouse = false;

            Cursor.lockState = CursorLockMode.Locked;
            //cap.SetActive(true);
            PickObjs();
            GameController.Instance.PlayerPlay();
        }
    }

    public virtual void MouseAction()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            transform.DOShakePosition(0.3f, new Vector3(0, 0.15f, 0));
            //ps.Play();

            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("CutFoods") | LayerMask.GetMask("Foods");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                //Debug.Log("hhh");
                GameObject obj = hit.transform.gameObject;
                SetCooking(obj);
            }
        }*/
    }

    /*public virtual void SetCooking(GameObject obj)
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
    }*/

    

}
